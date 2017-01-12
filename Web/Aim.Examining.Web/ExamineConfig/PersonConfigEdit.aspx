<%@ Page Title="配门及人员配置" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="PersonConfigEdit.aspx.cs" Inherits="Aim.Examining.Web.ExamineConfig.PersonConfigEdit" %>

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
        var tlBar1, store1, myData1, gridLeft;
        var tlBar2, store2, myData2, gridRight;
        var tlBar3, store3, myData3, grid3;
        var tlBar4, store4, myData4, grid4;
        var tlBar5, store5, myData5, grid5;
        var tlBarLeader, storeleader, gridLeader;
        var GroupTypeEnum = { 职能服务部门: '职能服务部门', 经营目标单位: '经营目标单位', 系统部门: '系统部门' };
        function onPgLoad() {
            setPgUI();
            IniButton();
            GroupTypeChange();
        }
        function setPgUI() {
            leader();
            InitialGrid1();
            InitialGrid2();
            InitialGrid3();
            InitialGrid4();
            InitialGrid5();
        }
        function IniButton() {
            FormValidationBind('btnSubmit', SuccessSubmit);
            $("#btnSave").click(function() {
                var recsLeader = storeleader.getRange();
                recsLeader = storeleader.getModifiedDataStringArr(recsLeader);
                var recs1 = store1.getRange();
                recs1 = store1.getModifiedDataStringArr(recs1);
                var recs2 = store2.getRange();
                recs2 = store2.getModifiedDataStringArr(recs2);
                var recs3 = store3.getRange();
                recs3 = store3.getModifiedDataStringArr(recs3);
                var recs4 = store4.getRange();
                recs4 = store4.getModifiedDataStringArr(recs4);
                var recs5 = store5.getRange();
                recs5 = store5.getModifiedDataStringArr(recs5);
                AimFrm.submit(pgAction, { dataLeader: recsLeader, data1: recs1, data2: recs2,
                    data3: recs3, data4: recs4, data5: recs5
                }, null, SubFinish);
            })
            $("#btnCancel").click(function() {
                window.close();
            });
        }
        //验证成功执行保存方法
        function SuccessSubmit() {
            var recsLeader = storeleader.getRange();
            recsLeader = storeleader.getModifiedDataStringArr(recsLeader);
            var recs1 = store1.getRange();
            recs1 = store1.getModifiedDataStringArr(recs1);
            var recs2 = store2.getRange();
            recs2 = store2.getModifiedDataStringArr(recs2);
            var recs3 = store3.getRange();
            recs3 = store3.getModifiedDataStringArr(recs3);
            var recs4 = store4.getRange();
            recs4 = store4.getModifiedDataStringArr(recs4);
            var recs5 = store5.getRange();
            recs5 = store5.getModifiedDataStringArr(recs5);
            AimFrm.submit(pgAction, { dataLeader: recsLeader, data1: recs1, data2: recs2,
                data3: recs3, data4: recs4, data5: recs5
            }, null, SubFinish);
        }
        function SubFinish(args) {
            RefreshClose();
        }
        function userSelect(val) {
            var style = "dialogWidth:750px; dialogHeight:570px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "../CommonPages/Select/UsrSelect/MUsrSelect.aspx?seltype=multi&rtntype=array&GroupID=" + $("#GroupID").val();
            OpenModelWin(url, {}, style, function() {
                if (this.data == null || this.data.length == 0 || !this.data.length) return;
                var gird = Ext.getCmp(val);
                var EntRecord = gird.getStore().recordType;
                for (var i = 0; i < this.data.length; i++) {
                    if (Ext.getCmp(val).store.find("Id", this.data[i].Id) != -1) continue; //筛选已经存在的人
                    var rec = new EntRecord({ Id: this.data[i].UserID, Name: this.data[i].Name });
                    gird.getStore().insert(gird.getStore().data.length, rec);
                }
            })

        };
        function leader() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            storeleader = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData,
                fields: [{ name: 'Id' }, { name: 'Name'}]
            });
            tlBarLeader = new Ext.ux.AimToolbar({
                items: [{ text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        userSelect("gridleader");
                    }
                }, '-',
               { text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function() {
                       var recs = gridLeader.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("确定删除所选记录？")) {
                           for (var i = 0; i < recs.length; i++) {
                               storeleader.remove(recs[i]);

                           }
                       }
                   }
               }
]
            });
            gridLeader = new Ext.ux.grid.AimGridPanel({
                id: "gridleader",
                store: storeleader,
                title: '部门正职',
                renderTo: "leader",
                height: 150,
                autoExpandColumn: 'Name',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.grid.MultiSelectionModel(),
                { id: 'Name', header: "姓名", width: 100, dataIndex: 'Name'}],
                tbar: pgOperation != "v" ? tlBarLeader : ""
            });
        }
        function InitialGrid1() {
            myData1 = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList1"] || []
            };
            store1 = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList1',
                idProperty: 'Id',
                data: myData1,
                fields: [{ name: 'Id' }, { name: 'Name'}]
            });
            tlBar1 = new Ext.ux.AimToolbar({
                items: [{ text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        userSelect("grid1");
                    }
                }, '-',
               { text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function() {
                       var recs = gridLeft.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("确定删除所选记录？")) {
                           for (var i = 0; i < recs.length; i++) {
                               store1.remove(recs[i]);

                           }
                       }
                   }
               }
]
            });
            gridLeft = new Ext.ux.grid.AimGridPanel({
                id: "grid1",
                store: store1,
                title: '部门副职',
                renderTo: "div1",
                height: 150,
                autoExpandColumn: 'Name',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.grid.MultiSelectionModel(),
                { id: 'Name', header: "姓名", width: 100, dataIndex: 'Name'}],
                tbar: pgOperation != "v" ? tlBar1 : ""
            });
        }
        function InitialGrid2() {
            myData2 = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList2"] || []
            };
            store2 = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList2',
                idProperty: 'Id',
                data: myData2,
                fields: [{ name: 'Id' }, { name: 'Name'}]
            });
            tlBar2 = new Ext.ux.AimToolbar({
                items: [
               { text: '添加',
                   iconCls: 'aim-icon-add',
                   handler: function() {
                       userSelect("grid2");
                       //store.insert(store.data.length, rec);
                   }
               }, '-',
               { text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function() {
                       var recs = gridRight.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("确定删除所选记录？")) {
                           for (var i = 0; i < recs.length; i++) {
                               store2.remove(recs[i]);
                           }
                       }
                   }
               }
]
            });
            gridRight = new Ext.ux.grid.AimGridPanel({
                id: "grid2",
                store: store2,
                title: '主持部门工作副职',
                height: 150,
                renderTo: "div2",
                autoExpandColumn: 'Name',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.grid.MultiSelectionModel(),
                { id: 'Name', header: "姓名", width: 100, dataIndex: 'Name'}],
                tbar: pgOperation != "v" ? tlBar2 : ""
            });
        }
        function InitialGrid3() {
            myData3 = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList3"] || []
            };
            store3 = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList3',
                idProperty: 'Id',
                data: myData3,
                fields: [{ name: 'Id' }, { name: 'Name'}]
            });
            tlBar3 = new Ext.ux.AimToolbar({
                items: [
               { text: '添加',
                   iconCls: 'aim-icon-add',
                   handler: function() {
                       userSelect("grid3");
                       //store.insert(store.data.length, rec);
                   }
               }, '-',
               { text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function() {
                       var recs = grid3.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("确定删除所选记录？")) {
                           for (var i = 0; i < recs.length; i++) {
                               store3.remove(recs[i]);
                           }
                       }
                   }
               }
]
            });
            grid3 = new Ext.ux.grid.AimGridPanel({
                id: 'grid3',
                store: store3,
                title: '院级员工代表',
                height: 180,
                renderTo: "div3",
                autoExpandColumn: 'Name',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.grid.MultiSelectionModel(),
                { id: 'Name', header: "姓名", width: 100, sortable: true, dataIndex: 'Name'}],
                tbar: pgOperation != "v" ? tlBar3 : ""
            });
        }

        function InitialGrid4() {
            myData4 = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList4"] || []
            };
            store4 = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList4',
                idProperty: 'Id',
                data: myData4,
                fields: [{ name: 'Id' }, { name: 'Name'}]
            });
            tlBar4 = new Ext.ux.AimToolbar({
                items: [
               { text: '添加',
                   iconCls: 'aim-icon-add',
                   handler: function() {
                       userSelect("grid4");
                       //store.insert(store.data.length, rec);
                   }
               }, '-',
               { text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function() {
                       var recs = grid4.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("确定删除所选记录？")) {
                           for (var i = 0; i < recs.length; i++) {
                               store4.remove(recs[i]);
                           }
                       }
                   }
               }
]
            });
            grid4 = new Ext.ux.grid.AimGridPanel({
                id: "grid4",
                store: store4,
                title: '部门员工代表',
                renderTo: "div4",
                height: 180,
                autoExpandColumn: 'Name',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.grid.MultiSelectionModel(),
                { id: 'Name', header: "姓名", width: 100, sortable: true, dataIndex: 'Name'}],
                tbar: pgOperation != "v" ? tlBar4 : ""
            });
        }
        function InitialGrid5() {
            myData5 = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList5"] || []
            };
            store5 = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList5',
                idProperty: 'Id',
                data: myData5,
                fields: [{ name: 'Id' }, { name: 'Name'}]
            });
            tlBar5 = new Ext.ux.AimToolbar({
                items: [
               { text: '添加',
                   iconCls: 'aim-icon-add',
                   handler: function() {
                       userSelect("grid5");
                   }
               }, '-',
               { text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function() {
                       var recs = grid5.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("确定删除所选记录？")) {
                           for (var i = 0; i < recs.length; i++) {
                               store5.remove(recs[i]);
                           }
                       }
                   }
               }
]
            });
            grid5 = new Ext.ux.grid.AimGridPanel({
                id: 'grid5',
                store: store5,
                title: '部门员工',
                renderTo: "div5",
                height: 180,
                autoExpandColumn: 'Name',
                columns: [
              new Ext.ux.grid.AimRowNumberer(),
                new Ext.grid.MultiSelectionModel(),
                { id: 'Name', header: "姓名", width: 100, sortable: true, dataIndex: 'Name'}],
                tbar: pgOperation != "v" ? tlBar5 : ""
            });
        }
        function GroupTypeChange() {
            if ($("#GroupType").val() == "系统部门") {
                $("#trGroupCode").show();
            }
            else {
                $("#trGroupCode").hide();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            部门及人员配置</h1>
    </div>
    <fieldset>
        <legend id='legend'>基本信息</legend>
        <div id="editDiv" align="center">
            <table class="aim-ui-table-edit" width="100%">
                <tr style="display: none">
                    <td>
                        <input id="Id" name="Id" />
                        <input id="GroupID" name="GroupID" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        部门名称
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input id="GroupName" name="GroupName" aimctrl="popup" class="validate[required]"
                            popurl="/CommonPages/Select/GrpSelect/MGrpSelect.aspx" popparam="GroupID:GroupID;GroupName:Name"
                            popstyle="width=800,height=400" />
                    </td>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        部门类型
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <select id="GroupType" name="GroupType" enum='GroupTypeEnum' class="validate[required]"
                            aimctrl='select' style="width: 90%" onchange="GroupTypeChange()">
                        </select>
                    </td>
                </tr>
                <tr id="trGroupCode" style="display: none">
                    <td class="aim-ui-td-caption">
                        部门编号
                    </td>
                    <td>
                        <input id="GroupCode" name="GroupCode" readonly="readonly" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td class="aim-ui-td-caption">
                        说明：
                    </td>
                    <td colspan="3">
                        1 添加部门可以从系统组织机构选择已经存在的部门，也可以手动输入创建部门；2 系统级部门为程序专用，不要随意修改
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%">
            <tr>
                <td style="width: 33%">
                    <div id="leader">
                    </div>
                </td>
                <td style="width: 33%">
                    <div id="div1">
                    </div>
                </td>
                <td style="width: 34%">
                    <div id="div2">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div3">
                    </div>
                </td>
                <td>
                    <div id="div4">
                    </div>
                </td>
                <td>
                    <div id="div5">
                    </div>
                </td>
            </tr>
        </table>
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
