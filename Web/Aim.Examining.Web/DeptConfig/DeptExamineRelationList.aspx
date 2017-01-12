<%@ Page Title="部门考核关系" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="DeptExamineRelationList.aspx.cs" Inherits="Aim.Examining.Web.DeptConfig.DeptExamineRelationList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=730,height=650,scrollbars=yes");
        var EditPageUrl = "FeedbackEdit.aspx";
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var FeedbackEnum = { 1: '已提交', 2: '已结束' };
        var Index = $.getQueryString({ ID: "Index" });
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData,
                fields: [
		        { name: 'Id' }, { name: 'RelationName' }, { name: 'BeUserIds' }, { name: 'BeUserNames' },
		        { name: 'UpLevelUserIds' }, { name: 'UpLevelUserNames' }, { name: 'UpLevelWeight' },
		        { name: "SameLevelUserIds" }, { name: "SameLevelUserNames" }, { name: "SameLevelWeight" },
	            { name: 'DownLevelUserIds' }, { name: 'DownLevelUserNames' }, { name: 'DownLevelWeight' },
		        { name: 'GroupID' }, { name: 'GroupName' }, { name: 'CreateId' },
		        { name: 'CreateName' }, { name: 'CreateTime' }
		        ],
                listeners: { aimbeforeload: function (proxy, options) {
                    options.data.Index = Index;
                }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                collapsed: false,
                columns: 4,
                items: [
                { fieldLabel: '考核关系名称', labelWidth: 100, id: 'RelationName', schopts: { qryopts: "{ mode: 'Like', field: 'RelationName' }"} },
                { fieldLabel: '姓名', id: 'BeUserNames', schopts: { qryopts: "{ mode: 'Like', field: 'BeUserNames' }"} }
                //                { fieldLabel: '职务', id: 'BeUserRoleName', schopts: { qryopts: "{ mode: 'Like', field: 'BeUserRoleName' }"} },
                //                { fieldLabel: '按钮', xtype: 'button', iconCls: 'aim-icon-search', width: 60, margins: '2 30 0 0', text: '查 询', handler: function() {
                //                    Ext.ux.AimDoSearch(Ext.getCmp("BeUserName"));
                //                }
                //                }
                ]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [
             {
                 text: '添加',
                 iconCls: 'aim-icon-add',
                 handler: function () {
                     // opencenterwin("DeptExamineRelationEdit.aspx?op=c", "", 1000, 650);
                     opencenterwin("ExamineRelationEdit.aspx?op=c", "", 1000, 650);
                 }
             }, '-',
                {
                    text: '修改',
                    iconCls: 'aim-icon-edit',
                    handler: function () {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要修改的记录！");
                            return;
                        }
                        //opencenterwin("DeptExamineRelationEdit.aspx?op=u&id=" + recs[0].get("Id"), "", 1000, 650);
                        opencenterwin("ExamineRelationEdit.aspx?op=u&id=" + recs[0].get("Id"), "", 1000, 650);
                    }
                }, '-', {
                    text: '删除',
                    iconCls: 'aim-icon-delete',
                    handler: function () {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要删除的记录！");
                            return;
                        }
                        if (confirm("确定要删除所选记录吗？")) {
                            $.ajaxExec("delete", { id: recs[0].get("Id") }, function (rtn) {
                                if (rtn.data.Allow == "T") {
                                    store.reload();
                                }
                                else {
                                    AimDlg.show("已关联考核阶段的考核关系不能删除！");
                                }
                            });
                        }
                    }
                }]
            });
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });
            var clnArr = [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'GroupName', dataIndex: 'GroupName', header: '部门', width: 100 },
                    { id: 'RelationName', dataIndex: 'RelationName', header: '考核关系名称', width: 120, sortable: true, renderer: RowRender },
					{ id: 'BeUserNames', dataIndex: 'BeUserNames', header: '考核对象', width: 200, sortable: true, renderer: RowRender },
					{ id: 'UpLevelUserNames', dataIndex: 'UpLevelUserNames', header: '上级评分人', width: 100, renderer: RowRender },
					{ id: 'UpLevelWeight', dataIndex: 'UpLevelWeight', header: '上级权重', width: 70, sortable: true },
					{ id: 'SameLevelUserNames', dataIndex: 'SameLevelUserNames', header: '同级评分人', width: 200, sortable: true, renderer: RowRender },
				    { id: 'SameLevelWeight', dataIndex: 'SameLevelWeight', header: '同级权重', width: 70, sortable: true },
				    { id: 'DownLevelUserNames', dataIndex: 'DownLevelUserNames', header: '下级评分人', width: 180, sortable: true, renderer: RowRender },
				    { id: 'DownLevelWeight', dataIndex: 'DownLevelWeight', header: '下级权重', width: 70, sortable: true }
					];
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                // title: '部门级考核关系',
                store: store,
                region: 'center',
                viewConfig: { forceFit: true },
                autoExpandColumn: 'DownLevelUserNames',
                //  height: 250,
                columns: clnArr,
                tbar: tlBar
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
        function ShowInfo(val, val2) {
            if (Index == "1") {
                opencenterwin("FeedbackEdit.aspx?op=v&id=" + val + "&State=" + val2, "", 1000, 600);
            }
            else {
                opencenterwin("FeedbackEdit.aspx?op=u&id=" + val + "&State=" + val2, "", 1000, 600);
            }
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "Id":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowInfo(\"" + value + "\",\"" + record.get("State") + "\")'>反馈信息</label>";
                    }
                    break;
                case "RelationName":
                case "BeUserNames":
                case "UpLevelUserNames":
                case "SameLevelUserNames":
                case "DownLevelUserNames":
                    if (value) {
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
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
