<%@ Page Title="自定义考核内容" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="PersonFirstIndicatorList.aspx.cs" Inherits="Aim.Examining.Web.DeptConfig.PersonFirstIndicatorList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=850,height=600,scrollbars=yes");
        var EditPageUrl = "PersonFirstIndicatorEdit.aspx"; 
        var Year = $.getQueryString({ ID: "Year" });
        var StageType = $.getQueryString({ ID: "StageType" });
        var CustomIndicatorId = $.getQueryString({ ID: "CustomIndicatorId" });
        var State = $.getQueryString({ ID: "State" });
        var Result = unescape($.getQueryString({ ID: "Result" }));
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var enumType = { '日常工作': '日常工作', '专项工作': '专项工作', '其他工作': '其他工作' };
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
			{ name: 'Id' }, { name: 'CustomIndicatorId' }, { name: 'PersonFirstIndicatorName' }, { name: 'Weight' }, { name: 'TotalWeight' },
			{ name: 'SortIndex' }, { name: 'IndicatorType' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime' }
			],
                listeners: { aimbeforeload: function(proxy, options) {
                    options.data.CustomIndicatorId = CustomIndicatorId;
                }
                }
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        var recType = store.recordType;
                        $.ajaxExec("create", { CustomIndicatorId: CustomIndicatorId }, function(rtn) {
                            if (rtn.data.Entity)
                                var rec = new recType({ Id: rtn.data.Entity.Id, CustomIndicatorId: CustomIndicatorId,
                                    TotalWeight: rtn.data.Entity.TotalWeight, CreateId: rtn.data.Entity.CreateId, SortIndex: rtn.data.Entity.SortIndex,
                                    CreateName: rtn.data.Entity.CreateName, CreateTime: rtn.data.Entity.CreateTime
                                });
                            store.insert(store.data.length, rec);
                        })
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
                        if (confirm("删除考核内容会连同其下量化指标一起删除，确定删除所选记录？")) {
                            var idarray = [];
                            $.each(recs, function() {
                                idarray.push(this.get("Id"));
                            })
                            $.ajaxExec("delete", { ids: idarray }, function() {
                                store.remove(recs);
                                if (store.data.length > 0) {
                                    frameContent.location.href = "PersonSecondIndicatorList.aspx?&PersonFirstIndicatorId=" + store.getAt(0).get("Id") +
                                     "&PersonFirstIndicatorName=" + escape(store.getAt(0).get("PersonIndicatorFirstName") || "") +
                                     "&maxScore=" + store.getAt(0).get("Weight") + "&State=" + State + "&Result=" + escape(Result);
                                }
                                else {
                                    frameContent.location.href = "PersonSecondIndicatorList.aspx?";
                                }
                            });
                        }
                    }
}]
                });
                var cb_IndicatorType = new Ext.ux.form.AimComboBox({
                    id: 'cb_IndicatorType',
                    enumdata: enumType,
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
                                    rec.set("IndicatorType", Ext.get('cb_IndicatorType').dom.value);
                                }
                            }
                        }
                    }
                });
                var title = AimState["UserInfo"].Name;
                if (Year != 'null' && Year != 'undefined') {
                    title += Year;
                }
                if (StageType != 'null' && StageType != 'undefined') {
                    if (StageType != '4') {
                        title += '第' + StageType + '季度工作业绩';
                    }
                    else {
                        title += '年度工作业绩';
                    }
                }
                else {
                    title += "工作业绩";
                }
                grid = new Ext.ux.grid.AimEditorGridPanel({
                    title: title,
                    store: store,
                    region: 'west',
                    width: 450,
                    split: true,
                    autoExpandColumn: 'PersonFirstIndicatorName',
                    columns: [
                    { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                     { id: 'IndicatorType', dataIndex: 'IndicatorType', header: '工作类型', width: 90, sortable: true, editor: cb_IndicatorType },
					{ id: 'PersonFirstIndicatorName', dataIndex: 'PersonFirstIndicatorName', header: '考核内容', sortable: true,
					    width: 100, editor: { xtype: 'textfield', allowBlank: false }, renderer: RowRender
					},
				    { id: 'Weight', dataIndex: 'Weight', header: '权重', width: 50, sortable: true,
				        editor: { xtype: 'numberfield', id: 'nf_weight', minValue: 0, allowBlank: false }
				    },
					{ id: 'SortIndex', dataIndex: 'SortIndex', header: '索引', width: 50, sortable: true,
					    editor: { xtype: 'numberfield', id: 'nf_SortIndex', minValue: 0, allowBlank: false, decimalPrecision: 0 }
					}
					],
                    tbar: (State == "1" || Result == "同意") ? "" : tlBar,
                    listeners: { rowclick: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if ((recs || recs.length > 0) && recs[0].get("Id")) {
                            frameContent.location.href = "PersonSecondIndicatorList.aspx?&PersonFirstIndicatorId=" + recs[0].get("Id") + "&PersonFirstIndicatorName=" +
                            escape(recs[0].get("PersonFirstIndicatorName") || "") + "&maxScore=" + recs[0].get("Weight") + "&State=" + State + "&Result=" + escape(Result);
                        }
                    }, beforeedit: function(e) {
                        var totalScore = e.record.get("TotalWeight");
                        var currentScore = 0;
                        var records = store.getRange();
                        for (var v = 0; v < records.length; v++) {
                            if (v == e.row) continue;
                            currentScore = parseInt(currentScore) + parseInt(records[v].get("Weight") || 0);
                        }
                        currentScore = parseInt(totalScore) - parseInt(currentScore);
                        Ext.getCmp("nf_weight").setMaxValue(parseInt(currentScore));
                    }, afteredit: function(e) {
                        if (e.value) {
                            var dt = store.getModifiedDataStringArr([e.record]);
                            $.ajaxExec("AutoUpdate", { data: dt }, function(rtn) { e.record.commit(); });
                        }
                    }
                    }
                });
                viewport = new Ext.ux.AimViewport({
                    layout: 'border',
                    items: [grid, {
                        id: 'frmcon',
                        region: 'center',
                        margins: '-1 0 -2 0',
                        html: '<iframe width="100%" height="100%" id="frameContent" name="frameContent" frameborder="0"></iframe>'}]
                    });
                    if (store.data.length > 0) {
                        frameContent.location.href = "PersonSecondIndicatorList.aspx?&PersonFirstIndicatorId=" + store.getAt(0).get("Id") + "&PersonFirstIndicatorName=" +
                        escape(store.getAt(0).get("PersonFirstIndicatorName") || "") + "&maxScore=" + store.getAt(0).get("Weight") + "&maxScore=" + store.getAt(0).get("Weight") + "&State=" + State + "&Result=" + escape(Result);
                    }
                    else {
                        frameContent.location.href = "PersonSecondIndicatorList.aspx?";
                    }
                    grid.getColumnModel().isCellEditable = function(colIndex, rowIndex) {
                        // var columnId = grid.getColumnModel().getColumnId(colIndex);
                        var rec = store.getAt(rowIndex);
                        if (State == "1" || Result == "同意") {
                            return false;
                        }
                        return Ext.grid.ColumnModel.prototype.isCellEditable.call(this, colIndex, rowIndex);
                    }
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
                        case "PersonFirstIndicatorName":
                            if (value) {
                                value = value || "";
                                cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                                rtn = value;
                            }
                            break;
                    }
                    return rtn;
                }
                // 提交数据成功后
                function onExecuted() {
                    store.reload();
                }
    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
