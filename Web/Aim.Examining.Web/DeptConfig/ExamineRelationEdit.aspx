<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamineRelationEdit.aspx.cs"
    Inherits="Aim.Examining.Web.DeptConfig.ExamineRelationEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Extjs42/resources/css/ext-all-gray.css" rel="stylesheet" />
    <script src="../Extjs42/bootstrap.js" type="text/javascript"></script>
    <link href="../iconfont/iconfont.css" rel="stylesheet" />
    <script src="common.js" type="text/javascript"></script>
    <script type="text/javascript">
        var id = getQueryString("id");
        Ext.onReady(function () {
            Ext.Ajax.request({
                url: "ExamineRelationEdit.aspx",
                params: { action: 'loadform', id: id },
                success: function (response, option) {
                    var data = Ext.decode(response.responseText);
                    var field_id = Ext.create('Ext.form.field.Hidden', {
                        name: 'Id'
                    });
                    var field_groupid = Ext.create('Ext.form.field.Hidden', {
                        name: 'GroupID'
                    });
                    var field_createid = Ext.create('Ext.form.field.Hidden', {
                        name: 'CreateId'
                    });
                    var field_createname= Ext.create('Ext.form.field.Hidden', {
                        name: 'CreateName'
                    });
                    var field_busiunit = Ext.create('Ext.form.field.Text', {
                        name: 'RelationName',
                        labelAlign: "right",
                        allowBlank: false,
                        fieldLabel: '考核关系名称',
                        blankText: '名称不能为空!'
                    });
                    var field_filestatus = Ext.create('Ext.form.field.Text', {
                        id: 'field_filestatus',
                        labelAlign: "right",
                        fieldLabel: ' 部门名称',
                        name: 'GroupName'
                    });
                    var field_uplevel = Ext.create('Ext.form.field.Number', {
                        fieldLabel: ' 上级权重',
                        name: "UpLevelWeight",
                        allowDecimals: false
                    });
                    var field_samelevel = Ext.create('Ext.form.field.Number', {
                        fieldLabel: ' 同级权重',
                        name: "SameLevelWeight",
                        allowDecimals: false
                    });
                    var field_downlevel = Ext.create('Ext.form.field.Number', {
                        fieldLabel: ' 下级权重',
                        name: "DownLevelWeight",
                        allowDecimals: false
                    });
                    var formpanel = Ext.create('Ext.form.Panel', {
                        title: '考核关系',
                        region: 'north',
                        height: 180,
                        border: 0,
                        fieldDefaults: {
                            margin: 10,
                            labelWidth: 80,
                            columnWidth: .50,
                            labelAlign: 'right',
                            labelSeparator: '',
                            msgTarget: 'under',
                            validateOnBlur: false,
                            validateOnChange: false
                        },
                        items: [
                { layout: 'column', height: 42, border: 0, items: [field_busiunit, field_filestatus] },
                { layout: 'column', height: 42, border: 0, items: [field_uplevel, field_samelevel] },
                { layout: 'column', height: 42, border: 0, items: [field_downlevel] },
                field_id, field_groupid, field_createid, field_createname
                ],
                        buttonAlign: 'center',
                        buttons: [
                { text: '<i class="iconfont">&#xe60c;</i>&nbsp;保存', handler: function () {
                    if (formpanel.getForm().isValid()) {
                        if (field_uplevel.getValue() + field_samelevel.getValue() + field_downlevel.getValue() != 100) {
                            Ext.MessageBox.alert("提示", "权重总和不等于100！")
                            return;
                        }
                        var formdata = Ext.encode(formpanel.getForm().getValues());
                        var data1 = Ext.encode(Ext.pluck(gridpanel1.store.data.items, 'data'));
                        var data2 = Ext.encode(Ext.pluck(gridpanel2.store.data.items, 'data'));
                        var data3 = Ext.encode(Ext.pluck(gridpanel3.store.data.items, 'data'));
                        var data4 = Ext.encode(Ext.pluck(gridpanel4.store.data.items, 'data'));
                        var mask = new Ext.LoadMask(Ext.get(Ext.getBody()), { msg: "数据保存中，请稍等..." });
                        mask.show();
                        Ext.Ajax.request({
                            url: "ExamineRelationEdit.aspx",
                            params: { action: 'save', formdata: formdata, data1: data1, data2: data2, data3: data3, data4: data4 },
                            success: function (response, option) {
                                mask.hide();
                                if (response.responseText == "true") {
                                    Ext.MessageBox.alert("提示", "保存成功！", function () {
                                       window.close();
                                    });
                                }
                                else {
                                    Ext.MessageBox.alert("提示", "保存失败！");
                                }
                            }
                        });
                    }
                }
                },
                { text: '<i class="iconfont">&#xe603;</i>&nbsp;关闭', handler: function () {
                    window.close();
                }
                }]
                    });
                    var tbar1 = Ext.create('Ext.toolbar.Toolbar', {
                        items: [
                {
                    text: '<i class="iconfont">&#xe60b;</i>&nbsp;添加人员', handler: function () {
                        selectuser(gridpanel1);
                    }
                },
                    { text: '<i class="iconfont">&#xe60b;</i>&nbsp;添加部门', handler: function () {
                        selectdept(gridpanel1);
                    }
                    },
                    { text: '<i class="iconfont">&#xe606;</i>&nbsp;删除', handler: function () {
                        var recs = gridpanel1.getSelectionModel().getSelection();
                        if (recs.length > 0) {
                            gridpanel1.store.remove(recs);
                        }
                    }
                    }
                ]
                    });
                    var store1 = Ext.create('Ext.data.JsonStore', {
                        fields: [{ name: 'UserID' }, { name: 'Name'}],
                        data: data.data1
                    });
                    var gridpanel1 = Ext.create('Ext.grid.Panel', {
                        title: '考核对象',
                        store: store1,
                        tbar: tbar1,
                        columnWidth: .25,
                        height: 400,
                        selModel: { selType: 'checkboxmodel' },
                        enableColumnHide: false,
                        columns: [
                    { xtype: 'rownumberer', width: 35 },
                    { header: 'UserID', dataIndex: 'UserID', hidden: true },
                    { header: '对象名称', dataIndex: 'Name' }
                    ],
                        viewConfig: {
                            enableTextSelection: true
                        }
                    });
                    var tbar2 = Ext.create('Ext.toolbar.Toolbar', {
                        items: [
                {
                    text: '<i class="iconfont">&#xe60b;</i>&nbsp;添加人员', handler: function () {
                        selectuser(gridpanel2);
                    }
                },
                { text: '<i class="iconfont">&#xe606;</i>&nbsp;删除', handler: function () {
                    var recs = gridpanel2.getSelectionModel().getSelection();
                    if (recs.length > 0) {
                        gridpanel2.store.remove(recs);
                    }
                }
                }
                ]
                    });
                    var store2 = Ext.create('Ext.data.JsonStore', {
                        fields: [{ name: 'UserID' }, { name: 'Name'}],
                        data: data.data2
                    });
                    var gridpanel2 = Ext.create('Ext.grid.Panel', {
                        title: '上级评分人',
                        store: store2,
                        tbar: tbar2,
                        columnWidth: .25,
                        height: 400,
                        selModel: { selType: 'checkboxmodel' },
                        enableColumnHide: false,
                        columns: [
                    { xtype: 'rownumberer', width: 35 },
                    { header: 'UserID', dataIndex: 'UserID', hidden: true },
                    { header: '姓名', dataIndex: 'Name' }
                    ],
                        viewConfig: {
                            enableTextSelection: true
                        }
                    });
                    var tbar3 = Ext.create('Ext.toolbar.Toolbar', {
                        items: [
                {
                    text: '<i class="iconfont">&#xe60b;</i>&nbsp;添加人员', handler: function () {
                        selectuser(gridpanel3);
                    }
                },
                { text: '<i class="iconfont">&#xe606;</i>&nbsp;删除', handler: function () {
                    var recs = gridpanel3.getSelectionModel().getSelection();
                    if (recs.length > 0) {
                        gridpanel3.store.remove(recs);
                    }
                }
                }
                ]
                    });
                    var store3 = Ext.create('Ext.data.JsonStore', {
                        fields: [{ name: 'UserID' }, { name: 'Name'}],
                        data: data.data3
                    });
                    var gridpanel3 = Ext.create('Ext.grid.Panel', {
                        title: '同级评分人',
                        store: store3,
                        tbar: tbar3,
                        columnWidth: .25,
                        height: 400,
                        selModel: { selType: 'checkboxmodel' },
                        enableColumnHide: false,
                        columns: [
                    { xtype: 'rownumberer', width: 35 },
                    { header: 'UserID', dataIndex: 'UserID', hidden: true },
                    { header: '姓名', dataIndex: 'Name' }
                    ],
                        viewConfig: {
                            enableTextSelection: true
                        }
                    });
                    var tbar4 = Ext.create('Ext.toolbar.Toolbar', {
                        items: [
                {
                    text: '<i class="iconfont">&#xe60b;</i>&nbsp;添加人员', handler: function () {
                        selectuser(gridpanel4);
                    }
                },
                { text: '<i class="iconfont">&#xe606;</i>&nbsp;删除', handler: function () {
                    var recs = gridpanel4.getSelectionModel().getSelection();
                    if (recs.length > 0) {
                        gridpanel4.store.remove(recs);
                    }
                }
                }
                ]
                    });
                    var store4 = Ext.create('Ext.data.JsonStore', {
                        fields: [{ name: 'UserID' }, { name: 'Name'}],
                        data: data.data4
                    });
                    var gridpanel4 = Ext.create('Ext.grid.Panel', {
                        title: '下级评分人',
                        store: store4,
                        tbar: tbar4,
                        columnWidth: .25,
                        height: 400,
                        selModel: { selType: 'checkboxmodel' },
                        enableColumnHide: false,
                        columns: [
                    { xtype: 'rownumberer', width: 35 },
                    { header: 'UserID', dataIndex: 'UserID', hidden: true },
                    { header: '姓名', dataIndex: 'Name' }
                    ],
                        viewConfig: {
                            enableTextSelection: true
                        }
                    });
                    var panel = Ext.create('Ext.panel.Panel', {
                        region: 'center',
                        layout: 'column',
                        border: 0,
                        items: [gridpanel1, gridpanel2, gridpanel3, gridpanel4]
                    })
                    var viewport = Ext.create('Ext.container.Viewport', {
                        layout: 'border',
                        items: [formpanel, panel]
                    });
                    formpanel.getForm().setValues(data.formdata);
                }
            });
        });
    </script>
</head>
<body>
    <div>
    </div>
</body>
</html>
