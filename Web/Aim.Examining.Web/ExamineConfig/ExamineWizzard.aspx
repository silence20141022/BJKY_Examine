<%@ Page Title="考核向导" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="ExamineWizzard.aspx.cs" Inherits="Aim.Examining.Web.ExamineWizzard" %>

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
        var result = null;
        var op = $.getQueryString({ ID: 'op' });
        var id = $.getQueryString({ ID: 'id' });
        function onPgLoad() {
            setPgUI();
            //          window.onbeforeunload = function() {width: 100%;
            //                $.ajaxExecSync("Close", { Id: guid || '' }, function(rtn) {
            //                    //判断是否存在store对象,然后进行刷新
            //                    if (typeof window.opener.store !== "undefined") {
            //                        window.opener.store.reload();
            //                    }
            //                    else {
            //                        window.opener.location.reload();
            //                    }
            //                    window.close();
            //                });
            //            }
        }
        function setPgUI() {
            initWizard();
        }

        function initWizard() {
            $('#wizard').smartWizard({
                labelNext: '下一步',
                labelPrevious: '上一步',
                labelFinish: '完成',
                onLeaveStep: leavestepCallback,
                onShowStep: showstepCallback,
                onFinish: onFinish
                //enableFinishButton: true
                // enableAllSteps: true  允许点击所有步骤
            });
        }


        //离开
        function leavestepCallback(obj) {
            var step_num = obj.attr('rel');
            if (step_num == "1") {
                result = stepOne.SuccessSubmit();
                return result;
            }
            else if (step_num == "2") {
                result = stepTwo.SuccessSubmit();
                return result;
            }
            else if (step_num == "3") {
                result = stepThree.SuccessSubmit();
                return result;
            }
            else if (step_num == "4") {
                return true;
            }
        }

        //完成结束
        function onFinish() {
            result = stepFour.SuccessSubmit();
            if (!result) {
                return;
            }
            window.location.href = "../ExamineConfig/ExamineStageList.aspx";
        }
        //保存成功后的事件
        function afterSave(StepNum, e) {

        }

        //显示该步
        function showstepCallback(obj) {
            var step_num = obj.attr('rel');  //操作符
            //            var opg = (pgAction == "c" || pgAction == "create") ? "c" : "r";
            if (step_num == "1") {
                if (stepOne.location.href.indexOf("ExamineStep1.aspx?step=1") > -1) return;
                stepOne.location.href = "ExamineStep1.aspx?step=1&op=" + op + "&id=" + id;
            }
            else if (step_num == "2") {
                if (stepTwo.location.href.indexOf("DeptExamineStep2.aspx?step=2") > -1) return;
                stepTwo.location.href = "ExamineStep2.aspx?step=2&id=" + result;
            }
            else if (step_num == "3") {
                if (stepThree.location.href.indexOf("ExamineStep3.aspx?step=3") > -1) return;
                stepThree.location.href = "ExamineStep3.aspx?step=3&id=" + result;
            }
            else if (step_num == "4") {
                if (stepFour.location.href.indexOf("ExamineStep4.aspx?step=4") > -1) return;
                stepFour.location.href = "ExamineStep4.aspx?step=4&id=" + result;
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
                <span class="stepDesc">参与部门</span></a></li>
            <li><a href="#step-3">
                <label class="stepNumber">
                    3</label>
                <span class="stepDesc">考核关系</span></a></li>
            <li><a href="#step-4">
                <label class="stepNumber">
                    4</label>
                <span class="stepDesc">考核指标</span></a></li>
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
            <iframe id="stepFour" name="stepFour" width="100%" height="100%" frameborder="0">
            </iframe>
        </div>
    </div>
</asp:Content>
