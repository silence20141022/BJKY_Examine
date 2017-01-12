<%@ Page Title="考核项目" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="IndicatorFirstList.aspx.cs" Inherits="Aim.Examine.Web.ExamineConfig.IndicatorFirstList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var ExamineIndicatorId = $.getQueryString({ ID: 'ExamineIndicatorId' });
        var IndicatorName = unescape($.getQueryString({ ID: 'IndicatorName' }));
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            // 表格数据
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            //debugger
            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData,
                fields: [
			        { name: 'Id' }, { name: 'ExamineIndicatorId' }, { name: 'IndicatorFirstName' }, { name: 'InsteadColumn' },
			        { name: 'SortIndex' }, { name: 'MaxScore' }, { name: 'CustomColumn' }
			    ],
                listeners: { aimbeforeload: function(proxy, options) {
                    options.data = options.data || {};
                    options.data.ExamineIndicatorId = ExamineIndicatorId;
                }
                }
            });
            var cb = new Ext.ux.form.AimComboBox({
                id: 'combo',
                enumdata: { F: "否", T: "是" },
                lazyRender: false,
                allowBlank: false,
                autoLoad: true,
                //forceSelection: true,
                //blankText: "none",
                //valueField: 'text',
                triggerAction: 'all',
                mode: 'local',
                listeners: {
                    collapse: function(obj) {
                        if (grid.activeEditor) {
                            var rec = store.getAt(grid.activeEditor.row);
                            if (rec) {
                                grid.stopEditing(); //把选择项的value 和name分别赋值给不同字段的方法
                                // rec.set("ToRoleName", Ext.get('combo').dom.value);
                                // rec.set("InsteadColumn", obj.value);
                            }
                        }
                    }
                }
            });
            var cb2 = new Ext.ux.form.AimComboBox({
                id: 'combo2',
                enumdata: { F: "否", T: "是" },
                lazyRender: false,
                allowBlank: false,
                autoLoad: true,
                //forceSelection: true,
                //blankText: "none",
                //valueField: 'text',
                triggerAction: 'all',
                mode: 'local',
                listeners: {
                    collapse: function(obj) {
                        if (grid.activeEditor) {
                            var rec = store.getAt(grid.activeEditor.row);
                            if (rec) {
                                grid.stopEditing(); //把选择项的value 和name分别赋值给不同字段的方法
                                // rec.set("ToRoleName", Ext.get('combo').dom.value);
                                // rec.set("InsteadColumn", obj.value);
                            }
                        }
                    }
                }
            });
            // 工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [
               { text: '添加',
                   iconCls: 'aim-icon-add',
                   handler: function() {
                       if (ExamineIndicatorId) {
                           var recType = store.recordType;
                           var maxIndex = 1;
                           store.each(function(record) {
                               if (record.get("SortIndex") >= maxIndex) {
                                   maxIndex = (record.get("SortIndex") || 0) + 1;
                               }
                           });
                           var rec = new recType({ "ExamineIndicatorId": ExamineIndicatorId,
                               "IndicatorName": IndicatorName, 'SortIndex': maxIndex
                           });
                           store.insert(store.data.length, rec);
                       }
                       else {
                           AimDlg.show("请先选择要添加考核项目的考核指标！");
                           return;
                       }
                   }
               }, '-',
               { text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function() {
                       var recs = grid.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("删除考核项目的时候会连同考核要素和考核标准一起删除，确定删除所选记录吗？")) {
                           ExtBatchOperate('batchdelete', recs, null, null, function() {
                               store.remove(recs);
                               recs = store.getRange();
                               if (recs.length > 0) {
                                   frameContent.location.href = "IndicatorSecondList.aspx?IndicatorFirstName=" + escape(recs[0].get("IndicatorFirstName")) + "&IndicatorFirstId=" + recs[0].get("Id") + "&ExamineIndicatorId=" + ExamineIndicatorId;
                               }
                               else {
                                   frameContent.location.href = "IndicatorSecondList.aspx?IndicatorFirstName=" + "&IndicatorFirstId=" + "&ExamineIndicatorId=" + ExamineIndicatorId;
                               }
                           });
                       }
                   }
               }
]
            });
            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar
            });
            grid = new Ext.ux.grid.AimEditorGridPanel({
                title: '【' + IndicatorName + '】' + '考核项目',
                store: store,
                clicksToEdit: 1,
                split: true,
                region: 'west',
                width: 380,
                autoExpandColumn: 'IndicatorFirstName',
                columns: [
                    { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'IndicatorFirstName', dataIndex: 'IndicatorFirstName', header: '考核项目', width: 100,
					    editor: { xtype: 'textfield' }, renderer: RowRender
					},
                    { id: 'MaxScore', dataIndex: 'MaxScore', header: '权重', width: 50, editor: { xtype: 'numberfield', minValue: 0, maxValue: 100, allowBlank: false} },
					{ id: 'SortIndex', dataIndex: 'SortIndex', header: '排序索引', width: 60, sortable: true,
					    editor: { xtype: 'numberfield', minValue: 0, allowBland: false }
					},
				   	{ id: 'InsteadColumn', dataIndex: 'InsteadColumn', header: '人力考核项', width: 80, editor: cb, renderer: RowRender },
				   	{ id: 'CustomColumn', dataIndex: 'CustomColumn', header: '自定义考核项', width: 80, editor: cb2, renderer: RowRender }
                    ],
                tbar: tlBar,
                listeners: { rowclick: function() {
                    var recs = grid.getSelectionModel().getSelections();
                    if (recs && recs.length > 0 && recs[0].get("IndicatorFirstName")) {
                        frameContent.location.href = "IndicatorSecondList.aspx?IndicatorFirstName=" + escape(recs[0].get("IndicatorFirstName")) + "&IndicatorFirstId=" + recs[0].get("Id") + "&ExamineIndicatorId=" + (ExamineIndicatorId || '') + "&Custom=" + recs[0].get("CustomColumn");
                    }
                }
                     , afteredit: function(e) {
                         if (e.record.get("IndicatorFirstName")) {
                             var dt = store.getModifiedDataStringArr([e.record]) || [];
                             jQuery.ajaxExec("Save", { data: dt }, function(rtn) {
                                 if (rtn.data.Id) {
                                     e.record.set("Id", rtn.data.Id); e.record.commit();
                                 }
                             });
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
                    frameContent.location.href = "IndicatorSecondList.aspx?IndicatorFirstName=" + escape(store.getAt(0).get("IndicatorFirstName")) + "&IndicatorFirstId=" + store.getAt(0).get("Id") + "&ExamineIndicatorId=" + (ExamineIndicatorId || '') + "&Custom=" + store.getAt(0).get("CustomColumn");
                }
                else {
                    frameContent.location.href = "IndicatorSecondList.aspx?IndicatorFirstName=";
                }
            }
            function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
                var rtn;
                switch (this.id) {
                    case "Number":
                        if (value) {
                            rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='window.open(\"../SaleManagement/SaleOrderView.aspx?id=" +
                                      record.get('OId') + "\",\"wind\",\"" + ViewWinStyle + "\")'>" + value + "</label>";
                        }
                        break;
                    case "IndicatorFirstName":
                        if (value == null || value == "")
                            return;
                        cellmeta.attr = 'ext:qtitle=""' + 'ext:qtip="' + value + '"';
                        rtn = value;
                        break;
                    case "InsteadColumn":
                    case "CustomColumn":
                        if (value) {
                            rtn = value == "T" ? "是" : "否";
                        }
                        break
                }
                return rtn;
            }         
    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
