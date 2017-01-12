<%@ Page Title="考核阶段" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="DeptExamineStep2.aspx.cs" Inherits="Aim.Examining.Web.DeptExamineStep2" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var grid, myData, store, tlBar, viewport;
        var id = $.getQueryString({ ID: 'id' });
        function onPgLoad() {
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
                    text: '添加考核关系',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        AddExamineStageDetail();
                    }
                }, '-', {
                    text: '删除考核关系',
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
                }, '->']
            });
            grid = new Ext.ux.grid.AimGridPanel({
                //   title: '考核关系',
                store: store,
                autoExpandColumn: 'DownLevelUserNames',
                region: 'center',
                tbar: tlBar,
                columnLines: true,
                columns: [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'ExamineRelationId', dataIndex: 'ExamineRelationId', header: '考核关系', width: 120, renderer: RowRender },
					{ id: 'BeUserNames', dataIndex: 'BeUserNames', header: '考核对象', width: 180, renderer: RowRender },
					{ id: 'UpLevelUserNames', dataIndex: 'UpLevelUserNames', header: '上级评分人', width: 120, renderer: RowRender },
					{ id: 'SameLevelUserNames', dataIndex: 'SameLevelUserNames', header: '同级评分人', width: 180, renderer: RowRender },
				    { id: 'DownLevelUserNames', dataIndex: 'DownLevelUserNames', header: '下级评分人', width: 220, renderer: RowRender }
				    //{ id: 'ExamineIndicatorId', dataIndex: 'ExamineIndicatorId', header: '考核指标', width: 120, renderer: RowRender }
					      ]
                // cls: 'grid-row-span',

            });
            // 页面视图 
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }

        function SuccessSubmit() {
            var result = false;
            var recs = store.getRange();
            if (recs.length > 0) {
                var dt = store.getModifiedDataStringArr(recs) || [];
                $.ajaxExecSync("update", { id: id, data: dt }, function(rtn) {
                    result = id;
                });
            }
            else {
                alert("请选择考核关系！");
                result = false;
            }
            return result;
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
        

    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <input id="LaunchDeptId" name="LaunchDeptId" />
</asp:Content>
