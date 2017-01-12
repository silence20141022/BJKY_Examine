<%@ Page Title="人员配置" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="GroupPersonConfig.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.GroupPersonConfig" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var GroupTypeEnum = { "经营目标单位": "经营目标单位", "职能服务部门": "职能服务部门" };
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
			    { name: 'Id' }, { name: 'GroupID' }, { name: 'GroupCode' }, { name: 'GroupName' }, { name: 'GroupType' },
			    { name: 'FirstLeaderIds' }, { name: 'FirstLeaderNames' }, { name: 'FirstLeaderGroupIds' }, { name: 'FirstLeaderGroupNames' },
			    { name: 'SecondLeaderIds' }, { name: 'SecondLeaderNames' }, { name: 'SecondLeaderGroupIds' }, { name: 'SecondLeaderGroupNames' },
			    { name: 'ChargeSecondLeaderIds' }, { name: 'ChargeSecondLeaderNames' }, { name: 'ChargeSecondLeaderGroupIds' }, { name: 'ChargeSecondLeaderGroupNames' },
			    { name: 'InstituteClerkDelegateIds' }, { name: 'InstituteClerkDelegateNames' }, { name: 'InstituteClerkDelegateGroupIds' }, { name: 'InstituteClerkDelegateGroupNames' },
			    { name: 'DeptClerkDelegateIds' }, { name: 'DeptClerkDelegateNames' }, { name: 'DeptClerkDelegateGroupIds' }, { name: 'DeptClerkDelegateGroupNames' },
			    { name: 'ClerkIds' }, { name: 'ClerkNames' }, { name: 'ClerkGroupIds' }, { name: 'ClerkGroupNames' },
			    { name: 'PeopleQuan' }, { name: 'ExcellentRate' }, { name: 'GoodRate' }, { name: 'ExcellentQuan' }, { name: 'GoodQuan' },
			    { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime'}],
                listeners: { aimbeforeload: function(proxy, options) {
                    //options.data.Index = Index;
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
                   { fieldLabel: '部门名称', id: 'GroupName', schopts: { qryopts: "{ mode: 'Like', field: 'GroupName' }"} },
                   { fieldLabel: '部门类型', id: 'GroupType', schopts: { qryopts: "{ mode: 'Like', field: 'GroupType' }"} },
                   { fieldLabel: '人员名称', id: 'ClerkNames', schopts: { qryopts: "{ mode: 'Like', field: 'ClerkNames' }"} }
                //                   { fieldLabel: '按钮', xtype: 'button', iconCls: 'aim-icon-search', width: 60, margins: '1 30 0 0', text: '查 询', handler: function() {
                //                       Ext.ux.AimDoSearch(Ext.getCmp("GroupName"));
                //                   }
                //                   }
		            ]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        opencenterwin("PersonConfigEdit.aspx?op=c", "", 1200, 650);
                    }
                }, '-', {
                    text: '修改',
                    iconCls: 'aim-icon-edit',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要修改的记录！");
                            return;
                        }
                        opencenterwin("PersonConfigEdit.aspx?op=u&id=" + recs[0].get("Id"), "", 1200, 650);
                    }
                }, '-', {
                    text: '删除',
                    iconCls: 'aim-icon-delete',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要删除的记录！");
                            return;
                        }
                        if (confirm("删除部门会连同部门下的人员配置信息一并删除，确定要删除所选记录吗？")) {
                            var ids = [];
                            $.each(recs, function() {
                                ids.push(this.get("Id"));
                            })
                            $.ajaxExec("delete", { ids: ids }, function(rtn) {
                                store.reload();
                            });
                        }
                    }
                },
                 '-', {
                     text: '考核任务修正',
                     iconCls: 'aim-icon-wrench',
                     handler: function() {
                         ExamineStageSelect();
                     }
                 }, '->']
            });
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });
            var columnsarray = [
                    { id: 'Id', dataIndex: 'Id', header: 'Id', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'GroupName', dataIndex: 'GroupName', header: '部门名称', width: 150, renderer: RowRender },
					{ id: 'GroupType', dataIndex: 'GroupType', header: '类型', width: 100 },
					{ id: 'FirstLeaderNames', dataIndex: 'FirstLeaderNames', header: '部门正职', width: 70 },
					{ id: 'SecondLeaderNames', dataIndex: 'SecondLeaderNames', header: '部门副职', width: 80 },
					{ id: 'ChargeSecondLeaderNames', dataIndex: 'ChargeSecondLeaderNames', header: '主持工作部门副职', width: 80 },
					{ id: 'InstituteClerkDelegateNames', dataIndex: 'InstituteClerkDelegateNames', header: '院级员工代表', width: 90, renderer: RowRender },
					{ id: 'DeptClerkDelegateNames', dataIndex: 'DeptClerkDelegateNames', header: '部门员工代表', width: 90, renderer: RowRender },
					{ id: 'ClerkNames', dataIndex: 'ClerkNames', header: '部门员工', width: 120, renderer: RowRender },
					{ id: 'PeopleQuan', dataIndex: 'PeopleQuan', header: '部门人数', width: 60 },
					{ id: 'ExcellentRate', dataIndex: 'ExcellentRate', header: '优秀率(%)', width: 70 },
					{ id: 'ExcellentQuan', dataIndex: 'ExcellentQuan', header: '<font color="red">优秀人数</font>', width: 60,
					    editor: { xtype: 'numberfield', allowBlank: false, decimalPrecision: 0, minValue: 0, maxValue: 100 }
					},
					{ id: 'GoodRate', dataIndex: 'GoodRate', header: '良好率(%)', width: 70 },
					{ id: 'GoodQuan', dataIndex: 'GoodQuan', header: '<font color="red">良好人数</font>', width: 60,
					    editor: { xtype: 'numberfield', allowBlank: false, decimalPrecision: 2, minValue: 0, maxValue: 100 }
					}
];
            // 表格面板
            grid = new Ext.ux.grid.AimEditorGridPanel({
                store: store,
                region: 'center',
                // viewConfig: { forceFit: true },
                autoExpandColumn: 'ClerkNames',
                columnLines: true,
                columns: columnsarray,
                //  bbar: pgBar,
                cls: 'grid-row-span',
                tbar: titPanel,
                listeners: { afteredit: function(e) {
                    if (e.field == "ExcellentQuan") {
                        $.ajaxExec("AutoSave", { id: e.record.get("Id"), ExcellentQuan: e.value }, function(rtn) {
                            if (rtn.data.ExcellentRate) {
                                e.record.set("ExcellentRate", rtn.data.ExcellentRate);
                                e.record.commit();
                            }
                        });
                    }
                    if (e.field == "GoodQuan") {
                        $.ajaxExec("AutoSave", { id: e.record.get("Id"), GoodQuan: e.value }, function(rtn) {
                            if (rtn.data.GoodRate) {
                                e.record.set("GoodRate", rtn.data.GoodRate);
                                e.record.commit();
                            }
                        });
                    }
                }
                }
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
        function ExamineStageSelect() {
            var style = "dialogWidth:800px; dialogHeight:400px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "ExamineStageSelect.aspx?seltype=single";
            OpenModelWin(url, {}, style, function() {
                if (this.data.Id) {
                    var ExamineStageId = this.data.Id;
                    Ext.getBody().mask("考核任务修正中。。。。");
                    $.ajaxExec("CreateTaskAgain", { ExamineStageId: ExamineStageId }, function() {
                        ShowAmendTaskList(ExamineStageId)
                        Ext.getBody().unmask();
                    });
                }
            });
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;.aspx
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowAmendTaskList(val) {
            opencenterwin("AmendTaskList.aspx?ExamineStageId=" + val, "newwin", 1000, 650);
        }
        function ShowGroupInfo(val) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function() {
                opencenterwin("PersonConfigEdit.aspx?op=v&id=" + val, "", 1200, 600);
            });
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn;
            switch (this.id) {
                case "GroupName":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowGroupInfo(\"" +
			            record.get('Id') + "\")'>" + value + "</label>";
                    }
                    break;
                case "ProfitRate":
                    if (value) {
                        rtn = Math.round(parseFloat(value) * 10000) / 100;
                        if (rtn >= 100) {
                            rtn = '<font color="green">' + rtn + '%' + '</font>';
                        } else {
                            rtn = '<font color="red">' + rtn + '%' + '</font>';
                        }
                    }
                    break;
                case "InstituteClerkDelegateNames":
                case "DeptClerkDelegateNames":
                case "ClerkNames":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
            }
            return rtn;
        }
        function BeforeShow() {
            var rec = store.getAt(grid.activeEditor.row);
            this.popUrl = "/commonpages/select/UsrSelect/MUsrSelect.aspx?GroupID=" + rec.get("GroupID");
        }
        
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
