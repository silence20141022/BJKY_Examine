<%@ Page Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="DeptEvaluation.aspx.cs" Inherits="Aim.Examining.Web.DetpConfig.DeptEvaluation"
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
                            colArray.push({ id: coldata[j].ColumnName, dataIndex: coldata[j].ColumnName, header: '部门', width: 100, align: 'center', locked: true, renderer: RowRender });
                            break;
                        case "Score":
                            colArray.push({ id: coldata[j].ColumnName, dataIndex: coldata[j].ColumnName, header: '合计分', width: 70, align: 'center', locked: true });
                            break;
                        case "Tag": //colIndex=6
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
            var summary = "";
            if (store.getCount() > 0 && store.getAt(0).get("Summary")) {
                summary += "【<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='DownLoad(\"" + store.getAt(0).get("Summary") + "\")'>查看工作总结</label>】"
            }
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
                            //var colMod = grid.getColumnModel();
                            for (var i = 5; i < store.fields.length; i++) {
                                //                                if (colMod.isCellEditable(parseInt(i) + 2, j)) { //通过是否可以编辑来判断 不可编辑的列不参与提交验证
                                //                                    if (!recs[j].get(store.fields.items[i].name)) {
                                //                                        allow = false;
                                //                                        break;
                                //                                    }
                                //                                } 
                                var val = recs[j].get(store.fields.items[i].name);
                                if (!val || parseFloat(val) < 0) {//判断考核指标是不是全部都打分了 含允许自定义指标列
                                    allow = false;
                                    break;
                                }
                            }
                        }
                        if (!allow) {
                            AimDlg.show("固定指标和自定义指标必须全部打分才能提交！");
                            return;
                        }
                        if (confirm("提交后考核打分将不允许修改，确定要提交吗？")) {
                            $.ajaxExec("Submit", { taskIds: ids }, function() { RefreshClose(); })
                        }
                    }
                }, '-', '<b>说明：1 各指标分输入后系统会自动保存；2 如有工作业绩需点击进行具体自定义指标打分</b>', '->']
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
                // autoExpandColumn: 'BeDeptName',
                viewConfig: { forceFit: true },
                tbar: Index == '0' ? tlBar : '',
                plugins: [new Ext.ux.grid.ColumnHeaderGroup()],
                listeners: { beforeedit: function(e) {
                    var max = e.field.slice(e.field.indexOf('(') + 1, e.field.indexOf(')'));
                    Ext.getCmp(e.field.slice(0, 36) + "_subscore").setMaxValue(max);
                }, afteredit: function(e) {
                    //var score = (e.record.get("Score") ? parseFloat(e.record.get("Score")) : 0) + parseFloat(e.value) - (e.originalValue ? parseFloat(e.originalValue) : 0);
                    //e.record.set("Score", score); Math.round(score * 100) / 100, Score: e.record.get("Score")
                    $.ajaxExec("SaveSubScore", { ExamineTaskId: e.record.get("Id"), IndicatorSecondId: e.field.slice(0, 36),
                        SubScore: e.value
                    },
                       function(rtn) {
                           if (rtn.data.Score) {
                               e.record.set("Score", rtn.data.Score)
                               e.record.commit();
                           }
                       })
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
                    if (columnId.indexOf("!T") > 0) {
                        return false;
                    }
                }
                return Ext.grid.ColumnModel.prototype.isCellEditable.call(this, colIndex, rowIndex);
            }
            // 页面视图
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        function onExecuted() {
            storeTask.reload();
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;ExamineResultView
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowCustomScore(val1, val2, rowIndex, columnId) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function() {
                var style = "dialogWidth:1100px; dialogHeight:600px; scroll:yes; center:yes; status:no; resizable:yes;";
                var url = "CustomIndicatorScore.aspx?seltype=multi&rtntype=array&ExamineTaskId=" + val1 + "&IndicatorSecondId=" + val2 + "&Index=" + Index;
                OpenModelWin(url, {}, style, function() {
                    if (this.data.Score) {
                        var rec = store.getAt(rowIndex);
                        var originalSubScore = rec.get(columnId);
                        rec.set(columnId, this.data.Score);
                        var score = (rec.get("Score") ? parseFloat(rec.get("Score")) : 0) + parseFloat(this.data.Score) - parseFloat(originalSubScore ? parseFloat(originalSubScore) : 0);
                        rec.set("Score", Math.round(score * 100) / 100);
                        $.ajaxExec("SaveSubScore", { ExamineTaskId: rec.get("Id"), IndicatorSecondId: val2,
                            SubScore: this.data.Score, Score: rec.get("Score")
                        },
                       function() { rec.commit(); });
                    }
                });
            });
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
                default:
                    var columnId = grid.getColumnModel().getColumnId(columnIndex);
                    if (columnId.indexOf("!T") > 0) {//如果是自定义打分指标列
                        if (Index == 0) {
                            rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowCustomScore(\"" + record.get("Id") + "\",\"" + columnId.slice(0, 36) + "\",\"" + rowIndex + "\",\"" + columnId + "\")'>" + (value ? value : "进入打分") + "</label>";
                        }
                        else {
                            rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowCustomScore(\"" + record.get("Id") + "\",\"" + columnId.slice(0, 36) + "\",\"" + rowIndex + "\",\"" + columnId + "\")'>" + (value ? value : "") + "</label>";
                        }
                    }
                    else {
                        rtn = value;
                    }
                    break;
            }
            return rtn;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
