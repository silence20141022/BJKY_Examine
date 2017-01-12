<%@ Page Title="绩效考核结果反馈及改进,发展计划表" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="FeedbackEdit.aspx.cs" Inherits="Aim.Examining.Web.ExamineTaskManage.FeedbackEdit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        body
        {
            background-color: #F2F2F2;
        }
        .aim-ui-td-data .quarter
        {
            border: none;
            size: 20;
            width: 50;
            background: E9F0FC;
            border-bottom: 1px #000000 solid;
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
    </style>

    <script type="text/javascript">
        var State = $.getQueryString({ ID: "State" });
        function onPgLoad() {
            setPgUI();
            $("#legend").text("【" + $("#Year").val() + "】绩效考核结果反馈及改进,发展计划表");
        }
        function setPgUI() {
            if (pgOperation == "v") {
                $("input:text,textarea").attr("readonly", "readonly");
            }
            if (!State) {
                $("#btnDisagree").hide(); //首环节不显示不同意按钮
                $("#btnAgree").text("提交");
            }
            if (State == 1) {
                $("input:text,textarea").attr("readonly", "readonly");
                $("#btnSave").hide();
            }
            //绑定按钮验证
            FormValidationBind('btnAgree', SuccessSubmit);
            $("#btnDisagree").click(function() {
                AimFrm.submit(pgAction, { Action: "Disagree" }, null, SubFinish);
            });
            $("#btnSave").click(function() {
                AimFrm.submit(pgAction, { Action: "TempSave" }, null, SubFinish);
            });
            $("#btnCancel").click(function() {
                window.close();
            });
        }
        //验证成功执行保存方法
        function SuccessSubmit() {
            if ($("#DirectLeaderIds").val()) {
                AimFrm.submit(pgAction, { Action: "Submit" }, null, SubFinish);
            }
            else {
                AimDlg.show("首次提交必须指定反馈面谈的直接上级！");
                return;
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
            绩效考核结果</h1>
    </div>
    <div id="editDiv" align="center">
        <fieldset>
            <legend id="legend"></legend>
            <table class="aim-ui-table-edit" width="100%" style="border: none">
                <tr style="display: none">
                    <td>
                        <input id="Id" name="Id" />
                        <input id="Year" name="Year" />
                        <input id="ExamYearResultId" name="ExamYearResultId" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        被考评人
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input id="UserId" name="UserId" type="hidden" />
                        <input id="UserName" name="UserName" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        部 门
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input id="DeptId" name="DeptId" type="hidden" />
                        <input id="DeptName" name="DeptName" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        直接上级(具体面谈的)
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="DirectLeaderIds" name="DirectLeaderIds" type="hidden" />
                        <input id="DirectLeaderNames" name="DirectLeaderNames" aimctrl="user" style="width: 155px"
                            relateid="DirectLeaderIds" />
                    </td>
                    <td class="aim-ui-td-caption">
                        职务/岗位
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="BeRoleCode" name="BeRoleCode" type="hidden" />
                        <input id="BeRoleName" name="BeRoleName" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        考评等级
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="ExamineGrade" name="ExamineGrade" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption">
                        年度综合得分
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="IntegrationScore" name="IntegrationScore" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <%--<td class="aim-ui-td-caption">
                        考核年度
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="Year" name="Year" readonly="readonly" />
                    </td>--%>
                    <td class="aim-ui-td-caption">
                        一季度得分
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="FirstQuarterScore" name="FirstQuarterScore" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption">
                        二季度得分
                    </td>
                    <td>
                        <input id="SecondQuarterScore" name="SecondQuarterScore" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        三季度
                    </td>
                    <td>
                        <input name="ThirdQuarterScore" id="ThirdQuarterScore" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption">
                        年度考核得分
                    </td>
                    <td>
                        <input id="YearScore" name="YearScore" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        被考核人优点及突出成绩
                    </td>
                    <td class="aim-ui-td-data" colspan="3">
                        <textarea id="Advantage" name="Advantage" style="width: 91%;" rows="4" cols=""></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        被考核人缺点及存在问题
                    </td>
                    <td class="aim-ui-td-data" colspan="3">
                        <textarea id="Shortcoming" name="Shortcoming" style="width: 91%;" rows="4" cols=""></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        被考评人发展计划及改进措施
                    </td>
                    <td class="aim-ui-td-data" colspan="3">
                        <textarea id="PlanAndMethod" name="PlanAndMethod" style="width: 91%;" rows="4" cols=""></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        被考核人提交时间
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="FeedbackTime" name="FeedbackTime" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption">
                        直接上级提交时间
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="DirectLeaderSignDate" name="DirectLeaderSignDate" readonly="readonly" />
                    </td>
                </tr>
                <%-- <tr width="100%">
                    <td class="aim-ui-td-caption">
                        直接上级(签字)
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="DirectLeaderSignName" name="DirectLeaderSignName" style="border: none;
                            size: 20; width: 90%; background: E9F0FC; border-bottom: 1px #000000 solid;" />
                    </td>
                    <td class="aim-ui-td-caption">
                        签字日期
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="DirectLeaderSignDate" name="DirectLeaderSignDate" style="border: none;
                            size: 20; width: 92%; background: E9F0FC; border-bottom: 1px #000000 solid;" />
                    </td>
                </tr>--%>
                <%--<tr width="100%">
                    <td class="aim-ui-td-caption">
                        部门领导(签字)
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="DeptLeaderSignName" name="DeptLeaderSignName" style="border: none; size: 20;
                            width: 90%; background: E9F0FC; border-bottom: 1px #000000 solid;" />
                    </td>
                    <td class="aim-ui-td-caption">
                        签字日期
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="DeptLeaderSignDate" name="DeptLeaderSignDate" style="border: none; size: 20;
                            width: 92%; background: E9F0FC; border-bottom: 1px #000000 solid;" />
                    </td>
                </tr>--%>
                <tr>
                    <td class="aim-ui-button-panel" colspan="8">
                        <a id="btnAgree" class="aim-ui-button submit">同意</a> <a id="btnDisagree" class="aim-ui-button submit">
                            不同意</a> <a id="btnSave" class="aim-ui-button submit">暂存</a> <a id="btnCancel" class="aim-ui-button cancel">
                                取消</a>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
