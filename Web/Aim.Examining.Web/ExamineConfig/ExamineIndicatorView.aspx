<%@ Page Title="被考核人员" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="ExamineIndicatorView.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.ExamineIndicatorView" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .grid-row-span .x-grid3-row
        {
            border-bottom: 0;
        }
        .grid-row-span .x-grid3-col
        {
            border-bottom: 1px solid gray;
        }
        .grid-row-span .row-span
        {
            border-bottom: 1px solid #fff;
        }
        .grid-row-span .row-span-first
        {
            position: relative;
        }
        .grid-row-span .row-span-first .x-grid3-cell-inner
        {
            position: absolute;
            border-right: 1px solid gray;
        }
        .grid-row-span .row-span-last
        {
            border-bottom: 1px solid gray !important;
        }
        .grid-row-span .row-span-first-last
        {
            border-right: 1px solid gray !important;
            border-bottom: 1px solid gray !important;
        }
    </style>

    <script type="text/javascript">
        var myData, store, grid;
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'UserID',
                data: myData,
                fields: [
			        { name: 'Id' }, { name: 'IndicatorFirstName' }, { name: 'IndicatorFirstId' }, { name: 'IndicatorSecondName' },
			        { name: 'MaxScore' }, { name: 'SortIndex' }, { name: 'BMaxScore' }, { name: 'BSortIndex'}]
            });
            var clnsArr = [
                    { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'IndicatorFirstName', dataIndex: 'IndicatorFirstName', header: '考核项', width: 150, renderer: RowRender },
                    { id: 'BMaxScore', dataIndex: 'BMaxScore', header: '权重', width: 100, renderer: RowRender2 },
                    { id: 'IndicatorSecondName', dataIndex: 'IndicatorSecondName', header: '考核要素', width: 150 },
                    { id: 'MaxScore', dataIndex: 'MaxScore', header: '权重', width: 100 }
                    ];
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: AimState["BaseInfo"],
                store: store,
                region: 'center',
                cls: 'grid-row-span',
                autoExpandColumn: 'IndicatorFirstName',
                columns: clnsArr
            });
            // 页面视图
            viewport = new Ext.ux.AimViewport({
                layout: 'border',
                items: [grid]
            })
        }
        function RowRender(value, meta, record, rowIndex, colIndex, store) {
            if (!value) {
                return '';
            }
            var first = !rowIndex || value !== store.getAt(rowIndex - 1).get(this.id), last = rowIndex >= store.getCount() - 1 || value !== store.getAt(rowIndex + 1).get(this.id);
            meta.css += 'row-span' + (first ? ' row-span-first' : '') + (last ? ' row-span-last' : '');
            if (first && last)
                meta.css = 'row-span' + ' row-span-first-last';
            if (first) {
                var i = rowIndex + 1;
                while (i < store.getCount() && value === store.getAt(i).get(this.id)) {
                    i++;
                }
                var rowHeight = 23, padding = 8, height = (rowHeight * (i - rowIndex) - padding) + 'px';
                meta.attr = 'style="height:' + height + ';line-height:' + height + ';"';
            }
            return first ? '<b>' + value + '</b>' : '';
        }


        function RowRender2(value, meta, record, rowIndex, colIndex, store) {
            if (!value) {
                return '';
            }

            var first = !rowIndex || record.get("IndicatorFirstId") !== store.getAt(rowIndex - 1).get("IndicatorFirstId"), last = rowIndex >= store.getCount() - 1 || record.get("IndicatorFirstId") !== store.getAt(rowIndex + 1).get("IndicatorFirstId");
            meta.css += 'row-span' + (first ? ' row-span-first' : '') + (last ? ' row-span-last' : '');
            if (first && last)
                meta.css = 'row-span' + ' row-span-first-last';
            if (first) {
                var i = rowIndex + 1;
                while (i < store.getCount() && record.get("IndicatorFirstId") === store.getAt(i).get("IndicatorFirstId")) {
                    i++;
                }
                var rowHeight = 23, padding = 8, height = (rowHeight * (i - rowIndex) - padding) + 'px';
                meta.attr = 'style="height:' + height + ';line-height:' + height + ';"';
            }
            return first ? '<b>' + value + '</b>' : '';
        }

    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
