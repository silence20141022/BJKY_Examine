<%@ Page Title="考核结果" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="TaskListByBeUser.aspx.cs" Inherits="Aim.Examining.Web.TaskListByBeUser" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var ExamineStageResultId = $.getQueryString({ ID: "ExamineStageResultId" });
        var StageInfo;
        function onPgLoad() {
            StageInfo = AimState["ExamineStage"];
            setPgUI();
            viewport.doLayout();
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
                { name: 'Id' }, { name: 'ExamineStageId' }, { name: 'ToUserId' }, { name: 'ToUserName' }, { name: 'ToDeptId' },
                { name: 'ToDeptName' }, { name: 'Score' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime' }
			    ],
                listeners: {
                    aimbeforeload: function(proxy, options) {
                        options.data = options.data || {};
                        options.data.ExamineStageResultId = ExamineStageResultId;
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
                columns: 2,
                collapsed: false,
                items: [
                { fieldLabel: '评分人', id: 'ToUserName', schopts: { qryopts: "{ mode: 'Like', field: 'ToUserName' }"} }
                ]
            });
            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                //    tbar: tlBar,
                items: [schBar]
            });
            // 表格面板
            var title = "";
            if (StageInfo) {
                title = title + StageInfo.StageName + StageInfo.Year + '年第' + StageInfo.StageType;
            }
            grid = new Ext.ux.grid.AimGridPanel({
                title: title + '季度【' + AimState["BeUserName"] + '】考核结果明细',
                store: store,
                region: 'west',
                columnLines: true,
                width: 400,
                // viewConfig: { forceFit: true },
                autoExpandColumn: 'ToDeptName',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'ToUserName', dataIndex: 'ToUserName', header: '姓名', width: 80, sortable: true },
                { id: 'ToDeptName', dataIndex: 'ToDeptName', header: '部门', width: 120, sortable: true },
				{ id: 'Score', dataIndex: 'Score', header: '打分', width: 100, sortable: true }
			    ],
                tbar: titPanel,
                listeners: { rowclick: function() {
                    var recs = grid.getSelectionModel().getSelections();
                    if ((recs && recs.length > 0) && recs[0].get("Id")) {
                        frameContent.location.href = "IndicatorScoreByTask.aspx?TaskId=" + recs[0].get("Id");
                    }
                }
                }
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid, {
                    id: 'frmcon',
                    region: 'center',
                    margins: '-1 0 -2 0',
                    html: '<iframe width="100%" height="100%" id="frameContent" name="frameContent" frameborder="0"></iframe>'}]
                });
                if (store.data.length > 0) {
                    frameContent.location.href = "IndicatorScoreByTask.aspx?TaskId=" + store.getAt(0).get("Id");
                }
            }
            function opencenterwin(url, name, iWidth, iHeight) {
                var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
                var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
                window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
            }
            function ShowScoreDetail(val) {
                opencenterwin("../ExamineTaskManage/ExamineEvaluation.aspx?id=" + val, "", 1200, 650);
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
                    case "CreateTime":
                        break;
                }
                return rtn;
            }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
