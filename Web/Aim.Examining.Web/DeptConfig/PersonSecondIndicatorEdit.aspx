<%@ Page Title="量化指标" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="PersonSecondIndicatorEdit.aspx.cs" Inherits="Aim.Examining.Web.DeptConfig.PersonSecondIndicatorEdit" %>

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
        var sortIndex = $.getQueryString({ ID: 'sortIndex' });
        var PersonFirstIndicatorId = $.getQueryString({ ID: 'PersonFirstIndicatorId' });
        var PersonFirstIndicatorName = unescape($.getQueryString({ ID: 'PersonFirstIndicatorName' }));
        var act = $.getQueryString({ ID: 'act' });
        var maxScore = $.getQueryString({ ID: 'maxScore' });
        function onPgLoad() {
            setPgUI();
            initGrid();
        }
        function setPgUI() {
            sortIndex && $("#SortIndex").val(sortIndex);
            PersonFirstIndicatorId && $("#PersonFirstIndicatorId").val(PersonFirstIndicatorId);
            PersonFirstIndicatorName && $("#PersonFirstIndicatorName").val(PersonFirstIndicatorName);
            (act == "show") && $("#btnSubmit").attr("disabled", true);
            maxScore > 0 ? $("#Weight").addClass("validate[" + "range[0," + maxScore + "] ]") : $("#Weight").val('0').attr('readonly', true);

            //绑定按钮验证
            FormValidationBind('btnSubmit', SuccessSubmit);

            $("#btnCancel").click(function() {
                window.close();
            });

        }

        function initGrid() {
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                isclient: true,
                data: { records: eval(AimState["DataList"]) || [] },
                fields: [{ name: 'Content' }, { name: 'ScoreRegion' }, { name: 'SortIndex'}]
            });
            //工具栏
            tlBar = new Ext.ux.AimToolbar({
                items: [
				{
				    text: '添加',
				    iconCls: 'aim-icon-add',
				    handler: function() {
				        var recType = store.recordType;
				        var maxIndex = store.data.length + 1;
				        var rec = new recType({ SortIndex: maxIndex });
				        store.insert(store.data.length, rec);
				    }
				}, '-',
				{
				    text: '删除',
				    iconCls: 'aim-icon-delete',
				    handler: function() {
				        var recs = grid.getSelectionModel().getSelections();
				        if (!recs || recs.length <= 0) {
				            AimDlg.show("请先选择要删除的记录！");
				            return;
				        }
				        if (confirm("确定删除所选记录？")) {
				            store.remove(recs);
				        }
				    }
				}, '->'
		    ]
            });
            grid = new Ext.ux.grid.AimEditorGridPanel({
                title: '<div align=left>评分标准描述</div>',
                store: store,
                renderTo: 'SubContent',
                autoHeight: true,
                autoExpandColumn: 'Content',
                columns: [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'Content', dataIndex: 'Content', header: '评分标准描述', editor: { xtype: 'textarea' }, width: 100 },
					{ id: 'ScoreRegion', dataIndex: 'ScoreRegion', header: '分值范围', editor: { xtype: 'textfield', allowBlank: false }, width: 100 },
				    { id: 'SortIndex', dataIndex: 'SortIndex', header: '索引', editor: { xtype: 'numberfield', minValue: 0, maxValue: 100, allowBlank: false }, width: 80 }
					],
                tbar: tlBar
            });
        }
        //验证成功执行保存方法
        function SuccessSubmit() {
            var recs = store.getRange();
            recs = store.getModifiedDataStringArr(recs);
            AimFrm.submit(pgAction, { data: recs }, null, SubFinish);
        }
        function SubFinish(args) {
            RefreshClose();
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            量化指标</h1>
    </div>
    <div id="editDiv" align="center">
        <table class="aim-ui-table-edit" width="100%">
            <tr style="display: none">
                <td>
                    <input id="Id" name="Id" />
                    <input id="PersonFirstIndicatorId" name="PersonFirstIndicatorId" type="hidden" />
                    <input id="PersonFirstIndicatorName" name="PersonFirstIndicatorName" type="hidden" />
                </td>
            </tr>
            <tr>
                <td class="aim-ui-td-caption">
                    分项量化指标
                </td>
                <td class="aim-ui-td-data" colspan="3">
                    <input id="PersonSecondIndicatorName" name="PersonSecondIndicatorName" class="validate[required]"
                        style="width: 97%" />
                </td>
            </tr>
            <tr>
                <td class="aim-ui-td-caption" style="width: 25%">
                    权重
                </td>
                <td class="aim-ui-td-data" style="width: 25%">
                    <input id="Weight" name="Weight" class="validate[required custom[onlyNumber]]" />
                </td>
                <td class="aim-ui-td-caption" style="width: 25%">
                    索引顺序
                </td>
                <td class="aim-ui-td-data" style="width: 25%">
                    <input id="SortIndex" name="SortIndex" />
                </td>
            </tr>
        </table>
    </div>
    <div id="SubContent" style="width: 100%;">
    </div>
    <table class="aim-ui-table-edit">
        <tr>
            <td class="aim-ui-button-panel" colspan="8">
                <a id="btnSubmit" class="aim-ui-button submit">保存</a> <a id="btnCancel" class="aim-ui-button cancel">
                    取消</a>
            </td>
        </tr>
    </table>
</asp:Content>
