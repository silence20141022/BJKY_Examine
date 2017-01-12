﻿<%@ Page Title="绩效考核结果汇总表" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="YearResult.aspx.cs" Inherits="Aim.Examining.Web.ExamineTaskManage.YearResult" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <link href="../App_Themes/Ext/ux/css/ColumnHeaderGroup.css" rel="stylesheet" type="text/css" />
    <script src="../js/ext/ux/ColumnHeaderGroup.js" type="text/javascript"></script> 
    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var ExamineStageId = $.getQueryString({ ID: "ExamineStageId" });
        var comboEnum = { 优秀: '优秀', 良好: '良好', 称职: '称职', 不称职: '不称职' };        
        function onPgLoad() {
            setPgUI(); 
        }
        function setPgUI() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData,
                fields: [
				    { name: 'Id' }, { name: 'ExamineStageId' }, { name: 'BeRoleCode' }, { name: 'BeRoleName' }, { name: 'UserId' }, { name: 'UserName' },
				    { name: 'DeptId' }, { name: 'DeptName' }, { name: 'IntegrationScore' }, { name: 'AvgScore' }, { name: 'FirstQuarterScore' },
			        { name: 'SecondQuarterScore' }, { name: 'ThirdQuarterScore' }, { name: 'FourthQuarterScore' }, { name: 'UpLevelScore' },
			        { name: 'SameLevelScore' }, { name: 'DownLevelScore' }, { name: 'AdviceLevel' }, { name: 'Year' },
			        { name: 'ApproveLevel' }, { name: 'ApproveScore' },
			        { name: 'AppealScore' }, { name: 'AppealLevel' }, { name: 'AppealEndTime' },
			        { name: 'SortIndex' }, { name: 'State' }, { name: 'SignLeaderId' }, { name: 'SignLeaderName' }, { name: 'CreateId' },
			        { name: 'CreateName' }, { name: 'CreateTime' }, { name: 'BeRoleName' }
			],
                listeners: { aimbeforeload: function(proxy, options) {
                    options.data = options.data || {};
                    options.data.ExamineStageId = ExamineStageId;
                }, load: function(store, records, options) {
                }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            // 搜索栏
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                collapsed: false,
                columns: 4,
                items: [
                { fieldLabel: '考核对象', id: 'UserName', schopts: { qryopts: "{ mode: 'Like', field: 'UserName' }"} }, 
                { fieldLabel: '考核对象部门', labelWidth: 100, id: 'DeptName', schopts: { qryopts: "{ mode: 'Like', field: 'DeptName' }"} },
                { fieldLabel: '评定等级', id: 'ApproveLevel', schopts: { qryopts: "{ mode: 'Like', field: 'ApproveLevel' }"} }
                ]
            });

            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '导出Excel',
                    iconCls: 'aim-icon-xls',
                    handler: function() {
                        ExtGridExportExcel(grid, { store: null, title: '标题' });
                    }
                }
                ]
            });

            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });
            var colArr = [
                { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'UserName', dataIndex: 'UserName', header: '考核对象', width: 120, sortable: true },
                { id: 'IntegrationScore', dataIndex: 'IntegrationScore', header: '年度考核综合分', width: 100, sortable: true, renderer: RowRender }, 
                { id: 'DeptName', dataIndex: 'DeptName', header: '考核对象所在部门', width: 120, sortable: true },
                { id: 'AvgScore', dataIndex: 'AvgScore', header: '平均分', width: 70, sortable: true, renderer: RowRender },
                { id: 'FirstQuarterScore', dataIndex: 'FirstQuarterScore', header: '一季度', width: 70, sortable: true },
                { id: 'SecondQuarterScore', dataIndex: 'SecondQuarterScore', header: '二季度', width: 70, sortable: true },
                { id: 'ThirdQuarterScore', dataIndex: 'ThirdQuarterScore', header: '三季度', width: 70, sortable: true },
                { id: 'FourthQuarterScore', dataIndex: 'FourthQuarterScore', header: '得分', width: 70, sortable: true },
                { id: 'UpLevelScore', dataIndex: 'UpLevelScore', header: '上级评价', width: 70, sortable: true, renderer: RowRender },
                { id: 'SameLevelScore', dataIndex: 'SameLevelScore', header: '同级评价', width: 70, sortable: true, renderer: RowRender },
                { id: 'DownLevelScore', dataIndex: 'DownLevelScore', header: '下级评价', width: 70, sortable: true, renderer: RowRender },
                { id: 'AdviceLevel', dataIndex: 'AdviceLevel', header: '等级', width: 80, sortable: true, renderer: RowRender }
                ];
            var rowArr = [
                        { header: '<b></b>', colspan: 6, align: 'center' },
                        { header: '<b>' + AimState["Obj"].Year + '季度考核</b>', colspan: 4, align: 'center' },
                        { header: '<b>' + AimState["Obj"].Year + '年度考核</b>', colspan: 4, align: 'center' },
                        { header: '<b></b>', colspan: 1, align: 'center' }
                ];
            var colModel = new Ext.grid.ColumnModel({
                columns: colArr,
                rows: [rowArr]
            });
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '【' + AimState["Obj"].ExamineStageName + '】考核结果',
                store: store, 
                region: 'center',
                cm: colModel,
                columnLines: true,
                autoExpandColumn: 'DeptName',
                plugins: [new Ext.ux.grid.ColumnHeaderGroup()],
                bbar: pgBar,
                tbar: titPanel
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowScoreDetail(year, userId, stageType) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function() {
                $.ajaxExec("FindExamineStageResultId", { Year: year, StageType: stageType, UserId: userId }, function(rtn) {
                    if (rtn.data.FindExamineStageResultId) {
                        opencenterwin("../ExamineTaskManage/TaskListByBeUser.aspx?ExamineStageResultId=" + rtn.data.FindExamineStageResultId, "", 1000, 650);
                    }
                }
                )
            })
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "id":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + getTip(record.get("id")) + '"';
                        rtn = value;
                    }
                    break;
                case "AdviceLevel":
                    if (!value) {//角色是副院级领导是没有等级概念的    BeDeputyDirector
                        cellmeta.style = "background-color:gray";
                    }
                    else {
                        rtn = value;
                        if (record.get("ApproveLevel") && record.get("ApproveLevel") != value) {
                            rtn = record.get("ApproveLevel");
                            cellmeta.style = " color:blue"; //如果评定等级存在且不和建议等级相同 用蓝色标记
                        }
                        if (record.get("AppealLevel")) {
                            rtn = record.get("AppealLevel");
                            cellmeta.style = "color:red;"; //如果申诉等级存在  用红色标记
                        }
                    }
                    break;
                case "IntegrationScore":
                    if (value) {
                        rtn = value;
                    }
                    if (record.get("ApproveScore")) {
                        rtn = record.get("ApproveScore");
                        cellmeta.style = "color:blue;;width:100px";
                    }
                    if (record.get("AppealScore")) {
                        rtn = record.get("AppealScore");
                        cellmeta.style = "color:red;width:100px";
                    }
                    break
                case "AvgScore":
                case "UpLevelScore":
                case "SameLevelScore":
                case "DownLevelScore":
                    if (value) {
                        rtn = value;
                    }
                    break;
                case "FirstQuarterScore":
                    if (value) {
                        rtn = ' <label style="cursor: pointer; color: Blue; text-decoration: underline" onclick=ShowScoreDetail(\'' + record.get("Year") + '\',\'' + record.get("UserId") + '\',"1")>' + value + '</label>';
                    }
                    break;
                case "SecondQuarterScore":
                    if (value) {
                        rtn = ' <label style="cursor: pointer; color: Blue; text-decoration: underline" onclick=ShowScoreDetail(\'' + record.get("Year") + '\',\'' + record.get("UserId") + '\',"2")>' + value + '</label>';
                    }
                    break;
                case "ThirdQuarterScore":
                    if (value) {
                        rtn = ' <label style="cursor: pointer; color: Blue; text-decoration: underline" onclick=ShowScoreDetail(\'' + record.get("Year") + '\',\'' + record.get("UserId") + '\',"3")>' + value + '</label>';
                    }
                    break;
                case "FourthQuarterScore":
                    if (value) {
                        rtn = ' <label style="cursor: pointer; color: Blue; text-decoration: underline" onclick=ShowScoreDetail(\'' + record.get("Year") + '\',\'' + record.get("UserId") + '\',"4")>' + value + '</label>';
                    }
                    break;
            }
            return rtn;
        }
        // 提交数据成功后
        function onExecuted() {
            store.reload();
        }    
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">   
</asp:Content>
