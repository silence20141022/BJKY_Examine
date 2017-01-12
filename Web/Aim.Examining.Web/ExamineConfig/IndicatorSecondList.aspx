<%@ Page Title="考核要素" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="IndicatorSecondList.aspx.cs" Inherits="Aim.Examine.Web.ExamineConfig.IndicatorSecondList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=685,height=500,scrollbars=yes,resizable =yes");
        var EditPageUrl = "IndicatorSecondEdit.aspx";
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var IndicatorFirstName = unescape($.getQueryString({ ID: "IndicatorFirstName" }));
        var IndicatorFirstId = $.getQueryString({ ID: "IndicatorFirstId" });
        var ExamineIndicatorId = $.getQueryString({ ID: "ExamineIndicatorId" });
        var Custom = $.getQueryString({ ID: 'Custom' });
        function onPgLoad() {

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
                fields: [{ name: 'Id' }, { name: 'IndicatorFirstId' }, { name: 'IndicatorFirstName' }, { name: 'IndicatorSecondName' },
                { name: 'SortIndex' }, { name: 'MaxScore' }, { name: 'ScoreStandard'}],
                listeners: {
                    aimbeforeload: function(proxy, options) {
                        options.data = options.data || {};
                        options.data.IndicatorFirstId = IndicatorFirstId;
                    }
                }
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        if (IndicatorFirstId) {
                            if (Custom.contains("T") && store.getCount() > 0) {
                                AimDlg.show("自定义指标只能添加一项");
                                return;
                            }
                            EditPageUrl += "?IndicatorFirstId=" + IndicatorFirstId;
                            EditPageUrl += "&IndicatorFirstName=" + IndicatorFirstName;
                            EditPageUrl += "&ExamineIndicatorId=" + ExamineIndicatorId;
                            ExtOpenGridEditWin(grid, EditPageUrl, "c", EditWinStyle);
                        }
                        else {
                            AimDlg.show("请先添加考核项目！");
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
                        EditPageUrl += "?IndicatorFirstId=" + IndicatorFirstId;
                        EditPageUrl += "&IndicatorFirstName=" + IndicatorFirstName;
                        EditPageUrl += "&ExamineIndicatorId=" + ExamineIndicatorId;
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
                        if (confirm("删除考核要素的时候会连同考核标准一起删除，确定删除所选记录吗？")) {
                            ExtBatchOperate('batchdelete', recs, null, null, function() { store.reload(); });
                        }
                    }
                }
			    ]
            });

            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar
            });
            var clnsArr = [{ id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                    { id: 'IndicatorFirstId', dataIndex: 'IndicatorFirstId', header: 'IndicatorFirstId', hidden: true },
                    { id: 'IndicatorFirstName', dataIndex: 'IndicatorFirstName', header: '考核项目', hidden: true },
		            new Ext.ux.grid.AimCheckboxSelectionModel(),
		            { id: 'IndicatorSecondName', dataIndex: 'IndicatorSecondName', header: '考核要素', width: 100, renderer: RowRender },
		            { id: 'MaxScore', dataIndex: 'MaxScore', header: '权重', width: 60, sortable: true },
		            { id: 'SortIndex', dataIndex: 'SortIndex', header: '排序索引', width: 70, sortable: true }
			    ];
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '【' + IndicatorFirstName + '】考核要素',
                store: store,
                region: 'center',
                autoExpandColumn: 'IndicatorSecondName',
                columns: clnsArr,
                tbar: tlBar
            });
            // 页面视图
            viewport = new Ext.ux.AimViewport({
                layout: 'border',
                items: [grid]
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
                case "IndicatorSecondName":
                    if (value) {
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + getTip(record.get("ScoreStandard")) + '"';
                        rtn = value;
                    }
                    break;
            }
            return rtn;
        }

        function getTip(val) {
            var result = '';
            if (val) {
                var strArr = val.split(";");
                var temp = "<table style='font-size:12px'><tr><td colspan='2' style='font-weight:bold'>考核标准描述</td></tr>";
                for (var i = 0; i < strArr.length; i++) {
                    if (strArr[i]) {
                        var temparray = strArr[i].split("#");
                        temp += "<tr><td>" + (i + 1) + ":" + temparray[0] + "</td><td>" + (temparray[1] ? temparray[1] : "") + "</td></tr>";
                    }
                }
                temp += "</table>";
                result = temp;
            }
            return result;
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
