<%@ Page Title="用户登录" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Aim.Portal.Web.Login" %>

<html>
<head>
<title>用户登录</title>
<meta http-equiv="Content-Type">
    <script src="/js/lib/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/js/common.js" type="text/javascript"></script>
    <script src="/js/pgform.js" type="text/javascript"></script>
    <script src="/js/lib/jquery.form.js" type="text/javascript"></script>
    <script src="/js/lib/jquery.plug-ins.js" type="text/javascript"></script>
    
    <style type="text/css">

	    body {
		    margin:0px;
		    FILTER:progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#FAFBFF,endColorStr=#C7D7FF);
		    font-size:12px;
		    color:#003399;
		    font-family:Verdana, Arial, Helvetica, sans-serif;
	    }
	    
	    .text-input
	    {
	        border:solid 1px #8FAACF;
	    }
	    
	    .lbl-message
	    {
	        color:Red;
	    }
    	
	    #main{
		    position: absolute; 
		    top:40%;
		    left:50%;
		    margin-left:-350px; 
		    margin-top:-150px;
	    }
    	
    </style>
    
    <script language="javascript" type="text/javascript">
        var islogining = false;

        function onPgLoad() {
            $(document).bind("keydown", function(e) {
                // 回车
                if (e.keyCode == 13 && !islogining) {
                    DoLogin();
                }
            });
            
            getCookie();    // 获取Cookie

            if (!$("#uname").val()) {
                $("#uname").focus();
            } else if (!$("#pwd").val()) {
                $("#pwd").focus();
            } else {
                $("#imgDoLogin").focus();
            }
        }

        function DoLogin() {
            if (islogining) {
                return;
            }
            
            setLoginStatus(true);

            if (!$("#uname").val()) {
                $("#message").text("提示：请输入用户名。");
                $("#uname").focus();

                setLoginStatus(false);
                return;
            } else {
                $("#message").text("");
            }
            
            setCookie();    // 设置Cookie

            $("form").ajaxSubmit({ data: { 'reqaction': 'login', 'asyncreq': true }, success: function(resp) {
                setLoginStatus(false);
                
                if (resp) {
                    if (resp.indexOf("success") == 0) {
                        var redurl = resp.substr("success".length + 1);
                        location.href = redurl;
                    } else {
                        $("#message").html("提示：" + resp);
                    }
                }
            }
            });
        }

        function OpenPwdChgPage() {
            rtn = OpenWin("/Modules/SysApp/OrgMag/UsrChgPwd.aspx", "_blank", CenterWin("width=350,height=180,scrollbars=yes"));
        }

        function setLoginStatus(flag) {
            if (flag) {
                islogining = true;
                $("input").attr("disabled", true);
                $("#imgDoLogin").attr("disabled", true);

                $("#span-loading").css("display", ""); // 显示进度条
            } else {
                islogining = false;
                $("input").attr("disabled", false);
                $("#imgDoLogin").attr("disabled", false);
                $("#span-loading").css("display", "none"); // 隐藏进度条
            }
        }

        function setCookie() {
            var isSaveAccount = $("#saveAcount").attr("checked");
            var isSavePassword = $("#savePassword").attr("checked");

            if (isSaveAccount) {
                $.cookie("uname", $("#uname").val());
                $.cookie("saveAcount", isSaveAccount);
            } else {
                $.cookie("uname", null, { expires: 300 });
                $.cookie("saveAcount", null, { expires: 300 });
            }

            if (isSavePassword) {
                $.cookie("pwd", $("#pwd").val());
                $.cookie("savePassword", isSavePassword);
            } else {
                $.cookie("pwd", null, { expires: 300 });
                $.cookie("savePassword", null, { expires: 300 });
            }
        }

        function getCookie() {
            var isSaveAccount = $.cookie("saveAcount");
            var isSavePassword = $.cookie("savePassword");

            if (isSaveAccount) {
                $("#saveAcount").attr("checked", true);
                $("#uname").val($.cookie("uname"));
            }

            if (isSavePassword) {
                $("#savePassword").attr("checked", true);
                $("#pwd").val($.cookie("pwd"));
            }
        }
        
    </script>
</head>
<body onload="onPgLoad()">
<form method="post">
    <div id="main" align="center">
        <div style="padding:5px; width:665; background-color:#e2eefe; border:1px solid #738DB4; height: 155px;" 
            align="center">
            <table id="__01" width="661" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="4" height="50" valign="middle"><font size="5" color="red"></font>
                        <%--<img src="images/portal/login/Login_Sliceup_01_new.gif" width="306" height="64" alt="">--%></td><!--Login_Sliceup_01.gif-->
                </tr>
                <tr>
                    <td colspan="4"> 
                        <img src="images/portal/logo.jpg" width="661" height="129" alt="">
                        </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <img src="images/portal/login/Login_Sliceup_04.gif" width="661" height="10" alt=""></td>
                </tr>
                <tr>
                    <td rowspan="2" style="background-image:url(images/portal/login/Login_Sliceup_05.gif); background-repeat:no-repeat; height:113px">
                        &nbsp;
                    </td>
                    <td colspan="2" style="background-color:#e2eefe; height:106px;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" 
                            style="height: 42px" style="padding:5px; font-size:12px;">
                          <tr>
                            <td colspan="3">
                                用户名： 
                                    <input id="uname" name="uname" class="text-input" style="width:120px;" />
                                &nbsp;&nbsp;
                                密&nbsp;&nbsp;码：
                                    <input id="pwd" name="pwd" class="text-input" type="password" style="width:120px;" />
                                &nbsp;&nbsp;
                                    <%--<input type="checkbox" name="saveAcountName" id="saveAcount" value=true />
                                    <label for="checkbox">保存帐号</label>
                                    <input type="checkbox" name="savePassword" id="savePassword" value=true />
                                    <label for="checkbox">保存密码</label>--%>
                            </td>
                          </tr>
                          <tr>
                            <td width="15%" valign="top">
                                <img id="imgDoLogin" onclick="DoLogin();" alt="" src="images/portal/login/Login_btn.png"  style="cursor:hand;" /></td>
                            <td width="20%">
                                <a href="#" onclick="OpenPwdChgPage()">修改密码</a>
                            </td>
                            <td>
                                <label class="lbl-message" id="message" name="message"></label>
                                <span id="span-loading" style="display:none;">
                                    <img src="images/portal/loading.gif" />
                                </span></td>
                          </tr>
                        </table>
                    </td>
                    <td rowspan="2" style="background-image:url(images/portal/login/Login_Sliceup_07.gif); background-repeat:no-repeat; height:113px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top"><img src="images/portal/login/Login_Sliceup_08.jpg" width="620" height="7" alt="" /></td>
                </tr>
                <tr>
                    <td><img src="images/portal/login/spacer.gif" width="21" height="1" alt=""></td>
                    <td><img src="images/portal/login/spacer.gif" width="285" height="1" alt=""></td>
                    <td><img src="images/portal/login/spacer.gif" width="335" height="1" alt=""></td>
                    <td><img src="images/portal/login/spacer.gif" width="20" height="1" alt=""></td>
                </tr>
            </table>
        </div>
    </div>
</form>
</body>
</html>
