<%@ Page Title="绩效填报" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="DeptExamineStep5.aspx.cs" Inherits="Aim.Examining.Web.DeptExamineStep5" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var id = $.getQueryString({ ID: 'id' });
        var enumState = { 0: '已生成', 1: '已启动', 2: '已提交', 3: '已结束' }; //考核任务生成的时候状态 0   启动考核后 状态变为1    提交后变为2   结束后变为3
        var enumQuater = { 1: '第一季度', 2: '第二季度', 3: '第三季度', 4: '年度' };
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
                idProperty: 'BeUserId',
                data: myData,
                fields: [
                //Remark
                //Summary
                //Opinion
                //ApproveUserId
                //ApproveUserName
                //ApproveTime  
                  {name: 'Id' }, { name: 'IndicatorNo' }, { name: 'ExamineStageId' }, { name: 'DeptId' }, { name: 'DeptName' },
                  { name: 'IndicatorSecondId' }, { name: 'IndicatorSecondName' }, { name: 'DeptIndicatorName' },
                  { name: 'DeptIndicatorId' }, { name: 'Weight' }, { name: 'Year' }, { name: 'StageType' }, { name: 'State' },
                  { name: 'Result' }, { name: 'BeDeptName' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime' }
			    ],
                listeners: {
                    aimbeforeload: function(proxy, options) {
                        options.data = options.data || {};
                        options.data.id = id;
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
                    text: '删除',
                    iconCls: 'aim-icon-delete',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (recs.count == 0) {
                            alert("请选择要删除的记录！");
                            return;
                        }
                        var ids = "";
                        $.each(recs, function() {
                            ids += (ids ? "," : "") + this.get("Id");
                        })
                        $.ajaxExec("delete", { ids: ids }, function() {
                            store.reload();
                        })
                    }
                }, '-', {
                    text: '撤销生成任务',
                    iconCls: 'aim-icon-redo',
                    handler: function() {
                        $.ajaxExec("canceltask", { id: id }, function() {
                            store.reload();
                        })
                    }
                }
			        ]
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 4,
                collapsed: false,
                items: [
                { fieldLabel: '考核对象', id: 'CreateName', schopts: { qryopts: "{ mode: 'Like', field: 'CreateName' }"} }
                ]
            });
            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                //tbar: tlBar,
                items: [schBar]
            });
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '绩效填报情况',
                store: store,
                region: 'center',
                columnLines: true,
                autoExpandColumn: 'CreateTime',
                columns: [
                { id: 'BeUserId', dataIndex: 'BeUserId', header: 'BeUserId', hidden: true },
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'IndicatorNo', dataIndex: 'IndicatorNo', header: '指标编号', width: 125 },
				{ id: 'CreateName', dataIndex: 'CreateName', header: '考核对象', width: 100 },
                { id: 'DeptName', dataIndex: 'DeptName', header: '所在部门', width: 130 },
				{ id: 'Year', dataIndex: 'Year', header: '年份', width: 70 },
				{ id: 'StageType', dataIndex: 'StageType', header: '阶段', width: 70, enumdata: enumQuater },
				{ id: 'IndicatorSecondName', dataIndex: 'IndicatorSecondName', header: '关联部门指标', width: 100 },
				{ id: 'State', dataIndex: 'State', header: '状态', width: 70, enumdata: enumState },
				{ id: 'Result', dataIndex: 'Result', header: '审批结果', width: 70 },
				{ id: 'CreateTime', dataIndex: 'CreateTime', header: '创建时间', width: 130 }
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
            window.open(url, name, 'height=' + iHeight + ',innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowTaskDetail(val) {
            opencenterwin("../ExamineConfig/ExamineTaskViewList.aspx?ToUserId=" + val + "&ExamineStageId=" + id, "", 1000, 650);
        }
        function onExecuted() {
            store.reload();
        }
        function SuccessSubmit() {
            var result = false;
            var isfill = true;
            var recs = store.getRange();
            $.each(recs, function() {
                if (this.get("Result") != "同意") {
                    isfill = false;
                    return false;
                }
            });
            if (!isfill) {
                if (confirm("还有考核对象的自定义指标未完成,确认要继续启动打分考核？")) {
                    result = id;
                }
            }
            else {
                result = id;
            }
            return result;
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "ToUserId":
                    if (value) {
                        rtn += "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowTaskDetail(\"" + value + " \")'>查看详细</label>   ";
                    }
                    if (parseInt(record.get("UnSubmitQuan")) > 0) {
                        rtn += "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='SendMessage(\"" + value + " \",\"" + record.get("Phone") + "\")'>发送短信催办</label>";
                    }
                    break;
                case "BeRoleName":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
                case "UnSubmitQuan":
                    if (parseInt(value) > 0) {
                        cellmeta.style = "color:red";
                    }
                    rtn = value;
                    break;
            }
            return rtn;
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
