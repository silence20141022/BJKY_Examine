<%@ Page Title="��Ա����" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="GroupPersonConfig.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.GroupPersonConfig" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var GroupTypeEnum = { "��ӪĿ�굥λ": "��ӪĿ�굥λ", "ְ�ܷ�����": "ְ�ܷ�����" };
        function onPgLoad() {
            setPgUI();
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
                   { fieldLabel: '��������', id: 'GroupName', schopts: { qryopts: "{ mode: 'Like', field: 'GroupName' }"} },
                   { fieldLabel: '��������', id: 'GroupType', schopts: { qryopts: "{ mode: 'Like', field: 'GroupType' }"} },
                   { fieldLabel: '��Ա����', id: 'ClerkNames', schopts: { qryopts: "{ mode: 'Like', field: 'ClerkNames' }"} }
                //                   { fieldLabel: '��ť', xtype: 'button', iconCls: 'aim-icon-search', width: 60, margins: '1 30 0 0', text: '�� ѯ', handler: function() {
                //                       Ext.ux.AimDoSearch(Ext.getCmp("GroupName"));
                //                   }
                //                   }
		            ]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '���',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        opencenterwin("PersonConfigEdit.aspx?op=c", "", 1200, 650);
                    }
                }, '-', {
                    text: '�޸�',
                    iconCls: 'aim-icon-edit',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("����ѡ��Ҫ�޸ĵļ�¼��");
                            return;
                        }
                        opencenterwin("PersonConfigEdit.aspx?op=u&id=" + recs[0].get("Id"), "", 1200, 650);
                    }
                }, '-', {
                    text: 'ɾ��',
                    iconCls: 'aim-icon-delete',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("����ѡ��Ҫɾ���ļ�¼��");
                            return;
                        }
                        if (confirm("ɾ�����Ż���ͬ�����µ���Ա������Ϣһ��ɾ����ȷ��Ҫɾ����ѡ��¼��")) {
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
                     text: '������������',
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
                    { id: 'GroupName', dataIndex: 'GroupName', header: '��������', width: 150, renderer: RowRender },
					{ id: 'GroupType', dataIndex: 'GroupType', header: '����', width: 100 },
					{ id: 'FirstLeaderNames', dataIndex: 'FirstLeaderNames', header: '������ְ', width: 70 },
					{ id: 'SecondLeaderNames', dataIndex: 'SecondLeaderNames', header: '���Ÿ�ְ', width: 80 },
					{ id: 'ChargeSecondLeaderNames', dataIndex: 'ChargeSecondLeaderNames', header: '���ֹ������Ÿ�ְ', width: 80 },
					{ id: 'InstituteClerkDelegateNames', dataIndex: 'InstituteClerkDelegateNames', header: 'Ժ��Ա������', width: 90, renderer: RowRender },
					{ id: 'DeptClerkDelegateNames', dataIndex: 'DeptClerkDelegateNames', header: '����Ա������', width: 90, renderer: RowRender },
					{ id: 'ClerkNames', dataIndex: 'ClerkNames', header: '����Ա��', width: 120, renderer: RowRender },
					{ id: 'PeopleQuan', dataIndex: 'PeopleQuan', header: '��������', width: 60 },
					{ id: 'ExcellentRate', dataIndex: 'ExcellentRate', header: '������(%)', width: 70 },
					{ id: 'ExcellentQuan', dataIndex: 'ExcellentQuan', header: '<font color="red">��������</font>', width: 60,
					    editor: { xtype: 'numberfield', allowBlank: false, decimalPrecision: 0, minValue: 0, maxValue: 100 }
					},
					{ id: 'GoodRate', dataIndex: 'GoodRate', header: '������(%)', width: 70 },
					{ id: 'GoodQuan', dataIndex: 'GoodQuan', header: '<font color="red">��������</font>', width: 60,
					    editor: { xtype: 'numberfield', allowBlank: false, decimalPrecision: 2, minValue: 0, maxValue: 100 }
					}
];
            // ������
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
            // ҳ����ͼ
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        // �ύ���ݳɹ���
        function onExecuted() {
            store.reload();
        }
        function ExamineStageSelect() {
            var style = "dialogWidth:800px; dialogHeight:400px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "ExamineStageSelect.aspx?seltype=single";
            OpenModelWin(url, {}, style, function() {
                if (this.data.Id) {
                    var ExamineStageId = this.data.Id;
                    Ext.getBody().mask("�������������С�������");
                    $.ajaxExec("CreateTaskAgain", { ExamineStageId: ExamineStageId }, function() {
                        ShowAmendTaskList(ExamineStageId)
                        Ext.getBody().unmask();
                    });
                }
            });
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //��ô��ڵĴ�ֱλ��;.aspx
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //��ô��ڵ�ˮƽλ��;
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
