<%@ Page Title="���˹�ϵ" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="ExamineRelationList.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.ExamineRelationList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var pgBar, schBar, tlBar, titPanel, myData, store, grid, viewport;
        var cb_be, multistore, lc_up, lc_same, lc_down;
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
			    { name: 'Id' }, { name: 'RelationName' }, { name: 'BeRoleCode' }, { name: 'BeRoleName' },
			    { name: 'UpLevelCode' }, { name: 'UpLevelName' }, { name: 'UpLevelWeight' },
			    { name: 'SameLevelCode' }, { name: 'SameLevelName' }, { name: 'SameLevelWeight' },
			    { name: 'DownLevelCode' }, { name: 'DownLevelName' }, { name: 'DownLevelWeight' },
			    { name: 'Remark' }, { name: 'CreateId' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime' }
			  ]
            });
            // ��ҳ��
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 5,
                collapsed: false,
                items: [
                { fieldLabel: '��ɫ����', labelWidth: 100, id: 'StageName', schopts: { qryopts: "{ mode: 'Like', field: 'StageName' }"} },
                { fieldLabel: 'ָ������', id: 'LaunchUserName', schopts: { qryopts: "{ mode: 'Like', field: 'LaunchUserName' }"} }
                              ]
            });

            function weightVerify() {  //Ȩ����֤
                var upWeight = 0, sameWeight = 0, downWeight = 0, tip = "";
                var recods = grid.getStore().getModifiedRecords();
                for (var i = 0; i < recods.length; i++) {
                    var up = parseFloat(recods[i].get("UpLevelWeight")) || 0;
                    var same = parseFloat(recods[i].get("SameLevelWeight")) || 0;
                    var down = parseFloat(recods[i].get("DownLevelWeight")) || 0;
                    upWeight += up, sameWeight += same, downWeight += down;
                    if (up + same + down != 100) tip += "��ǰ���б༭�ĵ�" + (i + 1) + "��,Ȩ��֮��Ϊ:" + (up + same + down) + ",ֵӦΪ100! ��������д.\r\n";
                }
                if ((upWeight + sameWeight + downWeight) == 100 * recods.length) {
                    return true;
                } else {
                    return alert(tip);
                }
            }

            // ������
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '���',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        var recType = store.recordType;
                        var rec = new recType({});
                        store.insert(store.data.length, rec);
                    }
                }, '-', {
                    text: '����',
                    iconCls: 'aim-icon-save',
                    handler: function() {
                        var recs = store.getModifiedRecords();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("û����Ҫ����ļ�¼��");
                            return;
                        }
                        var dt = store.getModifiedDataStringArr(recs) || [];
                        if (store.getAt(store.data.length - 1).get("BeRoleCode")) {
                            if (weightVerify()) {
                                $.ajaxExec("save", { data: dt }, function(rtn) {
                                    AimDlg.show("����ɹ���");
                                    store.commitChanges();
                                });
                            }
                        } else {
                            AimDlg.show("��ѡ�񱻿��˶���!");
                        }
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
                        if (confirm("ȷ��ɾ����ѡ��¼��")) {
                            ExtBatchOperate('batchdelete', recs, null, null, onExecuted);
                        }
                    }
                },
                 '->']
            });
            cb_be = new Ext.ux.form.AimComboBox({
                id: 'combo_be',
                enumdata: AimState["BeRoleName"],
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
                                rec.set("BeRoleName", Ext.get('combo_be').dom.value);
                                rec.set("BeRoleCode", obj.value);
                                if (verfify(Ext.get('combo_be').dom.value) >= 2) {
                                    rec.set("BeRoleName", "");
                                    rec.set("BeRoleCode", "");
                                    AimDlg.show("�Ѵ��ڸÿ��˶���,������ѡ��!");
                                }
                            }
                        }
                    }
                }
            });

            multistore = new Ext.ux.data.AimJsonStore({
                dsname: 'ToRoleName',
                idProperty: 'Value',
                data: {
                    records: AimState["ToRoleName"] || []
                },
                fields: [{ name: 'Name' }, { name: 'Value' }
		    ]
            });
            lc_up = new Ext.ux.form.MultiComboBox({
                id: 'combo_up',
                enableKeyEvents: true,
                width: 100,
                hideOnSelect: false,
                store: multistore,
                editable: true,
                allowBlank: false,
                triggerAction: 'all',
                valueField: 'Value',
                displayField: 'Name',
                mode: 'local',
                // , hiddenName: 'idCombo'
                lazyInit: false,
                listeners: {
                    blur: function(obj) {
                        if (grid.activeEditor) {
                            var rec = store.getAt(grid.activeEditor.row);
                            if (rec) {
                                rec.set("UpLevelCode", obj.getValue());
                                rec.set("UpLevelName", $("#combo_up").val());
                            }
                        }
                    }
                }
            });
            lc_same = new Ext.ux.form.MultiComboBox({
                id: 'combo_same',
                enableKeyEvents: true,
                width: 100,
                hideOnSelect: false,
                store: multistore,
                editable: true,
                allowBlank: false,
                triggerAction: 'all',
                valueField: 'Value',
                displayField: 'Name',
                mode: 'local',
                hiddenName: 'idCombo',
                lazyInit: false,
                listeners: {
                    blur: function(obj) {
                        if (grid.activeEditor) {
                            var rec = store.getAt(grid.activeEditor.row);
                            if (rec) {
                                rec.set("SameLevelCode", obj.getValue());
                                rec.set("SameLevelName", $("#combo_same").val());
                            }
                        }
                    }
                }
            });
            lc_down = new Ext.ux.form.MultiComboBox({
                id: 'combo_down',
                enableKeyEvents: true,
                width: 100,
                hideOnSelect: false,
                store: multistore,
                editable: true,
                allowBlank: false,
                triggerAction: 'all',
                valueField: 'Value',
                displayField: 'Name',
                mode: 'local',
                hiddenName: 'idCombo',
                lazyInit: false,
                listeners: {
                    blur: function(obj) {
                        if (grid.activeEditor) {
                            var rec = store.getAt(grid.activeEditor.row);
                            if (rec) {
                                rec.set("DownLevelCode", obj.getValue());
                                rec.set("DownLevelName", $("#combo_down").val());
                            }
                        }
                    }
                }
            });
            function verfify(inVal) {
                var count = 0;
                grid.getStore().each(function(val) {
                    if (inVal == val.get("BeRoleName")) count++;
                });
                return count;
            }
            var columnsarray = [
                    { id: 'Id', dataIndex: 'Id', header: 'Id', hidden: true },
                    { id: 'BeRoleName', dataIndex: 'BeRoleName', header: 'BeRoleName', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'RelationName', dataIndex: 'RelationName', header: '���˹�ϵ����', width: 160, editor: { xtype: 'textfield', allowBlank: false} },
                    { id: 'BeRoleCode', dataIndex: 'BeRoleCode', header: '���˶���', width: 120, editor: cb_be, renderer: RowRender },
                    { id: 'UpLevelCode', dataIndex: 'UpLevelCode', header: '<strong><font size=2>�ϼ�������</font></strong>',
                        width: 180, editor: lc_up, renderer: RowRender
                    },
                    { id: 'UpLevelWeight', dataIndex: 'UpLevelWeight', header: 'Ȩ��', width: 40,
                        editor: { xtype: 'numberfield', allowBlank: false, minValue: 0, maxValue: 100, decimalPrecision: 0 }, tooltip: 'Ȩ�ص�λΪ:%'
                    },
                    { id: 'SameLevelCode', dataIndex: 'SameLevelCode', header: '<strong><font size=2>ͬ��������</font></strong>',
                        width: 180, editor: lc_same, renderer: RowRender
                    },
                    { id: 'SameLevelWeight', dataIndex: 'SameLevelWeight', header: 'Ȩ��', width: 40,
                        editor: { xtype: 'numberfield', allowBlank: false, minValue: 0, maxValue: 100, decimalPrecision: 0 }, tooltip: 'Ȩ�ص�λΪ:%'
                    },
                    { id: 'DownLevelCode', dataIndex: 'DownLevelCode', header: '<strong><font size=2>�¼�������</font></strong>', width: 180,
                        editor: lc_down, renderer: RowRender
                    },
                    { id: 'DownLevelWeight', dataIndex: 'DownLevelWeight', header: 'Ȩ��', width: 40,
                        editor: { xtype: 'numberfield', allowBlank: false, minValue: 0, maxValue: 100, decimalPrecision: 0 }, tooltip: 'Ȩ�ص�λΪ:%'
                    }
				 ];
            grid = new Ext.ux.grid.AimEditorGridPanel({
                title: '���˹�ϵ',
                store: store,
                region: 'north',
                height: 250,
                viewConfig: { forceFit: true },
                autoExpandColumn: 'RelationName',
                columnLines: true,
                columns: columnsarray,
                cls: 'grid-row-span',
                tbar: pgOperation == "v" ? "" : tlBar,
                listeners: { rowclick: function() {
                    var recs = grid.getSelectionModel().getSelections();
                    if ((recs && recs.length > 0) && recs[0].get("Id")) {
                        frameContent.location.href = "UserBalanceList.aspx?RelationName=" + escape(recs[0].get("RelationName")) + "&ExamineRelationId=" + recs[0].get("Id") + "&op=" + pgOperation;
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
                    frameContent.location.href = "UserBalanceList.aspx?RelationName=" + escape(store.getAt(0).get("RelationName")) + "&ExamineRelationId=" + store.getAt(0).get("Id") + "&op=" + pgOperation;
                }
                else {
                    frameContent.location.href = "UserBalanceList.aspx?RelationName=&ExamineRelationId=" + "&op=" + pgOperation;
                }
            }
            function onExecuted() {
                store.reload();
            }
            function opencenterwin(url, name, iWidth, iHeight) {
                var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //��ô��ڵĴ�ֱλ��;
                var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //��ô��ڵ�ˮƽλ��;
                window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
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
                    case "UpLevelCode":
                        if (value) {
                            rtn = record.get("UpLevelName");
                        }
                        break;
                    case "SameLevelCode":
                        if (value) {
                            rtn = record.get("SameLevelName");
                        }
                        break;
                    case "DownLevelCode":
                        if (value) {
                            rtn = record.get("DownLevelName");
                        }
                        break;
                    case "BeRoleCode":
                        if (value) {
                            rtn = record.get("BeRoleName");
                        }
                        break;
                }
                return rtn;
            }
            function ShowDetail(val) {
                opencenterwin("UserBalanceEdit.aspx?op=u&id=" + val, "", 1000, 600);
            } 
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
