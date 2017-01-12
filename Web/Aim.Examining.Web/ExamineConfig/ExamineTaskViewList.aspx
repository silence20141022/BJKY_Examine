<%@ Page Title="考核任务" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="ExamineTaskViewList.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.ExamineTaskViewList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var ExamineStageId = $.getQueryString({ ID: "ExamineStageId" });
        var ToUserId = $.getQueryString({ ID: "ToUserId" });
        var StateEnum = { 0: '已生成', 1: '已启动', 2: '已提交', 3: '已结束' }; //考核任务生成的时候状态 0   启动考核后 状态变为1    提交后变为2   结束后变为3
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
                { name: 'ExamineIndicatorId' }, { name: 'ExamineRelationId' }
			        ],
                listeners: {
                    aimbeforeload: function(proxy, options) {
                        options.data = options.data || {};
                        options.data.ExamineStageId = ExamineStageId;
                        options.data.ToUserId = ToUserId;
                    }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 3,
                collapsed: false,
                items: [
                { fieldLabel: '考核对象', id: 'BeUserName', schopts: { qryopts: "{ mode: 'Like', field: 'BeUserName' }"} },
                { fieldLabel: '考核对象类型', id: 'BeRoleName', labelWidth: 100, schopts: { qryopts: "{ mode: 'Like', field: 'BeRoleName' }"} },
                { fieldLabel: '考核对象部门', id: 'BeDeptName', labelWidth: 100, schopts: { qryopts: "{ mode: 'Like', field: 'BeDeptName' }"} }
                ]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [
                {
                    text: '删除考核任务',
                    id: 'addInstitute',
                    iconCls: 'aim-icon-delete',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要删除的记录！");
                            return;
                        }
                        var taskIds = [];
                        var allowDelete = true;
                        $.each(recs, function() {
                            if (this.get("State") == "3") {
                                allowDelete = false;
                                return false;
                            }
                            taskIds.push(this.get("Id"))
                        })
                        if (!allowDelete) {
                            AimDlg.show("已结束的任务不能删除！");
                            return
                        }
                        if (confirm("确定要删除所选记录吗？")) {
                            $.ajaxExec("delete", { taskIds: taskIds, ExamineStageId: ExamineStageId }, function() {
                                store.remove(recs);
                            })
                        }
                    }
}]
                });
                titPanel = new Ext.ux.AimPanel({
                    tbar: tlBar,
                    items: [schBar]
                });

                // 表格面板
                grid = new Ext.ux.grid.AimGridPanel({
                    title: AimState["Obj"].ToUserName + '【' + AimState["Obj"].ExamineStageName + '】考核任务',
                    store: store,
                    region: 'center',
                    columnLines: true,
                    viewConfig: { forceFit: true },
                    // autoExpandColumn: 'BeDeptName',
                    columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'BeUserName', dataIndex: 'BeUserName', header: '考核对象', width: 80, sortable: true },
                { id: 'BeRoleName', dataIndex: 'BeRoleName', header: '考核对象类型', width: 100, sortable: true },
                { id: 'BeDeptName', dataIndex: 'BeDeptName', header: '考核对象部门', width: 110, sortable: true },
				{ id: 'ToUserName', dataIndex: 'ToUserName', header: '评分人', width: 80, sortable: true },
				{ id: 'ToRoleName', dataIndex: 'ToRoleName', header: '评分人角色', width: 150, sortable: true },
				{ id: 'ToDeptName', dataIndex: 'ToDeptName', header: '评分人部门', width: 100, sortable: true },
				{ id: 'State', dataIndex: 'State', header: '任务状态', width: 70, sortable: true, enumdata: StateEnum }
			    ],
                    tbar: titPanel
                    // bbar: pgBar
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
            function ShowTaskDetail(val) {
                opencenterwin("../ExamineTaskManage/ExamineEvaluation.aspx?id=" + val, "", 1200, 650);
            }
            function onExecuted() {
                store.reload();
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
