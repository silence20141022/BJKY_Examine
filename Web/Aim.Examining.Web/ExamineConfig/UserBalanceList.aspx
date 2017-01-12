<%@ Page Title="人员权重列表" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="UserBalanceList.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.UserBalanceList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        body
        {
            background-color: #F2F2F2;
        }
        .aim-ui-td-caption
        {
            text-align: right;
        }
        fieldset
        {
            border: solid 1px #8FAACF;
            margin: 15px;
            width: 100%;
            padding: 5px;
        }
        fieldset legend
        {
            font-size: 12px;
            font-weight: bold;
        }
        input
        {
            width: 90%;
        }
        .x-superboxselect-display-btns
        {
            width: 90% !important;
        }
        .x-form-field-trigger-wrap
        {
            width: 100% !important;
        }
    </style>

    <script type="text/javascript">
        var myData, store, tlBar, grid;
        var ExamineRelationId = $.getQueryString({ ID: "ExamineRelationId" });
        var RelationName = unescape($.getQueryString({ ID: 'RelationName' }));
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
			    { name: 'Id' }, { name: 'ExamineRelationId' }, { name: 'RelationName' }, { name: 'ToUserId' }, { name: 'ToUserName' },
			    { name: 'ToRoleCode' }, { name: 'ToRoleName' }, { name: 'Balance' }, { name: 'SortIndex' }
			  ]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加人员权重',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        var EntRecord = store.recordType;
                        insRowIdx = store.data.length;
                        $.ajaxExec("Create", { ExamineRelationId: ExamineRelationId, RelationName: RelationName, SortIndex: parseInt(insRowIdx) + 1 }, function(rtn) {
                            if (rtn.data.ubEnt) {
                                var p = new EntRecord(rtn.data.ubEnt);
                                store.insert(insRowIdx, p);
                            }
                        })
                        //BeRoleCode: BeRoleCode, BeRoleName: BeRoleName,if (store.find("ToUserId", data[i].UserID) != -1)   //筛掉已经存在的人
                    }
                }, '-', {
                    text: '删除人员权重',
                    iconCls: 'aim-icon-delete',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要删除的记录！");
                            return;
                        }
                        var ids = [];
                        $.each(recs, function() { ids.push(this.get("Id")) });
                        $.ajaxExec("delete", { ids: ids }, function() { store.remove(recs); })
                    }
                },
                 '->']
            });
            var store_cb = new Ext.ux.data.AimJsonStore({
                dsname: 'ToRoleName',
                idProperty: 'Value',
                data: {
                    records: [{ Name: '上级评分人', Value: 'UpLevel' }, { Name: '同级评分人', Value: 'SameLevel' }, { Name: '下级评分人', Value: 'DownLevel'}]
                },
                fields: [{ name: 'Name' }, { name: 'Value' }
		    ]
            });
            var singlecb = new Ext.ux.form.AimComboBox({
                id: 'singlecb',
                enumdata: { UpLevel: '上级评分人', SameLevel: '同级评分人', DownLevel: '下级评分人' },
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
                                rec.set("ToRoleName", Ext.get('singlecb').dom.value);
                                rec.set("ToRoleCode", obj.value);
                                $.ajaxExec("AutoSave", { id: rec.get("Id"), ToRoleCode: obj.value, ToRoleName: Ext.get('singlecb').dom.value }, function(rtn) {
                                    grid.stopEditing();
                                    rec.commit();
                                })
                            }
                        }
                    }
                }
            });
            grid = new Ext.ux.grid.AimEditorGridPanel({
                title: (RelationName ? RelationName : '') + '__评分人权重明细',
                store: store,
                region: 'center',
                autoExpandColumn: 'RelationName',
                columnLines: true,
                columns: [
                    { id: 'Id', dataIndex: 'Id', header: 'Id', hidden: true },
                    { id: 'ExamineRelationId', dataIndex: 'ExamineRelationId', header: 'ExamineRelationId', hidden: true },
                    { id: 'ToRoleCode', dataIndex: 'ToRoleCode', header: 'ToRoleCode', hidden: true },
                    { id: 'ToUserId', dataIndex: 'ToUserId', header: 'ToUserId', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'RelationName', dataIndex: 'RelationName', header: '考核关系名称', width: 150 },
                    { id: 'ToRoleName', dataIndex: 'ToRoleName', header: '评分角色类型', width: 150, editor: singlecb },
                    { id: 'ToUserName', dataIndex: 'ToUserName', header: '评分人员名称', width: 150,
                        editor: { xtype: 'aimuserselector', id: 'ToUserId' + '_sel', allowBlank: false, popAfter: UserSel, seltype: 'single',
                            popStyle: 'dialogWidth:750px;dialogHeight:500px', popUrl: "/commonpages/select/UsrSelect/MUsrSelect.aspx?GroupID="
                        }
                    },
                    { id: 'Balance', dataIndex: 'Balance', header: '权重(%)', width: 100,
                        editor: { xtype: 'numberfield', minValue: 0, maxValue: 100, decimalPrecision: 0, allowBlank: false }
                    }
                ],
                cls: 'grid-row-span',
                tbar: pgOperation == "v" ? "" : tlBar,
                listeners: { afteredit: function(e) {
                    if (e.field == "Balance") {
                        var recs = store.query("ToRoleCode", e.record.get("ToRoleCode"));
                        var balance = 0;
                        $.each(recs.items, function() {
                            balance = parseFloat(balance) + parseFloat(this.get("Balance"));
                        })
                        if (balance > 100) {
                            AimDlg.show("同一评分角色类型的权重之和不能大于100！");
                            return;
                        }
                        $.ajaxExec("AutoSave", { id: e.record.get("Id"), Balance: e.value }, function(rtn) {
                            grid.stopEditing();
                            e.record.commit();
                        })
                    }
                }
                }
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            })
        }
        function UserSel(rtn) {
            if (rtn && rtn.data && grid.activeEditor) {
                var rec = store.getAt(grid.activeEditor.row);
                if (rec) {
                    rec.set("ToUserId", rtn.data.UserID);
                    rec.set("ToUserName", rtn.data.Name);
                    var dt = store.getModifiedDataStringArr([rec]) || [];
                    $.ajaxExec("AutoSave", { id: rec.get("Id"), ToUserId: rtn.data.UserID, ToUserName: rtn.data.Name }, function(rtn) {
                        grid.stopEditing();
                        rec.commit();
                    })
                }
            }
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                //                case "Number":                  
                //                    if (value) {                  
                //                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='window.open(\"../SaleManagement/SaleOrderView.aspx?id=" +                  
                //                                      record.get('OId') + "\",\"wind\",\"" + ViewWinStyle + "\")'>" + value + "</label>";                  
                //                    }                  
                //                    break;                  
                //                case "ToRoleCode":                  
                //                    if (value) {                  
                //                        rtn = record.get("ToRoleName");                  
                //                    }                  
                //                    break;                  
                //                case "SameLevelCode":                  
                //                    if (value) {                  
                //                        rtn = record.get("SameLevelName");                  
                //                    }                  
                //                    break;                  
                //                case "DownLevelCode":                  
                //                    if (value) {                  
                //                        rtn = record.get("DownLevelName");                  
                //                    }                  
                //                    break;                  
            }
            return rtn;
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
