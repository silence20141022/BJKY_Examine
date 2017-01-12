<%@ Page Title="枚举视图" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true" CodeBehind="EnumView.aspx.cs" Inherits="Aim.Portal.Web.CommonPages.EnumView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <link href="/App_Themes/Ext/ux/TreeGrid/TreeGrid.css" rel="stylesheet" type="text/css" />

    <script src="/js/ext/ux/TreeGrid.js" type="text/javascript"></script>
    <script src="/js/ext/ux/FieldLabeler.js" type="text/javascript"></script>
    <script src="/js/pgfunc-ext-adv.js" type="text/javascript"></script>

    <script type="text/javascript">
        var EditWinStyle = "dialogWidth:600px; dialogHeight:330px; scroll:yes; center:yes; status:no; resizable:yes;";
        var EditPageUrl = "/Modules/SysApp/SysMag/Enum/SysEnumerationEdit.aspx";

        var viewport, store, grid, contextMenu;
        var DataRecord;

        var rootid, root, editable, showroot, editpageurl, editpagestyle, tag;

        function onPgLoad() {
            editable = $.getQueryString({ ID: "editable", DefaultValue: "" });
            showroot = $.getQueryString({ ID: "showroot", DefaultValue: "" });
            tag = $.getQueryString({ ID: "tag", DefaultValue: "" });
            
            editpageurl = $.getQueryString({ ID: "editpageurl", DefaultValue: "" });
            editpagestyle = $.getQueryString({ ID: "editpagestyle", DefaultValue: "" });

            if (!editpageurl) {
                editpageurl = EditPageUrl;
            } else {
                editpageurl = unescape(editpageurl);
            }

            if (tag) {
                editpageurl = $.combineQueryUrl(editpageurl, { tag: tag });
            }

            if (!editpagestyle) {
                editpagestyle = EditWinStyle;
            } else {
                editpagestyle = unescape(editpagestyle);
            }
            
            root = AimState["Root"];

            if (root) {
                rootid = root.EnumerationID;
            }
            
            setPgUI();
        }

        function setPgUI() {
            var data = adjustData(AimState["DtList"]) || [];

            DataRecord = Ext.data.Record.create([
			{ name: 'EnumerationID' },
			{ name: 'Code' },
			{ name: 'Name' },
			{ name: 'Value' },
			{ name: 'ParentID' },
			{ name: 'Path' },
			{ name: 'PathLevel' },
			{ name: 'IsLeaf' },
			{ name: 'SortIndex' },
			{ name: 'EditStatus' },
			{ name: 'Tag' },
			{ name: 'Description' },
			{ name: 'CreaterID' },
			{ name: 'CreaterName' },
			{ name: 'LastModifiedDate' },
			{ name: 'CreatedDate'}]);

            store = new Ext.ux.data.AimAdjacencyListStore({
                data: data,
                aimbeforeload: function(proxy, options) {
                    var rec = store.getById(options.anode);
                    options.reqaction = "querychildren";
                    options.data = options.data || {};
                    if (rec) {
                        options.data.pids = [rec.id];
                    }
                },
                reader: new Ext.ux.data.AimJsonReader({ id: 'EnumerationID', dsname: 'DtList',
                    aimread: function(rd, resp, dt) {
                        if (dt) {
                            dt = adjustData(dt);
                        }
                    }
                }, DataRecord)
            });

            // 搜索栏
            var schBar = new Ext.ux.AimSchPanel({
                items: []
            });

            // 工具栏
            var tlBar = new Ext.ux.AimToolbar({
                items: [{ text: '展开', handler: function() { store.expandAll(); }
},'-']
                });

            // 工具标题栏
            var titPanel = new Ext.Panel({
                tbar: tlBar,
                items: [schBar]
            });

            var sm = new Ext.grid.RowSelectionModel({ singleSelect: false,
                listeners: {
                    rowselect: function(g, ridx, e) {
                        var rec = store.getAt(ridx);

                        if (typeof (parent.OnViewRowSelect) == 'function') {
                            // 防止刷新时闪动（选中全部页面）
                            var task = new Ext.util.DelayedTask(function() {
                                parent.OnViewRowSelect.call(g, rec);
                            });

                            task.delay(100);
                        }
                    }
                }
            });

            // 表格面板
            grid = new Ext.ux.grid.AimEditorTreeGridPanel({
                store: store,
                master_column_id: 'Name',
                region: 'center',
                width: 300,
                minSize: 250,
                maxSize: 500,
                columns: [
				{ id: 'Name', dataIndex: 'Name', header: "名称", width: 150, sortable: true }
      ],
                sm: sm,
                autoExpandColumn: 'Name',
                tbar: titPanel
            });

            grid.on("rowcontextmenu", function(grid, rowIdx, e) {
                if (editable) {
                    e.preventDefault(); //这行是必须的
                    showContextMenu(rowIdx, e.getXY());
                }
            });

            grid.on("rowclick", function(grid, rowIndex, e) {
                var rec = grid.store.getAt(rowIndex);

                if (typeof (parent.OnSelViewRowClick) == 'function') {
                    parent.OnViewRowClick.call(this, rec);
                }
            });

            grid.on("rowdblclick", function(grid, rowIndex, e) {
                var rec = grid.store.getAt(rowIndex);

                if (typeof (parent.OnSelViewRowDblClick) == 'function') {
                    parent.OnViewRowDblClick.call(this, rec);
                }
            });

            // 页面视图
            viewport = new Ext.ux.AimViewport({
                layout: 'border',
                items: [{ xtype: 'box', region: 'north', applyTo: 'header', height: 30 }, grid]
            });

            store.expandAll();
        }

        function adjustData(jdata) {
            if ($.isArray(jdata)) {
                $.each(jdata, function() {
                    if (showroot && showroot != "0" && showroot.toLowerCase() != "false") {
                        if (rootid == this.EnumerationID) {
                            this.ParentID = null;
                        }
                    } else {
                        if (rootid && rootid == this.ParentID) {
                            this.ParentID = null;
                        }
                    }
            });

                return jdata;
            } else {
                return [];
            }
        }

        function showContextMenu(rowIdx, xy) {
            var rec = store.getAt(rowIdx);

            if (!contextMenu) {
                contextMenu = new Ext.menu.Menu({ id: 'contextMenu' });

                menuItemAddCls = new Ext.menu.Item({
                    id: 'menuItemAddSid',
                    text: '新增兄弟枚举'
                });

                menuItemAdd = new Ext.menu.Item({
                    id: 'menuItemAddSub',
                    text: '新增子枚举'
                });

                menuItemUpdate = new Ext.menu.Item({
                    id: 'menuItemUpdate',
                    text: '修改'
                });

                menuItemDelete = new Ext.menu.Item({
                    id: 'menuItemDelete',
                    text: '删除'
                });

                contextMenu.addItem(menuItemAddCls);
                contextMenu.addItem(menuItemAdd);

                contextMenu.addItem(menuItemUpdate);
                contextMenu.addItem(menuItemDelete);
            }

            menuItemAddCls.setHandler(function() { showEditWin('c', rec); })    // 创建节点
            menuItemAdd.setHandler(function() { showEditWin('cs', rec); })      // 创建子节点
            menuItemUpdate.setHandler(function() { showEditWin('u', rec); })    // 更新节点
            menuItemDelete.setHandler(function() { batchOperate('batchdelete', [rec]); })    // 删除节点

            if (!rec.get('ParentID')) {
                menuItemAddCls.setDisabled(true);
                menuItemDelete.setDisabled(true);
                menuItemUpdate.setDisabled(true);
            } else {
                menuItemAddCls.setDisabled(false);
                menuItemDelete.setDisabled(false);
                menuItemUpdate.setDisabled(false);
            }

            contextMenu.showAt(xy);
        }

        function batchOperate(action, recs, params, url) {
            if (!recs || recs.length <= 0) {
                AimDlg.show("请先选择要操作的记录！");
                return;
            } else if (!confirm("确定执行删除操作？")) {
                return;
            }

            ExtBatchOperate(action, recs, params, url, function(args) {
                if (args.status == "success") {
                    // 从store中删除已删除的记录
                    var pids = $.map(recs, function(n, i) {
                        var tpid = n.data["ParentID"];
                        store.remove(n);
                        return tpid;
                    });

                    // 刷新已删除的记录的父节点
                    store.reload({ ids: pids, pids: pids });

                    store.singleSort('CreatedDate', 'ASC');
                }
            });
        }

        function showEditWin(op, rec) {
            OpenModelWin(editpageurl, { op: op, id: rec.id }, editpagestyle, function() {
                var expnode = null; // 重新加载后需要展开的节点
                switch (op) {
                    case 'c':
                        if (rec && rec.get('ParentID')) {
                            var pnode = store.getById(rec.json.ParentID);
                            store.remove(pnode);
                            expnode = pnode;

                            store.reload({ ids: [pnode.id], pids: [pnode.id] });
                        }
                        break;
                    case 'cs':
                    case 'u':
                        // store.remove(rec);
                        store.reload({ ids: [rec.id] });

                        if (op == 'cs') {
                            expnode = rec;
                        }
                        break;
                }

                if (expnode && expnode.id) {
                    // 展开节点
                    $.ensureExec(function() {
                        var npnode = store.getById(expnode.id);
                        if (npnode) {
                            store.expandNode(npnode);
                            return true;
                        }

                        return false;
                    });
                }

                store.singleSort('CreatedDate', 'ASC');
            });
        }

        // 提交数据成功后
        function onExecuted() {
            // store.reload();
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header" style="display:none;"><h1>枚举视图</h1></div>
</asp:Content>
