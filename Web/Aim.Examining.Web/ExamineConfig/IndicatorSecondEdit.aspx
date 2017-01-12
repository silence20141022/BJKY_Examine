<%@ Page Title="考核要素" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="IndicatorSecondEdit.aspx.cs" Inherits="Aim.Examining.Web.IndicatorSecondEdit" %>

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
        var index = $.getQueryString({ ID: "Index" });
        var IndicatorFirstId = $.getQueryString({ ID: 'IndicatorFirstId' });
        var ExamineIndicatorId = $.getQueryString({ ID: 'ExamineIndicatorId' });
        var IndicatorFirstName = unescape($.getQueryString({ ID: "IndicatorFirstName" }));
        var store, grid;
        function onPgLoad() {
            setPgUI();
            renderGrid();
        }
        function setPgUI() {
            if (pgOperation == "c" || pgOperation == "cs") {
                $("#IndicatorFirstId").val(IndicatorFirstId);
                $("#IndicatorFirstName").val(IndicatorFirstName);
            }
            //绑定按钮验证
            FormValidationBind('btnSubmit', SuccessSubmit);

            $("#btnCancel").click(function() {
                window.close();
            });
        }
        function ItemChange() {
            var checkText = $("#IndicatorFirstId").find("option:selected").text();  //获取Select选择的Text
            //  var checkValue = $("#IndicatorFirstId").val();  //获取Select选择的Value
            $("#IndicatorFirstName").val(checkText);
        }
        /*渲染grid -指标评分选项说明*/
        function renderGrid() {
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                isclient: true,
                data: { records: AimState["DataList"] || [] },
                fields: [
                   	    { name: 'Id' }, { name: 'IndicatorSecondId' }, { name: 'IndicatorThirdName' }, { name: 'MaxScore' }, { name: 'SortIndex'}]
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
                title: '<div align=left>考核标准描述</div>',
                store: store,
                renderTo: 'SubContent',
                autoHeight: true,
                autoExpandColumn: 'IndicatorThirdName',
                columns: [
                    { id: 'Id', dataIndex: 'Id', hidden: true },
                    { id: 'IndicatorSecondId', dataIndex: 'IndicatorSecondId', hidden: true },
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'IndicatorThirdName', dataIndex: 'IndicatorThirdName', header: '考核标准描述', editor: { xtype: 'textarea' }, width: 100 },
					{ id: 'MaxScore', dataIndex: 'MaxScore', header: '分值范围', editor: { xtype: 'textfield', allowBlank: false }, width: 80 },
				    { id: 'SortIndex', dataIndex: 'SortIndex', header: '索引', editor: { xtype: 'numberfield', minValue: 0, maxValue: 100, allowBlank: false }, width: 80 }
					],
                tbar: tlBar
            });
        }

        var contTip = function(vals, p, rec) { //tooptip
            if (vals == null || vals == "")
                return;
            p.attr = 'ext:qtitle=""' + 'ext:qtip="' + vals + '"';
            return vals;
        };
        //验证成功执行保存方法
        function SuccessSubmit() {

            var recs = store.getRange();
            var dt = store.getModifiedDataStringArr(recs);
            AimFrm.submit(pgAction, { data: dt }, null, SubFinish);
        }

        function SubFinish(args) {
            RefreshClose();
        }
        window.onresize = function() {
            grid.setWidth(0);
            grid.setWidth(Ext.get("SubContent").getWidth());
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            考核要素及标准</h1>
    </div>
    <fieldset>
        <legend>基本信息</legend>
        <div id="editDiv" align="center">
            <table class="aim-ui-table-edit" width="100%" style="border: none">
                <tr style="display: none">
                    <td>
                        <input id="Id" name="Id" value="" />
                        <input type="hidden" value="" name="ExamineIndicatorId" id="ExamineIndicatorId" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        所属考核项目
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <select id="IndicatorFirstId" name="IndicatorFirstId" aimctrl='select' enum='IndictorFirstEnum'
                            class="validate[required]" style="width: 90%" onchange="ItemChange()">
                        </select>
                        <input type="hidden" name="IndicatorFirstName" id="IndicatorFirstName" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        索引顺序
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input id="SortIndex" name="SortIndex" class="validate[required custom[onlyInteger]]" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        权重
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="MaxScore" name="MaxScore" class="validate[required custom[onlyInteger] ]" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        考核要素
                    </td>
                    <td class="aim-ui-td-data" colspan="3">
                        <input id="IndicatorSecondName" name="IndicatorSecondName" style="width: 96.7%" class="validate[required]" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <div id="SubContent" name="SubContent" style="width: 100%">
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
