<%@ Page Title="考核阶段" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="True"
    CodeBehind="ExamineStageList.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.ExamineStageList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var EnumData = { 0: '已创建', 1: '已生成', 2: '已启动', 3: '已结束', 4: '已建议等级', 5: '已评定等级' };
        var StageEnum = { 1: '第一季度', 2: '第二季度', 3: '第三季度', 4: '年度考核' };
        function onPgLoad() {
            setPgUI();
            if (AimState["Remove"] && AimState["Remove"] == "T") {
                tlBar.remove("addInstitute");
            }
        }
        function setPgUI() {
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
			    { name: 'Id' }, { name: 'StageName' }, { name: 'StartTime' }, { name: 'EndTime' }, { name: 'State' }, { name: 'CreateId' },
			    { name: 'CreateName' }, { name: 'CreateTime' }, { name: 'Remark' }, { name: 'LaunchUserId' }, { name: 'LaunchUserName' },
			    { name: 'TaskQuan' }, { name: 'LaunchDeptId' }, { name: 'LaunchDeptName' }, { name: 'ExamineType' }, { name: 'SubmitQuan' },
			    { name: 'Year' }, { name: 'StageType' }],
                listeners: {
                    aimbeforeload: function (proxy, options) {
                    }
                }
            });

            // 分页栏
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 4,
                collapsed: false,
                items: [
                { fieldLabel: '考核阶段名称', labelWidth: 90, id: 'StageName', schopts: { qryopts: "{ mode: 'Like', field: 'StageName' }" } },
                { fieldLabel: '发起人', id: 'LaunchUserName', schopts: { qryopts: "{ mode: 'Like', field: 'LaunchUserName' }" } },
                { fieldLabel: '开始时间', id: 'StartTime', xtype: 'datefield', vtype: 'daterange', endDateField: 'EndTime', schopts: { qryopts: "{ mode: 'GreaterThan', datatype:'Date', field: 'StartTime' }" } },
                { fieldLabel: '结束时间', id: 'EndTime', xtype: 'datefield', vtype: 'daterange', startDateField: 'StartTime', schopts: { qryopts: "{ mode: 'LessThanEqual', datatype:'Date', field: 'EndTime' }" } }
                ]
            });
            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [
                {
                    text: '添加考核',
                    iconCls: 'aim-icon-add',
                    handler: function () {
                        window.location.href = "../DeptConfig/DeptExamineWizzard.aspx?op=c";
                    }
                },
                '-', {
                    text: '删除',
                    iconCls: 'aim-icon-delete',
                    handler: function () {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要删除的记录！");
                            return;
                        }
                        if (recs[0].get("State") != "0") {
                            AimDlg.show("已生成任务的考核不能删除！");
                            return;
                        }
                        if (confirm("确定要删除所选记录吗？")) {
                            $.ajaxExec("delete", { id: recs[0].get("Id") }, function (rtn) {
                                store.reload();
                            });
                        }
                    }
                }, '-', {
                    text: '生成任务',
                    iconCls: 'aim-icon-redo',
                    handler: function () {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要生成任务的考核阶段！");
                            return;
                        }
                        if (recs[0].get("State") != '0') {
                            AimDlg.show("已创建的考核才能生成任务！");
                            return;
                        }
                        if (!recs[0].get("StageName")) {
                            AimDlg.show("请先输入考核阶段名称！");
                            return;
                        }
                        if (confirm("确定要为本次考核生成任务吗？")) {
                            Ext.getBody().mask("考核任务生成中。。。。");
                            $.ajaxExec("CreateTask", { id: recs[0].get("Id") }, function (rtn) {
                                AimDlg.show(rtn.data.Result); store.reload();
                            });
                        }
                    }
                }, '-',
               {
                   text: '回收任务',
                   iconCls: 'aim-icon-undo',
                   handler: function () {
                       var recs = grid.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要回收的考核阶段！");
                           return;
                       }
                       if (recs[0].get("State") != "1") {
                           AimDlg.show("已生成的考核才能回收！");
                           return;
                       }
                       if (confirm("确定要回收本次考核阶段的任务吗？")) {
                           Ext.getBody().mask("考核任务回收中。。。。");
                           $.ajaxExec("TakeBack", { id: recs[0].get("Id") }, function (rtn) {
                               if (rtn.data.Result && rtn.data.Result == "T") {
                                   AimDlg.show("回收任务成功！"); store.reload();
                               }
                           });
                       }
                   }
               },
                 '-', {
                     text: '启动考核',
                     iconCls: 'aim-icon-run',
                     handler: function () {
                         var recs = grid.getSelectionModel().getSelections();
                         if (!recs || recs.length <= 0) {
                             AimDlg.show("请先选择要启动的考核阶段！");
                             return;
                         }
                         if (recs[0].get("State") != "1") {
                             AimDlg.show("已生成的考核才能启动！");
                             return;
                         }
                         $.ajaxExec("JudgeCustomIndicator", { id: recs[0].get("Id") }, function (rtn) {
                             if (rtn.data.Result == "F") {
                                 if (confirm("本次考核还有被考核人未自定义指标或者未审批，要继续启动考核吗？")) {
                                     Ext.getBody().mask("考核任务启动中。。。。");
                                     $.ajaxExec("Launch", { id: recs[0].get("Id") }, function (rtn) {
                                         if (rtn.data.Result == "T") {
                                             AimDlg.show("本次考核启动成功！"); store.reload();
                                         }
                                     });
                                 }
                             }
                             else {
                                 if (confirm("确定要启动本次考核吗？")) {
                                     Ext.getBody().mask("考核任务启动中。。。。");
                                     $.ajaxExec("Launch", { id: recs[0].get("Id") }, function (rtn) {
                                         if (rtn.data.Result == "T") {
                                             AimDlg.show("本次考核启动成功！"); store.reload();
                                         }
                                     });
                                 }
                             }
                         });
                     }
                 }, '-', {
                     text: '撤销启动',
                     iconCls: 'aim-icon-undo',
                     handler: function () {
                         var recs = grid.getSelectionModel().getSelections();
                         if (!recs || recs.length <= 0) {
                             AimDlg.show("请先选择要回收的考核阶段！");
                             return;
                         }
                         if (recs[0].get("State") != "2") {
                             AimDlg.show("已启动的考核才能撤销！");
                             return;
                         }
                         if (confirm("撤销考核会删除考核任务以及打分记录，确定要撤销本次考核吗？")) {
                             Ext.getBody().mask("考核任务撤销中。。。。");
                             $.ajaxExec("CancelLaunch", { id: recs[0].get("Id") }, function (rtn) {
                                 store.reload();
                             });
                         }
                     }
                 }, '-',
                {
                    text: '结束考核',
                    iconCls: 'aim-icon-stop',
                    handler: function () {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要结束的考核阶段！");
                            return;
                        }
                        if (recs[0].get("State") != '2') {
                            AimDlg.show("已启动的考核才能结束！");
                            return;
                        }
                        if (confirm("确定要结束本次考核吗？")) {
                            Ext.getBody().mask("考核任务结束中。。。。");
                            $.ajaxExec("EndExamine", { id: recs[0].get("Id") }, function (rtn) {
                                if (rtn.data.Result == "T") {
                                    AimDlg.show("本次考核结束成功！"); store.reload();
                                }
                            });
                        }
                    }
                }, '-',
                 {
                     text: '等级填报',
                     iconCls: 'aim-icon-grid',
                     handler: function () {
                         var recs = grid.getSelectionModel().getSelections();
                         if (!recs || recs.length <= 0) {
                             AimDlg.show("请先选择要填报等级的年终考核！");
                             return;
                         }
                         if (recs[0].get("State") != '3' || recs[0].get("StageType") != "4") {
                             AimDlg.show("已结束的年度考核才能填报等级！");
                             return;
                         }
                         opencenterwin("../ExamineTaskManage/LevelAdvice.aspx?ExamineStageId=" + recs[0].get("Id"), "", 1200, 650);
                     }
                 },
                 '->']
            });

            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });
            var columnsarray = [
                        new Ext.ux.grid.AimRowNumberer(),
                        new Ext.ux.grid.AimCheckboxSelectionModel(),
                        { id: 'StageName', dataIndex: 'StageName', header: '考核阶段名称', width: 180, renderer: RowRender, sortable: true },
                        { id: 'LaunchUserName', dataIndex: 'LaunchUserName', header: '发起人', width: 60, sortable: true },
                        { id: 'LaunchDeptName', dataIndex: 'LaunchDeptName', header: '发起人部门', width: 100, sortable: true },
                        { id: 'ExamineType', dataIndex: 'ExamineType', header: '考核类型', width: 80, sortable: true },
                        { id: 'Year', dataIndex: 'Year', header: '年份', width: 70, sortable: true },
                        { id: 'StageType', dataIndex: 'StageType', header: '阶段类型', width: 70, enumdata: StageEnum },
                        { id: 'StartTime', dataIndex: 'StartTime', header: '开始时间', width: 70, renderer: ExtGridDateOnlyRender, sortable: true },
                        { id: 'EndTime', dataIndex: 'EndTime', header: '结束时间', width: 70, renderer: ExtGridDateOnlyRender, sortable: true },
                        { id: 'State', dataIndex: 'State', header: '状态', width: 80, enumdata: EnumData, sortable: true },
                        { id: 'TaskQuan', dataIndex: 'TaskQuan', header: '任务数', width: 60, sortable: true },
                        { id: 'SubmitQuan', dataIndex: 'SubmitQuan', header: '提交数', width: 60 },
                        { id: 'Id', dataIndex: 'Id', header: '操作', sortable: true, width: 120, renderer: RowRender }
            ];
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                store: store,
                columnLines: true,
                region: 'center',
                //   viewConfig: { forceFit: true },
                autoExpandColumn: 'StageName',
                gridLine: true,
                columns: columnsarray,
                bbar: pgBar,
                cls: 'grid-row-span',
                tbar: titPanel
            });
            // 页面视图 
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        // 提交数据成功后
        function onExecuted() {
            store.reload();
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;ExamineResultView
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowTask(val, val2) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function () {
                opencenterwin("TaskGroupByToUser.aspx?ExamineStageId=" + val + "&ExamineStageName=" + escape(val2), "", 1000, 600);
            });
        }
        function ShowResult(val, val2, val3, val4) {
            if (val2 == "4" && val4 == "部门级考核") {
                var task = new Ext.util.DelayedTask();
                task.delay(100, function () {
                    opencenterwin("../ExamineTaskManage/YearResult.aspx?ExamineStageId=" + val + "&ExamineStageName=" + escape(val3), "", 1200, 600);
                });
            }
            else {
                var task = new Ext.util.DelayedTask();
                task.delay(100, function () {
                    opencenterwin("../ExamineTaskManage/QuarterResult.aspx?ExamineStageId=" + val + "&ExamineStageName=" + escape(val3), "", 1200, 600);
                });
            }
        }
        function ShowExamineStage(val, val2) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function () {
                window.location.href = "../DeptConfig/DeptExamineWizzard.aspx?op=u&id=" + val;
            });
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "StageName":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowExamineStage(\"" + record.get("Id") + "\",\"" + record.get("ExamineType") + "\")'>" + value + "</label>";
                    }
                    break;
                case "Id":
                    if (parseInt(record.get("State")) > 0) {
                        rtn += "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowTask(\"" + value + "\",\"" + record.get("StageName") + "\")'>考核任务</label>  ";
                    }
                    if (parseInt(record.get("State")) >= 3) {//ExamineType
                        rtn += "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowResult(\"" + value + "\",\"" + record.get("StageType") + "\",\"" + record.get("StageName") + "\",\"" + record.get("ExamineType") + "\")'>考核结果</label>";
                    }
                    break;
            }
            return rtn;
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
