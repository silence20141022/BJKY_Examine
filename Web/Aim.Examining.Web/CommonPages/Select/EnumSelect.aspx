<%@ Page Title="枚举选择" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="EnumSelect.aspx.cs" Inherits="Aim.Examining.Web.CommonPages.Select.EnumSelect" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script src="/js/pgfunc-ext-sel.js" type="text/javascript"></script>

    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=650,height=600,scrollbars=yes");
        var EditPageUrl = "P_SupplierEdit.aspx";

        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;

        function onSelPgLoad() {
            setPgUI();
        }

        function setPgUI() {

            // 表格数据
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["P_SupplierList"] || []
            };

            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'P_SupplierList',
                idProperty: 'Id',
                data: myData,
                fields: [
			{ name: 'Id' },
			{ name: 'Value' },
			{ name: 'Name' },
			{ name: 'CreateId' },
			{ name: 'CreateName' },
			{ name: 'CreateTime' }
			]
            });

            // 分页栏
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });

            // 搜索栏
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                items: []
            });

            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [{ text: '选择', iconCls: 'aim-icon-accept', handler: function() {
                    AimGridSelect();
                }
                }, '<font color=red>请点击复选框选择/取消选择记录</font>', '->']
            });

            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });

            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                store: store,
                region: 'center',
                checkOnly: true,
                autoExpandColumn: 'Name',
                columns: [
                    { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    AimSelCheckModel,
					{ id: 'Value', dataIndex: 'Value', header: '枚举值', width: 80, sortable: true },
					{ id: 'Name', dataIndex: 'Name', header: '枚举名', width: 110, sortable: true }
                    ],
                tbar: titPanel
            });
            AimSelGrid = grid;
            // 页面视图
            viewport = new Ext.ux.AimViewport({
                items: [{ xtype: 'box', region: 'north', applyTo: 'header', height: 30 }, grid]
            });
        }

        function linkRender(val, p, rec) {
            var rtn = "";
            switch (this.id) {
                case "PrjManage":
                    rtn = "<a class='aim-ui-link' onclick='openMdlWin(\"P_SupplyDetailList.aspx?SupplierId=" + rec.data.Id + "&Name=" + escape(rec.data.Name) + "&Code=" + rec.data.Code + "\")'>产品明细</a>";
                    break;
            }
            return rtn;
        }
        function openMdlWin(url) {
            // window.open(url, window, EditWinStyle);
            //window.open(url, "", EditWinStyle, "");
            style = CenterWin("width=550,height=500,scrollbars=yes");
            ExtOpenGridEditWin(grid, url, "c", style);
        }
        // 提交数据成功后
        function onExecuted() {
            store.reload();
        }
    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header" style="display: none;">
        <h1>
            标题</h1>
    </div>
</asp:Content>
