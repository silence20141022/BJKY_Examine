<%@ Page Title="考核对象选择" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="ExamineRelationSelect.aspx.cs" Inherits=" Aim.Examining.Web.ExamineConfig.ExamineRelationSelect" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script src="/js/pgfunc-ext-sel.js" type="text/javascript"></script>

    <script type="text/javascript">

        var store, myData;
        var pgBar, schBar, tlBar, titPanel, AimSelGrid, viewport;
        var ExamineType = unescape($.getQueryString({ ID: "ExamineType" }));
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
                idProperty: 'EnumerationID',
                data: myData,
                fields: [{ name: 'Id' }, { name: 'RelationName' }, { name: 'BeRoleCode' }, { name: 'BeRoleName' },
			    { name: 'UpLevelCode' }, { name: 'UpLevelName' }, { name: 'UpLevelWeight' },
			    { name: 'SameLevelCode' }, { name: 'SameLevelName' }, { name: 'SameLevelWeight' },
			    { name: 'DownLevelCode' }, { name: 'DownLevelName' }, { name: 'DownLevelWeight' },
			    { name: 'Remark' }, { name: 'CreateId' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime'}]
            });
            // 分页栏
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            // 搜索栏
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 4,
                collapsed: false,
                items: [{ fieldLabel: '产品名称', id: 'Name', schopts: { qryopts: "{ mode: 'Like', field: 'Name' }"} },
                { fieldLabel: '产品型号', id: 'Code', schopts: { qryopts: "{ mode: 'Like', field: 'Code' }"} },
                { fieldLabel: '条形码', id: 'Isbn', schopts: { qryopts: "{ mode: 'Like', field: 'Isbn' }"} },
                { fieldLabel: 'PCN', id: 'Pcn', schopts: { qryopts: "{ mode: 'Like', field: 'Pcn' }"} }
                ]
            });

            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: ['->',
                {
                    text: '查  询',
                    iconCls: 'aim-icon-search',
                    handler: function() {
                        if (!schBar.collapsed) {
                            Ext.ux.AimDoSearch(Ext.getCmp("Code"));
                        }
                        else {
                            schBar.toggleCollapse(false);
                            setTimeout("viewport.doLayout()", 50);
                        }
                    }
}]
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
                    Ext.override(Ext.grid.CheckboxSelectionModel, {
                        handleMouseDown: function(g, rowIndex, e) {
                            if (e.button !== 0 || this.isLocked()) {
                                return;
                            }
                            var view = this.grid.getView();
                            if (e.shiftKey && !this.singleSelect && this.last !== false) {
                                var last = this.last;
                                this.selectRange(last, rowIndex, e.ctrlKey);
                                this.last = last; // reset the last     
                                view.focusRow(rowIndex);
                            } else {
                                var isSelected = this.isSelected(rowIndex);
                                if (isSelected) {
                                    this.deselectRow(rowIndex);
                                } else if (!isSelected || this.getCount() > 1) {
                                    this.selectRow(rowIndex, true);
                                    view.focusRow(rowIndex);
                                }
                            }
                        }
                    });
                    var columnsarray = [
                    { id: 'Id', dataIndex: 'Id', header: 'Id', hidden: true },
                    { id: 'BeRoleName', dataIndex: 'BeRoleName', header: 'BeRoleName', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'RelationName', dataIndex: 'RelationName', header: '考核关系名称', width: 160 },
                    { id: 'BeRoleName', dataIndex: 'BeRoleName', header: '考核对象', width: 150 },
                    { id: 'UpLevelName', dataIndex: 'UpLevelName', header: '<strong><font size=2>上级评分人</font></strong>', width: 120 },
                    { id: 'UpLevelWeight', dataIndex: 'UpLevelWeight', header: '权重', width: 40 },
                    { id: 'SameLevelName', dataIndex: 'SameLevelName', header: '<strong><font size=2>同级评分人</font></strong>', width: 120 },
                    { id: 'SameLevelWeight', dataIndex: 'SameLevelWeight', header: '权重', width: 40 },
                    { id: 'DownLevelName', dataIndex: 'DownLevelName', header: '<strong><font size=2>下级评分人</font></strong>', width: 120 },
                    { id: 'DownLevelWeight', dataIndex: 'DownLevelWeight', header: '权重', width: 40 }
				 ];
                    AimSelGrid = new Ext.ux.grid.AimGridPanel({
                        title: '考核对象',
                        store: store,
                        region: 'center',
                        autoExpandColumn: 'RelationName',
                        sm: seltype == "single" ? new Ext.grid.RowSelectionModel({ singleSelect: true }) : "",
                        columns: columnsarray
                    });
                    // 页面视图
                    viewport = new Ext.ux.AimViewport({
                        items: [AimSelGrid, buttonPanel]
                    });
                }
                function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
                    var rtn;
                    switch (this.id) {
                        case "Name":
                            if (value) {
                                value = value || "";
                                cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                                rtn = value;
                            }
                            break;
                    }
                    return rtn;
                }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
