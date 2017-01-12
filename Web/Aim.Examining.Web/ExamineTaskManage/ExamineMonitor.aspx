<%@ Page Title="考核监控" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="ExamineMonitor.aspx.cs" Inherits="Aim.Examining.Web.ExamineTaskManage.ExamineMonitor" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var StageEnum = { 1: '第一季度', 2: '第二季度', 3: '第三季度', 4: '年度考核' };
        var EnumData = { 0: '已创建', 1: '已生成', 2: '已启动', 3: '已结束', 4: '已建议等级', 5: '已评定等级' };
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
			    { name: 'Id' }, { name: 'StageName' }, { name: 'StartTime' }, { name: 'EndTime' }, { name: 'State' }, { name: 'CreateId' },
			    { name: 'CreateName' }, { name: 'CreateTime' }, { name: 'Remark' }, { name: 'LaunchUserId' }, { name: 'LaunchUserName' },
			    { name: 'TaskQuan' }, { name: 'LaunchDeptId' }, { name: 'LaunchDeptName' }, { name: 'ExamineType' }, { name: 'SubmitQuan' },
			    { name: 'Year' }, { name: 'StageType'}],
                listeners: { aimbeforeload: function(proxy, options) {
                }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 6,
                collapsed: false,
                items: [
                { fieldLabel: '考核阶段名称', labelWidth: 100, id: 'StageName', schopts: { qryopts: "{ mode: 'Like', field: 'StageName' }"} },
                { fieldLabel: '发起人', id: 'LaunchUserName', schopts: { qryopts: "{ mode: 'Like', field: 'LaunchUserName' }"} },
                { fieldLabel: '开始时间', id: 'StartTime', xtype: 'datefield', vtype: 'daterange', endDateField: 'EndTime', schopts: { qryopts: "{ mode: 'GreaterThan', datatype:'Date', field: 'StartTime' }"} },
                { fieldLabel: '结束时间', id: 'EndTime', xtype: 'datefield', vtype: 'daterange', startDateField: 'StartTime', schopts: { qryopts: "{ mode: 'LessThanEqual', datatype:'Date', field: 'EndTime' }"} },
                { fieldLabel: '按钮', xtype: 'button', iconCls: 'aim-icon-search', width: 60, margins: '1 30 0 0', text: '查 询', handler: function() {
                    Ext.ux.AimDoSearch(Ext.getCmp("LaunchUserName"));
                }
                }
]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '评定等级',
                    iconCls: 'aim-icon-grid',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要评定等级的记录！");
                            return;
                        }
                        if (recs[0].get("State") != "4") {
                            AimDlg.show("未建议等级的考核不能评定等级！");
                            return;
                        }
                        opencenterwin("LevelApprove.aspx?ExamineStageId=" + recs[0].get("Id"), "", 1200, 650);
                    }
                }, '->'
                 ]
            });
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });
            grid = new Ext.ux.grid.AimGridPanel({
                store: store,
                autoExpandColumn: 'StageName',
                region: 'center',
                columnLines: true,
                columns: [{ id: 'StageName', dataIndex: 'StageName', header: '考核阶段名称', width: 180, sortable: true },
					{ id: 'LaunchUserName', dataIndex: 'LaunchUserName', header: '发起人', width: 60, sortable: true },
					{ id: 'LaunchDeptName', dataIndex: 'LaunchDeptName', header: '发起人部门', width: 100, sortable: true },
					{ id: 'ExamineType', dataIndex: 'ExamineType', header: '考核类型', width: 80 },
					{ id: 'Year', dataIndex: 'Year', header: '年份', width: 70, sortable: true },
                    { id: 'StageType', dataIndex: 'StageType', header: '阶段类型', width: 80, enumdata: StageEnum },
					{ id: 'StartTime', dataIndex: 'StartTime', header: '开始时间', width: 80, sortable: true },
					{ id: 'EndTime', dataIndex: 'EndTime', header: '结束时间', width: 80, sortable: true },
                    { id: 'State', dataIndex: 'State', header: '状态', width: 100, sortable: true, renderer: RowRender },
                    { id: 'Id', dataIndex: 'Id', header: '操作', sortable: true, width: 70, renderer: RowRender }
                   ],
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
        function ShowResult(val, val2, val3) {
            if (val2 == "4") {
                opencenterwin("../ExamineTaskManage/YearResult.aspx?IsMonitor=T&ExamineStageId=" + val + "&ExamineStageName=" + escape(val3), "", 1200, 600);
            }
            else {
                opencenterwin("../ExamineTaskManage/QuarterResult.aspx?IsMonitor=T&ExamineStageId=" + val + "&ExamineStageName=" + escape(val3), "", 1200, 600);
            }
        }
        function ShowExamineStage(val) {
            opencenterwin("ExamineStageEdit.aspx?op=v&id=" + val, "", 1200, 650);
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
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowResult(\"" + value + "\",\"" + record.get("StageType") + "\",\"" + record.get("StageName") + "\")'>考核结果</label>";
                    }
                    break;
                case "State":
                    switch (value) {
                        case 0:
                            rtn = "已创建";
                            break;
                        case 1:
                            rtn = "已生成";
                            break;
                        case 2:
                            rtn = "已启动";
                            break;
                        case 3:
                            rtn = "已结束";
                            break;
                        case 4:
                            rtn = "已建议等级";
                            break;
                        default:
                            rtn = "已评定等级";
                            break;
                        // 0: '已创建', 1: '已生成', 2: '已启动', 3: '已结束', 4: '已建议等级', 5: '已评定等级' };                
                    }
                    if (value == 4) {
                        rtn = "<b style='color:#ff7575'>" + rtn + "</b>";
                    }
                    break;
            }
            return rtn;
        }
        function onExecuted() {
            store.reload();
        }
    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <b style="color: "></b>
</asp:Content>
