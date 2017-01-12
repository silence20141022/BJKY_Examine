<%@ Page Title="考核指标选择" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="IndicatorSelect.aspx.cs" Inherits=" Aim.Examining.Web.ExamineConfig.IndicatorSelect" %>

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
                idProperty: 'Id',
                data: myData,
                fields: [
                { name: 'Id' }, { name: 'IndicatorName' }, { name: 'BeRoleName' }, { name: 'BelongDeptName' }, { name: 'CreateTime' }, { name: 'CreateName' }, { name: 'Remark'}]
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
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        ExtOpenGridEditWin(grid, EditPageUrl, "c", EditWinStyle);
                    }
                }, '-', {
                    text: '修改',
                    iconCls: 'aim-icon-edit',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要修改的记录！");
                            return;
                        }
                        ExtOpenGridEditWin(grid, EditPageUrl, "u", EditWinStyle);
                    }
                }, '-', {
                    text: '删除',
                    iconCls: 'aim-icon-delete',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要删除的记录！");
                            return;
                        }
                        if (confirm("删除考核指标会连同考核项目、考核要素、考核标准一起删除，确定删除所选记录吗？")) {
                            ExtBatchOperate('batchdelete', recs, null, null, function() {
                                store.remove(recs);
                                recs = store.getRange();
                                if (recs.length > 0) {
                                    frameContent.location.href = "IndicatorFirstList.aspx?IndicatorName=" + escape(recs[0].get("IndicatorName")) + "&ExamineIndicatorId=" + recs[0].get("Id");
                                }
                                else {
                                    frameContent.location.href = "IndicatorFirstList.aspx?IndicatorName=" + "&ExamineIndicatorId=";
                                }
                            });
                        }
                    }
                },
                 '-', {
                     text: '复制',
                     iconCls: 'aim-icon-copy',
                     handler: function() {
                         var recs = grid.getSelectionModel().getSelections();
                         if (!recs || recs.length <= 0) {
                             AimDlg.show("请先选择要复制的记录！");
                             return;
                         }
                         if (confirm("复制考核指标会连同考核项目、考核要素、考核标准一起复制，确定复制所选记录吗？")) {
                             CopyRecord(recs[0].get("Id"));
                         }
                     }
                 }
			]
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
                AimSelGrid = new Ext.ux.grid.AimGridPanel({
                    title: '考核对象',
                    store: store,
                    region: 'center',
                    sm: seltype == "single" ? new Ext.grid.RowSelectionModel({ singleSelect: true }) : "",
                    autoExpandColumn: 'IndicatorName',
                    columns: [
                        { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                        new Ext.ux.grid.AimRowNumberer(),
                        AimSelCheckModel,
                        { id: 'IndicatorName', dataIndex: 'IndicatorName', header: '考核指标名称', width: 180 },
                        { id: 'BeRoleName', dataIndex: 'BeRoleName', width: 100, header: '考核对象' },
                        { id: 'BelongDeptName', dataIndex: 'BelongDeptName', width: 100, header: '所属部门' },
                        { id: 'Remark', dataIndex: 'Remark', width: 100, header: '备注' },
                        { id: 'CreateName', dataIndex: 'CreateName', width: 100, header: '录入人' },
                        { id: 'CreateTime', dataIndex: 'CreateTime', width: 100, header: '录入时间' }
                        ]
                    // tbar: tlBar
                    // bbar: pgBar
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
                    case "ContractCode":
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
