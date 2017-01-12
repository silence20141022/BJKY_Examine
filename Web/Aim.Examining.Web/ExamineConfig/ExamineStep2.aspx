<%@ Page Title="考核阶段" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="ExamineStep2.aspx.cs" Inherits="Aim.Examining.Web.ExamineStep2" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var myData, store, tlBar, grid, viewport;
        var id = $.getQueryString({ ID: 'id' });
        function onPgLoad() {
            setPgUI();
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
			    { name: 'Id' }, { name: 'GroupID' }, { name: 'GroupName' }, { name: 'ExamineStageId' }, { name: 'GroupType' },
			    { name: 'FirstLeaderNames'}],
                listeners: { aimbeforeload: function(proxy, options) {
                }
                }
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加考核部门',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        MultiAddDept();
                    }
                }, '-', {
                    text: '删除考核部门',
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
                },
                 '->']
            });
            grid = new Ext.ux.grid.AimGridPanel({
                //title: '参与部门',
                store: store,
                region: 'center',
                autoExpandColumn: 'GroupName',
                columnLines: true,
                columns: [new Ext.ux.grid.AimRowNumberer(),
                          new Ext.ux.grid.AimCheckboxSelectionModel(),
                          { id: 'Id', dataIndex: 'Id', header: 'Id', hidden: true },
                          { id: 'GroupName', dataIndex: 'GroupName', header: '部门名称', width: 200 },
                          { id: 'GroupType', dataIndex: 'GroupType', header: '部门类型', width: 130 },
                          { id: 'FirstLeaderNames', dataIndex: 'FirstLeaderNames', header: '部门领导', width: 130 }
					      ],
                tbar: tlBar
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        function SuccessSubmit() {
            var result = false;
            var recs = store.getRange();
            var dt = store.getModifiedDataStringArr(recs) || [];
            if (recs.length > 0) {
                $.ajaxExecSync("update", { id: id, data: dt }, function() {
                    result = id;
                });
            }
            else {
                alert("请选择院级考核参与部门！");
                result = false;
            }
            return result;
        }
        function MultiAddDept() {
            var style = "dialogWidth:700px; dialogHeight:500px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "ExamineGroupSelect.aspx?seltype=multi&rtntype=array";
            OpenModelWin(url, {}, style, function() {
                if (this.data == null || this.data.length == 0 || !this.data.length) return;
                var EntRecord = store.recordType;
                var data = this.data;
                for (var i = 0; i < data.length; i++) {
                    var p = new EntRecord({ GroupID: data[i].Id, GroupName: data[i].GroupName, GroupType: data[i].GroupType, FirstLeaderNames: data[i].FirstLeaderNames });
                    if (store.find("GroupID", data[i].Id) != -1) continue; //筛掉已经存在的部门
                    insRowIdx = store.data.length;
                    store.insert(insRowIdx, p);
                }
            });
        }
        function AddExamineStageDetail() {
            var style = "dialogWidth:1000px; dialogHeight:500px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "ExamineRelationSelect.aspx?seltype=multi&rtntype=array";
            OpenModelWin(url, {}, style, function() {
                if (this.data == null || this.data.length == 0 || !this.data.length) return;
                var EntRecord = store0.recordType;
                var data = this.data;
                for (var i = 0; i < data.length; i++) {//Code Name
                    var p = new EntRecord({ BeRoleCode: data[i].BeRoleCode, BeRoleName: data[i].BeRoleName,
                        ExamineRelationId: data[i].Id, RelationName: data[i].RelationName
                    });
                    if (store0.find("BeRoleCode", data[i].Code) != -1) continue; //筛掉已经存在的考核对象
                    insRowIdx = store0.data.length;
                    store0.insert(insRowIdx, p);
                }
            });
        }
        function SetExamineIndicator(val) {
            var style = "dialogWidth:800px; dialogHeight:400px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "IndicatorSelect.aspx?seltype=single&rtntype=array&ExamineType=" + escape($("#ExamineType").val()) + "&LaunchDeptId=" + $("#LaunchDeptId").val();
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
            opencenterwin("ExamineRelationList.aspx?op=v&id=" + val, "", 1000, 500);
        }
        function ShowBeUser(val) {
            opencenterwin("BeUserList.aspx?BeRoleCode=" + val + "&ExamineStageId=" + $("#Id").val() + "&LaunchDeptId=" + $("#LaunchDeptId").val(), "", 900, 500);
        }
        function ShowIndicator(val) {
            opencenterwin("ExamineIndicatorView.aspx?id=" + val, "", 900, 500);
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
                case "BeRoleCode":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowBeUser(\"" + value + "\")'>考核对象</label>";
                    }
                    break;
                case "BeRoleName":
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
</asp:Content>
