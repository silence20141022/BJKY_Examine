<%@ Page Title="��������" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="ExamineAppealEdit.aspx.cs" Inherits="Aim.Examining.Web.ExamineTaskManage.ExamineAppealEdit" %>

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
        }
        function setPgUI() {
            if (pgOperation == "v") {
                $("input:text,textarea").attr("readonly", "readonly");
            }
            else {
                if (!State) {
                    $("#DealAdvices,#DeptLeaderOpinion,#HrOpinion,#ModifiedScore,").attr("readonly", "readonly");
                    $("#ModifiedLevel").attr("disabled", "disabled");
                    $("#AppealEvent,#AppealReason").css("border", "solid 1 blue");
                    $("#btnDisagree").hide(); //�׻��ڲ���ʾ��ͬ�ⰴť
                    $("#btnAgree").text("�ύ");
                }
                if (parseInt(State) == 1) {
                    $("#AppealEvent,#AppealReason,#DeptLeaderOpinion,#HrOpinion,#ModifiedScore").attr("readonly", "readonly");
                    $("#ModifiedLevel").attr("disabled", "disabled");
                    $("#DealAdvices").css("border", "solid 1 blue");
                    $("#btnAgree").text("����");
                }
                if (parseInt(State) == 2) {
                    $("#AppealEvent,#AppealReason, #DealAdvices,#HrOpinion,#ModifiedScore").attr("readonly", "readonly");
                    $("#ModifiedLevel").attr("disabled", "disabled");
                    $("#DeptLeaderOpinion").css("border", "solid 1 blue");
                    $("#btnDisagree").hide();
                    $("#btnAgree").text("����������Դ��");
                }
                if (parseInt(State) == 3) {
                    $("#AppealEvent,#AppealReason, #DealAdvices,#DeptLeaderOpinion").attr("readonly", "readonly");
                    $("#HrOpinion").css("border", "solid 1 blue");
                    $("#btnDisagree").hide();
                }
            }
            if ($("#ExamineType").val() == "Ժ������") {
                $("#trDeptLeader1,#trDeptLeader2").hide();
            }
            //�󶨰�ť��֤
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
        //��֤�ɹ�ִ�б��淽��
        function SuccessSubmit() {
            if (parseInt(State) == 3) {
                if ($("#ModifiedScore").val() == '' || $("#ModifiedLevel").val() == '') {
                    AimDlg.show("����������ߣ������߽���еķ����͵ȼ�Ϊ�����");
                    return;
                }
            }
            AimFrm.submit(pgAction, { Action: "Agree" }, null, SubFinish);
        }
        function SubFinish(args) {
            RefreshClose();
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            ��Ч�������ߵ�</h1>
    </div>
    <fieldset>
        <legend>��������</legend>
        <div id="editDiv" align="center">
            <table class="aim-ui-table-edit" width="100%" style="border: 0px">
                <tr style="display: none">
                    <td>
                        <input id="Id" name="Id" />
                        <input id="ExamYearResultId" name="ExamYearResultId" />
                        <input id="ExamineStageId" name="ExamineStageId" />
                        <input id="State" name="State" />
                        <input id="ExamineType" name="ExamineType" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption" style="width: 17%">
                        ������
                    </td>
                    <td class="aim-ui-td-data" style="width: 17%">
                        <input id="AppealUserId" name="AppealUserId" type="hidden" />
                        <input id="AppealUserName" name="AppealUserName" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 17%">
                        ��λ�����ţ�
                    </td>
                    <td class="aim-ui-td-data" style="width: 16%">
                        <input id="DeptId" name="DeptId" type="hidden" />
                        <input id="DeptName" name="DeptName" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 17%">
                        ְ��
                    </td>
                    <td style="width: 16%">
                        <input id="BeRoleCode" name="BeRoleCode" type="hidden" />
                        <input id="BeRoleName" name="BeRoleName" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        ��������
                    </td>
                    <td>
                        <input id="AppealTime" name="AppealTime" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption">
                        �����ۺϵ÷�
                    </td>
                    <td>
                        <input id="OriginalScore" name="OriginalScore" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption">
                        �����ȼ�
                    </td>
                    <td>
                        <input id="OriginalLevel" name="OriginalLevel" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        �����¼�
                    </td>
                    <td class="aim-ui-td-data" colspan="7">
                        <textarea id="AppealEvent" name="AppealEvent" style="width: 100%;" rows="3" cols=""></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        ��������
                    </td>
                    <td class="aim-ui-td-data" colspan="5">
                        <textarea id="AppealReason" name="AppealReason" style="width: 100%;" rows="3"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        ���ߴ������
                    </td>
                    <td class="aim-ui-td-data" colspan="5">
                        <textarea id="DealAdvices" name="DealAdvices" style="width: 100%;" rows="3" cols=""></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        ������Դ��������
                    </td>
                    <td>
                        <input id="AcceptUserId" name="AcceptUserId" type="hidden" />
                        <input id="AcceptUserName" name="AcceptUserName" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption">
                        ����
                    </td>
                    <td>
                        <input id="AcceptSubmitTime" name="AcceptSubmitTime" readonly="readonly" />
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr id="trDeptLeader1">
                    <td class="aim-ui-td-caption">
                        ���Ÿ��������
                    </td>
                    <td class="aim-ui-td-data" colspan="5">
                        <textarea id="DeptLeaderOpinion" name="DeptLeaderOpinion" style="width: 100%;" rows="3"
                            cols=""></textarea>
                    </td>
                </tr>
                <tr id="trDeptLeader2">
                    <td class="aim-ui-td-caption">
                        ���Ÿ�����
                    </td>
                    <td>
                        <input id="DeptLeaderId" name="DeptLeaderId" type="hidden" />
                        <input id="DeptLeaderName" name="DeptLeaderName" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption">
                        ����
                    </td>
                    <td>
                        <input id="DeptLeaderSubmitTime" name="DeptLeaderSubmitTime" readonly="readonly" />
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        ������Դ�����������
                    </td>
                    <td class="aim-ui-td-data" colspan="5">
                        <textarea id="HrOpinion" name="HrOpinion" style="width: 100%;" rows="3"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        ��������ۺϵ÷�
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="ModifiedScore" name="ModifiedScore" />
                    </td>
                    <td class="aim-ui-td-caption">
                        ������Ŀ����ȼ�
                    </td>
                    <td class="aim-ui-td-data">
                        <select id="ModifiedLevel" name="ModifiedLevel" aimctrl='select' enum='AimState["ExamineLevel"]'
                            style="width: 153px">
                        </select>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        ������Դ��������
                    </td>
                    <td>
                        <input id="HrUserId" name="HrUserId" type="hidden" />
                        <input id="HrUserName" name="HrUserName" readonly="readonly" />
                    </td>
                    <td class="aim-ui-td-caption">
                        �ύ����
                    </td>
                    <td>
                        <input id="HrSubmitTime" name="HrSubmitTime" readonly="readonly" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-button-panel" colspan="8">
                        <a id="btnAgree" class="aim-ui-button submit">ȷ��</a> <a id="btnDisagree" class="aim-ui-button submit">
                            ���</a> <a id="btnSave" class="aim-ui-button submit">�ݴ�</a> <a id="btnCancel" class="aim-ui-button cancel">
                                ȡ��</a>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
</asp:Content>
