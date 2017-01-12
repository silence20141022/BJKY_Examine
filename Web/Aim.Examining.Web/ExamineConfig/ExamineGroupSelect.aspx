<%@ Page Title="考核部门选择" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="ExamineGroupSelect.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.ExamineGroupSelect" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script src="/js/pgfunc-ext-sel.js" type="text/javascript"></script>

    <script type="text/javascript">
        var seltype = $.getQueryString({ ID: "seltype" });
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, AimSelGrid, viewport;

        function onSelPgLoad() {
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
                { name: 'Id' }, { name: 'GroupID' }, { name: 'GroupName' }, { name: 'GroupType' }, { name: 'GroupCode' }, { name: 'FirstLeaderNames' }
			    ]
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            // 搜索栏
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                collapsed: false,
                columns: 3,
                items: [
                { fieldLabel: '合同号', id: 'ContractCode', schopts: { qryopts: "{ mode: 'Like', field: 'ContractCode' }"} },
                { fieldLabel: '合同名称', id: 'ContractName', schopts: { qryopts: "{ mode: 'Like', field: 'ContractName' }"} },
                { fieldLabel: '按钮', xtype: 'button', iconCls: 'aim-icon-search', width: 60, margins: '1 30 0 0', text: '查 询', handler: function() {
                    Ext.ux.AimDoSearch(Ext.getCmp("ContractCode"));
                } }]
                });

                // 工具栏
                tlBar = new Ext.ux.AimToolbar({
                    items: ['<font color=red>说明：双击行可以直接完成选择</font>']
                });

                // 工具标题栏
                titPanel = new Ext.ux.AimPanel({
                    tbar: tlBar,
                    items: [schBar]
                });
                var buttonPanel = new Ext.form.FormPanel({
                    region: 'south',
                    frame: true,
                    buttonAlign: 'center',
                    buttons: [{ text: '确定', handler: function() { AimGridSelect(); } }, { text: '取消', handler: function() {
                        window.close();
                    } }]
                    });
                    // 表格面板
                    AimSelGrid = new Ext.ux.grid.AimGridPanel({
                        title: ' 部门列表',
                        store: store,
                        forceFit: true,
                        sm: seltype == "single" ? new Ext.grid.RowSelectionModel({ singleSelect: true }) : "",
                        //  bbar: pgBar,
                        region: 'center',
                        checkOnly: false,
                        autoExpandColumn: 'GroupName',
                        columns: [
                        { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                         new Ext.ux.grid.AimRowNumberer(), AimSelCheckModel,
                        { id: 'GroupName', dataIndex: 'GroupName', header: '部门名称', width: 200, sortable: true },
                        { id: 'GroupType', dataIndex: 'GroupType', header: '部门类型', width: 120, sortable: true },
                        { id: 'FirstLeaderNames', dataIndex: 'FirstLeaderNames', header: '部门领导', width: 130, sortable: true }
                    ]
                        //  tbar: titPanel
                    });
                    viewport = new Ext.ux.AimViewport({
                        items: [AimSelGrid, buttonPanel]
                    });
                }          
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
