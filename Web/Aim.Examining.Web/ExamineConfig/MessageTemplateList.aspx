<%@ Page Title="短信模版管理" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master"
    AutoEventWireup="true" CodeBehind="MessageTemplateList.aspx.cs" Inherits="Aim.Examining.Web.ExamineManage.MessageTemplateList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData,
                fields: [
		        { name: 'Id' }, { name: 'TemplateType' }, { name: 'TemplateName' }, { name: 'TemplateContent' },
		        { name: 'State' }, { name: 'Remark' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'CreateTime' }
		        ],
                listeners: { aimbeforeload: function(proxy, options) {
                }
                }
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                collapsed: false,
                columns: 4,
                items: [
                { fieldLabel: '模版类型', id: 'TemplateType', schopts: { qryopts: "{ mode: 'Like', field: 'TemplateType' }"} },
                { fieldLabel: '模版内容', id: 'TemplateContent', schopts: { qryopts: "{ mode: 'Like', field: 'TemplateContent' }"} }
                //{ fieldLabel: '职务', id: 'BeUserRoleName', schopts: { qryopts: "{ mode: 'Like', field: 'BeUserRoleName' }"} }
                ]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [
               {
                   text: '添加',
                   iconCls: 'aim-icon-add',
                   handler: function() {
                       var recType = store.recordType;
                       $.ajaxExec("create", {}, function(rtn) {
                           if (rtn.data.Entity)
                               var rec = new recType({ Id: rtn.data.Entity.Id, CreateName: rtn.data.Entity.CreateName,
                                   CreateTime: rtn.data.Entity.CreateTime
                               });
                           store.insert(store.data.length, rec);
                       })

                   }
               }, '-', {
                   text: '删除',
                   iconCls: 'aim-icon-delete',
                   handler: function() {
                       var recs = grid.getSelectionModel().getSelections();
                       if (!recs || recs.length <= 0) {
                           AimDlg.show("请先选择要删除的记录！");
                           return;
                       }
                       if (confirm("删除考核内容会连同其下量化指标一起删除，确定删除所选记录？")) {
                           var idarray = [];
                           $.each(recs, function() {
                               idarray.push(this.get("Id"));
                           })
                           $.ajaxExec("delete", { ids: idarray }, function() {
                               store.remove(recs);
                               if (store.data.length > 0) {
                                   frameContent.location.href = "PersonSecondIndicatorList.aspx?&PersonFirstIndicatorId=" + store.getAt(0).get("Id") +
                                     "&PersonFirstIndicatorName=" + decodeURIComponent(store.getAt(0).get("PersonIndicatorFirstName")) +
                                     "&maxScore=" + store.getAt(0).get("Weight") + "&State=" + State + "&Result=" + escape(Result);
                               }
                               else {
                                   frameContent.location.href = "PersonSecondIndicatorList.aspx?";
                               }
                           });
                       }
                   }
}]
            });
            titPanel = new Ext.ux.AimPanel({
                tbar: tlBar,
                items: [schBar]
            });
            var clnArr = [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'TemplateType', dataIndex: 'TemplateType', header: '模版类型', width: 100 },
					{ id: 'TemplateContent', dataIndex: 'TemplateContent', header: '模版内容', width: 80 },
					{ id: 'State', dataIndex: 'State', header: '状态', width: 80, sortable: true },
               		{ id: 'CreateName', dataIndex: 'CreateName', header: '创建人', width: 80 },
					{ id: 'CreateTime', dataIndex: 'CreateTime', header: '创建时间', width: 100, sortable: true, renderer: ExtGridDateOnlyRender }
];
            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                // title: '考核反馈',
                store: store,
                region: 'center',
                autoExpandColumn: 'TemplateContent',
                columns: clnArr,
                bbar: pgBar,
                tbar: titPanel
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowInfo(val, val2) {
            if (Index == "1") {
                opencenterwin("FeedbackEdit.aspx?op=v&id=" + val + "&State=" + val2, "", 1000, 600);
            }
            else {
                opencenterwin("FeedbackEdit.aspx?op=u&id=" + val + "&State=" + val2, "", 1000, 600);
            }
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "Id":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowInfo(\"" + value + "\",\"" + record.get("State") + "\")'>反馈信息</label>";
                    }
                    break;
                case "PlanAndMethod":
                    if (value) {
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
            }
            return rtn;
        }
        // 提交数据成功后
        function onExecuted() {
            store.reload();
        }    
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
