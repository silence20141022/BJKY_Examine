<%@ Page Title="系统配置" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="SysConfigEdit.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.SysConfigEdit" %>

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
        var ValidEnum = { 否: '否', 是: '是' };
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            FormValidationBind('btnSubmit', SuccessSubmit);
            $("#btnSubmit").show();
        }
        //验证成功执行保存方法
        function SuccessSubmit() {
            if ($("#Id").val()) {
                AimFrm.submit("update", { UserBalanceValid: $("#UserBalanceValid").val() }, null, function() { document.location.reload(); });
            }
            else {
                AimFrm.submit("create", { UserBalanceValid: $("#UserBalanceValid").val() }, null, function() { document.location.reload(); });
            }
        }
        
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            考核参数配置</h1>
    </div>
    <fieldset>
        <legend>部门级考核阶段权重——部门员工</legend>
        <div id="editDiv" align="center">
            <table class="aim-ui-table-edit" width="100%" style="border: none">
                <tr style="display: none">
                    <td>
                        <input id="Id" name="Id" value="" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        季度考核权重
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input name="ClerkQuarterWeight" id="ClerkQuarterWeight" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        年度考核权重
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input id="ClerkYearWeight" name="ClerkYearWeight" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend>部门级考核阶段权重——部门副职</legend>
        <div id="Div1" align="center">
            <table class="aim-ui-table-edit" width="100%" style="border: none">
                <tr>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        半年度考核权重
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input name="DeptSecondLeaderQuarterWeight" id="DeptSecondLeaderQuarterWeight" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        年度考核权重
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input id="DeptSecondLeaderYearWeight" name="DeptSecondLeaderYearWeight" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend>院级考核等级比例</legend>
        <div id="Div2" align="center">
            <table class="aim-ui-table-edit" width="100%" style="border: none">
                <tr>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        优秀所占比例
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input name="FirstLeaderExcellentPercent" id="FirstLeaderExcellentPercent" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        良好所占比例
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input id="FirstLeaderGoodPercent" name="FirstLeaderGoodPercent" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend>部门级考核等级比例</legend>
        <div id="Div3" align="center">
            <table class="aim-ui-table-edit" width="100%" style="border: none">
                <tr>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        优秀所占比例
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input name="DeptExcellentPercent" id="DeptExcellentPercent" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        良好所占比例
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input id="DeptGoodPercent" name="DeptGoodPercent" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset>
        <legend>其他参数</legend>
        <div id="Div4" align="center">
            <table class="aim-ui-table-edit" width="100%" style="border: none">
                <tr>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        申诉期限(天)
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input name="AppealDays" id="AppealDays" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        考核关系人员权重开启
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <select id="UserBalanceValid" name="UserBalanceValid" aimctrl='select' enum='ValidEnum'
                            style="width: 90%">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        考核任务短信提醒开启
                    </td>
                    <td class="aim-ui-td-data">
                        <select id="MessageValid" name="MessageValid" aimctrl='select' enum='ValidEnum' style="width: 90%">
                        </select>
                    </td>
                    <td class="aim-ui-td-caption">
                        部门级等级比例限制
                    </td>
                    <td class="aim-ui-td-data">
                        <select id="DeptLevelLimit" name="DeptLevelLimit" aimctrl='select' enum='ValidEnum'
                            style="width: 90%">
                        </select>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <table class="aim-ui-table-edit">
        <tr>
            <td>
                说明：季度考核权重和年度考核权重参与年终考核结果计算。如若不指定，系统默认各阶段平均
            </td>
        </tr>
        <tr>
            <td class="aim-ui-button-panel">
                <a id="btnSubmit" class="aim-ui-button submit">保存</a>
            </td>
        </tr>
    </table>
</asp:Content>
