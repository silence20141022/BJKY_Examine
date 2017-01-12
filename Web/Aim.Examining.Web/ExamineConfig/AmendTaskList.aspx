<%@ Page Title="修正任务列表" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="AmendTaskList.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.AmendTaskList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var ExamineStageId = $.getQueryString({ ID: "ExamineStageId" });
        var ExamineStageName = unescape($.getQueryString({ ID: "ExamineStageName" }));
        var AmendStateEnum = { '-': '待删除', '+': '待添加' };
        //考核任务生成的时候状态 0   启动考核后 状态变为1    提交后变为2   结束后变为3
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            // 表格数据
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
                { name: 'Id' }, { name: 'ExamineStageId' }, { name: 'ToUserId' }, { name: 'ToUserName' }, { name: 'ToDeptId' },
                { name: 'ToDeptName' }, { name: 'ToRoleCode' }, { name: 'ToRoleName' }, { name: 'BeUserId' }, { name: 'BeUserName' },
                { name: 'BeDeptId' }, { name: 'BeDeptName' }, { name: 'BeRoleCode' }, { name: 'BeRoleName' }, { name: 'Score' },
                { name: 'StartTime' }, { name: 'EndTime' }, { name: 'State' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime' },
                { name: 'ExamineIndicatorId' }, { name: 'ExamineRelationId' }, { name: 'AmendState' }
			        ],
                listeners: {
                    aimbeforeload: function(proxy, options) {
                        options.data = options.data || {};
                        options.data.ExamineStageId = ExamineStageId;
                    }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                    }
                }
			        ]
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 3,
                collapsed: false,
                items: [
                { fieldLabel: '被考核人', id: 'BeUserName', schopts: { qryopts: "{ mode: 'Like', field: 'BeUserName' }"} },
                { fieldLabel: '被考角色', id: 'BeRoleName', schopts: { qryopts: "{ mode: 'Like', field: 'BeRoleName' }"} },
                { fieldLabel: '被考部门', id: 'BeDeptName', schopts: { qryopts: "{ mode: 'Like', field: 'BeDeptName' }"} },
                { fieldLabel: '考核人', id: 'ToUserName', schopts: { qryopts: "{ mode: 'Like', field: 'ToUserName' }"} },
                { fieldLabel: '考核角色', id: 'ToRoleName', schopts: { qryopts: "{ mode: 'Like', field: 'ToRoleName' }"} },
                { fieldLabel: '考核部门', id: 'ToDeptName', schopts: { qryopts: "{ mode: 'Like', field: 'ToDeptName' }"} }
                ]
            });
            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                //    tbar: tlBar,
                items: [schBar]
            });

            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '【' + AimState["StageName"] + '】修复任务列表',
                store: store,
                region: 'center',
                columnLines: true,
                viewConfig: { forceFit: true },
                // autoExpandColumn: 'BeDeptName',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'BeUserName', dataIndex: 'BeUserName', header: '被考核人', width: 80, sortable: true },
                { id: 'BeRoleName', dataIndex: 'BeRoleName', header: '被考角色', width: 100, sortable: true },
                { id: 'BeDeptName', dataIndex: 'BeDeptName', header: '被考核人部门', width: 110, sortable: true },
				{ id: 'ToUserName', dataIndex: 'ToUserName', header: '考核人', width: 80, sortable: true },
				{ id: 'ToRoleName', dataIndex: 'ToRoleName', header: '考核人角色', width: 150, sortable: true },
				{ id: 'ToDeptName', dataIndex: 'ToDeptName', header: '考核人部门', width: 100, sortable: true },
				{ id: 'AmendState', dataIndex: 'AmendState', header: '操作类型', width: 70, enumdata: AmendStateEnum }
                //				{ id: 'Score', dataIndex: 'Score', header: '考核分数', width: 70, sortable: true },
                //				{ id: 'Id', dataIndex: 'Id', header: '查看详细', width: 80, renderer: RowRender }
			    ],
                tbar: titPanel,
                bbar: pgBar
            });
            var buttonPanel = new Ext.form.FormPanel({
                region: 'south',
                height: 25,
                frame: true,
                buttonAlign: 'center',
                buttons: [{ text: '确定', handler: function() { UpdataTask(); } }, { text: '取消', handler: function() {
                    CancelAmendTask();
                } }]
                });
                viewport = new Ext.ux.AimViewport({
                    items: [grid, buttonPanel]
                });
            }
            function opencenterwin(url, name, iWidth, iHeight) {
                var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
                var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
                window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
            }
            function ShowTaskDetail(val) {
                opencenterwin("../ExamineTaskManage/ExamineEvaluation.aspx?id=" + val, "", 1200, 650);
            }
            function onExecuted() {
                store.reload();
            }
            function UpdataTask() {
                if (confirm("确定要修正上述列表中的任务吗？")) {
                    $.ajaxExec("AmendTask", { ExamineStageId: ExamineStageId }, function(rtn) {
                        AimDlg.show(AimState["Result"]);
                        window.close();
                    });
                }
            }
            function CancelAmendTask() {
                if (confirm("确定要放弃本次任务修正吗？")) {
                    $.ajaxExec("CancelAmendTask", { ExamineStageId: ExamineStageId }, function(rtn) {
                        window.close();
                    });
                }
            }
            function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
                var rtn;
                switch (this.id) {
                    case "Id":
                        if (value) {
                            rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowTaskDetail(\"" + value + " \")'>查看详细</label>";
                        }
                        break;
                    case "BeRoleName":
                        if (value) {
                            value = value || "";
                            cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                            rtn = value;
                        }
                        break;
                    case "CreateTime":
                        break;
                }
                return rtn;
            }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
