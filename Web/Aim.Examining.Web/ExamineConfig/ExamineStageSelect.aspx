<%@ Page Title="考核阶段选择" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="ExamineStageSelect.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.ExamineStageSelect" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script src="/js/pgfunc-ext-sel.js" type="text/javascript"></script>

    <script type="text/javascript">
        var seltype = $.getQueryString({ ID: "seltype" });
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, AimSelGrid, viewport;
        var StageEnum = { 1: '第一季度', 2: '第二季度', 3: '第三季度', 4: '年度考核' };
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
			    { name: 'Id' }, { name: 'StageName' }, { name: 'ExamineType' }, { name: 'LaunchDeptName' }, { name: 'StageType' }, { name: 'Year'}]
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
                        title: ' 考核阶段',
                        store: store,
                        forceFit: true,
                        sm: seltype == "single" ? new Ext.grid.RowSelectionModel({ singleSelect: true }) : "",
                        //  bbar: pgBar,
                        region: 'center',
                        checkOnly: false,
                        autoExpandColumn: 'StageName',
                        columns: [
                    { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(), AimSelCheckModel,
                    { id: 'StageName', dataIndex: 'StageName', header: '考核阶段', width: 180, sortable: true },
                    { id: 'ExamineType', dataIndex: 'ExamineType', header: '考核类型', width: 100 },
                    { id: 'LaunchDeptName', dataIndex: 'LaunchDeptName', header: '发起部门', width: 100 },
                    { id: 'Year', dataIndex: 'Year', header: '年份', width: 100 },
                    { id: 'StageType', dataIndex: 'StageType', header: '阶段', width: 100, enumdata: StageEnum }
                    ]
                    });
                    viewport = new Ext.ux.AimViewport({
                        items: [AimSelGrid, buttonPanel]
                    });
                }          
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
