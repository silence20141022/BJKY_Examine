<%@ Page Title="考核对象选择" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="True" CodeBehind="DeptExamineRelationSelect.aspx.cs" Inherits=" Aim.Examining.Web.DeptConfig.DeptExamineRelationSelect" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <script src="/js/pgfunc-ext-sel.js" type="text/javascript"></script>
    <script type="text/javascript">

        var store, myData;
        var pgBar, schBar, tlBar, titPanel, AimSelGrid, viewport;
        var ExamineType = unescape($.getQueryString({ ID: "ExamineType" }));
        function onSelPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                //idProperty: 'BeUserNames',
                data: myData,
                fields: [
                { name: 'Id' }, { name: 'RelationName' }, { name: 'BeUserIds' }, { name: 'BeUserNames' },
		        { name: 'UpLevelUserIds' }, { name: 'UpLevelUserNames' }, { name: 'UpLevelWeight' },
		        { name: "SameLevelUserIds" }, { name: "SameLevelUserNames" }, { name: "SameLevelWeight" },
	            { name: 'DownLevelUserIds' }, { name: 'DownLevelUserNames' }, { name: 'DownLevelWeight' },
		        { name: 'GroupID' }, { name: 'GroupName' }, { name: 'CreateId' },
		        { name: 'CreateName' }, { name: 'CreateTime'}]
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            var buttonPanel = new Ext.form.FormPanel({
                region: 'south',
                frame: true,
                buttonAlign: 'center',
                buttons: [{ text: '确定', handler: function () {
                    AimGridSelect();
                }
                }, { text: '取消', handler: function () {
                    window.close();
                } 
                }]
            });
            Ext.override(Ext.grid.CheckboxSelectionModel, {
                handleMouseDown: function (g, rowIndex, e) {
                    if (e.button !== 0 || this.isLocked()) {
                        return;
                    }
                    var view = this.grid.getView();
                    if (e.shiftKey && !this.singleSelect && this.last !== false) {
                        var last = this.last;
                        this.selectRange(last, rowIndex, e.ctrlKey);
                        this.last = last; // reset the last     
                        view.focusRow(rowIndex);
                    } else {
                        var isSelected = this.isSelected(rowIndex);
                        if (isSelected) {
                            this.deselectRow(rowIndex);
                        } else if (!isSelected || this.getCount() > 1) {
                            this.selectRow(rowIndex, true);
                            view.focusRow(rowIndex);
                        }
                    }
                }
            });
            AimSelGrid = new Ext.ux.grid.AimGridPanel({
                title: '考核对象及关系',
                store: store,
                region: 'center',
                autoExpandColumn: 'BeUserNames',
                sm: seltype == "single" ? new Ext.grid.RowSelectionModel({ singleSelect: true }) : "",
                columns: [
                        new Ext.ux.grid.AimRowNumberer(),
                        new Ext.ux.grid.AimCheckboxSelectionModel(),
                        { id: 'GroupName', dataIndex: 'GroupName', header: '部门', width: 100 },
                        { id: 'RelationName', dataIndex: 'RelationName', header: '考核关系名称', width: 120, sortable: true, renderer: RowRender },
					    { id: 'BeUserNames', dataIndex: 'BeUserNames', header: '考核对象', width: 200, sortable: true, renderer: RowRender },
					    { id: 'UpLevelUserNames', dataIndex: 'UpLevelUserNames', header: '上级评分人', width: 120, renderer: RowRender },
					    { id: 'UpLevelWeight', dataIndex: 'UpLevelWeight', header: '上级权重', width: 70, sortable: true },
					    { id: 'SameLevelUserNames', dataIndex: 'SameLevelUserNames', header: '同级评分人', width: 120, sortable: true, renderer: RowRender },
				        { id: 'SameLevelWeight', dataIndex: 'SameLevelWeight', header: '同级权重', width: 70, sortable: true },
				        { id: 'DownLevelUserNames', dataIndex: 'DownLevelUserNames', header: '下级评分人', width: 120, sortable: true, renderer: RowRender },
				        { id: 'DownLevelWeight', dataIndex: 'DownLevelWeight', header: '下级权重', width: 70, sortable: true }
                        ]
            });
            viewport = new Ext.ux.AimViewport({
                items: [AimSelGrid, buttonPanel]
            });
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn;
            switch (this.id) {
                case "Name":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
                case "RelationName":
                case "BeUserNames":
                case "UpLevelUserNames":
                case "SameLevelUserNames":
                case "DownLevelUserNames":
                    if (value) {
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
