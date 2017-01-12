<%@ Page Title="阶段考核结果填报" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="QuarterResultWrite.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.QuarterResultWrite" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var ExamineStageId = $.getQueryString({ ID: "ExamineStageId" });
        var ExamineStageName = unescape($.getQueryString({ ID: "ExamineStageName" }));
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            // 表格数据
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'UserId',
                data: myData,
                fields:
                [{ name: 'UserId' }, { name: 'UserName' }, { name: 'DeptId' }, { name: 'DeptName' }, { name: 'Year' },
                { name: 'FirstScore' }, { name: 'SecondScore' }, { name: 'ThirdScore' }
			    ],
                listeners: {
                    aimbeforeload: function(proxy, options) {
                        options.data = options.data || {};
                    }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });

            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 3,
                collapsed: false,
                items: [
                { fieldLabel: '姓名', id: 'UserName', schopts: { qryopts: "{ mode: 'Like', field: 'UserName' }"} },
                { fieldLabel: '被考部门', id: 'DeptName', schopts: { qryopts: "{ mode: 'Like', field: 'DeptName' }"} }
                ]
            });
            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                //    tbar: tlBar,
                items: [schBar]
            });
            grid = new Ext.ux.grid.AimEditorGridPanel({
                title: '2013年' + (AimState["GroupName"] ? AimState["GroupName"] : "") + "季度考核结果填报",
                store: store,
                region: 'center',
                columnLines: true,
                viewConfig: { forceFit: true },
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.ux.grid.AimCheckboxSelectionModel(),
                { id: 'UserName', dataIndex: 'UserName', header: '姓名', width: 80, sortable: true },
                { id: 'DeptName', dataIndex: 'DeptName', header: '部门', width: 130, sortable: true },
				{ id: 'Year', dataIndex: 'Year', header: '年度', width: 100 },
				{ id: 'FirstScore', dataIndex: 'FirstScore', header: '<font color="red">一季度评分</font>', width: 100, renderer: RowRender,
				    editor: { xtype: 'numberfield', allowBlank: false, minValue: 0, maxValue: 100, decimalPrecision: 2 }
				},
				{ id: 'SecondScore', dataIndex: 'SecondScore', header: '<font color="red">二季度评分</font>', width: 100, renderer: RowRender,
				    editor: { xtype: 'numberfield', allowBlank: false, minValue: 0, maxValue: 100, decimalPrecision: 2 }
				},
				{ id: 'ThirdScore', dataIndex: 'ThirdScore', header: '<font color="red">三季度评分</font>', width: 100, renderer: RowRender,
				    editor: { xtype: 'numberfield', allowBlank: false, minValue: 0, maxValue: 100, decimalPrecision: 2 }
				}
			    ],
                // tbar: titPanel,
                bbar: pgBar,
                listeners: { afteredit: function(e) {
                    var dt = store.getModifiedDataStringArr([e.record]) || [];
                    $.ajaxExec("AutoSave", { data: dt }, function(rtn) { e.record.commit(); })
                }
                }
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowTaskDetail(val) {
            opencenterwin("../ExamineTaskManage/ExamineEvaluation.aspx?id=" + val, "", 1200, 650);
        }
        function onExecuted() {
            store.reload();
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn;
            switch (this.id) {
                case "Id":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowTaskDetail(\"" + value + " \")'>查看详细</label>";
                    }
                    break;
                case "BeRoleName":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
                case "FirstScore":
                case "SecondScore":
                case "ThirdScore":
                    if (value) {
                        rtn = value;
                    }
                    break;
            }
            return rtn;
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
