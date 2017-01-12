<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="UsrView.aspx.cs" Inherits="Aim.Examining.Web.UsrView" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var StatusEnum = { '1': '有效', '0': '无效' };
        var usrEditStyle = "dialogWidth:450px; dialogHeight:300px; scroll:yes; center:yes; status:no; resizable:yes;";

        var viewport;
        var store;
        var grid, pgBar;
        var qtype, op, id;

        function onPgLoad() {
            setPgUI();
            qtype = $.getQueryString({ "ID": "type" });
            op = $.getQueryString({ "ID": "op" });
            id = $.getQueryString({ "ID": "id" });
        }

        function setPgUI() {
            // 表格数据
            var myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["UsrList"] || []
            };

            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'UsrList',
                idProperty: 'UserID',
                data: myData,
                fields: [
                { name: 'UserID' }, { name: 'GroupName' }, { name: 'GroupId' }, { name: 'Name' },
                { name: 'LoginName' }, { name: 'WorkNo' },
                { name: 'Status' },
                { name: 'Email' },
                { name: 'Remark' },
                { name: 'CreateDate', type: 'date'}],
                proxy: new Ext.ux.data.AimRemotingProxy({
                    aimbeforeload: function(proxy, options) {
                        options.data = { type: qtype, id: id, op: op };
                    }
                })
            });

            // 分页栏
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });

            // 搜索栏
            /*  var schBar = new Ext.Panel({
            collapsed: true,
            unstyled: true,
            padding: 5,
            layout: 'column',
            items: [
            items: [new Ext.app.AimSearchField({ fieldLabel: '姓名', anchor: '90%', name: 'Name', store: store, aimgrp: "usrgrp", qryopts: "{ mode: 'Like', field: 'Name' }" })]
            { layout: 'form', unstyled: true, columnWidth: .33, }, { layout: 'form', unstyled: true, columnWidth: .33,
            items: [new Ext.app.AimSearchField({ fieldLabel: '登录名', anchor: '90%', name: 'LoginName', store: store, aimgrp: "usrgrp", qryopts: "{ mode: 'Like', field: 'LoginName' }" })]
            }]
            });*/
            var schBar = new Ext.ux.AimSchPanel({
                store: store,
                collapsed: false,
                columns: 4,
                items: [
                    { fieldLabel: '姓名', id: 'Name', schopts: { qryopts: "{ mode: 'Like', field: 'Name' }"} },
                //                    { fieldLabel: '拟稿人', id: 'CreateName', schopts: { qryopts: "{ mode: 'Like', field: 'CreateName' }"} },
                //                    { fieldLabel: '开始时间', id: 'StartTime', xtype: 'datefield', vtype: 'daterange', endDateField: 'EndTime', schopts: { qryopts: "{ mode: 'GreaterThan', datatype:'Date', field: 'BeginDate' }"} },
                //                    { fieldLabel: '结束时间', id: 'EndTime', xtype: 'datefield', vtype: 'daterange', startDateField: 'StartTime', schopts: { qryopts: "{ mode: 'LessThan', datatype:'Date', field: 'EndDate' }"} },
                    {fieldLabel: '按钮', xtype: 'button', iconCls: 'aim-icon-search', width: 60, margins: '2 30 0 0', text: '查 询', handler: function() {
                        Ext.ux.AimDoSearch(Ext.getCmp("Name"));
                    }
                }
                ]
            });
            // 工具栏
            var tlBar = new Ext.ux.AimToolbar({
                items: [{ text: '添加人员', iconCls: 'aim-icon-user-add', aimexecutable: true,
                    handler: function() {
                        //openMdlWin("/CommonPages/Select/UsrSelect/MUsrSelect.aspx?rtntype=array", "addgrpuser");
                        var url = "/CommonPages/Select/UsrSelect/MUsrSelect.aspx?rtntype=array";
                        var style = "dialogWidth:750px; dialogHeight:550px; scroll:yes; center:yes; status:no; resizable:yes;";
                        var rtn = window.showModalDialog(url, window, style);
                        if (rtn && rtn.data.length > 0) {
                            var uids = [];
                            var usrs = rtn.data;
                            $.each(usrs, function() {
                                if (this.UserID) {
                                    uids.push(this.UserID);
                                }
                            });
                            $.ajaxExec("addUserGroup", { id: id, UserIds: uids }, onExecuted);
                        }
                    }
                }, { text: '移除人员', iconCls: 'aim-icon-user-delete', aimexecutable: true,
                    handler: function() {
                        var UserIds = []; var GroupIds = [];
                        var recs = grid.getSelectionModel().getSelections();
                        $.each(recs, function() {
                            UserIds.push(this.json.UserId);
                            GroupIds.push(this.json.GroupId);
                        })
                        $.ajaxExec("delete", { UserIds: UserIds, GroupIds: GroupIds }, function() { onExecuted(); });
                        //UpdateGroupUser('delete');
                    }
                }, '->']
                //, { text: '查询:' }, new Ext.app.AimSearchField({ store: store, pgbar: pgBar, schbutton: true, qryopts: "{ type: 'fulltext' }" })
            });

            // 工具标题栏
            var titPanel = new Ext.Panel({
                tbar: tlBar,
                items: [schBar]
            });

            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                store: store,
                region: 'center',
                columns: [
                { id: 'UserID', header: 'UserID', dataIndex: 'UserID', hidden: true },
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'Name', header: '姓名', width: 100, sortable: true, dataIndex: 'Name' }, //renderer: linkRender, 
                {id: 'WorkNo', header: '工号', width: 100, sortable: true, dataIndex: 'WorkNo' },
                { id: 'Email', header: '邮箱', width: 200, sortable: true, dataIndex: 'Email' },
                { id: 'GroupName', header: '部门/角色', width: 200, sortable: true, dataIndex: 'GroupName' }
                //{ id: 'Status', header: '状态', width: 100, align: 'center', renderer: linkRender, sortable: true, dataIndex: 'Status' },
            ],
                bbar: pgBar,
                tbar: titPanel,
                autoExpandColumn: 'Email'
            });

            // 页面视图
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }

        // 链接渲染
        function linkRender(val, p, rec) {
            var rtn = val;
            switch (this.dataIndex) {
                case "Name":
                    rtn = "<a class='aim-ui-link' onclick='openMdlWin(\"UsrEdit.aspx?id=" + rec.id + "\", null, usrEditStyle)'>" + val + "</a>";
                    break;
                case "Status":
                    rtn = StatusEnum[val];
                    break;
            }

            return rtn;
        }

        // 打开模态窗口
        function openMdlWin(url, op, style) {
            op = op || "r";
            style = style || "dialogWidth:750px; dialogHeight:550px; scroll:yes; center:yes; status:no; resizable:yes;";
            var params = [];
            params[params.length] = "op=" + op;
            url = $.combineQueryUrl(url, params)
            rtn = window.showModalDialog(url, window, style);
            if (rtn && rtn.result) {
                if (rtn.result === 'success') {
                    if (op == 'addgrpuser') {
                        var uids = [];
                        var usrs = rtn.data;
                        $.each(usrs, function() {
                            if (this.UserID) {
                                uids.push(this.UserID);
                            }
                        });

                        $.ajaxExec("adduserbycc", { id: id, UserIDs: uids }, onExecuted);
                        //UpdateGroupUser('add', uids);
                    }
                }
            }
        }
        function onExecuted() {
            store.reload();
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
