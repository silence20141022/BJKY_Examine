function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
function selectdept(grid) {
    var store_gp = Ext.create('Ext.data.JsonStore', {
        fields: ['GroupID', 'Name'],
        proxy: {
            url: '../Common.aspx?action=loaddept',
            type: 'ajax',
            reader: {
                type: 'json',
                root: 'rows'
            }
        },
        autoLoad: true
    })
    var grid_gp = Ext.create('Ext.grid.Panel', {
        store: store_gp,
        selModel: { selType: 'checkboxmodel', mode: 'SIMPLE' },
        region: 'center',
        columns: [
                    { xtype: 'rownumberer', width: 35 },
                    { header: 'GroupID', dataIndex: 'GroupID', hidden: true },
                    { header: '部门名称', dataIndex: 'Name', flex: 1 }
        ]
    });
    var win_gp = Ext.create("Ext.window.Window", {
        title: '部门选择',
        width: 700,
        height: 570,
        modal: true,
        layout: 'border',
        items: [grid_gp],
        buttonAlign: 'center',
        buttons: [{
            text: '<i class="fa fa-check-square-o"></i>&nbsp;确定', handler: function () {
                var recs = grid_gp.getSelectionModel().getSelection();
                var store_t = grid.store;
                Ext.each(recs, function (rec) {
                    var num = store_t.findExact('UserID', rec.get("GroupID"));
                    if (num < 0) {
                        store_t.insert(store_t.data.length, { UserID: rec.data.GroupID, Name: rec.data.Name });
                    }
                })
                win_gp.close();
            }
        }, {
            text: '<i class="fa fa-times"></i>&nbsp;取消', handler: function () {
                win_gp.close();
            }
        }]
    });
    win_gp.show();
};
function selectuser(grid) {
    var store_group = Ext.create('Ext.data.JsonStore', {
        fields: ['GroupID', 'Name'],
        proxy: {
            url: '../Common.aspx?action=loaddept',
            type: 'ajax',
            reader: {
                type: 'json',
                root: 'rows',
                totalProperty: 'total'
            }
        },
        autoLoad: true
    });
    var grid_group = Ext.create('Ext.grid.Panel', {
        store: store_group,
        selModel: { selType: 'checkboxmodel' },
        region: 'west',
        width: 400,
        columns: [
                    { xtype: 'rownumberer', width: 35 },
                    { header: 'GroupID', dataIndex: 'GroupID', hidden: true },
                    { header: '部门名称', dataIndex: 'Name', flex: 1 }
                    ],
        listeners: {
            itemclick: function (grid, record, item, index, e, eOpts) {
                Ext.Ajax.request({
                    url: '../Common.aspx',
                    params: { deptid: record.get("GroupID"), action: 'loaduser' },
                    success: function (response, opts) {
                        var obj = Ext.decode(response.responseText);
                        store_user.loadData(obj);
                    }
                });
            }
        }
    });
    var store_user = Ext.create('Ext.data.JsonStore', {
        fields: ['UserID', 'Name', 'GroupName']
    });
    var grid_user = Ext.create('Ext.grid.Panel', {
        store: store_user,
        selModel: { selType: 'checkboxmodel', mode: 'SIMPLE' },
        region: 'center',
        columns: [
                    { xtype: 'rownumberer', width: 35 },
                    { header: 'UserID', dataIndex: 'UserID', hidden: true },
                    { header: '姓名', dataIndex: 'Name', width: 110 },
                    { header: '所在部门', dataIndex: 'GroupName', flex: 1}]
    });
    var toolbar = Ext.create('Ext.toolbar.Toolbar', {
        items: [
                            {
                                xtype: 'textfield', fieldLabel: '人员姓名', id: 'field_username'
                            },
                            {
                                xtype: 'button', text: '<i class="iconfont">&#xe615;</i>&nbsp;查询', handler: function () {
                                    if (Ext.getCmp('field_username').getValue()) {
                                        Ext.Ajax.request({
                                            url: '../Common.aspx',
                                            params: { username: Ext.getCmp("field_username").getValue(), action: 'loaduser' },
                                            success: function (response, opts) {
                                                var obj = Ext.decode(response.responseText);
                                                store_user.loadData(obj);
                                            }
                                        });
                                    }
                                }
                            }, '->'
                ]
    });
    var win_seluser = Ext.create("Ext.window.Window", {
        title: '人员选择',
        width: 800,
        tbar: toolbar,
        height: 570,
        modal: true,
        layout: 'border',
        items: [grid_group, grid_user],
        buttonAlign: 'center',
        buttons: [{
            text: '<i class="iconfont">&#xe60c;</i>&nbsp;确定', handler: function () {
                var recs = grid_user.getSelectionModel().getSelection();
                var store_t = grid.store;
                Ext.each(recs, function (rec) {
                    var num = store_t.findExact('UserID', rec.get("UserID"));
                    if (num < 0) {
                        store_t.insert(store_t.data.length, rec);
                    }
                })
                win_seluser.close();
            }
        }, {
            text: '<i class="iconfont">&#xe620;</i>&nbsp;取消', handler: function () {
                win_seluser.close();
            }
        }]
    });
    win_seluser.show();
}
