<%@ Page Title="被考核人员" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="BeUserList.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.BeUserList" %>

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
        var myData, store, grid;
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
			       { name: 'Id' }, { name: 'UserID' }, { name: 'UserName' }, { name: 'DeptName'}]

            });
            var clnsArr = [
                    { id: 'UserID', dataIndex: 'UserID', header: '标识', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'UserName', dataIndex: 'UserName', header: '被考核人', width: 100, sortable: true },
            // { id: 'RoleName', dataIndex: 'RoleName', header: '角色名称', width: 100, sortable: true },
                    {id: 'DeptName', dataIndex: 'DeptName', header: '所属部门', width: 150, sortable: true }
                    ];
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '被考核人员',
                store: store,
                region: 'center',
                autoExpandColumn: 'DeptName',
                columns: clnsArr
            });
            // 页面视图
            viewport = new Ext.ux.AimViewport({
                layout: 'border',
                items: [grid]
            })
        }
             
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
