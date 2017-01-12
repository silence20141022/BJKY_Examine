<%@ Page Title="考核结果" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="PersonalResult.aspx.cs" Inherits="Aim.Examining.Web.ExamineTaskManage.PersonalResult" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var Index = $.getQueryString({ ID: "Index" });
        var StageEnum = { 1: '第一季度', 2: '第二季度', 3: '第三季度', 4: '年度考核' };
        var AppealEnum = { 1: '已提交', 2: '已受理', 3: '审批中', 4: '已结束' };
        var FeedbackEnum = { 1: '已提交', 2: '已结束' };
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
                data: myData, //这里把年度考核结果 和季度考核结果的字段放一起了
                fields: [{ name: 'Id' }, { name: 'ExamineStageId' }, { name: 'BeRoleCode' }, { name: 'UserId' },
                    { name: 'UserName' }, { name: 'BeRoleName' },
				    { name: 'DeptId' }, { name: 'DeptName' },
				     { name: 'IntegrationScore' }, { name: 'ApproveScore' }, { name: 'AppealScore' },
				    { name: 'AvgScore' }, { name: 'FirstQuarterScore' },
			        { name: 'SecondQuarterScore' }, { name: 'ThirdQuarterScore' }, { name: 'FourthQuarterScore' }, { name: 'UpLevelScore' },
			        { name: 'SameLevelScore' }, { name: 'DownLevelScore' },
			        { name: 'AdviceLevel' }, { name: 'ApproveLevel' }, { name: 'AppealLevel' }, { name: 'Year' },
			        { name: 'SortIndex' }, { name: 'State' }, { name: 'SignLeaderId' }, { name: 'SignLeaderName' }, { name: 'CreateId' },
			        { name: 'CreateName' }, { name: 'CreateTime' }, { name: 'BeRoleName' }, { name: 'StageName' }, { name: 'ExamineType' },
			        { name: 'StageType' }, { name: 'ApproveTime' }, { name: 'Days' }, { name: 'AppealId' }, { name: 'AppealState' },
			        { name: 'FeedbackId' }, { name: 'FeedbackState' }, { name: 'AppealResult' }, { name: 'FeedbackResult' }, { name: 'Score' },
			        { name: 'UpAvgScore' }, { name: 'SameAvgScore' }, { name: 'DownAvgScore' }
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
                columns: 4,
                collapsed: false,
                items: [
                { fieldLabel: '考核阶段名称', labelWidth: 90, id: 'StageName', schopts: { qryopts: "{ mode: 'Like', field: 'StageName' }"} },
                { fieldLabel: '年份', id: 'Year', schopts: { qryopts: "{ mode: 'Like', field: 'Year' }"} }
              ]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '考核申诉',
                    iconCls: 'aim-icon-user-comment',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要申诉的考核结果！");
                            return;
                        }
                        if (parseInt(recs[0].get("Days")) > parseInt(AimState["SysConfig"].AppealDays)) {
                            AimDlg.show("已超过申诉期限的考核结果不能申诉！");
                            return;
                        }
                        if (recs[0].get("AppealId")) {
                            AimDlg.show("该考核结果已填写申诉信息！");
                            return;
                        }
                        if (recs[0].get("FeedbackId")) {
                            AimDlg.show("已反馈考核结果不能填写申诉信息！");
                            return;
                        }
                        else {//创建
                            opencenterwin("ExamineAppealEdit.aspx?op=c&ExamYearResultId=" + recs[0].get("Id"), "", 1000, 600);
                        }
                    }
                }, '-', {
                    text: '反馈及发展计划',
                    iconCls: 'aim-icon-pdf',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要反馈的考核结果！");
                            return;
                        }
                        if (recs[0].get("AppealId") && parseInt(recs[0].get("AppealState")) < 4) {
                            AimDlg.show("申诉中的考核结果不能填写反馈信息！");
                            return;
                        }
                        if (recs[0].get("FeedbackId")) {
                            AimDlg.show("该考核结果已填写反馈信息！");
                            return;
                        }
                        else {//创建
                            opencenterwin("FeedbackEdit.aspx?op=c&ExamYearResultId=" + recs[0].get("Id"), "", 1000, 600);
                        }
                    }
                }, '<b>说明：1 申诉人必须在评定结束' + AimState["SysConfig"].AppealDays + '日内提出申诉，否则无效;2 反馈后考核结果不能申诉</b>', '->'
                 ]
            });
            titPanel = new Ext.ux.AimPanel({
                tbar: Index == "0" ? tlBar : "",
                items: [schBar]
            });
            var colarray = [];
            if (Index == "0") {
                colarray = [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'UserName', dataIndex: 'UserName', header: '考核对象', width: 70, sortable: true },
                //{ id: 'BeRoleName', dataIndex: 'BeRoleName', header: '角色', width: 120, sortable: true },
					{id: 'DeptName', dataIndex: 'DeptName', header: '部门', width: 100, sortable: true },
					{ id: 'IntegrationScore', dataIndex: 'IntegrationScore', header: '综合得分', width: 70, sortable: true, renderer: RowRender },
					{ id: 'AdviceLevel', dataIndex: 'AdviceLevel', header: '评定等级', width: 70, sortable: true, renderer: RowRender },
					{ id: 'StageName', dataIndex: 'StageName', header: '考核阶段名称', width: 150, sortable: true, renderer: RowRender },
					{ id: 'Year', dataIndex: 'Year', header: '年份', width: 50, sortable: true },
                // { id: 'ApproveTime', dataIndex: 'ApproveTime', header: '评定日期', width: 100, sortable: true, renderer: ExtGridDateOnlyRender },
                    {id: 'AppealState', dataIndex: 'AppealState', header: '申诉状态', width: 70, enumdata: AppealEnum },
                    { id: 'AppealResult', dataIndex: 'AppealResult', header: '申诉结果', width: 70 },
                    { id: 'FeedbackState', dataIndex: 'FeedbackState', header: '反馈状态', width: 70, enumdata: FeedbackEnum },
                     { id: 'FeedbackResult', dataIndex: 'FeedbackResult', header: '反馈结果', width: 70 },
                    { id: 'Id', dataIndex: 'Id', header: '操作', width: 100, renderer: RowRender }
                 ];
            }
            else {
                colarray = [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'UserName', dataIndex: 'UserName', header: '考核对象', width: 70, sortable: true },
					//{ id: 'BeRoleName', dataIndex: 'BeRoleName', header: '角色', width: 120, sortable: true },
					{ id: 'DeptName', dataIndex: 'DeptName', header: '部门', width: 100, sortable: true },
                    { id: 'Score', dataIndex: 'Score', header: '得分', width: 80, sortable: true },
                    { id: 'UpAvgScore', dataIndex: 'UpAvgScore', header: '上级评分', width: 80, sortable: true },
                    { id: 'SameAvgScore', dataIndex: 'SameAvgScore', header: '同级评分', width: 80, sortable: true },
                    { id: 'DownAvgScore', dataIndex: 'DownAvgScore', header: '下级评分', width: 80, sortable: true },
                    { id: 'StageName', dataIndex: 'StageName', header: '考核阶段名称', width: 180, sortable: true, renderer: RowRender },
                    { id: 'ExamineType', dataIndex: 'ExamineType', header: '考核类型', width: 80 },
	                { id: 'Year', dataIndex: 'Year', header: '年份', width: 70, sortable: true },
	                { id: 'StageType', dataIndex: 'StageType', header: '阶段类型', width: 80, enumdata: StageEnum, sortable: true }
                    ];
            }
            grid = new Ext.ux.grid.AimGridPanel({
                store: store,
                autoExpandColumn: 'StageName',
                region: 'center',
                columnLines: true,
                columns: colarray,
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
        function ShowTask(val, val2) {
            opencenterwin("ExamineTaskViewList.aspx?ExamineStageId=" + val + "&ExamineStageName=" + escape(val2), "", 1000, 600);
        }
        function ShowAppeal(val, val2) {
            if (parseInt(val2) >= 1) {
                var task = new Ext.util.DelayedTask();
                task.delay(100, function() {
                    opencenterwin("ExamineAppealEdit.aspx?op=v&id=" + val + "&State=" + val2, "", 1000, 600);
                });
            }
            else {
                var task = new Ext.util.DelayedTask();
                task.delay(100, function() {
                    opencenterwin("ExamineAppealEdit.aspx?op=u&id=" + val + "&State=", "", 1000, 600);
                });
            }
        }
        function ShowFeedback(val, val2) {
            if (parseInt(val2) >= 1) {
                var task = new Ext.util.DelayedTask();
                task.delay(100, function() {
                    opencenterwin("FeedbackEdit.aspx?op=v&id=" + val + "&State=" + val2, "", 1000, 600);
                });
            }
            else {
                var task = new Ext.util.DelayedTask();
                task.delay(100, function() {
                    opencenterwin("FeedbackEdit.aspx?op=u&id=" + val + "&State=", "", 1000, 600);
                });
            }
        }
        function ShowExamineStage(val) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function() {
                opencenterwin("/ExamineConfig/ExamineStageEdit.aspx?op=v&id=" + val, "", 1200, 650);
            });
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "Temp":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowExamineStage(\"" + record.get("ExamineStageId") + "\")'>" + value + "</label>";
                    }
                    break;
                case "AdviceLevel":
                    if (!value) {//角色是副院级领导是没有等级概念的    BeDeputyDirector
                        cellmeta.style = "background-color:gray";
                    }
                    else {
                        rtn = value;
                        if (record.get("ApproveLevel") && record.get("ApproveLevel") != value) {
                            rtn = record.get("ApproveLevel");
                            cellmeta.style = " color:blue"; //如果评定等级存在且不和建议等级相同 用蓝色标记
                        }
                        if (record.get("AppealLevel")) {
                            rtn = record.get("AppealLevel");
                            cellmeta.style = " color:red"; //如果申诉等级存在  用红色标记
                        }
                    }
                    break;
                case "IntegrationScore":
                    if (value) {
                        rtn = value;
                    }
                    if (record.get("ApproveScore")) {
                        rtn = record.get("ApproveScore");
                        cellmeta.style = " color:blue";
                    }
                    if (record.get("AppealScore")) {
                        rtn = record.get("AppealScore");
                        cellmeta.style = " color:red";
                    }
                    break
                case "StageName":
                    if (value) {
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
                case "Id":
                    if (value) {
                        if (Index == "0") {
                            if (record.get("AppealId")) {
                                rtn += "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowAppeal(\"" + record.get("AppealId") + "\",\"" + record.get("AppealState") + "\")'>申诉信息</label>  ";
                            }
                            if (record.get("FeedbackId")) {
                                rtn += "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowFeedback(\"" + record.get("FeedbackId") + "\",\"" + record.get("FeedbackState") + "\")'>反馈信息</label>";
                            }
                        }
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
    <b style="color: "></b>
</asp:Content>
