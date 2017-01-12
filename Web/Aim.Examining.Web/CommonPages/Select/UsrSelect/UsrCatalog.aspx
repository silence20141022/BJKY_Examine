<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true" CodeBehind="UsrCatalog.aspx.cs" Inherits="Aim.Portal.Web.CommonPages.UsrCatalog" %>
<%@ OutputCache Duration="1" VaryByParam="None" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <link href="/App_Themes/Ext/ux/TreeGrid/TreeGrid.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/Ext/ux/TreeGrid/TreeGridLevels.css" rel="stylesheet" type="text/css" />

    <script src="/js/ext/ux/TreeGrid.js" type="text/javascript"></script>

    <script type="text/javascript">
        var StatusEnum = { 1: "启用", 0: "停用" };
        var usrCatalogType = [['group', '组']];

        var viewport, store, grid;
        var DataRecord, selCombo;

        function onPgLoad() {
            setPgUI();
        }

        function setPgUI() {
            var data = adjustData(AimState["DtList"]);

            DataRecord = Ext.data.Record.create([
                { name: 'ID', type: 'string' },
                { name: 'ParentID', type: 'string' },
                { name: 'IsLeaf', type: 'bool' },
                { name: 'Name' },
                { name: 'Code' },
                { name: 'Type' },
                { name: 'Status' },
                { name: 'Description' },
                { name: 'CreateDate'}]);

            store = new Ext.ux.maximgb.tg.AdjacencyListStore({
                autoLoad: true,
                parent_id_field_name: 'ParentID',
                leaf_field_name: 'IsLeaf',
                data: data,
                reader: new Ext.ux.data.AimJsonReader({ id: 'ID', dsname: 'DtList', aimread: function(rd, resp, dt) {
                    if (dt) {
                        dt = adjustData(dt);
                    }
                }
                }, DataRecord),
                proxy: new Ext.ux.data.AimRemotingProxy({
                    aimbeforeload: function(proxy, options) {
                        var rec = store.getById(options.anode);
                        options.reqaction = "querychildren";
                        options.type = rec.data.Type;
                        options.data = rec ? (rec.json || {}) : {};
                    }
                })
            });

            selCombo = new Ext.form.ComboBox({
            forceSelection: true,
                width: 120,
                triggerAction: 'all',
                emptyText: '选择视图...',
                store: usrCatalogType,
                listeners: {
                    "select": function(combo, rec, idx) {
                        if (parent.SetCatalog) {
                            parent.SetCatalog(combo.getValue());
                        }
                    },
                    "afterrender": function() {
                        if (!this.getValue()) {
                            this.setValue('group');
                        }
                    }
                }
            });

            // 工具栏
            var tlBar = new Ext.Toolbar({
                items: [{ text: "视图：" }, selCombo ]
            });

            // 表格面板
            grid = new Ext.ux.maximgb.tg.GridPanel({
                store: store,
                master_column_id: 'Name',
                region: 'center',
                border: false,
                width: 180,
                minSize: 100,
                maxSize: 200,
                columns: [
				{ id: 'Name', header: "名称", width: 160, sortable: true, dataIndex: 'Name' }
      ],
                autoExpandColumn: 'Name',
                tbar: tlBar
            });

            grid.getSelectionModel().on('rowselect', function(sm, rowIdx, r) {
                if (parent.SetCatalog) {
                    parent.SetCatalog(selCombo.getValue(), { id: r.id, type: r.data.Type });
                }
            });

            // 页面视图
            viewport = new Ext.Viewport({
                layout: 'border',
                items: [{ xtype: 'box', region: 'north', applyTo: 'header', height: 30 }, grid]
            });

            //store.expandAll();

            //grid.getSelectionModel().selectFirstRow();
        }

        // 应用或模块数据适配
        function adjustData(jdata) {
            if ($.isArray(jdata)) {
                $.each(jdata, function() {
                    if (this.GroupID) {
                        this.ID = this.GroupID;
                        this.ParentID = $.isSetted(this.ParentID) ? this.ParentID : this.Type;
                    } else if (this.GroupTypeID) {
                        this.ID = this.GroupTypeID;
                        this.Type = "GType";
                        this.ParentID = null;
                        this.IsLeaf = $.isSetted(this.HasGroup) ? !this.HasGroup : false;
                    }
                });

                return jdata;
            } else {
                return [];
            }
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header" style="display:none;"><h1>组列表</h1></div>
</asp:Content>
