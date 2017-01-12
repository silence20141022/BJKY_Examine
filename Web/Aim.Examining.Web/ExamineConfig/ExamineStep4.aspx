<%@ Page Title="考核阶段" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="True" CodeBehind="ExamineStep4.aspx.cs" Inherits="Aim.Examining.Web.ExamineStep4" %>

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
			    { name: 'Id' }, { name: 'ExamineStageId' }, { name: 'BeRoleCode' }, { name: 'BeRoleName' }, { name: 'PersonQuan' },
			    { name: 'ExamineRelationId' }, { name: 'RelationName' }, { name: 'ExamineIndicatorId' }, { name: 'IndicatorName'}],
                listeners: { aimbeforeload: function(proxy, options) {
                }
                }
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
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
                //   title: '考核关系',
                store: store,
                autoExpandColumn: 'ExamineRelationId',
                region: 'center',
                tbar: tlBar,
                columnLines: true,
                columns: [new Ext.ux.grid.AimRowNumberer(),
                          new Ext.ux.grid.AimCheckboxSelectionModel(),
                          { id: 'Id', dataIndex: 'Id', header: 'Id', hidden: true },
                          { id: 'BeRoleName', dataIndex: 'BeRoleName', header: '考核角色', width: 150 },
                          { id: 'BeRoleCode', dataIndex: 'BeRoleCode', header: '考核对象', width: 100, renderer: RowRender },
					      { id: 'ExamineRelationId', dataIndex: 'ExamineRelationId', header: '考核关系', width: 160, renderer: RowRender },
					      { id: 'ExamineIndicatorId', dataIndex: 'ExamineIndicatorId', header: '考核指标', width: 160, renderer: RowRender }
					      ]
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }

        function SuccessSubmit() {
            var result = true;
            var recs = store.getRange();
            $.each(recs, function() {
                if (!this.get("ExamineIndicatorId")) {
                    result = false;
                    return false;
                }
            })
            if (!result) {
                alert("请选择考核指标！");
            }
            return result;
        } 
        function SetExamineIndicator() {
            var style = "dialogWidth:800px; dialogHeight:400px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "IndicatorSelect.aspx?seltype=single&rtntype=array&ExamineType=" + escape("院级考核") + "&LaunchDeptId=" + $("#LaunchDeptId").val();
            OpenModelWin(url, {}, style, function() {
                if (this.data) {   //是单选  所以不需要循环
                    var recs = grid.getSelectionModel().getSelections();
                    for (var i = 0; i < recs.length; i++) {
                        recs[i].set("ExamineIndicatorId", this.data.Id);
                        recs[i].set("IndicatorName", this.data.IndicatorName);
                        $.ajaxExec("update", { did: recs[i].get("Id"), ExamineIndicatorId: recs[i].get("ExamineIndicatorId") }, function() {
                        })
                    }
                    store.commitChanges();
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
            opencenterwin("BeUserList.aspx?BeRoleCode=" + val + "&ExamineStageId=" + id, "", 900, 500);
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
    <input id="LaunchDeptId" name="LaunchDeptId" />
</asp:Content>
