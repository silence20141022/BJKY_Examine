<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="UsrList.aspx.cs" Inherits="Aim.Portal.Web.Modules.SysApp.OrgMag.UsrList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var StatusEnum = { '1': '有效', '0': '无效' };
        var viewport;
        var store;
        var grid, pgBar;
        var create = false;

        function onPgLoad() {
            setPgUI();
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
                fields: [{ name: 'UserID' }, { name: 'Name' }, { name: 'LoginName' }, { name: 'WorkNo' }, { name: 'Status' }, { name: 'Email' }, { name: 'Remark' }, { name: 'CreateDate', type: 'date' },
                { name: 'Phone' }, { name: 'Ext1'}]
            });

            // 分页栏
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store,
                displayInfo: true,
                displayMsg: '当前条目 {0} - {1}, 总条目 {2}',
                emptyMsg: "无条目显示",
                items: ['-']
            });

            // 搜索栏
            var schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 4,
                padding: 0,
                collapsed: false,
                items: [{ fieldLabel: '姓名', id: 'Name', schopts: { qryopts: "{ mode: 'Like', field: 'Name' }"} },
                { fieldLabel: '工号', id: 'WorkNo', schopts: { qryopts: "{ mode: 'Like', field: 'WorkNo' }"} },
                { fieldLabel: '登录名', id: 'LoginName', schopts: { qryopts: "{ mode: 'Like', field: 'LoginName' }"} },
                 { fieldLabel: '按钮', xtype: 'button', iconCls: 'aim-icon-search', width: 60, margins: '2 30 0 0', text: '查 询', handler: function() {
                     Ext.ux.AimDoSearch(Ext.getCmp("Name"));
                 }
                 }
                ]
            });

            // 工具栏
            var tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        openMdlWin("UsrEdit.aspx", "c");
                    }
                }, {
                    text: '修改',
                    iconCls: 'aim-icon-edit',
                    handler: function() {
                        openMdlWin("UsrEdit.aspx", "u");
                    }
                }, {
                    text: '删除',
                    iconCls: 'aim-icon-delete',
                    handler: function() {
                        openMdlWin("UsrEdit.aspx", "d");
                    }
                }, '-', {
                    text: '下载导入模板',
                    iconCls: 'aim-icon-xls',
                    handler: function() {
                        window.open("UserTemplate.XLS");
                    }
                }, {
                    text: '导入',
                    iconCls: 'aim-icon-xls',
                    handler: function() {
                        UploadFile();
                    }
                }, {
                    text: '导出Excel',
                    iconCls: 'aim-icon-xls',
                    handler: function() {
                        ExtGridExportExcel(grid, { store: null, reloaddata: false, title: '人员基本信息' });
                    }
                }, {
                    text: '全部生成随机密码', hidden: true,
                    iconCls: 'aim-icon-execute',
                    handler: function() {
                        if (!confirm("确定要全部重置为随机密码吗?")) return;
                        $.ajaxExec('setpass', {}, function() {
                            AimDlg.show("用户已全部生成随机密码!");
                            store.reload();
                        }
                );
                    }
                }, '-',
                {
                    text: '发送邮件',
                    iconCls: 'aim-icon-email-go',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (recs.length == 0) {
                            AimDlg.show("请选择要发送的人员!");
                            return;
                        }
                        if (!confirm("确定要发送邮件吗?")) return;
                        Ext.getBody().mask("开始发送邮件,请耐心等待");
                        ExtBatchOperate('sendmail', recs, null, null, function() {
                            AimDlg.show("发送成功!");
                            Ext.getBody().unmask();
                        });
                    }
                }, {
                    text: '配置',
                    iconCls: 'aim-icon-cog',
                    menu: [{ text: '用户权限', handler: function() { viewport.doLayout(); } },
                    { text: '用户角色', handler: function() { viewport.doLayout(); } },
                    { text: '用户组', handler: function() { viewport.doLayout(); } }]
                }, '->'
                ]
            });

            // 工具标题栏
            var titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });

            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                store: store,
                region: 'center',
                monitorResize: true,
                columns: [
            { id: 'UserID', header: 'UserID', dataIndex: 'UserID', hidden: true },
            new Ext.ux.grid.AimRowNumberer(),
            new Ext.ux.grid.AimCheckboxSelectionModel(),
            { id: 'Name', header: '姓名', width: 100, renderer: linkRender, sortable: true, dataIndex: 'Name' },
            { id: 'LoginName', header: '登录名', width: 100, sortable: true, dataIndex: 'LoginName' },
            { id: 'WorkNo', header: '工号', width: 100, sortable: true, dataIndex: 'WorkNo' },
            { id: 'Status', header: '状态', width: 50, align: 'center', renderer: enumRender, sortable: true, dataIndex: 'Status' },
			{ id: 'Manage', header: '操作', width: 60, sortable: true, renderer: linkRender, dataIndex: 'CreateDate' },
            { id: 'Email', header: '邮箱', width: 150, sortable: true, dataIndex: 'Email' },
            { id: 'Phone', header: '电话', width: 100, sortable: true, dataIndex: 'Phone' },
            { id: 'Ext1', header: '离退休', width: 60, sortable: true, renderer: linkRender, dataIndex: 'Ext1' },
            { id: 'Remark', header: '备注', width: 65, sortable: true, dataIndex: 'Remark' },
            { id: 'CreateDate', header: '创建时间', width: 150, align: 'center', renderer: Ext.util.Format.dateRenderer('m/d/Y'), hidden: true, dataIndex: 'CreateDate' }
            ],
                bbar: pgBar,
                tbar: titPanel,
                frame: true,
                forceLayout: true,
                stripeRows: true,
                autoExpandColumn: 'Remark',
                stateful: true,
                stateId: 'grid'
            });

            // 页面视图
            viewport = new Ext.ux.AimViewport({
                layout: 'border',
                items: [{ xtype: 'box', region: 'north', applyTo: 'header', height: 30 }, grid]
            });
        }

        // 链接渲染
        function linkRender(val, p, rec) {
            var rtn = val;
            switch (this.id) {
                case "Name":
                    rtn = "<a class='aim-ui-link' onclick='openMdlWin(\"UsrEdit.aspx?id=" + rec.id + "\")'>" + val + "</a>";
                    break;
                case "Manage":
                    rtn = "<a class='aim-ui-link' onclick='ClearPass(\"" + rec.id + "\")'>清空密码</a>";
                    break;
                case "Ext1":
                    if (val == "1") {
                        rtn = "是";
                    }
                    break;
            }
            return rtn;
        }

        // 枚举渲染
        function enumRender(val, p, rec) {
            var rtn = val;
            switch (this.dataIndex) {
                case "Status":
                    rtn = StatusEnum[val];
                    break;
            }

            return rtn;
        }
        function ClearPass(id) {
            if (!confirm("确定要清空该人员密码吗?")) return;
            $.ajaxExec('clearpass', { "UserId": id }, function() {
                AimDlg.show("该用户密码已清空成功!");
                store.reload();
            }
                );
        }

        // 打开模态窗口
        function openMdlWin(url, op, style) {
            op = op || "r";
            style = style || "dialogWidth:450px; dialogHeight:300px; scroll:yes; center:yes; status:no; resizable:yes;";

            var sels = grid.getSelectionModel().getSelections();
            var sel;
            if (sels.length > 0) sel = sels[0];

            var params = [];
            params[params.length] = "op=" + op;
            if (op !== "c") {
                if (sel) {
                    if (url.indexOf("id=") < 0) {
                        params[params.length] = "id=" + sel.json.UserID;
                    }
                } else {
                    AimDlg.show('请选择需要操作的行。', '提示', 'alert');
                    return;
                }
            }

            url = $.combineQueryUrl(url, params)
            rtn = window.showModalDialog(url, window, style);
            if (rtn && rtn.result) {
                if (rtn.result === 'success') {
                    store.reload();
                }
            }
        }

        function UploadFile() {
            var form = new Ext.form.FormPanel({
                labelAlign: 'right',
                title: '请选择要上传的文件',
                labelWidth: 80,
                frame: true,
                fileUpload: true,
                url: 'UsrList.aspx',
                items: [{
                    xtype: 'textfield',
                    fieldLabel: '文件选择',
                    name: 'file',
                    width: 400,
                    inputType: 'file'
}],
                    buttons: [{
                        text: '上传',
                        handler: function() {
                            Ext.getBody().mask("数据导入中,请稍等...");
                            form.getForm().submit({
                                success: function(form, action) {
                                    w.close();
                                    Ext.getBody().unmask();
                                    AimDlg.show("导入成功！");
                                },
                                failure: function() {
                                    Ext.Msg.alert('错误', '失败');
                                }
                            });
                        }
}]
                    });
                    var w = new Ext.Window({
                        title: '上传Excel',
                        width: 600,
                        autoHeight: true,
                        items: form
                    }).show();
                }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header" style="display: none;">
        <h1>
            人员列表</h1>
    </div>
</asp:Content>
