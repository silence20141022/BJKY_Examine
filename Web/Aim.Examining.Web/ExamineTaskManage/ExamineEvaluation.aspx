<%@ Page Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="ExamineEvaluation.aspx.cs" Inherits="Aim.Examining.Web.ExamineTaskManage.ExamineEvaluation"
    Title="考核任务" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <link href="../App_Themes/Ext/ux/css/ColumnHeaderGroup.css" rel="stylesheet" type="text/css" />

    <script src="../js/ext/ux/ColumnHeaderGroup.js" type="text/javascript"></script>

    <style type="text/css">
        .x-grid3-hd-inner
        {
            white-space: normal;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="/App_Themes/Ext/ux/css/LockingGridView.css" />

    <script type="text/javascript">
        var pgBar, schBar, tlBar, titPanel, grid, viewport, myData, store;
        var Index = $.getQueryString({ ID: "Index" });
        var fieldArray = [];
        var colArray = [];
        var rowArray = [];
        function onPgLoad() {
            setPageUI();
        }
        function setPageUI() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"]
            };
            if (AimState["ColumnData"]) {
                var coldata = AimState["ColumnData"];
                for (var i = 0; i < coldata.length; i++) {
                    fieldArray.push(coldata[i].ColumnName);
                }
                colArray = [new Ext.ux.grid.AimRowNumberer(), new Ext.ux.grid.AimCheckboxSelectionModel()];
                for (var j = 0; j < coldata.length; j++) {
                    switch (coldata[j].ColumnName) {
                        case "Id":
                            colArray.push({ id: coldata[j].ColumnName, dataIndex: coldata[j].ColumnName, header: coldata[j].ColumnName, hidden: true, width: 100 });
                            break;
                        case "BeUserName":
                            colArray.push({ id: coldata[j].ColumnName, dataIndex: coldata[j].ColumnName, header: '考核对象', width: 80, align: 'center', locked: true });
                            break;
                        case "BeDeptName":
                            colArray.push({ id: coldata[j].ColumnName, dataIndex: coldata[j].ColumnName, header: '考核对象部门', width: 100, align: 'center', locked: true, renderer: RowRender });
                            break;
                        case "Score":
                            colArray.push({ id: coldata[j].ColumnName, dataIndex: coldata[j].ColumnName, header: '合计分', width: 70, align: 'center', locked: true });
                            break;
                        case "Tag":
                            colArray.push({ id: coldata[j].ColumnName, dataIndex: coldata[j].ColumnName, header: coldata[j].ColumnName, width: 70, hidden: true });
                            break;
                        default:
                            var index = coldata[j].ColumnName.indexOf('!'); //!号的索引位置
                            var header = coldata[j].ColumnName.slice(36, index).replace(/\s+/g, "");
                            var tpindex = coldata[j].ColumnName.indexOf('#');
                            var strTip = coldata[j].ColumnName.slice(tpindex + 1, coldata[j].ColumnName.length);
                            var arrayTip = strTip.split("$");
                            var formatTip = "<table style='font-size:10px'><tr><td colspan='2' style='font-weight:bold'>考核标准描述</td></tr>";
                            for (var n = 0; n < arrayTip.length; n++) {
                                if (arrayTip[n]) {
                                    var temparray = arrayTip[n].split("@");
                                    formatTip += "<tr><td>" + temparray[0] + '</td><td>' + (temparray[1] ? temparray[1] : "") + "</td></tr>";
                                }
                            }
                            formatTip += "</table>"
                            colArray.push({ id: coldata[j].ColumnName.replace(/\s+/g, ""), dataIndex: coldata[j].ColumnName.replace(/\s+/g, ""), header: header, width: 100, tooltip: formatTip, renderer: RowRender,
                                align: 'center', editor: { xtype: 'numberfield', minValue: 0, id: coldata[j].ColumnName.slice(0, 36) + '_subscore', allowBlank: false, decimalPrecision: 2 }
                            });
                            break;
                    }
                }
            }
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData,
                fields: fieldArray
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '提交',
                    iconCls: 'aim-icon-submit',
                    handler: function() {
                        var recs = store.getRange();
                        var allow = true;
                        var ids = [];
                        for (var j = 0; j < recs.length; j++) {
                            ids.push(recs[j].get("Id"));
                            var colMod = grid.getColumnModel();
                            for (var i = 5; i < store.fields.length; i++) {
                                if (colMod.isCellEditable(parseInt(i) + 2, j)) { //通过是否可以编辑来判断 不可编辑的列不参与提交验证
                                    if (!recs[j].get(store.fields.items[i].name)) {
                                        allow = false;
                                        break;
                                    }
                                }
                                if (!allow) {
                                    break;
                                }
                            }
                        }
                        if (!allow) {
                            AimDlg.show("您还有未输入的打分项，请输入完毕后再提交！");
                            return;
                        }
                        if (confirm("提交后考核打分将不允许修改，确定要提交吗？")) {
                            $.ajaxExec("Submit", { taskIds: ids }, function() { RefreshClose(); })
                        }
                    }
                }, '-', '<b>说明：1 各指标分输入后系统会自动保存；2 所有分数输入完毕方可提交；3 鼠标悬停列头会显示具体的考核标准</b>', '->']
            });
            var rowarray0 = [];
            rowArray = [{ header: '', colspan: 7, align: 'center'}];
            if (AimState["DataList1"]) {
                var data2 = AimState["DataList1"];
                var columns = 7;
                for (var k = 0; k < data2.length; k++) {
                    columns += parseInt(data2[k].SecondCount);
                    rowArray.push({ header: '<b>' + data2[k].IndicatorFirstName + '</b>', colspan: parseInt(data2[k].SecondCount), align: 'center' });
                }
                rowarray0.push({ header: '<font style="color:blue;font-size:18px"><b>' + AimState["Title"] + '</b></font>', colspan: columns, align: 'center' });
            }
            taskCm = new Ext.grid.ColumnModel({
                columns: colArray,
                rows: [rowarray0, rowArray]
            });
            grid = new Ext.ux.grid.AimEditorGridPanel({
                store: store,
                cm: taskCm,
                region: 'center',
                columnLines: true,
                viewConfig: { forceFit: true },
                tbar: Index == '0' ? tlBar : '',
                plugins: [new Ext.ux.grid.ColumnHeaderGroup()],
                listeners: { beforeedit: function(e) {
                    var max = e.field.slice(e.field.indexOf('(') + 1, e.field.indexOf(')'));
                    Ext.getCmp(e.field.slice(0, 36) + "_subscore").setMaxValue(max);
                }, afteredit: function(e) {
                    var score = (e.record.get("Score") ? parseFloat(e.record.get("Score")) : 0) + parseFloat(e.value) - (e.originalValue ? parseFloat(e.originalValue) : 0);
                    e.record.set("Score", Math.round(parseFloat(score) * 100) / 100);
                    $.ajaxExec("SaveSubScore", { ExamineTaskId: e.record.get("Id"), IndicatorSecondId: e.field.slice(0, 36),
                        SubScore: e.value, Score: e.record.get("Score")
                    },function() { e.record.commit(); })                       
                }
                }
            });
            grid.getColumnModel().isCellEditable = function(colIndex, rowIndex) {
                if (Index != "0") {
                    return false;
                }
                else {
                    var columnId = grid.getColumnModel().getColumnId(colIndex);
                    var rec = store.getAt(rowIndex);
                    if (rec.get("Tag") != "1")//如果 不是人力资源部业绩填报人登录
                    {
                        if (columnId.indexOf("!T") > 0) {
                            return false;
                        }
                    }
                    else {
                        if (columnId.indexOf("!T") < 0) {
                            return false;
                        }
                    }
                }
                return Ext.grid.ColumnModel.prototype.isCellEditable.call(this, colIndex, rowIndex);
            } 
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        function onExecuted() {
            storeTask.reload();
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn;
            switch (this.id) {
                case "StageName":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowExamineStage(\"" + record.get("Id") + "\")'>" + value + "</label>";
                    }
                    break;
                case "Id":
                    if (record.get("State") != '0') {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowTask(\"" + value + "\",\"" + record.get("StageName") + "\")'>查看任务</label>";
                    }
                    else {
                        rtn = "";
                    }
                    break;
                case "BeDeptName":
                    if (value) {
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break; 
                default: //record中的Tag字段专门用来标记 对工作业绩打分的特殊任务  Tag='1'
                    var columnId = grid.getColumnModel().getColumnId(columnIndex);
                    if (record.get("Tag") != "1") {//如果 不是人力资源部业绩填报人登录
                        if (columnId.indexOf("!T") > 0) {
                            cellmeta.style = 'background-color: gray';
                        }
                    }
                    else {
                        if (columnId.indexOf("!T") < 0) {
                            cellmeta.style = 'background-color: gray';
                        }
                    }
                    rtn = value;
                    break;
            }
            return rtn;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
