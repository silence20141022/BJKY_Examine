<%@ Page Title="考核反馈及发展计划" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="FeedbackList.aspx.cs" Inherits="Aim.Examining.Web.ExamineTaskManage.FeedbackList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=730,height=650,scrollbars=yes");
        var EditPageUrl = "FeedbackEdit.aspx";
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var FeedbackEnum = { 1: '已提交', 2: '已结束' };
        var Index = $.getQueryString({ ID: "Index" });
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
		        { name: 'Id' }, { name: 'ExamYearResultId' }, { name: 'ExamineStageId' }, { name: 'Year' },
		        { name: 'UserId' }, { name: 'UserName' }, { name: 'BeRoleCode' }, { name: 'BeRoleName' }, { name: 'DeptId' },
		        { name: "DeptName" }, { name: "DirectLeaderIds" }, { name: "DirectLeaderNames" }, { name: "ExamineGrade" },
	            { name: 'IntegrationScore' }, { name: 'FirstQuarterScore' }, { name: 'SecondQuarterScore' }, { name: 'ThirdQuarterScore' },
		        { name: 'YearScore' }, { name: 'Advantage' }, { name: 'Shortcoming' }, { name: 'PlanAndMethod' },
	            { name: 'State' }, { name: 'Result' }, { name: 'FeedbackTime' }, { name: 'DirectLeaderSignDate' }, { name: 'CreateId' },
		        { name: 'CreateName' }, { name: 'CreateTime' }
		        ],
                listeners: { aimbeforeload: function(proxy, options) {
                    options.data.Index = Index;
                }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                collapsed: false,
                columns: 6,
                items: [
                { fieldLabel: '姓名', id: 'BeUserName', labelWidth: 100, schopts: { qryopts: "{ mode: 'Like', field: 'BeUserName' }"} },
                { fieldLabel: '部门', id: 'BeDeptName', schopts: { qryopts: "{ mode: 'Like', field: 'BeDeptName' }"} },
                { fieldLabel: '职务', id: 'BeUserRoleName', schopts: { qryopts: "{ mode: 'Like', field: 'BeUserRoleName' }"} },
                { fieldLabel: '按钮', xtype: 'button', iconCls: 'aim-icon-search', width: 60, margins: '2 30 0 0', text: '查 询', handler: function() {
                    Ext.ux.AimDoSearch(Ext.getCmp("BeUserName"));
                }
                }
                ]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [
                {
                    text: '反馈查询',
                    iconCls: 'aim-icon-search',
                    handler: function() {
                        schBar.toggleCollapse(false);
                        setTimeout("viewport.doLayout()", 50);
                    }
}]
                });
                titPanel = new Ext.ux.AimPanel({
                    // tbar: tlBar,
                    items: [schBar]
                });
                var clnArr = [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'UserName', dataIndex: 'UserName', header: '姓名', width: 70, sortable: true },
					{ id: 'DeptName', dataIndex: 'DeptName', header: '部门', width: 80, sortable: true },
					{ id: 'BeRoleName', dataIndex: 'BeRoleName', header: '职务/岗位', width: 80, sortable: true },
					{ id: 'ExamineGrade', dataIndex: 'ExamineGrade', header: '考评等级', width: 70, sortable: true },
				    { id: 'FirstQuarter', dataIndex: 'FirstQuarterScore', header: '一季度分数', width: 70, sortable: true },
				    { id: 'SecondQuarter', dataIndex: 'SecondQuarterScore', header: '二季度分数', width: 70, sortable: true },
				    { id: 'ThirdQuarter', dataIndex: 'ThirdQuarterScore', header: '三季度分数', width: 70, sortable: true },
				    { id: 'YearScore', dataIndex: 'YearScore', header: '年度分数', width: 70, sortable: true },
				    { id: 'IntegrationScore', dataIndex: 'IntegrationScore', header: '年度综合分数', width: 90, sortable: true },
				    { id: 'PlanAndMethod', dataIndex: 'PlanAndMethod', header: '发展计划及改进措施', width: 150 },
				    { id: 'State', dataIndex: 'State', header: '反馈状态', width: 60, enumdata: FeedbackEnum },
				    { id: 'Result', dataIndex: 'Result', header: '反馈结果', width: 60 },
                    { id: 'FeedbackTime', dataIndex: 'FeedbackTime', header: '反馈日期', width: 70, sortable: true, renderer: ExtGridDateOnlyRender },
               		{ id: 'Id', dataIndex: 'Id', header: '操作', width: 70, renderer: RowRender }
					];
                // 表格面板
                grid = new Ext.ux.grid.AimGridPanel({
                    title: '考核反馈',
                    store: store,
                    region: 'center',
                    autoExpandColumn: 'PlanAndMethod',
                    columns: clnArr,
                    bbar: pgBar,
                    tbar: titPanel
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
            function ShowInfo(val, val2) {
                if (Index == "1") {
                    opencenterwin("FeedbackEdit.aspx?op=v&id=" + val + "&State=" + val2, "", 1000, 600);
                }
                else {
                    opencenterwin("FeedbackEdit.aspx?op=u&id=" + val + "&State=" + val2, "", 1000, 600);
                }
            }
            function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
                var rtn = "";
                switch (this.id) {
                    case "Id":
                        if (value) {
                            rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowInfo(\"" + value + "\",\"" + record.get("State") + "\")'>反馈信息</label>";
                        }
                        break;
                    case "PlanAndMethod":
                        if (value) {
                            cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                            rtn = value;
                        }
                        break;
                }
                return rtn;
            }
            // 提交数据成功后
            function onExecuted() {
                store.reload();
            }    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
