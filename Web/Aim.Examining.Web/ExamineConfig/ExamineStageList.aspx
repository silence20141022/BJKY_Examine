<%@ Page Title="���˽׶�" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="True"
    CodeBehind="ExamineStageList.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.ExamineStageList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var EnumData = { 0: '�Ѵ���', 1: '������', 2: '������', 3: '�ѽ���', 4: '�ѽ���ȼ�', 5: '�������ȼ�' };
        var StageEnum = { 1: '��һ����', 2: '�ڶ�����', 3: '��������', 4: '��ȿ���' };
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
            // �������Դ
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

            // ��ҳ��
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 4,
                collapsed: false,
                items: [
                { fieldLabel: '���˽׶�����', labelWidth: 90, id: 'StageName', schopts: { qryopts: "{ mode: 'Like', field: 'StageName' }" } },
                { fieldLabel: '������', id: 'LaunchUserName', schopts: { qryopts: "{ mode: 'Like', field: 'LaunchUserName' }" } },
                { fieldLabel: '��ʼʱ��', id: 'StartTime', xtype: 'datefield', vtype: 'daterange', endDateField: 'EndTime', schopts: { qryopts: "{ mode: 'GreaterThan', datatype:'Date', field: 'StartTime' }" } },
                { fieldLabel: '����ʱ��', id: 'EndTime', xtype: 'datefield', vtype: 'daterange', startDateField: 'StartTime', schopts: { qryopts: "{ mode: 'LessThanEqual', datatype:'Date', field: 'EndTime' }" } }
                ]
            });
            // ������
            tlBar = new Ext.ux.AimToolbar({
                items: [
                {
                    text: '��ӿ���',
                    iconCls: 'aim-icon-add',
                    handler: function () {
                        window.location.href = "../DeptConfig/DeptExamineWizzard.aspx?op=c";
                    }
                },
                '-', {
                    text: 'ɾ��',
                    iconCls: 'aim-icon-delete',
                    handler: function () {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("����ѡ��Ҫɾ���ļ�¼��");
                            return;
                        }
                        if (recs[0].get("State") != "0") {
                            AimDlg.show("����������Ŀ��˲���ɾ����");
                            return;
                        }
                        if (confirm("ȷ��Ҫɾ����ѡ��¼��")) {
                            $.ajaxExec("delete", { id: recs[0].get("Id") }, function (rtn) {
                                store.reload();
                            });
                        }
                    }
                }, '-', {
                    text: '��������',
                    iconCls: 'aim-icon-redo',
                    handler: function () {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("����ѡ��Ҫ��������Ŀ��˽׶Σ�");
                            return;
                        }
                        if (recs[0].get("State") != '0') {
                            AimDlg.show("�Ѵ����Ŀ��˲�����������");
                            return;
                        }
                        if (!recs[0].get("StageName")) {
                            AimDlg.show("�������뿼�˽׶����ƣ�");
                            return;
                        }
                        if (confirm("ȷ��ҪΪ���ο�������������")) {
                            Ext.getBody().mask("�������������С�������");
                            $.ajaxExec("CreateTask", { id: recs[0].get("Id") }, function (rtn) {
                                AimDlg.show(rtn.data.Result); store.reload();
                            });
                        }
                    }
                }, '-',
               {
                   text: '��������',
                   iconCls: 'aim-icon-undo',
                   handler: function () {
                       var recs = grid.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("����ѡ��Ҫ���յĿ��˽׶Σ�");
                           return;
                       }
                       if (recs[0].get("State") != "1") {
                           AimDlg.show("�����ɵĿ��˲��ܻ��գ�");
                           return;
                       }
                       if (confirm("ȷ��Ҫ���ձ��ο��˽׶ε�������")) {
                           Ext.getBody().mask("������������С�������");
                           $.ajaxExec("TakeBack", { id: recs[0].get("Id") }, function (rtn) {
                               if (rtn.data.Result && rtn.data.Result == "T") {
                                   AimDlg.show("��������ɹ���"); store.reload();
                               }
                           });
                       }
                   }
               },
                 '-', {
                     text: '��������',
                     iconCls: 'aim-icon-run',
                     handler: function () {
                         var recs = grid.getSelectionModel().getSelections();
                         if (!recs || recs.length <= 0) {
                             AimDlg.show("����ѡ��Ҫ�����Ŀ��˽׶Σ�");
                             return;
                         }
                         if (recs[0].get("State") != "1") {
                             AimDlg.show("�����ɵĿ��˲���������");
                             return;
                         }
                         $.ajaxExec("JudgeCustomIndicator", { id: recs[0].get("Id") }, function (rtn) {
                             if (rtn.data.Result == "F") {
                                 if (confirm("���ο��˻��б�������δ�Զ���ָ�����δ������Ҫ��������������")) {
                                     Ext.getBody().mask("�������������С�������");
                                     $.ajaxExec("Launch", { id: recs[0].get("Id") }, function (rtn) {
                                         if (rtn.data.Result == "T") {
                                             AimDlg.show("���ο��������ɹ���"); store.reload();
                                         }
                                     });
                                 }
                             }
                             else {
                                 if (confirm("ȷ��Ҫ�������ο�����")) {
                                     Ext.getBody().mask("�������������С�������");
                                     $.ajaxExec("Launch", { id: recs[0].get("Id") }, function (rtn) {
                                         if (rtn.data.Result == "T") {
                                             AimDlg.show("���ο��������ɹ���"); store.reload();
                                         }
                                     });
                                 }
                             }
                         });
                     }
                 }, '-', {
                     text: '��������',
                     iconCls: 'aim-icon-undo',
                     handler: function () {
                         var recs = grid.getSelectionModel().getSelections();
                         if (!recs || recs.length <= 0) {
                             AimDlg.show("����ѡ��Ҫ���յĿ��˽׶Σ�");
                             return;
                         }
                         if (recs[0].get("State") != "2") {
                             AimDlg.show("�������Ŀ��˲��ܳ�����");
                             return;
                         }
                         if (confirm("�������˻�ɾ�����������Լ���ּ�¼��ȷ��Ҫ�������ο�����")) {
                             Ext.getBody().mask("�����������С�������");
                             $.ajaxExec("CancelLaunch", { id: recs[0].get("Id") }, function (rtn) {
                                 store.reload();
                             });
                         }
                     }
                 }, '-',
                {
                    text: '��������',
                    iconCls: 'aim-icon-stop',
                    handler: function () {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("����ѡ��Ҫ�����Ŀ��˽׶Σ�");
                            return;
                        }
                        if (recs[0].get("State") != '2') {
                            AimDlg.show("�������Ŀ��˲��ܽ�����");
                            return;
                        }
                        if (confirm("ȷ��Ҫ�������ο�����")) {
                            Ext.getBody().mask("������������С�������");
                            $.ajaxExec("EndExamine", { id: recs[0].get("Id") }, function (rtn) {
                                if (rtn.data.Result == "T") {
                                    AimDlg.show("���ο��˽����ɹ���"); store.reload();
                                }
                            });
                        }
                    }
                }, '-',
                 {
                     text: '�ȼ��',
                     iconCls: 'aim-icon-grid',
                     handler: function () {
                         var recs = grid.getSelectionModel().getSelections();
                         if (!recs || recs.length <= 0) {
                             AimDlg.show("����ѡ��Ҫ��ȼ������տ��ˣ�");
                             return;
                         }
                         if (recs[0].get("State") != '3' || recs[0].get("StageType") != "4") {
                             AimDlg.show("�ѽ�������ȿ��˲�����ȼ���");
                             return;
                         }
                         opencenterwin("../ExamineTaskManage/LevelAdvice.aspx?ExamineStageId=" + recs[0].get("Id"), "", 1200, 650);
                     }
                 },
                 '->']
            });

            // ���߱�����
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });
            var columnsarray = [
                        new Ext.ux.grid.AimRowNumberer(),
                        new Ext.ux.grid.AimCheckboxSelectionModel(),
                        { id: 'StageName', dataIndex: 'StageName', header: '���˽׶�����', width: 180, renderer: RowRender, sortable: true },
                        { id: 'LaunchUserName', dataIndex: 'LaunchUserName', header: '������', width: 60, sortable: true },
                        { id: 'LaunchDeptName', dataIndex: 'LaunchDeptName', header: '�����˲���', width: 100, sortable: true },
                        { id: 'ExamineType', dataIndex: 'ExamineType', header: '��������', width: 80, sortable: true },
                        { id: 'Year', dataIndex: 'Year', header: '���', width: 70, sortable: true },
                        { id: 'StageType', dataIndex: 'StageType', header: '�׶�����', width: 70, enumdata: StageEnum },
                        { id: 'StartTime', dataIndex: 'StartTime', header: '��ʼʱ��', width: 70, renderer: ExtGridDateOnlyRender, sortable: true },
                        { id: 'EndTime', dataIndex: 'EndTime', header: '����ʱ��', width: 70, renderer: ExtGridDateOnlyRender, sortable: true },
                        { id: 'State', dataIndex: 'State', header: '״̬', width: 80, enumdata: EnumData, sortable: true },
                        { id: 'TaskQuan', dataIndex: 'TaskQuan', header: '������', width: 60, sortable: true },
                        { id: 'SubmitQuan', dataIndex: 'SubmitQuan', header: '�ύ��', width: 60 },
                        { id: 'Id', dataIndex: 'Id', header: '����', sortable: true, width: 120, renderer: RowRender }
            ];
            // ������
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
            // ҳ����ͼ 
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        // �ύ���ݳɹ���
        function onExecuted() {
            store.reload();
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //��ô��ڵĴ�ֱλ��;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //��ô��ڵ�ˮƽλ��;ExamineResultView
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowTask(val, val2) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function () {
                opencenterwin("TaskGroupByToUser.aspx?ExamineStageId=" + val + "&ExamineStageName=" + escape(val2), "", 1000, 600);
            });
        }
        function ShowResult(val, val2, val3, val4) {
            if (val2 == "4" && val4 == "���ż�����") {
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
                        rtn += "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowTask(\"" + value + "\",\"" + record.get("StageName") + "\")'>��������</label>  ";
                    }
                    if (parseInt(record.get("State")) >= 3) {//ExamineType
                        rtn += "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowResult(\"" + value + "\",\"" + record.get("StageType") + "\",\"" + record.get("StageName") + "\",\"" + record.get("ExamineType") + "\")'>���˽��</label>";
                    }
                    break;
            }
            return rtn;
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
