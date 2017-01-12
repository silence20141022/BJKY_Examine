<%@ Page Title="绩效考核结果汇总表" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="LevelApprove.aspx.cs" Inherits="Aim.Examining.Web.ExamineTaskManage.LevelApprove" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <link href="../App_Themes/Ext/ux/css/ColumnHeaderGroup.css" rel="stylesheet" type="text/css" />

    <script src="../js/ext/ux/ColumnHeaderGroup.js" type="text/javascript"></script>

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var ExamineStageId = $.getQueryString({ ID: "ExamineStageId" });
        var comboEnum = { 优秀: '优秀', 良好: '良好', 称职: '称职', 不称职: '不称职' };
        //    var EnumData = { 0: '已创建', 1: '已生成', 2: '已启动', 3: '已结束' }; //4 表示已建议   5 已评定   阶段状态规则
        function onPgLoad() {
            setPgUI();
            if (AimState["Obj"].State != "4") {
                tlBar.remove("submit");
            }
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
				    { name: 'Id' }, { name: 'ExamineStageId' }, { name: 'BeRoleCode' }, { name: 'UserId' }, { name: 'UserName' },
				    { name: 'DeptId' }, { name: 'DeptName' }, { name: 'IntegrationScore' }, { name: 'AvgScore' }, { name: 'FirstQuarterScore' },
			        { name: 'SecondQuarterScore' }, { name: 'ThirdQuarterScore' }, { name: 'FourthQuarterScore' }, { name: 'UpLevelScore' },
			        { name: 'SameLevelScore' }, { name: 'DownLevelScore' }, { name: 'AdviceLevel' }, { name: 'ApproveLevel' }, { name: 'ApproveScore' },
			        { name: 'Year' }, { name: 'SortIndex' }, { name: 'State' }, { name: 'SignLeaderId' }, { name: 'SignLeaderName' },
			        { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime' }, { name: 'BeRoleName' }
			]
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            // 搜索栏
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                items: [
                { fieldLabel: '名称', id: 'Name', schopts: { qryopts: "{ mode: 'Like', field: 'Name' }"} },
                { fieldLabel: '编码', id: 'Code', schopts: { qryopts: "{ mode: 'Like', field: 'Code' }"} },
                { fieldLabel: '创建人', id: 'CreateName', schopts: { qryopts: "{ mode: 'Like', field: 'CreateName' }"}}]
            });

            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '提交',
                    id: 'submit',
                    iconCls: 'aim-icon-submit',
                    handler: function() {
                        var recs = store.getModifiedRecords();
                        var dt = store.getModifiedDataStringArr(recs) || [];
                        if (confirm("评定等级和分数提交后将不允许修改，确定要提交吗？")) {
                            $.ajaxExec("Submit", { ExamineStageId: ExamineStageId, data: dt }, function() { RefreshClose(); })
                        }
                    }
                }, '-', {
                    text: '暂存',
                    iconCls: 'aim-icon-save',
                    handler: function() {
                        var recs = store.getModifiedRecords();
                        var dt = store.getModifiedDataStringArr(recs) || [];
                        $.ajaxExec("TempSave", { data: dt }, function() {
                            AimDlg.show("保存成功！");
                            store.commitChanges();
                        })
                    }
                }
                ]
            });

            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });
            var cb_be = new Ext.ux.form.AimComboBox({
                id: 'combo_be',
                enumdata: comboEnum,
                lazyRender: false,
                allowBlank: false,
                autoLoad: true,
                forceSelection: true,
                //blankText: "none",
                //valueField: 'text',
                triggerAction: 'all',
                mode: 'local',
                listeners: {
                    collapse: function(obj) {
                        //                                        if (grid.activeEditor) {
                        //                                                  var rec = store.getAt(grid.activeEditor.row);
                        //                                              if (rec) {
                        grid.stopEditing();
                        //                      rec.set("AdviceLevel", Ext.get('combo_be').dom.value);
                        //                                //  rec.set("AdviceLevel", obj.value);                                
                        //                            }
                        //                        }
                    }
                }
            });

            var colArr = [
                { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'UserName', dataIndex: 'UserName', header: '姓名', width: 70, sortable: true },
                { id: 'IntegrationScore', dataIndex: 'IntegrationScore', header: '年度考核综合分', width: 100, sortable: true, renderer: RowRender },
                { id: 'BeRoleName', dataIndex: 'BeRoleName', header: '角色', width: 120 },
                { id: 'DeptName', dataIndex: 'DeptName', header: '部门', width: 100 },
                { id: 'AvgScore', dataIndex: 'AvgScore', header: '平均分', width: 60, sortable: true, renderer: RowRender },
                { id: 'FirstQuarterScore', dataIndex: 'FirstQuarterScore', header: '一季度', width: 60, sortable: true, renderer: RowRender },
                { id: 'SecondQuarterScore', dataIndex: 'SecondQuarterScore', header: '二季度', width: 60, sortable: true, renderer: RowRender },
                { id: 'ThirdQuarterScore', dataIndex: 'ThirdQuarterScore', header: '三季度', width: 60, sortable: true, renderer: RowRender }, //10
                {id: 'FourthQuarterScore', dataIndex: 'FourthQuarterScore', header: '得分', width: 60, sortable: true, renderer: RowRender },
                { id: 'UpLevelScore', dataIndex: 'UpLevelScore', header: '上级评价', width: 70, sortable: true, renderer: RowRender },
                { id: 'SameLevelScore', dataIndex: 'SameLevelScore', header: '同级评价', width: 70, sortable: true, renderer: RowRender },
                { id: 'DownLevelScore', dataIndex: 'DownLevelScore', header: '下级评价', width: 70, sortable: true, renderer: RowRender },
                { id: 'AdviceLevel', dataIndex: 'AdviceLevel', header: '建议等级', width: 80, sortable: true, renderer: RowRender },
                { id: 'ApproveLevel', dataIndex: 'ApproveLevel', header: '<font style="color:red">评定等级</font>', width: 80, sortable: true, editor: cb_be, renderer: RowRender },
                { id: 'ApproveScore', dataIndex: 'ApproveScore', header: '<font style="color:red">评定分数</font>', width: 80, editor: { xtype: 'numberfield', minValue: 0, maxValue: 100, allowBlank: false, decimalPrecision: 1} }
            // { id: 'SignLeaderName', dataIndex: 'SignLeaderName', header: '签字领导', width: 100, sortable: true },//不含正职的优秀和良好 在设定等级的时候要加以约束
            //{ id: 'CreateTime', dataIndex: 'CreateTime', header: '创建时间', renderer: ExtGridDateOnlyRender, width: 100, sortable: true }
                ];
            var rowArr = [
                        { header: '<b></b>', colspan: 7, align: 'center' },
                        { header: '<b>' + AimState["Obj"].Year + '季度考核</b>', colspan: 4, align: 'center' },
                        { header: '<b>' + AimState["Obj"].Year + '年度考核</b>', colspan: 4, align: 'center' },
                        { header: '<b></b>', colspan: 3, align: 'center' }
                ];
            var colModel = new Ext.grid.ColumnModel({
                columns: colArr,
                rows: [rowArr]
            });
            // 表格面板
            grid = new Ext.ux.grid.AimEditorGridPanel({
                title: "【" + AimState["Obj"].ExamineStageName + "】等级评定",
                store: store,
                region: 'center',
                columnLines: true,
                cm: colModel,
                autoExpandColumn: 'DeptName',
                plugins: [new Ext.ux.grid.ColumnHeaderGroup()],
                //  bbar: pgBar,
                tbar: titPanel
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
            grid.getColumnModel().isCellEditable = function(colIndex, rowIndex) {
                var rec = store.getAt(rowIndex); //员工等级由部门填报 人力资源部备案
                if ((rec.get("BeRoleCode") == "BeDeputyDirector" || rec.get("BeRoleCode") == "BeDeptClerk") && colIndex != 17) {
                    return false;
                }
                return Ext.grid.ColumnModel.prototype.isCellEditable.call(this, colIndex, rowIndex);
            }
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
                    if (record.get("BeRoleCode") == "BeDeputyDirector") {
                        cellmeta.style = "background-color:gray";
                    }
                    rtn = value;
                    break;
                case "ApproveLevel":
                    if (record.get("BeRoleCode") == "BeDeputyDirector" || record.get("BeRoleCode") == "BeDeptClerk") {
                        cellmeta.style = "background-color:gray";
                    }
                    rtn = value;
                    break;
                case "IntegrationScore":
                case "AvgScore":
                case "FirstQuarterScore":
                case "SecondQuarterScore":
                case "ThirdQuarterScore":
                case "FourthQuarterScore":
                case "UpLevelScore":
                case "SameLevelScore":
                case "DownLevelScore":
                    if (value) {
                        rtn = value;
                    }
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
