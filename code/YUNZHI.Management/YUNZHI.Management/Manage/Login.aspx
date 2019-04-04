<%@ Page Title="" Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Login.aspx.cs" Inherits="YUNZHI.Management.Manage.Login" %>

<!DOCTYPE HTML>
<html>
<head>
    <meta charset="utf-8">
    <meta name="renderer" content="webkit|ie-comp|ie-stand">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <!--[if lt IE 9]>
<script type="text/javascript" src="lib/html5shiv.js"></script>
<script type="text/javascript" src="lib/respond.min.js"></script>
<![endif]-->
    <link href="static/h-ui/css/H-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="static/h-ui.admin/css/H-ui.login.css" rel="stylesheet" type="text/css" />
    <link href="static/h-ui.admin/css/style.css" rel="stylesheet" type="text/css" />
    <link href="lib/Hui-iconfont/1.0.8/iconfont.css" rel="stylesheet" type="text/css" />
    <!--[if IE 6]>
<script type="text/javascript" src="lib/DD_belatedPNG_0.0.8a-min.js" ></script>
<script>DD_belatedPNG.fix('*');</script>
<![endif]-->
    <title>登录</title>
    <style type="text/css">
        .mybtn {
            margin-bottom: 40px;
        }
    </style>
    <script type="text/javascript">
        function S4() { return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1); }
        function NewGuid() { return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4()); }

        //processing
        function Processing() {
            //debugger;
            try {
                var wh = $WH(), ww = $WW();
                var processingBg = document.createElement("DIV");
                processingBg.id = "ProcessingBg";
                processingBg.className = "processing-bg";
                processingBg.style.top = ((wh / 2) - 300) + "px";
                processingBg.style.left = (ww / 2 - 20) + "px";
                //processingBg.innerHTML = "处理中...";

                //    processingBg.style.marginTop = document.documentElement.scrollTop+"px";

                var processingShield = document.createElement("DIV");
                processingShield.id = "processingShield";
                processingShield.className = "processing-shield";
                processingShield.style.height = wh + "px";
                processingShield.style.width = ww + "px";

                var processingIfm = document.createElement("IFRAME");
                processingIfm.id = "ifm";
                processingIfm.className = "processing-ifm";
                processingIfm.style.height = wh + "px";
                processingIfm.style.width = ww + "px";
                processingIfm.src = "temp.htm";

                document.body.appendChild(processingIfm);
                document.body.appendChild(processingBg);
                document.body.appendChild(processingShield);
                this.setOpacity = function (obj, opacity) {
                    if (opacity >= 1)
                        opacity = opacity / 100;
                    try {
                        obj.style.opacity = opacity;
                    }
                    catch (e) { }
                    try {
                        if (obj.filters.length > 0 && obj.filters("alpha"))
                            obj.filters("alpha").opacity = opacity * 100;
                        else
                            obj.style.filter = "alpha(opacity=\"" + (opacity * 100) + "\")";
                    }
                    catch (e) { }
                }
                var c = 0;
                this.doAlpha = function () {
                    if (++c > 20) {
                        clearInterval(ad);
                        return 0;
                    }
                    setOpacity(processingShield, c);
                }
                var ad = setInterval("doAlpha()", 1);

                document.body.onselectstart = function () { return false; }
                document.body.oncontextmenu = function () { return false; }
            }
            catch (err) { }
        }
        //page height
        function $WH() {
            return window.screen.height < document.body.scrollHeight ? document.body.scrollHeight + 18 : window.screen.height;
        }
        //page width
        function $WW() {
            return window.screen.width - 21;
        }

        function closePop() {
            var empname = document.getElementById("username").value;

            if (empname == "" || empname == "用户名") {
                alert("请输入用户名!");
                document.getElementById("username").focus();
                return false;
            }

            if (document.getElementById("password").value == "" || document.getElementById("password").value == "密码") {
                alert("请输入密码!");
                document.getElementById("password").focus();
                return false;
            }

            Processing();
            return true;
        }
    </script>
</head>
<body>
    <form id="login_Form" runat="server">
        <input type="hidden" id="TenantId" name="TenantId" value="" />

        <div class="loginWraper">
            <div id="loginform" class="loginBox">
                    <div class="row cl mybtn">
                        <label class="form-label col-xs-3" style="text-align:right;"><i class="Hui-iconfont">&#xe60d;</i></label>
                        <div class="formControls col-xs-8">
                            <%--<input id="" name="" type="text" placeholder="账户" class="input-text size-L">--%>

                            <asp:TextBox class="input-text size-L" name="login_account" id="username" value="" placeholder="用户名" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row cl mybtn">
                        <label class="form-label col-xs-3" style="text-align:right;"><i class="Hui-iconfont">&#xe60e;</i></label>
                        <div class="formControls col-xs-8">
<%--                            <input id="" name="" type="password" placeholder="密码" class="input-text size-L">--%>

                            <asp:TextBox class="input-text size-L" name="login_pwd" id="password" placeholder="密码"  type="password" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row cl mybtn">
                        <div class="formControls col-xs-8 col-xs-offset-3" style="display: none;">
                            <input class="input-text size-L" type="text" placeholder="验证码" onblur="if(this.value==''){this.value='验证码:'}" onclick="if (this.value == '验证码:') { this.value = ''; }" value="验证码:" style="width: 150px;">
                            <img src="">
                            <a id="kanbuq" href="javascript:;">看不清，换一张</a>
                        </div>
                    </div>
                    <div class="row cl mybtn">
                        <div class="formControls col-xs-8 col-xs-offset-3" style="display: none;">
                            <label for="online">
                                <input type="checkbox" name="online" id="online" value="">
                                使我保持登录状态</label>
                        </div>
                    </div>
                    <div class="row cl mybtn">
                        <div class="formControls col-xs-8 col-xs-offset-3">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-success radius size-L" runat="server" OnClientClick="return closePop();" OnClick="btnSubmit_Click" Text="&nbsp;登&nbsp;&nbsp;&nbsp;&nbsp;录&nbsp;" />
                           <%-- <button type="submit" class="btn btn-success radius size-L">&nbsp;登&nbsp;&nbsp;&nbsp;&nbsp;录&nbsp;</button>
                            <button type="reset" class="btn btn-default radius size-L">&nbsp;取&nbsp;&nbsp;&nbsp;&nbsp;消&nbsp;</button>--%>
                        </div>
                    </div>
            </div>
        </div>
        <%--<div class="footer">Copyright 云智医疗</div>--%>
        <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>
        <script type="text/javascript" src="static/h-ui/js/H-ui.min.js"></script>
    </form>
</body>
</html>
