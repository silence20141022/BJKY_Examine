<%@ Page Title="申诉处理" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="AppealProcess.aspx.cs" Inherits="Aim.Examining.Web.ExamineTaskManage.AppealProcess" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var EditWinStyle = CenterWin("width=760,height=700,scrollbars=yes");
        var EditPageUrl = "ExamineAppelEdit.aspx";
        var store, myData;
        var pgBar, schBar, tlBar, titPanel, grid, viewport;
        var AppealEnum = { '': '未提交', 1: '已提交', 2: '已受理', 3: '审批中', 4: '已结束' };
        var Index = $.getQueryString({ ID: "Index" });
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
			{ name: 'Id' }, { name: 'AppealUserId' }, { name: 'AppealUserName' }, { name: 'ExamineStageId' }, { name: 'ExamYearResultId' },
			{ name: 'DeptId' }, { name: 'DeptName' }, { name: 'BeRoleCode' }, { name: 'BeRoleName' }, { name: 'AppealTime' },
			{ name: 'AppealEvent' }, { name: 'AppealReason' }, { name: 'DealAdvices' }, { name: 'AcceptUserId' }, { name: 'AcceptUserName' },
			{ name: 'AcceptDateTime' }, { name: 'HrUserId' }, { name: 'HrUserName' }, { name: 'HrSignDateTime' }, { name: 'AppealUserLeaderID' },
			{ name: 'DealResult' }, { name: 'AppealUserLeaderName' }, { name: 'AppealUserLeaderSignDateTime' }, { name: 'ModifiedScore' },
			{ name: 'ModifiedAdviceLevel' }, { name: 'State' }, { name: 'CreateTime' }, { name: 'Result' }
			]
            });
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                items: [
                { fieldLabel: '申诉人', id: 'Name', schopts: { qryopts: "{ mode: 'Like', field: 'Name' }"} },
                { fieldLabel: '编码', id: 'Code', schopts: { qryopts: "{ mode: 'Like', field: 'Code' }"} },
                { fieldLabel: '创建人', id: 'CreateName', schopts: { qryopts: "{ mode: 'Like', field: 'CreateName' }"}}]
            });
            tlBar = new Ext.ux.AimToolbar({
                items: [{
                    text: '添加',
                    iconCls: 'aim-icon-add',
                    handler: function() {
                        ExtOpenGridEditWin(grid, EditPageUrl, "c", EditWinStyle);
                    }
                }, {
                    text: '修改',
                    iconCls: 'aim-icon-edit',
                    handler: function() {
                        ExtOpenGridEditWin(grid, EditPageUrl, "u", EditWinStyle);
                    }
                }, '->']
            });

            // 工具标题栏
            titPanel = new Ext.ux.AimPanel({
                // tbar: tlBar,
                items: [schBar]
            });

            // 表格面板
            grid = new Ext.ux.grid.AimGridPanel({
                title: '考核申诉',
                store: store,
                region: 'center',
                autoExpandColumn: 'AppealReason',
                columns: [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
					{ id: 'AppealUserName', dataIndex: 'AppealUserName', header: '申诉人', width: 80, sortable: true },
					{ id: 'DeptName', dataIndex: 'DeptName', header: '单位部门', width: 100, sortable: true },
					{ id: 'AppealReason', dataIndex: 'AppealReason', header: '申诉事由', width: 180, renderer: RowRender },
                //					{ id: 'AcceptHrUserName', dataIndex: 'AcceptHrUserName', header: '受理人', width: 100, sortable: true }, 
					{id: 'State', dataIndex: 'State', header: '申诉状态', width: 80, sortable: true, enumdata: AppealEnum },
					{ id: 'Result', dataIndex: 'Result', header: '申诉结果', width: 80, sortable: true },
					{ id: 'AppealTime', dataIndex: 'AppealTime', header: '申诉时间', width: 100, renderer: ExtGridDateOnlyRender, sortable: true },
					{ id: 'Id', dataIndex: 'Id', header: '操作', renderer: RowRender }
                    ],
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
                opencenterwin("ExamineAppealEdit.aspx?op=v&id=" + val + "&State=" + val2, "", 1000, 600);
            }
            else {
                opencenterwin("ExamineAppealEdit.aspx?op=u&id=" + val + "&State=" + val2, "", 1000, 600);
            }
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn;
            switch (this.id) {
                case "Id":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowInfo(\"" + value + "\",\"" + record.get("State") + "\")'>申诉信息</label>";
                    }
                    break;
                case "AppealReason":
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
