<%@ Page Title="考核任务" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="ExamineTaskGroup.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.ExamineTaskGroup" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var Index = $.getQueryString({ ID: "Index" });
        var StageEnum = { 1: '第一季度', 2: '第二季度', 3: '第三季度', 4: '年度考核' };
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
                 { name: 'Id' }, { name: 'ExamineStageId' }, { name: 'ToUserId' }, { name: 'BeRoleCode' }, { name: 'BeRoleName' }, { name: 'StageType' },
                 { name: 'ToRoleCode' }, { name: 'ToRoleName' }, { name: 'StageName' }, { name: 'TaskQuan' }, { name: 'ExamineType' }, { name: 'CreateTime' },
                 { name: 'BeUserNames' }, { name: 'ExamineRelationId'}],
                listeners: {
                    aimbeforeload: function(proxy, options) {
                        options.data = options.data || {};
                        options.data.Index = Index;
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
                    text: '撤销提交',
                    iconCls: 'aim-icon-undo',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要回收的考核任务！");
                            return;
                        }
                        if (confirm("确定要回收已提交的考核任务吗？")) {
                            Ext.getBody().mask("考核任务回收中。。。。");
                            $.ajaxExec("TakeBack", { ExamineStageId: recs[0].get("ExamineStageId"), Index: Index, ToRoleCode: recs[0].get("ToRoleCode"),
                                BeRoleCode: recs[0].get("BeRoleCode")
                            }, function(rtn) {
                                store.reload();
                            });
                        }
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
                tbar: Index == "1" ? tlBar : "",
                items: [schBar]
            });

            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '考核任务',
                store: store,
                region: 'center',
                autoExpandColumn: 'StageName',
                columns: [
                { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'BeRoleName', dataIndex: 'BeRoleName', header: '考核对象', width: 140, renderer: RowRender },
                { id: 'ToRoleName', dataIndex: 'ToRoleName', header: '评分人', width: 140, renderer: RowRender },
                { id: 'StageName', dataIndex: 'StageName', header: '考核阶段名称', width: 200 },
                { id: 'ExamineType', dataIndex: 'ExamineType', header: '考核类型', width: 120 },
                { id: 'StageType', dataIndex: 'StageType', header: '阶段类型', width: 100, enumdata: StageEnum },
				{ id: 'TaskQuan', dataIndex: 'TaskQuan', header: '考核任务数', width: 100 },
				{ id: 'ExamineStageId', dataIndex: 'ExamineStageId', header: '操作', width: 100, renderer: RowRender }
			    ],
                // tbar: Index == "1" ? tlBar : "",
                bbar: pgBar
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        // 提交数据成功后
        function onExecuted() {
            store.reload();
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowTask(val1, val2, val3) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function() {
                opencenterwin("ExamineEvaluation.aspx?ExamineStageId=" + val1 + "&ExamineRelationId=" + val2 + "&BeRoleCode=" + val3 + "&Index=" + Index, "", 1200, 650);
            });
        }
        function ShowTask2(val1, val2) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function() {
                opencenterwin("../DeptConfig/DeptEvaluation.aspx?ExamineStageId=" + val1 + "&ExamineRelationId=" + val2 + "&Index=" + Index, "", 1200, 650);
            });
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "ExamineStageId":
                    if (value) {
                        if (record.get("ExamineType") == "院级考核") {
                            rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowTask(\"" + value + "\", \"" + record.get("ExamineRelationId") + "\",\"" + record.get("BeRoleCode") + "\")'>" + (Index != '0' ? '查看详细' : '进入打分') + "</label>";
                        }
                        else {
                            rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowTask2(\"" + value + "\", \"" + record.get("ExamineRelationId") + "\")'>" + (Index != '0' ? '查看详细' : '进入打分') + "</label>";
                        }
                    }
                    break;
                case "BeRoleName":
                    if (value) {
                        rtn = value;
                    }
                    else {
                        rtn = record.get("BeUserNames");
                    }
                    break;
                case "ToRoleName":
                    if (value) {
                        rtn = value;
                    }
                    else {
                        rtn = AimState["UserInfo"].Name;
                    }
                    break;
            }
            return rtn;
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
