<%@ Page Title="自定义指标审批" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="CustomIndicatorApprove.aspx.cs" Inherits="Aim.Examining.Web.DeptConfig.CustomIndicatorApprove" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        body
        {
            background-color: #F2F2F2;
        }
        .aim-ui-td-caption
        {
            text-align: right;
        }
        .aim-ui-td-data .quarter
        {
            border: none;
            size: 20;
            width: 50;
            background: E9F0FC;
            border-bottom: 1px #000000 solid;
        }
        fieldset
        {
            border: solid 1px #8FAACF;
            margin: 15px;
            width: 100%;
            padding: 5px;
        }
        fieldset legend
        {
            font-size: 12px;
            font-weight: bold;
        }
    </style>
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
        var myData, store, grid, store1, grid1;
        function onPgLoad() {
            setPgUI();
            if (pgOperation != "u") {
                $("#trSign").show();
                $("#Opinion").attr("readonly", "readonly");
            }
            var title = AimState["Entity"].Year + "年第" + AimState["Entity"].StageType + "季度" + AimState["Entity"].DeptName + AimState["Entity"].CreateName + "【" + AimState["Entity"].IndicatorSecondName + "】自定义指标表";
            $("#divTitle").text(title);
            InitialOpinionGrid();
            grid1.getColumnModel().isCellEditable = function(colIndex, rowIndex) {
                if (pgOperation == "v") {
                    return false;
                }
                return Ext.grid.ColumnModel.prototype.isCellEditable.call(this, colIndex, rowIndex);
            }
            IniButton();
            window.onresize = function() {
                grid.setWidth(0);
                grid.setWidth(Ext.get("div1").getWidth());
                grid1.setWidth(0);
                grid1.setWidth(Ext.get("div2").getWidth());
            }
            if (pgOperation == "u") {
                var recType = store1.recordType;
                var rec = new recType({ CustomIndicatorId: $("#Id").val() });
                var task = new Ext.util.DelayedTask();
                task.delay(300, function() {
                    store1.insert(store1.data.length, rec);
                });
            }
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
			        { name: 'Id' }, { name: 'PersonSecondIndicatorName' }, { name: 'Weight' }, { name: 'ToolTip' }, { name: 'SelfRemark' },
			        { name: 'PersonFirstIndicatorId' }, { name: 'PersonFirstIndicatorName' }, { name: 'FirstWeight' }, { name: 'IndicatorType'}]
            });
            var clnsArr = [
                    { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                    { id: 'PersonFirstIndicatorId', dataIndex: 'PersonFirstIndicatorId', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'IndicatorType', dataIndex: 'IndicatorType', header: '工作类型', width: 100, renderer: RowRender },
                    { id: 'PersonFirstIndicatorName', dataIndex: 'PersonFirstIndicatorName', header: '考核指标', width: 150, renderer: RowRender },
                    { id: 'FirstWeight', dataIndex: 'FirstWeight', header: '权重', width: 60, renderer: RowRender },
                    { id: 'PersonSecondIndicatorName', dataIndex: 'PersonSecondIndicatorName', header: '量化指标', width: 150, renderer: RowRender },
                    { id: 'Weight', dataIndex: 'Weight', header: '权重', width: 60 },
                    { id: 'SelfRemark', dataIndex: 'SelfRemark', header: '自我评价', width: 150, renderer: RowRender }
                    ];
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '说明：鼠标悬停量化指标会显示具体的打分标准',
                store: store,
                // region: 'center',
                autoHeight: true,
                renderTo: 'div1',
                cls: 'grid-row-span',
                autoExpandColumn: 'PersonSecondIndicatorName',
                columns: clnsArr
            });
        }
        function InitialOpinionGrid() {
            var myData1 = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList1"] || []
            };
            store1 = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList1',
                idProperty: 'Id',
                data: myData1,
                fields: [
			        { name: 'Id' }, { name: 'CustomIndicatorId' }, { name: 'Opinion' }, { name: 'CreateId' }, { name: 'CreateName' },
			        { name: 'CreateTime'}]
            });
            grid1 = new Ext.ux.grid.AimEditorGridPanel({
                // title: AimState["BaseInfo"],
                store: store1,
                autoHeight: true,
                renderTo: 'div2',
                cls: 'grid-row-span',
                autoExpandColumn: 'Opinion',
                columns: [{ id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                    { id: 'CustomIndicatorId', dataIndex: 'CustomIndicatorId', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'Opinion', dataIndex: 'Opinion', header: '<font color="red">审批意见</font>', width: 200, editor: { xtype: 'textfield', allowBlank: false} },
                    { id: 'CreateName', dataIndex: 'CreateName', header: '审批人', width: 70 },
                    { id: 'CreateTime', dataIndex: 'CreateTime', header: '审批时间', width: 120}]
            });
        }
        function IniButton() {
            $("#btnSubmit").click(function() {
                var rec = store1.getAt(store1.data.length - 1);
                if (!rec.get("Opinion")) {
                    AimDlg.show("必须填写审批意见,才能提交！");
                    return;
                }
                var dt = store1.getModifiedDataStringArr([rec]) || [];
                $.ajaxExec("agree", { data: dt, id: $("#Id").val() }, function() { RefreshClose(); });
            });
            $("#btnSave").click(function() {
                var rec = store1.getAt(store1.data.length - 1);
                if (!rec.get("Opinion")) {
                    AimDlg.show("必须填写审批意见,才能提交！");
                    return;
                }
                var dt = store1.getModifiedDataStringArr([rec]) || [];
                $.ajaxExec("disagree", { data: dt, id: $("#Id").val() }, function() { RefreshClose(); });
            });
            $("#btnCancel").click(function() {
                window.close();
            });
        }
        function SuccessSubmit() {
        }
        function RowRender(value, meta, record, rowIndex, colIndex, store) {
            rtn = "";
            switch (this.id) {
                case "PersonFirstIndicatorName":
                case "IndicatorType":
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
                        var rowHeight = 27, padding = 10, height = (rowHeight * (i - rowIndex) - padding) + 'px';
                        meta.attr = 'style="height:' + height + ';line-height:' + height + ';"';
                    }
                    rtn = first ? '<b>' + value + '</b>' : '';
                    break;
                case "FirstWeight":
                    if (!value) {
                        return '';
                    }
                    var first = !rowIndex || record.get("PersonFirstIndicatorId") !== store.getAt(rowIndex - 1).get("PersonFirstIndicatorId"), last = rowIndex >= store.getCount() - 1 || record.get("PersonFirstIndicatorId") !== store.getAt(rowIndex + 1).get("PersonFirstIndicatorId");
                    meta.css += 'row-span' + (first ? ' row-span-first' : '') + (last ? ' row-span-last' : '');
                    if (first && last)
                        meta.css = 'row-span' + ' row-span-first-last';
                    if (first) {
                        var i = rowIndex + 1;
                        while (i < store.getCount() && record.get("PersonFirstIndicatorId") === store.getAt(i).get("PersonFirstIndicatorId")) {
                            i++;
                        }
                        var rowHeight = 27, padding = 10, height = (rowHeight * (i - rowIndex) - padding) + 'px';
                        meta.attr = 'style="height:' + height + ';line-height:' + height + ';"';
                    }
                    rtn = first ? '<b>' + value + '</b>' : '';
                    break;
                case "PersonSecondIndicatorName":
                    if (value) {
                        if (record.get("ToolTip")) {
                            var standard = eval(record.get("ToolTip"));
                            var tooltip = "<table style='font-size:12px'><tr style='font-weight:bold'><td style='width:30px'>序号</td><td style='width:150px'>内容</td><td style='width:120px'>打分标准</td></tr>";
                            for (var i = 0; i < standard.length; i++) {
                                tooltip += "<tr><td>" + (standard[i].SortIndex ? standard[i].SortIndex : '') + "</td><td>" + (standard[i].Content ? standard[i].Content : '') + "</td><td>" + (standard[i].ScoreRegion ? standard[i].ScoreRegion : '') + "</td></tr>";
                            }
                            tooltip += "</table>";
                            meta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + tooltip + '"';
                        }
                        rtn = value;
                    }
                    break;
                case "SelfRemark":
                    if (value) {
                        meta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
            }
            return rtn;
        }
       
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            自定义指标审批</h1>
    </div>
    <fieldset>
        <legend>自定义指标详细信息</legend>
        <div id="divTitle" style="text-align: center; font-size: 18px; color: Blue; font-weight: bold">
        </div>
        <div id="div1">
        </div>
    </fieldset>
    <fieldset>
        <legend>审批区</legend>
        <div id="div2">
        </div>
        <table class="aim-ui-table-edit" style="border: none">
            <tr style="display: none">
                <td colspan="4">
                    <input id="Id" name="Id" />
                </td>
            </tr>
        </table>
        <%-- <tr>
                <td class="aim-ui-td-caption">
                    主管领导意见
                </td>
                <td class="aim-ui-td-data" colspan="3">
                    <textarea id="Opinion" name="Opinion" style="width: 91%;" rows="3" cols="" class="validate[required]"></textarea>
                </td>
            </tr>
            <tr id="trSign" style="display: none">
                <td class="aim-ui-td-caption" style="width: 25%">
                    审批人
                </td>
                <td style="width: 25%">
                    <input id="ApproveUserName" name="ApproveUserName" readonly="readonly" />
                    <input id="ApproveUserId" name="ApproveUserId" type="hidden" />
                </td>
                <td style="width: 25%" class="aim-ui-td-caption">
                    审批时间
                </td>
                <td style="width: 25%">
                    <input id="ApproveTime" name="ApproveTime" readonly="readonly" />
                </td>
            </tr>
        </table>--%>
    </fieldset>
    <div style="width: 100%">
        <table class="aim-ui-table-edit">
            <tr>
                <td class="aim-ui-button-panel" colspan="4">
                    <a id="btnSubmit" class="aim-ui-button submit">同意</a>&nbsp;&nbsp;<a id="btnSave"
                        class="aim-ui-button submit">不同意</a>&nbsp;&nbsp; <a id="btnCancel" class="aim-ui-button cancel">
                            关闭</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
