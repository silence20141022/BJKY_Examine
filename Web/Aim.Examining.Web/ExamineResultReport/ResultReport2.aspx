<%@ Page Title="考核结果报表2" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="ResultReport2.aspx.cs" Inherits="Aim.Examining.Web.ResultReport2" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <link href="../App_Themes/Ext/ux/css/ColumnHeaderGroup.css" rel="stylesheet" type="text/css" />

    <script src="../js/ext/ux/ColumnHeaderGroup.js" type="text/javascript"></script>

    <script src="../FusionChart32/FusionCharts.js" type="text/javascript"></script>

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
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
				     { name: 'Year' }, { name: 'Total' }, { name: '优秀' }, { name: '优秀占比' },
				     { name: '良好' }, { name: '良好占比' }, { name: '称职' }, { name: '称职占比' },
				     { name: '不称职' }, { name: '不称职占比'}],
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
                { id: 'Total', dataIndex: 'Total', header: '总人数', width: 80 },
                { id: '优秀', dataIndex: '优秀', header: '优秀人数', width: 100, renderer: RowRender },
                { id: '优秀占比', dataIndex: '优秀占比', header: '优秀占比%', width: 100 },
                { id: '良好', dataIndex: '良好', header: '良好人数', width: 100, renderer: RowRender },
                { id: '良好占比', dataIndex: '良好占比', header: '良好占比%', width: 100 },
                { id: '称职', dataIndex: '称职', header: '称职人数', width: 100, renderer: RowRender },
                { id: '称职占比', dataIndex: '称职占比', header: '称职占比%', width: 100 },
                { id: '不称职', dataIndex: '不称职', header: '不称职人数', width: 100, renderer: RowRender },
                { id: '不称职占比', dataIndex: '不称职占比', header: '不称职占比%', width: 100 }
                ];
            grid = new Ext.ux.grid.AimGridPanel({
                store: store,
                margins: '280 0 0 0',
                region: 'center',
                columns: colArr,
                columnLines: true,
                autoExpandColumn: 'Year'
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "优秀":
                case "良好":
                case "称职":
                case "不称职":
                    if (value > 0) {
                        rtn += "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowDetail(\"" + record.get("Year") + "\",\"" + this.id + "\")'>" + value + "</label>";
                    }
                    break;
            }
            return rtn;
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
            opencenterwin("YearResultByYearAndLevel.aspx?year=" + val1 + "&level=" + escape(val2), "", 1100, 600);
        }
        function CreateFusionChart(store) {
            var categoryarray = [];
            var fieldarray = ['优秀', '良好', '称职', '不称职'];
            for (var i = 0; i < fieldarray.length; i++) {
                categoryarray.push({ label: fieldarray[i] })
            }
            var datasetarray = new Array();
            // var seriesarray = ['销售金额(万元)', '回款金额(万元)']; 
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
            var jsondata = { chart: { decimalPrecision: '1', caption: "等级评定统计分析", formatNumberScale: '0', placeValuesInside: '0',
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
