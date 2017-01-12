<%@ Page Title="考核指标" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="ExamineIndicatorList.aspx.cs" Inherits="Aim.Examine.Web.ExamineConfig.ExamineIndicatorList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=580,height=460,scrollbars=yes");
        var EditPageUrl = "ExamineIndicatorEdit.aspx";
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var cbBeRoleName;
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            // 表格数据
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData,
                fields: [
			        { name: 'Id' }, { name: 'IndicatorName' }, { name: 'BeRoleCode' },
			        { name: 'BeRoleName' }, { name: 'BelongDeptId' }, { name: 'BelongDeptName' },
			        { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime' }
			    ], listeners: { aimbeforeload: function(proxy, options) {
			        options.data = options.data || {};
			    }
			    }
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        ExtOpenGridEditWin(grid, EditPageUrl, "c", EditWinStyle);
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
                        ExtOpenGridEditWin(grid, EditPageUrl, "u", EditWinStyle);
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
                        if (confirm("删除考核指标会连同考核项目、考核要素、考核标准一起删除，确定删除所选记录吗？")) {
                            ExtBatchOperate('batchdelete', recs, null, null, function() {
                                store.remove(recs);
                                recs = store.getRange();
                                if (recs.length > 0) {
                                    frameContent.location.href = "IndicatorFirstList.aspx?IndicatorName=" + escape(recs[0].get("IndicatorName")) + "&ExamineIndicatorId=" + recs[0].get("Id");
                                }
                                else {
                                    frameContent.location.href = "IndicatorFirstList.aspx?IndicatorName=" + "&ExamineIndicatorId=";
                                }
                            });
                        }
                    }
                },
                 '-', {
                     text: '复制',
                     iconCls: 'aim-icon-copy',
                     handler: function() {
                         var recs = grid.getSelectionModel().getSelections();
                         if (!recs || recs.length <= 0) {
                             AimDlg.show("请先选择要复制的记录！");
                             return;
                         }
                         if (confirm("复制考核指标会连同考核项目、考核要素、考核标准一起复制，确定复制所选记录吗？")) {
                             CopyRecord(recs[0].get("Id"));
                         }
                     }
                 }
			]
            });
            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar
            });
            var clnsArr = [
                    { id: 'Id', dataIndex: 'Id', header: '标识', hidden: true },
                    { id: 'BeRoleCode', dataIndex: 'BeRoleCode', header: 'BeRoleCode', hidden: true },
                    { id: 'BelongDeptId', dataIndex: 'BelongDeptId', header: 'BelongDeptId', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'IndicatorName', dataIndex: 'IndicatorName', header: '考核指标', width: 180, renderer: RowRender },
                    { id: 'BeRoleName', dataIndex: 'BeRoleName', header: '考核对象', width: 100, sortable: true, renderer: RowRender },
                    { id: 'BelongDeptName', dataIndex: 'BelongDeptName', header: '所属部门', width: 90, sortable: true, renderer: RowRender }
                    ];
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '考核指标',
                store: store,
                split: true,
                region: 'west',
                width: 450,
                autoExpandColumn: 'IndicatorName',
                columns: clnsArr,
                tbar: tlBar,
                listeners: { rowclick: function() {
                    var recs = grid.getSelectionModel().getSelections();
                    if (recs && recs.length > 0 && recs[0].get("IndicatorName")) {
                        frameContent.location.href = "IndicatorFirstList.aspx?IndicatorName=" + escape(recs[0].get("IndicatorName")) + "&ExamineIndicatorId=" + recs[0].get("Id");
                    }
                }
                }
            });

            // 页面视图
            viewport = new Ext.ux.AimViewport({
                layout: 'border',
                items: [grid, {
                    id: 'frmcon',
                    region: 'center',
                    margins: '-1 0 -2 0',
                    html: '<iframe width="100%" height="100%" id="frameContent" name="frameContent" frameborder="0"></iframe>'}]
                });
                if (store.data.length > 0) {
                    frameContent.location.href = "IndicatorFirstList.aspx?IndicatorName=" + escape(store.getAt(0).get("IndicatorName")) + "&ExamineIndicatorId=" + store.getAt(0).get("Id");
                }
                else {
                    frameContent.location.href = "IndicatorFirstList.aspx?IndicatorName=&ExamineIndicatorId=";
                }
            }
            // 提交数据成功后
            function onExecuted() {
                store.reload();
            }
            function opencenterwin(url, name, iWidth, iHeight) {
                var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
                var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
                window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
            }
            function CopyRecord(val) {
                opencenterwin("ExamineIndicatorEdit.aspx?id=" + val + "&op=copy", "", 580, 460);
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
                    case "IndicatorName":
                    case "BelongDeptName":
                    case "BeRoleName":
                        if (value) {
                            value = value || "";
                            cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
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
