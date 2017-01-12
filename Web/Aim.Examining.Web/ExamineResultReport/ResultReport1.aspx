<%@ Page Title="考核结果报表1" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="ResultReport1.aspx.cs" Inherits="Aim.Examining.Web.ResultReport1" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <link href="../App_Themes/Ext/ux/css/ColumnHeaderGroup.css" rel="stylesheet" type="text/css" />

    <script src="../js/ext/ux/ColumnHeaderGroup.js" type="text/javascript"></script>

    <script src="../FusionChart32/FusionCharts.js" type="text/javascript"></script>

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var comboEnum = { 优秀: '优秀', 良好: '良好', 称职: '称职', 不称职: '不称职' };
        //    var EnumData = { 0: '已创建', 1: '已生成', 2: '已启动', 3: '已结束' }; //4 表示已建议   5 已评定   阶段状态规则
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Year',
                data: myData,
                fields: [
				    { name: 'Year' }, { name: 'Total' }, { name: '95->100' }, { name: '95->100占比' },
				     { name: '90->94' }, { name: '90->94占比' }, { name: '85->89' }, { name: '85->89占比' },
				     { name: '80->84' }, { name: '80->84占比' }, { name: '75->79' }, { name: '75->79占比' },
				     { name: '70->74' }, { name: '70->74占比' }, { name: '0->69' }, { name: '0->69占比'}],
                listeners: { aimbeforeload: function(proxy, options) {
                    options.data = options.data || {};
                }, load: function(store, records, options) {
                    CreateFusionChart(store);
                }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            var colArr = [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'Year', dataIndex: 'Year', header: '年份', width: 120 },
                { id: 'Total', dataIndex: 'Total', header: '总人数', width: 60 },
                { id: '95->100', dataIndex: '95->100', header: '95->100人数', width: 80, renderer: RowRender },
                { id: '95->100占比', dataIndex: '95->100占比', header: '占比%', width: 50 },
                { id: '90->94', dataIndex: '90->94', header: '90->94人数', width: 80, renderer: RowRender },
                { id: '90->94占比', dataIndex: '90->94占比', header: '占比%', width: 50 },
                { id: '85->89', dataIndex: '85->89', header: '85->89人数', width: 80, renderer: RowRender },
                { id: '85->89占比', dataIndex: '85->89占比', header: '占比%', width: 50 },
                { id: '80->84', dataIndex: '80->84', header: '80->84人数', width: 80, renderer: RowRender },
                { id: '80->84占比', dataIndex: '80->84占比', header: '占比%', width: 50 },
                { id: '75->79', dataIndex: '75->79', header: '75->79人数', width: 80, renderer: RowRender },
                { id: '75->79占比', dataIndex: '75->79占比', header: '占比%', width: 50 },
                { id: '70->74', dataIndex: '70->74', header: '70->74人数', width: 80, renderer: RowRender },
                { id: '70->74占比', dataIndex: '70->74占比', header: '占比%', width: 50 },
                { id: '0->69', dataIndex: '0->69', header: '0->69人数', width: 80, renderer: RowRender },
                { id: '0->69占比', dataIndex: '0->69占比', header: '占比%', width: 50 }
                ];
            grid = new Ext.ux.grid.AimGridPanel({
                store: store,
                margins: '280 0 0 0',
                region: 'center',
                columns: colArr,
                columnLines: true,
                autoExpandColumn: 'Year'
                // bbar: pgBar,
                // tbar: titPanel
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowDetail(val1, val2) { 
            if (!val2) {
                var parameters = val1.split(",");
                if (parameters.length >= 2) {
                    val1 = parameters[0];
                    val2 = parameters[1];
                }
            }
            opencenterwin("YearResultByYearAndScore.aspx?year=" + val1 + "&scoreZone=" + val2, "", 1100, 600);
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "95->100":
                case "90->94":
                case "85->89":
                case "80->84":
                case "75->79":
                case "70->74":
                case "0->69":
                    if (value > 0) {
                        rtn += "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowDetail(\"" + record.get("Year") + "\",\"" + this.id + "\")'>" + value + "</label>";
                    }
                    break;
            }
            return rtn;
        }
        function CreateFusionChart(store) {
            var categoryarray = [];
            var fieldarray = ['95->100', '90->94', '85->89', '80->84', '75->79', '70->74', '0->69'];
            for (var i = 0; i < fieldarray.length; i++) {
                categoryarray.push({ label: fieldarray[i] })
            }
            var datasetarray = new Array();
            // var seriesarray = ['销售金额(万元)', '回款金额(万元)']; link: 'j-ShowDetail-' + myData[k].Date
            var seriesarray = [];
            var myData = store.getRange();
            $.each(myData, function() {
                seriesarray.push(this.get("Year"));
            })
            for (var j = 0; j < myData.length; j++) {
                var dataarray = new Array();
                for (var k = 0; k < fieldarray.length; k++) {
                    dataarray.push({ value: myData[j].get(fieldarray[k]), link: 'j-ShowDetail-' + myData[j].get("Year") + ',' + fieldarray[k] });
                }
                datasetarray.push({ SeriesName: seriesarray[j], data: dataarray });
            }
            var jsondata = { chart: { decimalPrecision: '1', caption: "分数结果统计分析", formatNumberScale: '0', placeValuesInside: '0',
                chartTopMargin: '5', chartBottomMargin: '5', showValues: '0', unescapeLinks: '0'
            },
                categories: { category: categoryarray },
                dataset: datasetarray
            };
            var mychart = new FusionCharts("/FusionChart32/MSColumn3D.swf", "myChartId", Ext.getBody().getWidth(), '280');
            mychart.setJSONData(jsondata);
            mychart.render('div1-1');
        } 
    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="div1-1">
    </div>
</asp:Content>
