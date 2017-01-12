<%@ Page Title="部门考核向导" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="DeptExamineWizzard.aspx.cs" Inherits="Aim.Examining.Web.DeptExamineWizzard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <link href="/js/smart_wizard.css" rel="stylesheet" type="text/css" />

    <script src="/js/jquery.smartWizard.js" type="text/javascript"></script>

    <style type="text/css">
        .swMain .stepContainer div.content
        {
            clear: both;
            height: 400px;
        }
    </style>

    <script type="text/javascript">
        var id = $.getQueryString({ ID: 'id' });
        var result = true;
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            initWizard();
        }
        function initWizard() {
            $('#wizard').smartWizard({
                labelNext: '下一步',
                labelPrevious: '上一步',
                labelFinish: '完成',
                onLeaveStep: onLeaveStep,
                onShowStep: showstepCallback,
                onFinish: onFinish
                //enableFinishButton: true
                // enableAllSteps: true  允许点击所有步骤
            });
        }
        function leaveAStepCallback(obj, context) {
            alert("Leaving step " + context.fromStep + " to go to step " + context.toStep);
            return true; // return false to stay on step and true to continue navigation 
        }
        //离开
        function onLeaveStep(obj, context) {
            if (context.toStep > context.fromStep) {
                var step_num = obj.attr('rel');
                switch (step_num) {
                    case "1":
                        result = stepOne.SuccessSubmit();
                        if (result) {
                            id = result;
                        }
                        break;
                    case "2":
                        result = stepTwo.SuccessSubmit();
                        if (result) {
                            id = result;
                        }
                        break;
                    case "3":
                        result = stepThree.SuccessSubmit();
                        if (result) {
                            id = result;
                        }
                        break;
                    case "4":
                        result = stepFour.SuccessSubmit();
                        if (result) {
                            id = result;
                        }
                        break;
                    case "5":
                        result = stepFive.SuccessSubmit();
                        if (result) {
                            id = result;
                        }
                        break;
                    case "6":
                        result = stepSix.SuccessSubmit();
                        if (result) {
                            id = result;
                        }
                        break;
                    default:
                        resul = true;
                        break;
                }
                return result;
            }
            else {
                return true;
            }
        }

        //完成结束
        function onFinish() {
            result = stepSeven.SuccessSubmit();
            if (!result) {
                return;
            }
            window.location.href = "../ExamineConfig/ExamineStageList.aspx";
            //            });
        }

        //保存成功后的事件
        function afterSave(StepNum, e) {

        }
        //显示该步
        function showstepCallback(obj) {
            var step_num = obj.attr('rel');  //操作符 
            if (step_num == "1") {
                //   if (stepOne.location.href.indexOf("DeptExamineStep1.aspx?step=1") > -1) return;
                stepOne.location.href = "DeptExamineStep1.aspx?step=1&id=" + id;
            }
            else if (step_num == "2") {
                // if (stepTwo.location.href.indexOf("DeptExamineStep2.aspx?step=2") > -1) return;
                stepTwo.location.href = "DeptExamineStep2.aspx?step=2&id=" + id;
            }
            else if (step_num == "3") {
                //if (stepThree.location.href.indexOf("DeptExamineStep3.aspx?step=3") > -1) return;
                stepThree.location.href = "DeptExamineStep3.aspx?step=3&id=" + id;
            }
            else if (step_num == "4") {
                //if (stepFour.location.href.indexOf("DeptExamineStep4.aspx?step=4") > -1) return;
                stepFour.location.href = "DeptExamineStep4.aspx?step=4&id=" + id;
            }
            else if (step_num == "5") {
                //  if (stepFive.location.href.indexOf("DeptExamineStep5.aspx?step=5") > -1) return;
                stepFive.location.href = "DeptExamineStep5.aspx?step=5&id=" + id;
            }
            else if (step_num == "6") {
                // if (stepSix.location.href.indexOf("DeptExamineStep6.aspx?step=6") > -1) return;
                stepSix.location.href = "DeptExamineStep6.aspx?step=6&id=" + id;
            }
            else if (step_num == "7") {
                // if (stepSeven.location.href.indexOf("DeptExamineStep7.aspx?step=7") > -1) return;
                stepSeven.location.href = "DeptExamineStep7.aspx?step=7&id=" + id;
            }
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        } 
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="wizard" class="swMain">
        <ul>
            <li><a href="#step-1">
                <label class="stepNumber">
                    1</label>
                <span class="stepDesc">基本信息 </span></a></li>
            <li><a href="#step-2">
                <label class="stepNumber">
                    2</label>
                <span class="stepDesc">考核关系</span></a></li>
            <li><a href="#step-3">
                <label class="stepNumber">
                    3</label>
                <span class="stepDesc">考核指标</span></a></li>
            <li><a href="#step-4">
                <label class="stepNumber">
                    4</label>
                <span class="stepDesc">生成任务</span></a></li>
            <li><a href="#step-5">
                <label class="stepNumber">
                    5</label>
                <span class="stepDesc">绩效填报</span></a></li>
            <li><a href="#step-6">
                <label class="stepNumber">
                    6</label>
                <span class="stepDesc">启动考核</span></a></li>
            <li><a href="#step-7">
                <label class="stepNumber">
                    7</label>
                <span class="stepDesc">结束考核</span></a></li>
        </ul>
        <div id="step-1">
            <iframe id="stepOne" name="stepOne" width="100%" height="100%" frameborder="0"></iframe>
        </div>
        <div id="step-2">
            <iframe id="stepTwo" name="stepTwo" width="100%" height="100%" frameborder="0"></iframe>
        </div>
        <div id="step-3">
            <iframe id="stepThree" name="stepThree" width="100%" height="100%" frameborder="0">
            </iframe>
        </div>
        <div id="step-4">
            <iframe id="IframeFour" name="stepFour" width="100%" height="100%" frameborder="0">
            </iframe>
        </div>
        <div id="step-5">
            <iframe id="IframeFive" name="stepFive" width="100%" height="100%" frameborder="0">
            </iframe>
        </div>
        <div id="step-6">
            <iframe id="IframeSix" name="stepSix" width="100%" height="100%" frameborder="0">
            </iframe>
        </div>
        <div id="step-7">
            <iframe id="IframeSeven" name="stepSeven" width="100%" height="100%" frameborder="0">
            </iframe>
        </div>
    </div>
</asp:Content>
