<%@ Page Title="自定义指标分" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="CustomIndicatorScore.aspx.cs" Inherits="Aim.Examining.Web.DeptConfig.CustomIndicatorScore" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <link href="../App_Themes/Ext/ux/css/ColumnHeaderGroup.css" rel="stylesheet" type="text/css" />

    <script src="../js/ext/ux/ColumnHeaderGroup.js" type="text/javascript"></script>

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var ExamineTaskId = $.getQueryString({ ID: "ExamineTaskId" });
        var Index = $.getQueryString({ ID: "Index" });
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData,
                fields: [
		        { name: 'Id' }, { name: 'PersonFirstIndicatorId' }, { name: 'PersonSecondIndicatorName' }, { name: 'Weight' },
		        { name: "SortIndex" }, { name: "ToolTip" }, { name: "SelfRemark" }, { name: 'CreateId' }, { name: 'CreateName' },
		        { name: 'CreateTime' }, { name: 'IndicatorType' }, { name: 'PersonFirstIndicatorName' },
		        { name: 'FirstWeight' }, { name: 'Score' }, { name: 'FirstIndex' }, { name: 'Summary' }
		        ],
                listeners: { aimbeforeload: function(proxy, options) {
                }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                collapsed: false,
                columns: 4,
                items: [
                { fieldLabel: '姓名', id: 'BeUserNames', schopts: { qryopts: "{ mode: 'Like', field: 'BeUserNames' }"} }
                ]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [
             {
                 text: '添加',
                 iconCls: 'aim-icon-add',
                 handler: function() {
                     opencenterwin("DeptExamineRelationEdit.aspx?op=c", "", 1000, 650);
                 }
             }
                 ]
            });
            //            titPanel = new Ext.ux.AimPanel({
            //                tbar: tlBar,
            //                items: [schBar]
            //            });
            var clnArr = [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'IndicatorType', dataIndex: 'IndicatorType', header: '类型', width: 80 },
					{ id: 'PersonFirstIndicatorName', dataIndex: 'PersonFirstIndicatorName', header: '考核内容', width: 120, renderer: RowRender },
                    { id: 'FirstWeight', dataIndex: 'FirstWeight', header: '考核内容权重', width: 90 },
					{ id: 'PersonSecondIndicatorName', dataIndex: 'PersonSecondIndicatorName', header: '分项量化考核指标', width: 200, renderer: RowRender },
				    { id: 'Weight', dataIndex: 'Weight', header: '权重', width: 50 },
				    { id: 'ToolTip', dataIndex: 'ToolTip', header: '分项指标评分标准', width: 120, renderer: RowRender },
				    { id: 'SelfRemark', dataIndex: 'SelfRemark', header: '自我评价', width: 300, renderer: RowRender,
				        summaryRenderer: function(v, params, data) { return "合计:" }
				    },
                    { id: 'Score', dataIndex: 'Score', header: '打分', width: 80, summaryType: 'sum', summaryRenderer: function(v, params, data) { return Math.round(v * 100) / 100; },
                        editor: { id: 'nf_Score', xtype: 'numberfield', allowBlank: false, minValue: 0, decimalPrecision: 2 }, renderer: RowRender
                    }
					];
            var title = AimState["BaseInfo"].DeptName + '-' + AimState["BaseInfo"].BeUserName + '-' + AimState["BaseInfo"].IndicatorSecondName + '(' + AimState["BaseInfo"].MaxScore + ')-自定义指标评分表';
            var rowArray = [{ header: '<font style="color:blue;font-size:18px"><b>' + title + '</b></font>', colspan: 10, align: 'center'}];
            var colmodel = new Ext.grid.ColumnModel({
                columns: clnArr,
                rows: [rowArray]
            });
            var description = '说明：1 只需对考核类容打分，量化指标、打分标准及自我评价为打分提供参考；2 对每个自定义的考核内容打分系统会自动保存';
            if (store.getCount() > 0 && store.getAt(0).get("Summary")) {
                description += "【<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='DownLoad(\"" + store.getAt(0).get("Summary") + "\")'>查看工作总结</label>】"
            }
            grid = new Ext.ux.grid.AimEditorGridPanel({
                title: description,
                store: store,
                region: 'center',
                viewConfig: { forceFit: true },
                autoExpandColumn: 'PersonFirstIndicatorName',
                plugins: [new Ext.ux.grid.ColumnHeaderGroup(), new Ext.ux.grid.GridSummary()],
                listeners: { beforeedit: function(e) {
                    Ext.getCmp("nf_Score").setMaxValue(parseInt(e.record.get("FirstWeight")));
                }, afteredit: function(e) {
                    if (e.value) {
                        $.ajaxExec("AutoSave", { PersonFirstIndicatorId: e.record.get("PersonFirstIndicatorId"), Score: e.value,
                            ExamineTaskId: ExamineTaskId
                        }, function() {
                            e.record.commit();
                        })
                    }
                }
                },
                cm: colmodel
            });
            var btnPanel = new Ext.form.FormPanel({
                region: 'south',
                frame: true,
                hidden: Index != '0',
                buttonAlign: 'center',
                buttons: [{ text: '提交', handler: function() {
                    grid.stopEditing();
                    var recs = store.getRange();
                    var allow = true;
                    for (var j = 0; j < recs.length; j++) {
                        var colMod = grid.getColumnModel();
                        if (colMod.isCellEditable(9, j)) { //通过是否可以编辑来判断 不可编辑的列不参与提交验证
                            if (!recs[j].get("Score")) {
                                allow = false;
                                break;
                            }
                        }
                    }
                    if (!allow) {
                        AimDlg.show("您还有未输入的打分项，请填写完毕后再提交！");
                        return;
                    }
                    var score = 0.0;
                    $.each(store.getRange(), function() {
                        if (this.get("Score")) {
                            score = parseFloat(score) + parseFloat(this.get("Score"));
                        }
                    })
                    Aim.PopUp.ReturnValue({ Score: Math.round(parseFloat(score) * 100) / 100 });
                }
                }, { text: '取消', handler: function() {
                    window.close();
                } }]
                });
                viewport = new Ext.ux.AimViewport({
                    items: [grid, btnPanel]
                });
                grid.getColumnModel().isCellEditable = function(colIndex, rowIndex) {
                    if (Index != '0') {
                        return false;
                    }
                    var record = store.getAt(rowIndex);
                    if (!record.get("PersonFirstIndicatorId")) {
                        return false;
                    }
                    return Ext.grid.ColumnModel.prototype.isCellEditable.call(this, colIndex, rowIndex);
                }
            }
            function opencenterwin(url, name, iWidth, iHeight) {
                var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
                var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
                window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
            }
            function DownLoad(val) {
                opencenterwin("../CommonPages/File/DownLoad.aspx?id=" + val, "", 120, 120);
            }
            function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
                var rtn = "";
                switch (this.id) {
                    case "Id":
                        if (value) {
                            rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowInfo(\"" + value + "\",\"" + record.get("State") + "\")'>反馈信息</label>";
                        }
                        break;
                    case "ToolTip":
                        if (value) {
                            var val = eval(value);
                            var temp = "";
                            for (var v = 0; v < val.length; v++) {
                                if (val[v].Content || val[v].ScoreRegion) {
                                    temp += (val[v].Content || '') + (val[v].ScoreRegion ? ' :' + val[v].ScoreRegion : '') + ";";
                                }
                                cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + temp + '"';
                                rtn = temp;
                            }
                        }
                        break;
                    case "PersonFirstIndicatorName":
                    case "PersonSecondIndicatorName":
                    case "SelfRemark":
                        if (value) {
                            cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                            rtn = value;
                        }
                        break;
                    case "Score":
                        if (record.get("PersonFirstIndicatorId")) {
                            rtn = '<img src="../images/shared/user_edit.png" />&nbsp; &nbsp;' + (value ? value : "");
                        }
                        else {
                            cellmeta.style = 'background-color: gray';
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
