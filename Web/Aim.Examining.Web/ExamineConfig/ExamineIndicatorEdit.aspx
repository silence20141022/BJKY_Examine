<%@ Page Title="考核指标" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="ExamineIndicatorEdit.aspx.cs" Inherits="Aim.Examining.Web.ExamineIndicatorEdit" %>

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
    </style>

    <script type="text/javascript">

        function onPgLoad() {
            setPgUI();
            if (pgOperation == "copy") {
                $("#btnSubmit").show();
            }
        }
        function setPgUI() {
            FormValidationBind('btnSubmit', SuccessSubmit);
            $("#btnCancel").click(function() {
                window.close();
            });
        }
        //验证成功执行保存方法
        function SuccessSubmit() {
            var checkText = $("#BelongDeptId").find("option:selected").text();  //获取Select选择的Text
            $("#BelongDeptName").val(checkText);
            //  var checkValue = $("#select_id").val();  //获取Select选择的Value
            if (pgOperation != "copy") {
                AimFrm.submit(pgAction, {}, null, SubFinish);
            }
            else {
                AimFrm.submit("copy", { id: $("#Id").val() }, null, SubFinish);
            }
        }
        function SubFinish(args) {
            RefreshClose();
        }        
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            考核指标</h1>
    </div>
    <div id="editDiv" align="center">
        <table class="aim-ui-table-edit">
            <tr style="display: none">
                <td>
                    <input id="Id" name="Id" />
                </td>
            </tr>
            <tr>
                <td class="aim-ui-td-caption">
                    指标名称
                </td>
                <td class="aim-ui-td-data" colspan="3">
                    <input id="IndicatorName" style="width: 100%" name="IndicatorName" class="validate[required]" />
                </td>
            </tr>
            <tr>
                <td class="aim-ui-td-caption" style="width: 25%">
                    考核对象
                </td>
                <td class="aim-ui-td-data" style="width: 25%">
                    <select id="BeRoleCode" name="BeRoleCode" aimctrl='select' enum="AimState['BeRoleEnum']"
                        class="validate[required]" style="width: 150px">
                    </select>
                    <input type="hidden" id="BeRoleName" name="BeRoleName" />
                </td>
                <td class="aim-ui-td-caption" style="width: 25%">
                    所属部门
                </td>
                <td class="aim-ui-td-data" style="width: 25%">
                    <select id="BelongDeptId" name="BelongDeptId" aimctrl='select' class="validate[required]"
                        enum="AimState['enumDept']" style="width: 130px">
                    </select>
                    <input id="BelongDeptName" name="BelongDeptName" type="hidden" />
                </td>
            </tr>
            <tr>
                <td class="aim-ui-td-caption">
                    备注
                </td>
                <td class="aim-ui-td-data" colspan="3">
                    <textarea id="Remark" name="Remark" style="width: 100%;" rows="3" cols=""></textarea>
                </td>
            </tr>
            <tr>
                <td class="aim-ui-button-panel" colspan="4">
                    <a id="btnSubmit" class="aim-ui-button submit">保存</a> <a id="btnCancel" class="aim-ui-button cancel">
                        取消</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
