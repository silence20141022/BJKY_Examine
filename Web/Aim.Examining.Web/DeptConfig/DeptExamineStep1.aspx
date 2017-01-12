<%@ Page Title="基本信息" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="DeptExamineStep1.aspx.cs" Inherits="Aim.Examining.Web.DeptExamineStep1" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
        body
        {
            background-color: #F2F2F2;
        }
        .aim-ui-td-caption
        {
            text-align: right;
        }
        input
        {
            width: 90%;
        }
        select
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
        var StageEnum = { 1: '第一季度', 2: '第二季度', 3: '第三季度', 4: '年度考核' };
        var Enum_ExamineType = { "部门级考核": "部门级考核", "院级考核": "院级考核" };
        var id = $.getQueryString({ ID: 'id' });
        function onPgLoad() {
            //$("#ExamineType").val("部门级考核");
        }
        function SuccessSubmit() {
            var checkText = $("#LaunchDeptId").find("option:selected").text(); //获取Select选择的Text
            $("#LaunchDeptName").val(checkText);
            var result = null;
            if ($("#StageName").val()) {
                $.ajaxExecSync($("#Id").val() ? "update" : "create", { JsonString: AimFrm.getJsonString() }, function(rtn) {
                    result = rtn.data.Id;
                });
            }
            //            var recs = store.getRange();
            //            var dt = store.getModifiedDataStringArr(recs) || [];
            //            AimFrm.submit(pgAction, { data: dt }, null, SubFinish);
            else {
                alert("考核阶段名称不能为空！")
                result = false;
            }
            return result;
        }

        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
           
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <table class="aim-ui-table-edit" style="border: none">
        <tr style="display: none">
            <td>
                <input id="Id" name="Id" />
                <input id="CreateId" name="CreateId" />
                <input id="CreateName" name="CreateName" />
                <input id="CreateTime" name="CreateTime" />
                <input id="State" name="State" />
                <input id="TaskQuan" name="TaskQuan" />
            </td>
        </tr>
        <tr>
            <td class="aim-ui-td-caption" style="width: 25%">
                考核阶段名称
            </td>
            <td class="aim-ui-td-data" style="width: 25%">
                <input id="StageName" name="StageName" class="validate[required]" />
            </td>
            <td class="aim-ui-td-caption" style="width: 25%">
                考核类型
            </td>
            <td style="width: 25%">
              <%--  <input id="ExamineType" name="ExamineType" readonly="readonly" />--%>
                <select id="ExamineType" name="ExamineType" aimctrl='select' enum='Enum_ExamineType' class="validate[required]">
                </select>
            </td>
        </tr>
        <tr>
            <td class="aim-ui-td-caption">
                年份
            </td>
            <td class="aim-ui-td-data">
                <select id="Year" name="Year" aimctrl='select' enum="AimState['EnumYear']" class="validate[required]">
                </select>
            </td>
            <td class="aim-ui-td-caption">
                阶段类型
            </td>
            <td class="aim-ui-td-data">
                <select id="StageType" name="StageType" aimctrl='select' enum='StageEnum' class="validate[required]">
                </select>
            </td>
        </tr>
        <tr>
            <td class="aim-ui-td-caption">
                发起人
            </td>
            <td class="aim-ui-td-data">
                <input id="LaunchUserName" name="LaunchUserName" readonly="readonly" />
                <input id="LaunchUserId" name="LaunchUserId" type="hidden" />
            </td>
            <td class="aim-ui-td-caption">
                发起人部门
            </td>
            <td class="aim-ui-td-data">
                <select id="LaunchDeptId" name="LaunchDeptId" aimctrl='select' class="validate[required]"
                    enum="AimState['enumDept']">
                </select>
                <input id="LaunchDeptName" name="LaunchDeptName" type="hidden" />
            </td>
        </tr>
        <tr>
            <td class="aim-ui-td-caption">
                开始时间
            </td>
            <td class="aim-ui-td-data">
                <input id="StartTime" name="StartTime" class="Wdate" onfocus="var date=$('#EndTime').val()?$('#EndTime').val():'';                                             
                         WdatePicker({maxDate:date,minDate:'%y-%M-%d'})" />
            </td>
            <td class="aim-ui-td-caption">
                结束时间
            </td>
            <td>
                <input id="EndTime" name="EndTime" class="Wdate" onfocus="var date=$('#StartTime').val()?$('#StartTime').val():new Date();  
                        WdatePicker({minDate:date})" />
            </td>
        </tr>
        <tr>
            <td class="aim-ui-td-caption">
                备注
            </td>
            <td class="aim-ui-td-data" colspan="3">
                <textarea id="Remark" name="Remark" style="width: 96.7%;" rows="3" cols=""></textarea>
            </td>
        </tr>
    </table>
</asp:Content>
