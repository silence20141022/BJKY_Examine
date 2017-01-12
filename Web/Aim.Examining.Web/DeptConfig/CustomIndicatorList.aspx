<%@ Page Title="自定义考核指标" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="CustomIndicatorList.aspx.cs" Inherits="Aim.Examining.Web.DeptConfig.CustomIndicatorList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var pgBar, schBar, tlBar, titPanel, myData, store, grid, viewport;
        var enumQuater = { 1: '第一季度', 2: '第二季度', 3: '第三季度', 4: '年度' };
        var enumState = { 0: '已创建', 1: '已提交', 2: '已审批', 3: '已结束' };
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
			    { name: 'Id' }, { name: 'IndicatorNo' }, { name: 'Year' }, { name: 'StageType' }, { name: 'State' }, { name: 'Result' },
			    { name: 'DeptId' }, { name: 'DeptName' }, { name: 'Remark' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'Opinion' },
			    { name: 'CreateTime' }, { name: 'IndicatorSecondId' }, { name: 'IndicatorSecondName' }, { name: 'Weight' }, { name: 'Remark' },
			    { name: 'ApproveUserId' }, { name: 'ApproveUserName' }, { name: 'DeptIndicatorName' }, { name: 'Summary' }, { name: 'ExamineStageId' },
			    { name: 'StageName' }, { name: 'ApproveTime' }
			  ]
            });
            // 分页栏
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 5,
                collapsed: false,
                items: [
                { fieldLabel: '角色名称', labelWidth: 100, id: 'StageName', schopts: { qryopts: "{ mode: 'Like', field: 'StageName' }"} },
                { fieldLabel: '指标名称', id: 'LaunchUserName', schopts: { qryopts: "{ mode: 'Like', field: 'LaunchUserName' }"} }
                              ]
            });
            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [
                /*{
                text: '添加',
                iconCls: 'aim-icon-add',
                handler: function() {
                SelectDeptIndicator();
                }
                }, '-', {
                text: '重新关联部门指标',
                iconCls: 'aim-icon-connect',
                handler: function() {
                SetDeptIndicator();
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
                var allow = true;
                $.each(recs, function() {
                if (this.get("State") == "1" || this.get("Result") == "同意") {
                allow = false;
                return false;
                }
                })
                if (!allow) {
                AimDlg.show("审批中或者已同意自定义指标不能删除！");
                return;
                }
                if (confirm("删除自定义考核指标会连同其下考核内容和量化指标一起删除，确定要删除所选记录吗？")) {
                var idarray = [];
                $.each(recs, function() {
                idarray.push(this.get("Id"));
                })
                $.ajaxExec("delete", { ids: idarray }, function() {
                store.remove(recs);
                if (store.data.length > 0) {
                frameContent.location.href = "PersonFirstIndicatorList.aspx?CustomIndicatorId=" + store.getAt(0).get("Id") + "&IndicatorNo=" + store.getAt(0).get("IndicatorNo") + "&State=" + store.getAt(0).get("State") + "&Result=" + escape(store.getAt(0).get("Result"));
                }
                else {
                frameContent.location.href = "PersonFirstIndicatorList.aspx?CustomIndicatorId=&IndicatorNo=";
                }
                })
                }
                }
                }, '-', */
                {
                text: '提交审批',
                iconCls: 'aim-icon-user-edit',
                handler: function() {
                    var recs = grid.getSelectionModel().getSelections();
                    if (!recs || recs.length <= 0) {
                        AimDlg.show("请先选择要提交审批的记录！");
                        return;
                    }
                    if (recs[0].get("State") == "1" || recs[0].get("Result") == "同意") {
                        AimDlg.show("审批中或者已同意的自定义指标不能再次提交！");
                        return;
                    }
                    //提交的时候还要做一些验证   1 年度 和阶段 审批人是否已选择  2  自定义的权重是否已全部分配到量化指标
                    if (!recs[0].get("Year") || !recs[0].get("StageType") || !recs[0].get("ApproveUserId")) {
                        AimDlg.show("自定义指标所属的年度、阶段以及审批人必须指定后方可提交！");
                        return;
                    }
                    if (confirm("提交审批后将不能修改，确定要提交吗？")) {
                        $.ajaxExec("submit", { id: recs[0].get("Id") }, function(rtn) {
                            if (rtn.data.Result == "T") {
                                AimDlg.show("自定义指标提交成功！");
                                store.reload();
                                //提交后防止下面子页面还能编辑 需要重新加载 框架  '<b>-->说明：年度和阶段类型为必选项</b>',
                                frameContent.location.href = "PersonFirstIndicatorList.aspx?CustomIndicatorId=" + recs[0].get("Id") + "&Year=" + recs[0].get("Year") + "&StageType=" + recs[0].get("StageType") + "&State=1" + "&Result=" + escape(recs[0].get("Result"));
                            }
                            else {
                                AimDlg.show("自定义指标提交失败，请检查所有量化指标权重之和是否等于总权重！");
                            }
                        });
                    }
                }
            }, '-',
                { text: '上传工作总结', iconCls: 'aim-icon-upload', handler: function() {
                    var recs = grid.getSelectionModel().getSelections();
                    if (!recs || recs.length <= 0) {
                        AimDlg.show("请先选择要附加工作总结自定义指标！");
                        return;
                    }
                    if (recs[0].get("StageType") != "4") {
                        AimDlg.show("年度考核才需要上传工作总结！");
                        return;
                    }
                    var UploadStyle = "dialogHeight:405px; dialogWidth:465px; help:0; resizable:0;status:0;scroll=0";
                    var uploadurl = '../CommonPages/File/Upload.aspx?IsSingle=true';
                    var rtn = window.showModalDialog(uploadurl, window, UploadStyle);
                    if (rtn != undefined) {
                        $.ajaxExec("UpLoadSummary", { id: recs[0].get("Id"), Summary: rtn.substring(0, 36) }, function(rtn) {
                            if (rtn.data.Result)
                            { recs[0].set("Summary", rtn.data.Result); }

                        });
                    }
                }
                }, '-', {
                    text: '选择部门指标并复制绩效',
                    iconCls: 'aim-icon-copy',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要复制的记录！");
                            return;
                        }
                        CopyRecord(recs[0].get("Id"));
                    }
                },
                 '->']
        });
        cb_year = new Ext.ux.form.AimComboBox({
            id: 'cb_year',
            enumdata: AimState["EnumYear"],
            lazyRender: false,
            allowBlank: false,
            autoLoad: true,
            forceSelection: true,
            //blankText: "none",
            //valueField: 'text',
            triggerAction: 'all',
            mode: 'local',
            listeners: {
                blur: function(obj) {
                    if (grid.activeEditor) {
                        var rec = store.getAt(grid.activeEditor.row);
                        if (rec) {
                            grid.stopEditing();
                            rec.set("Year", Ext.get('cb_year').dom.value);
                            $.ajaxExec("AutoUpdate", { id: rec.get("Id"), Year: rec.get("Year") }, function(rtn) { rec.commit(); });
                        }
                    }
                }
            }
        });
        cb_StageType = new Ext.ux.form.AimComboBox({
            id: 'cb_StageType',
            enumdata: enumQuater,
            lazyRender: false,
            allowBlank: false,
            autoLoad: true,
            forceSelection: true,
            //blankText: "none",
            //valueField: 'text',
            triggerAction: 'all',
            mode: 'local',
            listeners: {
                blur: function(obj) {
                    if (grid.activeEditor) {
                        var rec = store.getAt(grid.activeEditor.row);
                        if (rec) {
                            grid.stopEditing();
                            rec.set("StageType", obj.value);
                            $.ajaxExec("AutoUpdate", { id: rec.get("Id"), StageType: rec.get("StageType") }, function(rtn) { rec.commit(); });
                        }
                    }
                }
            }
        });
        var columnsarray = [
                    { id: 'Id', dataIndex: 'Id', header: 'Id', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'StageName', dataIndex: 'StageName', header: '考核名称', width: 160 },
                    { id: 'DeptName', dataIndex: 'DeptName', header: '部门', width: 100 },
                    { id: 'Year', dataIndex: 'Year', header: '年度', width: 60 },
                    { id: 'StageType', dataIndex: 'StageType', header: '阶段类型', width: 80, enumdata: enumQuater },
                    { id: 'CreateName', dataIndex: 'CreateName', header: '所属人', width: 60 },
                    { id: 'DeptIndicatorName', dataIndex: 'DeptIndicatorName', header: '部门指标', width: 150, renderer: RowRender },
                    { id: 'IndicatorSecondName', dataIndex: 'IndicatorSecondName', header: '部门二级指标', width: 100 },
                    { id: 'Weight', dataIndex: 'Weight', header: '权重', width: 50 },
                    { id: 'ApproveUserName', dataIndex: 'ApproveUserName', header: '<font color="red">审批人</font>', width: 80,
                        editor: { xtype: 'aimuserselector', allowBlank: false, popAfter: userSelect, seltype: 'single', popUrl: '',
                            popStyle: 'dialogWidth:750px;dialogHeight:500px', listeners: { beforeshow: BeforeShow }
                        }
                    },
                    { id: 'ApproveTime', dataIndex: 'ApproveTime', header: '审批时间', width: 80, renderer: ExtGridDateOnlyRender },
                    { id: 'State', dataIndex: 'State', header: '状态', width: 60, enumdata: enumState },
                    { id: 'Result', dataIndex: 'Result', header: '审批结果', width: 70, renderer: RowRender },
                   { id: 'Summary', dataIndex: 'Summary', header: '工作总结', width: 70, renderer: RowRender }
				 ];
        grid = new Ext.ux.grid.AimEditorGridPanel({
            store: store,
            region: 'north',
            height: 200,
            viewConfig: { forceFit: true },
            autoExpandColumn: 'IndicatorName',
            columnLines: true,
            columns: columnsarray,
            cls: 'grid-row-span',
            tbar: pgOperation == "v" ? "" : tlBar,
            listeners: { rowclick: function() {
                var recs = grid.getSelectionModel().getSelections();
                if ((recs && recs.length > 0) && recs[0].get("Id")) {//已经保存后
                    frameContent.location.href = "PersonFirstIndicatorList.aspx?CustomIndicatorId=" + recs[0].get("Id") + "&Year=" + recs[0].get("Year") + "&StageType=" + recs[0].get("StageType") + "&State=" + recs[0].get("State") + "&Result=" + escape(recs[0].get("Result"));
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
                frameContent.location.href = "PersonFirstIndicatorList.aspx?CustomIndicatorId=" + store.getAt(0).get("Id") + "&Year=" + store.getAt(0).get("Year") + "&StageType=" + store.getAt(0).get("StageType") + "&State=" + store.getAt(0).get("State") + "&Result=" + escape(store.getAt(0).get("Result"));
            }
            else {
                frameContent.location.href = "PersonFirstIndicatorList.aspx?CustomIndicatorId=&IndicatorNo=";
            }
            grid.getColumnModel().isCellEditable = function(colIndex, rowIndex) {
                // var columnId = grid.getColumnModel().getColumnId(colIndex);
                var rec = store.getAt(rowIndex);
                if (rec.get("State") == "1" || rec.get("Result") == "同意") {
                    return false;
                }
                return Ext.grid.ColumnModel.prototype.isCellEditable.call(this, colIndex, rowIndex);
            }
        }
        function onExecuted() {
            store.reload();
        }
        function SelectDeptIndicator(val) {
            var style = "dialogWidth:800px; dialogHeight:300px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "DeptIndicatorSelect.aspx?seltype=single&rtntype=array";
            OpenModelWin(url, {}, style, function() {
                if (this.data.Id && this.data.MaxScore) {
                    $.ajaxExec("create", { IndicatorSecondId: this.data.Id, IndicatorSecondName: this.data.IndicatorSecondName, originalId: val ? val : "",
                        Weight: this.data.MaxScore, DeptId: this.data.BelongDeptId, DeptName: this.data.BelongDeptName, DeptIndicatorName: this.data.IndicatorName
                    }, function(rtn) {
                        if (rtn.data.Entity) {
                            var recType = store.recordType;
                            var rec = new recType({ Id: rtn.data.Entity.Id, IndicatorNo: rtn.data.Entity.IndicatorNo,
                                DeptName: rtn.data.Entity.DeptName, State: rtn.data.Entity.State, Remark: rtn.data.Entity.Remark,
                                DeptIndicatorName: rtn.data.Entity.DeptIndicatorName, IndicatorSecondId: rtn.data.Entity.SencondIndicatorId,
                                IndicatorSecondName: rtn.data.Entity.IndicatorSecondName, Weight: rtn.data.Entity.Weight,
                                CreateName: rtn.data.Entity.CreateName, CreateTime: rtn.data.Entity.CreateTime
                            });
                            store.reload();
                        }
                    })
                }
            });
        }
        function SetDeptIndicator() {
            var style = "dialogWidth:800px; dialogHeight:300px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "DeptIndicatorSelect.aspx?seltype=single&rtntype=array";
            OpenModelWin(url, {}, style, function() {
                if (this.data.Id && this.data.MaxScore) {
                    var recs = grid.getSelectionModel().getSelections();
                    if (recs[0].get("IndicatorSecondId") != this.data.Id) {//如果和先前关联的部门二级指标不同 ，则更新
                        recs[0].set("IndicatorSecondName", this.data.IndicatorSecondName);
                        recs[0].set("IndicatorSecondId", this.data.Id);
                        recs[0].set("DeptIndicatorName", this.data.IndicatorName);
                        $.ajaxExec("AutoUpdate", { id: recs[0].get("Id"), IndicatorSecondId: recs[0].get("IndicatorSecondId"),
                            IndicatorSecondName: recs[0].get("IndicatorSecondName"), DeptIndicatorName: recs[0].get("DeptIndicatorName")
                        },
                            function(rtn) { recs[0].commit(); });
                    }
                }
            });
        }
        function CopyRecord(id) {
            SelectDeptIndicator(id);
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function userSelect(rtn) {
            if (rtn && rtn.data && grid.activeEditor) {
                var rec = store.getAt(grid.activeEditor.row);
                if (rec) {
                    rec.set("ApproveUserId", rtn.data.UserID);
                    rec.set("ApproveUserName", rtn.data.Name);
                    $.ajaxExec("AutoUpdate", { id: rec.get("Id"), ApproveUserId: rtn.data.UserID, ApproveUserName: rtn.data.Name }, function(rtn) {
                        grid.stopEditing();
                        rec.commit();
                    })
                }
            }
        };
        function BeforeShow() {
            var rec = store.getAt(grid.activeEditor.row);
            this.popUrl = "/commonpages/select/UsrSelect/MUsrSelect.aspx?seltype=single&GroupID=" + rec.get("Remark");
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "Number":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='window.open(\"../SaleManagement/SaleOrderView.aspx?id=" +
                                      record.get('OId') + "\",\"wind\",\"" + ViewWinStyle + "\")'>" + value + "</label>";
                    }
                    break;
                case "Result":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + record.get("Opinion") + '"';
                        rtn = value;
                    }
                    break;
                case "DeptIndicatorName":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
                case "Summary":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='DownLoad(\"" + value + "\")'>工作总结</label>";
                    }
                    break;
            }
            return rtn;
        }
        function ShowDetail(val) {
            opencenterwin("UserBalanceEdit.aspx?op=u&id=" + val, "", 1000, 600);
        }
        function DownLoad(val) {
            opencenterwin("../CommonPages/File/DownLoad.aspx?id=" + val, "", 120, 120);
        }         
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
