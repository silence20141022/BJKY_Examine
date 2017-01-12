﻿<%@ Page Title="指标分明细" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="IndicatorScoreByTask.aspx.cs" Inherits="Aim.Examining.Web.IndicatorScoreByTask" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var TaskId = $.getQueryString({ ID: "TaskId" });
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
                fields: [
                { name: 'Id' }, { name: 'ExamineTaskId' }, { name: 'IndicatorFirstId' }, { name: 'IndicatorFirstName' }, { name: 'FirstMaxScore' },
                { name: 'FirstSortIndex' }, { name: 'IndicatorSecondId' }, { name: 'IndicatorSecondName' }, { name: 'SecondMaxScore' }, { name: 'SubQuan' },
                { name: 'SecondSortIndex' }, { name: 'ToolTip' }, { name: 'SubScore' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime' }
			    ],
                listeners: {
                    aimbeforeload: function(proxy, options) {
                        options.data = options.data || {};
                        options.data.TaskId = TaskId;
                    }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                    }
                }
			        ]
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 3,
                collapsed: false,
                items: [
                { fieldLabel: '一级指标', id: 'IndicatorFirstName', schopts: { qryopts: "{ mode: 'Like', field: 'IndicatorFirstName' }"} },
                { fieldLabel: '二级指标', id: 'IndicatorSecondName', schopts: { qryopts: "{ mode: 'Like', field: 'IndicatorSecondName' }"} }
                ]
            });
            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                //    tbar: tlBar,
                items: [schBar]
            });
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '【' + AimState["TaskInfo"].ToUserName + '】所评指标分详细',
                store: store,
                region: 'center',
                columnLines: true,
                autoExpandColumn: 'IndicatorSecondName',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'IndicatorFirstName', dataIndex: 'IndicatorFirstName', header: '一级指标', width: 120, sortable: true },
                { id: 'IndicatorSecondName', dataIndex: 'IndicatorSecondName', header: '二指标级', width: 120, sortable: true },
                { id: 'SecondMaxScore', dataIndex: 'SecondMaxScore', header: '二级指标满分', width: 100, sortable: true },
				{ id: 'SubScore', dataIndex: 'SubScore', header: '实际评分', width: 100, sortable: true, renderer: RowRender }
			    ],
                tbar: titPanel
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            }
            )
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowCustomIndicatorScore(val) {//通过考核二级指标的Id 以及考核任务的ID  找到该任务下
            opencenterwin("../ExamineTaskManage/CustomIndicatorScoreByTask.aspx?TaskId=" + TaskId, "", 1000, 650);
        }
        function onExecuted() {
            store.reload();
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn;
            switch (this.id) {
                case "Id":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowScoreDetail(\"" + value + " \")'>查看详细</label>";
                    }
                    break;
                case "BeRoleName":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
                case "SubScore":
                    if (value) {
                        if (record.get("SubQuan") != '0') {
                            rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowCustomIndicatorScore(\"" + record.get("IndicatorSecondId") + " \")'>" + value + "</label>";
                        }
                        else {
                            rtn = value;
                        }
                    }
                    break;
            }
            return rtn;
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>