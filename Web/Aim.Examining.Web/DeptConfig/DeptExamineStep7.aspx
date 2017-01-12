<%@ Page Title="生成考核任务" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="DeptExamineStep7.aspx.cs" Inherits="Aim.Examining.Web.DeptExamineStep7" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var id = $.getQueryString({ ID: "id" });
        var ExamineStageName = unescape($.getQueryString({ ID: "ExamineStageName" }));
        var StateEnum = { 0: '已生成', 1: '已启动', 2: '已提交', 3: '已结束' }; //考核任务生成的时候状态 0   启动考核后 状态变为1    提交后变为2   结束后变为3
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
                idProperty: 'ToUserId',
                data: myData,
                fields: [
                { name: 'ToUserId' }, { name: 'ToUserName' }, { name: 'TaskQuan' }, { name: 'UnSubmitQuan' }, { name: 'Phone' }
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
                    text: '结束本次考核',
                    iconCls: 'aim-icon-stop',
                    id: 'endbtn',
                    disabled: AimState["State"] != 2,
                    handler: function() {
                        var unsubmit = false;
                        $.each(store.getRange(), function() {
                            if (this.get("TaskQuan") != this.get("UnSubmitQuan")) {
                                unsubmit = true;
                                return false;
                            }
                        })
                        if (unsubmit) {
                            if (confirm("本次考核还有未提交的任务，确认需要结束考核吗？")) {
                                $.ajaxExecSync("endexamine", { id: id }, function(rtn) {
                                    if (rtn.data.Id) {
                                        alert("本次考核已成功结束！");
                                        Ext.getCmp("endbtn").setDisabled(true);
                                    }
                                });
                            }
                        }
                        else {
                            $.ajaxExecSync("endexamine", { id: id }, function(rtn) {
                                if (rtn.data.Id) {
                                    alert("本次考核已成功结束！");
                                    Ext.getCmp("endbtn").setDisabled(true);
                                }
                            });
                        }
                    }
                }
			        ]
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 3,
                collapsed: false,
                items: [
                { fieldLabel: '评分人', id: 'ToUserName', schopts: { qryopts: "{ mode: 'Like', field: 'ToUserName' }"} }
                ]
            });
            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '考核任务提交情况',
                store: store,
                region: 'center',
                columnLines: true,
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
				{ id: 'ToUserName', dataIndex: 'ToUserName', header: '评分人', width: 100, sortable: true },
                //{ id: 'ToRoleName', dataIndex: 'ToRoleName', header: '考核人角色', width: 150, sortable: true },
                //{ id: 'ToDeptName', dataIndex: 'ToDeptName', header: '考核人部门', width: 100, sortable: true },
				{id: 'TaskQuan', dataIndex: 'TaskQuan', header: '任务数量', width: 90, sortable: true },
              	{ id: 'UnSubmitQuan', dataIndex: 'UnSubmitQuan', header: '未提交数量', width: 90, sortable: true, renderer: RowRender },
				{ id: 'ToUserId', dataIndex: 'ToUserId', header: '操作', width: 80, renderer: RowRender }
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
        function ShowTaskDetail(val) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function() {
                opencenterwin("../ExamineConfig/ExamineTaskViewList.aspx?ToUserId=" + val + "&ExamineStageId=" + id, "", 1000, 650);
            });
        }
        function SuccessSubmit() {
            //            var result = false;
            //            var unsubmit = false;
            //            $.each(store.getRange(), function() {
            //                if (this.get("TaskQuan") != this.get("UnSubmitQuan")) {
            //                    unsubmit = true;
            //                    return false;
            //                }
            //            })
            //            if (unsubmit) {
            //                if (confirm("本次考核还有未提交的任务，确认需要结束考核吗？")) {
            //                    $.ajaxExecSync("endexamine", { id: id }, function() {
            //                        result = id;
            //                    });
            //                }
            //            }
            //            else {
            //                $.ajaxExecSync("endexamine", { id: id }, function() {
            //                    result = id;
            //                });
            //            }
            return true;
        }
        function onExecuted() {
            store.reload();
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
