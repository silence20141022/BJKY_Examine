<%@ Page Title="请先选择部门指标" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="True" CodeBehind="DeptIndicatorSelect.aspx.cs" Inherits=" Aim.Examining.Web.DeptConfig.DeptIndicatorSelect" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script src="/js/pgfunc-ext-sel.js" type="text/javascript"></script>

    <script type="text/javascript">

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
                { name: 'Id' }, { name: 'MaxScore' }, { name: 'IndicatorSecondName' }, { name: 'BelongDeptName' }, { name: 'BeRoleName' }, { name: 'IndicatorName'}]
            });
            // 分页栏
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            //            titPanel = new Ext.ux.AimPanel({
            //                tbar: tlBar,
            //                items: [schBar]
            //            });
            var buttonPanel = new Ext.form.FormPanel({
                region: 'south',
                frame: true,
                buttonAlign: 'center',
                buttons: [{ text: '确定', handler: function() {
                    AimGridSelect();
                }
                }, { text: '取消', handler: function() {
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
                    title: '部门允许自定义指标选择--》说明：请正确选择本部门针对自己的考核指标，注意区分角色或组',
                    store: store,
                    region: 'center',
                    autoExpandColumn: 'IndicatorName',
                    sm: seltype == "single" ? new Ext.grid.RowSelectionModel({ singleSelect: true }) : "",
                    columns: [
                        new Ext.ux.grid.AimRowNumberer(),
                        new Ext.ux.grid.AimCheckboxSelectionModel(),
                        { id: 'BelongDeptId', dataIndex: 'BelongDeptId', header: '部门Id', hidden: true },
					    { id: 'BelongDeptName', dataIndex: 'BelongDeptName', header: '部门', width: 120 },
					    { id: 'IndicatorName', dataIndex: 'IndicatorName', header: '部门指标名称', width: 200 },
					    { id: 'BeRoleName', dataIndex: 'BeRoleName', header: '考核角色', width: 100 },
					    { id: 'IndicatorSecondName', dataIndex: 'IndicatorSecondName', header: '部门允许自定义二级指标名称', width: 180 },
					    { id: 'MaxScore', dataIndex: 'MaxScore', header: '权重', width: 100 }
                        ]
                    // tbar: titPanel,
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
