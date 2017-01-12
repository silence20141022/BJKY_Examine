<%@ Page Title="考核阶段" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="DeptExamineEdit.aspx.cs" Inherits="Aim.Examining.Web.DeptConfig.DeptExamineEdit" %>

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
    <style type="text/css">
        .x-view-selected
        {
            -moz-background-clip: border;
            -moz-background-inline-policy: continuous;
            -moz-background-origin: padding;
            background-color: #FFC0CB;
        }
        .thumb
        {
            background-color: #dddddd;
            padding: 4px;
            text-align: center;
            height: 40px;
        }
        .thumb-activated
        {
            background-color: #33dd33;
            padding: 4px;
            text-align: center;
            border: 2px;
            border-style: dashed;
            border-color: Red;
            height: 40px;
        }
        .thumb-activateded
        {
            background-color: blue;
            padding: 4px;
            text-align: center;
            border: 2px;
            border-style: dashed;
            border-color: Red;
            height: 40px;
        }
        .thumb-separater
        {
            float: left;
            width: 160;
            padding: 5px;
            margin: '5 5 5 5';
            vertical-align: middle;
            text-align: center;
            border: 1px solid gray;
        }
        .thumb-wrap-out
        {
            float: left;
            width: 80px;
            margin-right: 0;
            padding: 0px; /*background-color:#8DB2E3;*/
        }
        .thumb-wrap
        {
            font-size: 12px;
            font-weight: bold;
            padding: 2px;
        }
        .remark
        {
            font-size: 12px;
            padding: 2px;
        }
        .tblusing
        {
            background-color: #FF8247;
        }
        .tblunusing
        {
            background-color: Gray;
        }
    </style>

    <script type="text/javascript">
        var grid, myData, store, tlBar;
        var StageEnum = { 1: '第一季度', 2: '第二季度', 3: '第三季度', 4: '年度考核' };
        function onPgLoad() {
            setPgUI();
            IniButton();
        }
        function setPgUI() {
            IniGrid();
        }
        function IniGrid() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData,
                fields: [
                 { name: 'Id' }, { name: 'ExamineStageId' }, { name: 'BeRoleCode' }, { name: 'BeRoleName' },
			    { name: 'ExamineRelationId' }, { name: 'RelationName' }, { name: 'ExamineIndicatorId' }, { name: 'IndicatorName' },
                { name: 'BeUserIds' }, { name: 'BeUserNames' },
		        { name: 'UpLevelUserIds' }, { name: 'UpLevelUserNames' }, { name: 'UpLevelWeight' },
		        { name: "SameLevelUserIds" }, { name: "SameLevelUserNames" }, { name: "SameLevelWeight" },
	            { name: 'DownLevelUserIds' }, { name: 'DownLevelUserNames' }, { name: 'DownLevelWeight' },
		        { name: 'GroupID' }, { name: 'GroupName' }, { name: 'CreateId' },
		        { name: 'CreateName' }, { name: 'CreateTime'}],
                listeners: { aimbeforeload: function(proxy, options) {
                }
                }
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加考核对象',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        AddExamineStageDetail();
                    }
                }, '-', {
                    text: '删除考核对象',
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
                }, '-', {
                    text: '设置考核指标',
                    iconCls: 'aim-icon-wrench',
                    handler: function() {
                        var recs = grid.getSelectionModel().getSelections();
                        if (!recs || recs.length <= 0) {
                            AimDlg.show("请先选择要设置考核指标的记录！");
                            return;
                        }
                        SetExamineIndicator();
                    }
                },
                 '->']
            });
            grid = new Ext.ux.grid.AimGridPanel({
                title: '考核对象|考核关系|考核指标',
                store: store,
                renderTo: 'div1',
                autoHeight: true,
                autoExpandColumn: 'DownLevelUserNames',
                columnLines: true,
                columns: [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(), 
                    { id: 'ExamineRelationId', dataIndex: 'ExamineRelationId', header: '考核关系', width: 120, renderer: RowRender },
					{ id: 'BeUserNames', dataIndex: 'BeUserNames', header: '考核对象', width: 180, renderer: RowRender },
					{ id: 'UpLevelUserNames', dataIndex: 'UpLevelUserNames', header: '上级评分人', width: 180, renderer: RowRender },
					{ id: 'SameLevelUserNames', dataIndex: 'SameLevelUserNames', header: '同级评分人', width: 180, renderer: RowRender },
				    { id: 'DownLevelUserNames', dataIndex: 'DownLevelUserNames', header: '下级评分人', width: 180, renderer: RowRender },				   
				    { id: 'ExamineIndicatorId', dataIndex: 'ExamineIndicatorId', header: '考核指标', width: 120, renderer: RowRender }
					      ],
                cls: 'grid-row-span',
                tbar: pgOperation != "v" ? tlBar : ""
            });
        }
        function IniButton() {
            FormValidationBind('btnSubmit', SuccessSubmit);
            $("#btnSave").click(function() {
                SuccessSubmit();
            })
            $("#btnCancel").click(function() {
                window.close();
            });
        }
        //验证成功执行保存方法
        function SuccessSubmit() {
            var checkText = $("#LaunchDeptId").find("option:selected").text(); //获取Select选择的Text
            $("#LaunchDeptName").val(checkText);
            var recs = store.getRange();
            var dt = store.getModifiedDataStringArr(recs) || [];
            AimFrm.submit(pgAction, { data: dt }, null, SubFinish);
        }
        function AddExamineStageDetail() {
            var style = "dialogWidth:1000px; dialogHeight:500px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "DeptExamineRelationSelect.aspx?seltype=multi&rtntype=array&GroupID=" + $("#LaunchDeptId").val();
            OpenModelWin(url, {}, style, function() {
                if (this.data == null || this.data.length == 0 || !this.data.length) return;
                var EntRecord = store.recordType;
                var data = this.data;
                for (var i = 0; i < data.length; i++) {
                    var p = new EntRecord({ ExamineRelationId: data[i].Id, BeUserNames: data[i].BeUserNames,
                        UpLevelUserNames: data[i].UpLevelUserNames, SameLevelUserNames: data[i].SameLevelUserNames,
                        DownLevelUserNames: data[i].DownLevelUserNames, RelationName: data[i].RelationName
                    });
                    if (store.find("ExamineRelationId", data[i].Id) != -1) continue; //筛掉已经存在的考核对象
                    insRowIdx = store.data.length;
                    store.insert(insRowIdx, p);
                }
            });
        }
        function SetExamineIndicator() {
            var style = "dialogWidth:800px; dialogHeight:400px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "../ExamineConfig/IndicatorSelect.aspx?seltype=single&rtntype=array&ExamineType=" + escape($("#ExamineType").val()) + "&LaunchDeptId=" + $("#LaunchDeptId").val();
            OpenModelWin(url, {}, style, function() {
                if (this.data) {   //是单选  所以不需要循环
                    var recs = grid.getSelectionModel().getSelections();
                    if (recs.length > 0) {
                        recs[0].set("ExamineIndicatorId", this.data.Id);
                        recs[0].set("IndicatorName", this.data.IndicatorName);
                    }
                }
            });
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowRelation(val) {
            opencenterwin("DeptExamineRelationEdit.aspx?op=v&id=" + val, "", 1000, 500);
        }
        function ShowBeUser(val) {
            opencenterwin("BeUserList.aspx?BeRoleCode=" + val + "&ExamineStageId=" + $("#Id").val() + "&LaunchDeptId=" + $("#LaunchDeptId").val(), "", 900, 500);
        }
        function ShowIndicator(val) {
            opencenterwin("../ExamineConfig/ExamineIndicatorView.aspx?id=" + val, "", 900, 500);
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn;
            switch (this.id) {
                case "ExamineRelationId":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowRelation(\"" + value + "\")'>" + record.get("RelationName") + "</label>";
                    }
                    break;
                case "ExamineIndicatorId":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowIndicator(\"" + value + "\")'>" + record.get("IndicatorName") + "</label>";
                    }
                    break;
                case "BeUserNames":
                case "UpLevelUserNames":
                case "SameLevelUserNames":
                case "DownLevelUserNames":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
                case "CreateTime":
                    break;
            }
            return rtn;
        }
        function SubFinish(args) {
            RefreshClose();
        }
        window.onresize = function() {
            grid.setWidth(0);
            grid.setWidth(Ext.get("div1").getWidth());
        }

    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            考核阶段</h1>
    </div>
    <fieldset>
        <legend>基本信息</legend>
        <table class="aim-ui-table-edit" style="border: none">
            <tr style="display: none">
                <td>
                    <input id="Id" name="Id" />
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
                    <input id="ExamineType" name="ExamineType" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td class="aim-ui-td-caption">
                    年份
                </td>
                <td class="aim-ui-td-data">
                    <select id="Year" name="Year" aimctrl='select' enum="AimState['EnumYear']" class="validate[required]"
                        style="width: 130px">
                    </select>
                </td>
                <td class="aim-ui-td-caption">
                    阶段类型
                </td>
                <td class="aim-ui-td-data">
                    <select id="StageType" name="StageType" aimctrl='select' enum='StageEnum' class="validate[required]"
                        style="width: 130px">
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
                    <%--   <input id="LaunchDeptName" name="LaunchDeptName" />
                    <input id="LaunchDeptId" name="LaunchDeptId" type="hidden" />--%>
                    <select id="LaunchDeptId" name="LaunchDeptId" aimctrl='select' class="validate[required]"
                        enum="AimState['enumDept']" style="width: 130px">
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
    </fieldset>
    <fieldset>
        <div id="div1">
        </div>
    </fieldset>
    <div style="width: 100%">
        <table class="aim-ui-table-edit">
            <tbody>
                <tr>
                    <td class="aim-ui-button-panel" colspan="4">
                        <a id="btnSubmit" class="aim-ui-button submit">提交</a>&nbsp;&nbsp;<a id="btnSave"
                            class="aim-ui-button submit">暂存</a>&nbsp;&nbsp; <a id="btnCancel" class="aim-ui-button cancel">
                                取消</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
