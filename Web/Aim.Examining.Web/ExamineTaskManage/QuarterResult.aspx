<%@ Page Title="考核结果" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="QuarterResult.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.QuarterResult" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var ExamineStageId = $.getQueryString({ ID: "ExamineStageId" });
        var ExamineStageName = unescape($.getQueryString({ ID: "ExamineStageName" }));
        var IsMonitor = $.getQueryString({ ID: "IsMonitor" });
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
                { name: 'Id' }, { name: 'ExamineStageId' }, { name: 'UserNo' }, { name: 'UserId' }, { name: 'UserName' }, { name: 'DeptId' },
                { name: 'DeptName' }, { name: 'Quater' }, { name: 'Year' }, { name: 'UpAvgScore' }, { name: 'SameAvgScore' }, { name: 'DownAvgScore' },
                { name: 'Score' }, { name: 'SortIndex' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime' }
			    ],
                listeners: {
                    aimbeforeload: function(proxy, options) {
                        options.data = options.data || {};
                        options.data.ExamineStageId = ExamineStageId;
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
                    text: '导出Excel',
                    iconCls: 'aim-icon-xls',
                    handler: function() {
                        ExtGridExportExcel(grid, { store: null, title: '标题' });
                    }
                }
                ]
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 3,
                collapsed: false,
                items: [
                { fieldLabel: '姓名', id: 'UserName', schopts: { qryopts: "{ mode: 'Like', field: 'UserName' }"} },
                { fieldLabel: '被考部门', id: 'DeptName', schopts: { qryopts: "{ mode: 'Like', field: 'DeptName' }"} }
                ]
            });
            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });

            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '【' + ExamineStageName + '】考核结果',
                store: store,
                region: 'center',
                columnLines: true,
                autoExpandColumn: 'DeptName',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'UserName', dataIndex: 'UserName', header: '姓名', width: 80, sortable: true },
                { id: 'DeptName', dataIndex: 'DeptName', header: '部门', width: 110, sortable: true },
				{ id: 'Score', dataIndex: 'Score', header: '得分', width: 100, sortable: true },
				{ id: 'UpAvgScore', dataIndex: 'UpAvgScore', header: '上级评分', width: 100, sortable: true },
				{ id: 'SameAvgScore', dataIndex: 'SameAvgScore', header: '同级评分', width: 100, sortable: true },
				{ id: 'DownAvgScore', dataIndex: 'DownAvgScore', header: '下级评分', width: 100, sortable: true },
                { id: 'Id', dataIndex: 'Id', header: '查看详细', width: 100, renderer: RowRender, hidden: IsMonitor != "T" }
			    ],
                tbar: titPanel,
                bbar: pgBar
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
        function ShowTaskByUser(val) {
            opencenterwin("../ExamineTaskManage/TaskListByBeUser.aspx?ExamineStageResultId=" + val, "", 1200, 650);
        }
        function onExecuted() {
            store.reload();
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn;
            switch (this.id) {
                case "Id":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowTaskByUser(\"" + value + " \")'>查看详细</label>";
                    }
                    break;
                case "BeRoleName":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
                case "CreateTime":
                    break;
            }
            return rtn;
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
