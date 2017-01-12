
<%@ Page Title="数据导入" Language="C#" MasterPageFile="~/Masters/Ext/formpage.Master" AutoEventWireup="true" CodeBehind="DataImport.aspx.cs" Inherits="Aim.Portal.Web.CommonPages.DataImport" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        
        function onPgLoad() {
            setPgUI();
        }

        function setPgUI() {            
            //绑定按钮验证
            FormValidationBind('btnSubmit', SuccessSubmit);

            $("#btnCancel").click(function() {
                window.close();
            });
        }

        //验证成功执行保存方法
        function SuccessSubmit() {
            AimFrm.submit("import", {}, null, SubFinish);
        }

        function SubFinish(args) {
            if (window.onProcessFinish) {
                window.onProcessFinish.call(this, args);
            }
        
            window.close();
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyHolder" runat="server">
    <div id="header"><h1>数据导入</h1></div>
    
    <div id="editDiv" align="center">
        <table class="aim-ui-table-edit">
            <tbody>
                <tr>
                    <td class="aim-ui-td-caption">
                        模版编码
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="Code" name="Code" readonly class="validate[required]" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">
                        模版名称
                    </td>
                    <td class="aim-ui-td-data">
                        <input id="Name" name="Name" readonly class="validate[required]" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-td-caption">数据文件文件</td>
                    <td class="aim-ui-td-data" valign="top">
                        <input id="DataFileID" name="DataFileID" class="validate[required]" aimctrl='file' mode="single" Filter="Excel文件 (*.xls;*.xlsx)|*.xls;*.xlsx" />
                    </td>
                </tr>
                <tr>
                    <td class="aim-ui-button-panel" colspan="4">
                        <a id="btnSubmit" class="aim-ui-button">导入数据</a>
                        <a id="btnCancel" class="aim-ui-button">取消</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>


