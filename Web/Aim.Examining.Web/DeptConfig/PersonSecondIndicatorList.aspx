<%@ Page Title="自定义量化指标" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="PersonSecondIndicatorList.aspx.cs" Inherits="Aim.Examining.Web.DeptConfig.PersonSecondIndicatorList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=650,height=600,scrollbars=yes");
        var EditPageUrl = "PersonSecondIndicatorEdit.aspx";
        var PersonFirstIndicatorId = $.getQueryString({ ID: 'PersonFirstIndicatorId' });
        var PersonFirstIndicatorName = unescape($.getQueryString({ ID: 'PersonFirstIndicatorName' }));
        var maxScore = $.getQueryString({ ID: 'maxScore' });
        var State = $.getQueryString({ ID: 'State' });
        var Result = unescape($.getQueryString({ ID: 'Result' }));
        var store, myData, pgBar, schBar, tlBar, titPanel, grid, viewport;
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
                idProperty: 'Id',
                data: myData,
                fields: [
			{ name: 'Id' }, { name: 'PersonFirstIndicatorId' }, { name: 'PersonFirstIndicatorName' }, { name: 'PersonSecondIndicatorName' },
			{ name: 'Weight' }, { name: 'SortIndex' }, { name: 'ToolTip' }, { name: 'SelfRemark' }, { name: 'CreateTime' },
			{ name: 'CreateId' }, { name: 'CreateName' }
			],
                listeners: { aimbeforeload: function(proxy, options) {
                    options.data.PersonFirstIndicatorId = PersonFirstIndicatorId;
                    options.data.PersonFirstIndicatorName = PersonFirstIndicatorName;
                }
                }
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        if (PersonFirstIndicatorId && parseInt(maxScore) > 0) {
                            var sortIndex = 0;
                            var lastRec = store.getRange();
                            var currentScore = 0;
                            $(lastRec).each(function() {
                                sortIndex = parseInt(this.get("SortIndex")) > sortIndex ? this.get("SortIndex") : sortIndex;
                                currentScore += parseInt(this.get("Weight"));
                            });
                            if (maxScore == currentScore) {
                                AimDlg.show("权重已闭合,无法新增");
                                return;
                            }

                            EditPageUrl += "?sortIndex=" + (sortIndex + 1);
                            EditPageUrl += "&maxScore=" + (maxScore - currentScore);
                            EditPageUrl += "&PersonFirstIndicatorId=" + PersonFirstIndicatorId;
                            EditPageUrl += "&PersonFirstIndicatorName=" + PersonFirstIndicatorName;
                            ExtOpenGridEditWin(grid, EditPageUrl, "c", EditWinStyle);
                            EditPageUrl = "PersonSecondIndicatorEdit.aspx";
                        }
                        else {
                            AimDlg.show("请先选择上级考核指标并配置上级权重！");
                            return;
                        }
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
                        var sortIndex = 0;
                        var lastRec = store.getRange();
                        var currentScore = 0;
                        $(lastRec).each(function() {
                            sortIndex = parseInt(this.get("SortIndex")) > sortIndex ? this.get("SortIndex") : sortIndex;
                            currentScore += parseInt(this.get("Weight"));
                        });
                        currentScore = maxScore - currentScore + grid.getSelectionModel().getSelections()[0].get("Weight") || 0;
                        EditPageUrl += "?maxScore=" + currentScore;
                        ExtOpenGridEditWin(grid, EditPageUrl, "u", EditWinStyle);
                        EditPageUrl = "PersonSecondIndicatorEdit.aspx";
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
                        if (confirm("确定删除所选记录？")) {
                            ExtBatchOperate('delete', recs, null, null, onExecuted);
                        }
                    }
}]
                });
                grid = new Ext.ux.grid.AimEditorGridPanel({
                    title: '【' + (PersonFirstIndicatorName ? PersonFirstIndicatorName : "") + '】量化指标-->自我评价在审批同意后直接在列表编辑，系统自动保存；考核启动后不能修改',
                    store: store,
                    region: 'center',
                    autoExpandColumn: 'SelfRemark',
                    columns: [
                    { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'PersonSecondIndicatorName', dataIndex: 'PersonSecondIndicatorName', header: '量化指标', width: 120, renderer: RowRender },
					{ id: 'Weight', dataIndex: 'Weight', header: '权重', width: 50, sortable: true },
					{ id: 'ToolTip', dataIndex: 'ToolTip', header: '打分标准', width: 80, renderer: RowRender },
					{ id: 'SelfRemark', dataIndex: 'SelfRemark', header: '<font color="red">自我评价</font>', width: 120, renderer: RowRender, editor: { xtype: 'textarea'} }
                    ],
                    tbar: (State == "1" || Result == "同意") ? "" : tlBar,
                    listeners: { afteredit: function(e) {
                        if (e.value) {
                            $.ajaxExec("AutoSave", { id: e.record.get("Id"), SelfRemark: e.value }, function() {
                                e.record.commit();
                            })
                        }
                    }
                    }
                });
                viewport = new Ext.ux.AimViewport({
                    items: [grid]
                });
                grid.getColumnModel().isCellEditable = function(colIndex, rowIndex) {
                    if (Result != "同意" || State == "3") {
                        return false;
                    }
                    return Ext.grid.ColumnModel.prototype.isCellEditable.call(this, colIndex, rowIndex);
                }
            }
            function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
                var rtn;
                switch (this.id) {
                    case "PersonSecondIndicatorName":
                    case "SelfRemark":
                        if (value) {
                            value = value || "";
                            cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                            rtn = value;
                        }
                        break;
                    case "ToolTip":
                        if (value) {
                            var val = eval(value);
                            var temp = "";
                            for (var v = 0; v < val.length; v++) {
                                if (val[v].Content || val[v].ScoreRegion) {
                                    temp += (val[v].Content || '') + (val[v].ScoreRegion ? ' :' + val[v].ScoreRegion : '') + ";";
                                }
                            }
                            cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + temp + '"';
                            rtn = temp;
                        }
                        break;
                }
                return rtn;
            }
            function OepnWin() {
                EditPageUrl += "?act=show";
                ExtOpenGridEditWin(grid, EditPageUrl, "", EditWinStyle);
            }
            function onExecuted() {
                store.reload();
            }
    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
