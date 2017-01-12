<%@ Page Title="生成考核任务" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="DeptExamineStep4.aspx.cs" Inherits="Aim.Examining.Web.DeptExamineStep4" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
        }
    </style>

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var id = $.getQueryString({ ID: 'id' });
        var StateEnum = { 0: '已生成', 1: '已启动', 2: '已提交', 3: '已结束' }; //考核任务生成的时候状态 0   启动考核后 状态变为1    提交后变为2   结束后变为3
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
                idProperty: 'Id',
                data: myData,
                fields: [
                  { name: 'Id' }, { name: 'ToUserId' }, { name: 'ToUserName' },
                  { name: 'ToDeptName' }, { name: 'BeUserId' }, { name: 'BeUserName' }
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
                items: [{ text: '生成任务',
                    iconCls: 'aim-icon-delete',
                    disabled: AimState["State"] !=0,
                    handler: function() { 
                        $.ajaxExec("createtask", { id: id }, function() {
                            store.reload();
                        })
                    }
                }, '-', {
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
                },'->','说明：生成考核任务的同时，会连同考核对象的自定义指标一起生成！'
			        ]
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 4,
                collapsed: false,
                items: [
                { fieldLabel: '评分人', id: 'ToUserName', schopts: { qryopts: "{ mode: 'Like', field: 'ToUserName' }"} },
                { fieldLabel: '考核对象', id: 'BeUserName', schopts: { qryopts: "{ mode: 'Like', field: 'BeUserName' }"} }
                ]
            });
            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '考核任务列表',
                store: store,
                region: 'center',
                columnLines: true,
                columns: [
                { id: 'Id', dataIndex: 'Id', header: 'Id', width: 80, hidden: true },
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
				{ id: 'ToUserName', dataIndex: 'ToUserName', header: '评分人', width: 80, sortable: true },
                { id: 'ToDeptName', dataIndex: 'ToDeptName', header: '评分人部门', width: 140, sortable: true },
				{ id: 'BeUserName', dataIndex: 'BeUserName', header: '考核对象', width: 90, sortable: true }
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
            var result = null;
            var recs = store.getRange();
            if (recs.length == 0) {
                alert("必须生成考核任务才能绩效填报！")
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
