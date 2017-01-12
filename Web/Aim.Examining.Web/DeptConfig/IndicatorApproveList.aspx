<%@ Page Title="指标审批" Language="C#" MasterPageFile="~/Masters/Ext/Site.Master" AutoEventWireup="true"
    CodeBehind="IndicatorApproveList.aspx.cs" Inherits="Aim.Examining.Web.DeptConfig.IndicatorApproveList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        var pgBar, schBar, tlBar, titPanel, myData, store, grid, viewport;
        var enumQuater = { 1: '第一季度', 2: '第二季度', 3: '第三季度', 4: '年度' };
        var year = new Date().getFullYear();
        var lastYear = year - 1;
        var enumYear = { year: year, lastYear: lastYear };
        var enumState = { 0: '已创建', 1: '已提交', 2: '已审批', 3: '已结束' };
        var Index = $.getQueryString({ ID: "Index" });
        function onPgLoad() {
            setPgUI();
        }
        function setPgUI() {
            myData = {
                total: AimSearchCrit["RecordCount"],
                records: AimState["DataList"] || []
            };
            // 表格数据源
            store = new Ext.ux.data.AimJsonStore({
                dsname: 'DataList',
                idProperty: 'Id',
                data: myData,
                fields: [
			    { name: 'Id' }, { name: 'IndicatorNo' }, { name: 'Year' }, { name: 'StageType' }, { name: 'State' }, { name: 'Result' },
			    { name: 'DeptId' }, { name: 'DeptName' }, { name: 'Remark' }, { name: 'CreateId' }, { name: 'CreateName' }, { name: 'Summary' },
			    { name: 'CreateTime' }, { name: 'IndicatorSecondId' }, { name: 'IndicatorSecondName' }, { name: 'DeptIndicatorName' }, { name: 'Weight' }
			  ]
            });
            // 分页栏
            pgBar = new Ext.ux.AimPagingToolbar({
                pageSize: AimSearchCrit["PageSize"],
                store: store
            });
            schBar = new Ext.ux.AimSchPanel({
                store: store,
                columns: 5,
                collapsed: false,
                items: [
                { fieldLabel: '角色名称', labelWidth: 100, id: 'StageName', schopts: { qryopts: "{ mode: 'Like', field: 'StageName' }"} },
                { fieldLabel: '指标名称', id: 'LaunchUserName', schopts: { qryopts: "{ mode: 'Like', field: 'LaunchUserName' }"} }
                              ]
            });
            var columnsarray = [
                    new Ext.ux.grid.AimRowNumberer(),
                    new Ext.ux.grid.AimCheckboxSelectionModel(),
                    { id: 'IndicatorNo', dataIndex: 'IndicatorNo', header: '指标编号', width: 130 },
                    { id: 'DeptName', dataIndex: 'DeptName', header: '部门', width: 120 },
                    { id: 'DeptIndicatorName', dataIndex: 'DeptIndicatorName', header: '部门指标名称', width: 150, renderer: RowRender },
                    { id: 'IndicatorSecondName', dataIndex: 'IndicatorSecondName', header: '所属部门指标' },
                    { id: 'Weight', dataIndex: 'Weight', header: '权重', width: 80 },
                    { id: 'Year', dataIndex: 'Year', header: '年度', width: 80 },
                    { id: 'StageType', dataIndex: 'StageType', header: '阶段类型', width: 80, enumdata: enumQuater },
                    { id: 'State', dataIndex: 'State', header: '状态', width: 80, enumdata: enumState },
                    { id: 'Result', dataIndex: 'Result', header: '审批结果', width: 80 },
                    { id: 'CreateName', dataIndex: 'CreateName', header: '创建人', width: 80 },
                    { id: 'CreateTime', dataIndex: 'CreateTime', header: '创建时间', width: 100, renderer: ExtGridDateOnlyRender },
                    { id: 'Summary', dataIndex: 'Summary', header: '工作总结', width: 80, renderer: RowRender },
                    { id: 'Id', dataIndex: 'Id', header: '操作', width: 100, renderer: RowRender }
				 ];
            grid = new Ext.ux.grid.AimGridPanel({
                store: store,
                region: 'center',
                viewConfig: { forceFit: true },
                autoExpandColumn: 'DeptIndicatorName',
                columnLines: true,
                columns: columnsarray,
                cls: 'grid-row-span'
            });
            viewport = new Ext.ux.AimViewport({
                items: [grid]
            });
        }
        function onExecuted() {
            store.reload();
        }
        function opencenterwin(url, name, iWidth, iHeight) {
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2; //获得窗口的垂直位置;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2; //获得窗口的水平位置;
            window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function ShowCustomIndicator(val) {
            var task = new Ext.util.DelayedTask();
            task.delay(100, function() {
                if (Index == "0") {
                    opencenterwin("CustomIndicatorApprove.aspx?op=u&id=" + val, "", 1000, 600);
                }
                else {
                    opencenterwin("CustomIndicatorApprove.aspx?op=v&id=" + val, "", 1000, 600);
                }
            });
        }
        function DownLoad(val) {
            opencenterwin("../CommonPages/File/DownLoad.aspx?id=" + val, "", 120, 120);
        }
        function RowRender(value, cellmeta, record, rowIndex, columnIndex, store) {
            var rtn = "";
            switch (this.id) {
                case "Id":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='ShowCustomIndicator(\"" +
                                      value + "\")'>" + (Index == "0" ? "指标审批" : "查看详细") + "</label>";
                    }
                    break;
                case "DeptIndicatorName":
                    if (value) {
                        value = value || "";
                        cellmeta.attr = 'ext:qtitle =""' + ' ext:qtip ="' + value + '"';
                        rtn = value;
                    }
                    break;
                case "Summary":
                    if (value) {
                        rtn = "<label style='color:Blue; cursor:pointer; text-decoration:underline;' onclick='DownLoad(\"" + value + "\")'>工作总结</label>";
                    }
                    break;
            }
            return rtn;
        }
        function ShowDetail(val) {
            opencenterwin("UserBalanceEdit.aspx?op=u&id=" + val, "", 1000, 600);
        }            
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
</asp:Content>
