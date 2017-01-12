<%@ Page Title="部门考核关系" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master"
    AutoEventWireup="true" CodeBehind="DeptExamineRelationEdit.aspx.cs" Inherits="Aim.Examining.Web.DeptConfig.DeptExamineRelationEdit" %>

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
        var tlBar1, store1, myData1, grid1;
        var tlBar2, store2, myData2, grid2;
        var tlBar3, store3, myData3, grid3;
        var tlBar4, store4, myData4, grid4;
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            InitialGrid1(); InitialGrid2();
            InitialGrid3(); InitialGrid4();
            $("#btnSubmit").click(function() {
                SuccessSubmit();
            });
            $("#btnCancel").click(function() {
                window.close();
            });
        }
        function SuccessSubmit() {
            var checkText = $("#GroupID").find("option:selected").text();  //获取Select选择的Text
            var checkValue = $("#GroupID").val();  //获取Select选择的Value
            $("#GroupName").val(checkText);
            //            $("#GroupID").val(checkValue);
            var recs1 = store1.getRange();
            recs1 = store1.getModifiedDataStringArr(recs1);

            var recs2 = store2.getRange();
            recs2 = store2.getModifiedDataStringArr(recs2);

            var recs3 = store3.getRange();
            recs3 = store3.getModifiedDataStringArr(recs3);

            var recs4 = store4.getRange();
            recs4 = store4.getModifiedDataStringArr(recs4);
            AimFrm.submit(pgAction, { data1: recs1, data2: recs2, data3: recs3, data4: recs4 }, null, SubFinish);
        }
        function SubFinish(args) {
            RefreshClose();
        }
        function userSelect(val) {
            var style = "dialogWidth:750px; dialogHeight:570px; scroll:yes; center:yes; status:no; resizable:yes;";
            var url = "../CommonPages/Select/UsrSelect/MUsrSelect.aspx?seltype=multi&rtntype=array&GroupID=" + (AimState["GroupID"] ? AimState["GroupID"] : "");
            OpenModelWin(url, {}, style, function() {
                if (this.data == null || this.data.length == 0 || !this.data.length) return;
                var gird = Ext.getCmp(val);
                var EntRecord = gird.getStore().recordType;
                for (var i = 0; i < this.data.length; i++) {
                    if (Ext.getCmp(val).store.find("Id", this.data[i].UserID) != -1) continue; //筛选已经存在的人
                    var rec = new EntRecord({ Id: this.data[i].UserID, Name: this.data[i].Name });
                    gird.getStore().insert(gird.getStore().data.length, rec);
                }
            })
        };
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
                },
               { text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function() {
                       var recs = grid1.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("确定删除所选记录？")) {
                           store1.remove(recs);
                       }
                   }
               }
]
            });
            grid1 = new Ext.ux.grid.AimGridPanel({
                id: "grid1",
                store: store1,
                title: '考核对象',
                renderTo: "div1",
                height: 250,
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
                fields: [{ name: 'Id' }, { name: 'Name' }, { name: 'Weight'}]
            });
            tlBar2 = new Ext.ux.AimToolbar({
                items: [
               { text: '添加',
                   iconCls: 'aim-icon-add',
                   handler: function() {
                       userSelect("grid2");
                   }
               }, '-',
               { text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function() {
                       var recs = grid2.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("确定删除所选记录？")) {
                           store2.remove(recs);
                       }
                   }
               }
]
            });
            grid2 = new Ext.ux.grid.AimEditorGridPanel({
                id: "grid2",
                store: store2,
                title: '上级评分人',
                height: 250,
                renderTo: "div2",
                autoExpandColumn: 'Name',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.grid.MultiSelectionModel(),
                { id: 'Name', header: "姓名", width: 80, dataIndex: 'Name' },
                { id: 'Weight', header: "权重", width: 60, dataIndex: 'Weight', editor: { xtype: 'numberfield', allowBlank: false, minValue: 0, maxValue: 100}}],
                tbar: pgOperation != "v" ? tlBar2 : "",
                listeners: { afteredit: function(e) {
                    var total = 0;
                    $.each(store2.getRange(), function() {
                        total = total + (this.get("Weight") ? parseInt(this.get("Weight")) : 0);
                    })
                    if (total > 100) {
                        AimDlg.show("人员权重之和不能大于100！");
                        e.record.set("Weight", e.originalValue);
                    }
                }
                }
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
                fields: [{ name: 'Id' }, { name: 'Name' }, { name: 'Weight'}]
            });
            tlBar3 = new Ext.ux.AimToolbar({
                items: [
               { text: '添加',
                   iconCls: 'aim-icon-add',
                   handler: function() {
                       userSelect("grid3");
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
                           store3.remove(recs);
                       }
                   }
               }
]
            });
            grid3 = new Ext.ux.grid.AimEditorGridPanel({
                id: 'grid3',
                store: store3,
                title: '同级评分人',
                height: 250,
                renderTo: "div3",
                autoExpandColumn: 'Name',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.grid.MultiSelectionModel(),
                { id: 'Name', header: "姓名", width: 80, dataIndex: 'Name' },
                { id: 'Weight', header: "权重", width: 60, dataIndex: 'Weight', editor: { xtype: 'numberfield', allowBlank: false, minValue: 0, maxValue: 100}}],
                tbar: pgOperation != "v" ? tlBar3 : "",
                listeners: { afteredit: function(e) {
                    var total = 0;
                    $.each(store3.getRange(), function() {
                        total = total + (this.get("Weight") ? parseInt(this.get("Weight")) : 0);
                    })
                    if (total > 100) {
                        AimDlg.show("人员权重之和不能大于100！");
                        e.record.set("Weight", e.originalValue);
                    }
                }
                }
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
                fields: [{ name: 'Id' }, { name: 'Name' }, { name: 'Weight'}]
            });
            tlBar4 = new Ext.ux.AimToolbar({
                items: [
               { text: '添加',
                   iconCls: 'aim-icon-add',
                   handler: function() {
                       userSelect("grid4");
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
                           store4.remove(recs);
                       }
                   }
               }
]
            });
            grid4 = new Ext.ux.grid.AimEditorGridPanel({
                id: 'grid4',
                store: store4,
                title: '下级评分人',
                height: 250,
                renderTo: "div4",
                autoExpandColumn: 'Name',
                columns: [
                new Ext.ux.grid.AimRowNumberer(),
                new Ext.grid.MultiSelectionModel(),
                { id: 'Name', header: "姓名", width: 80, dataIndex: 'Name' },
                { id: 'Weight', header: "权重", width: 60, dataIndex: 'Weight', editor: { xtype: 'numberfield', allowBlank: false, minValue: 0, maxValue: 100}}],
                tbar: pgOperation != "v" ? tlBar4 : "",
                listeners: { afteredit: function(e) {
                    var total = 0;
                    $.each(store4.getRange(), function() {
                        total = total + (this.get("Weight") ? parseInt(this.get("Weight")) : 0);
                    })
                    if (total > 100) {
                        AimDlg.show("人员权重之和不能大于100！");
                        e.record.set("Weight", e.originalValue);
                    }
                }
                }
            });
        } 
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header">
        <h1>
            部门考核关系</h1>
    </div>
    <fieldset>
        <legend id='legend'></legend>
        <div id="editDiv" align="center">
            <table class="aim-ui-table-edit" width="100%">
                <tr style="display: none">
                    <td>
                        <input id="Id" name="Id" />
                        <input id="GroupName" name="GroupName" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        考核关系名称/组名
                    </td>
                    <td colspan="3">
                        <input id="RelationName" name="RelationName" style="width: 96.7%" class="validate[required]" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        部门名称
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <select id="GroupID" name="GroupID" aimctrl='select' enum='AimState["GroupEnum"]'
                            class="validate[required]" style="width: 90%">
                        </select>
                    </td>
                    <td class="aim-ui-td-caption" style="width: 25%">
                        上级权重
                    </td>
                    <td class="aim-ui-td-data" style="width: 25%">
                        <input id="UpLevelWeight" name="UpLevelWeight" class="validate[custom[onlyInteger]]" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        同级权重
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="SameLevelWeight" name="SameLevelWeight" class="validate[ custom[onlyInteger]]" />
                    </td>
                    <td class="aim-ui-td-caption">
                        下级权重
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="DownLevelWeight" name="DownLevelWeight" class="validate[ custom[onlyInteger]]" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" style="table-layout: fixed">
            <tr>
                <td style="width: 23%">
                    <div id="div1">
                    </div>
                </td>
                <td style="width: 23%">
                    <div id="div2">
                    </div>
                </td>
                <td style="width: 23%">
                    <div id="div3">
                    </div>
                </td>
                <td style="width: 24%">
                    <div id="div4">
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
    <div style="width: 100%">
        <table class="aim-ui-table-edit">
            <tr>
                <td class="aim-ui-button-panel" colspan="4">
                    <a id="btnSubmit" class="aim-ui-button submit">保存</a>&nbsp;&nbsp; <a id="btnCancel"
                        class="aim-ui-button cancel">取消</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
