<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="YUNZHI.Management.Manage.Logout" %>

<!DOCTYPE html>
<html>
<head>
    <title>注销中，请稍后......</title>
</head>
<body>
    <p class="lead" style="text-align: center">注销中，请稍后......</p>
</body>
</html>

<script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>

<script type="text/javascript">
    $(function () {
        CloseWebPage();
    });
    $(document).ready(function () {
        CloseWebPage();
    });

    function CloseWebPage() {
        var userAgent = navigator.userAgent;
        if (userAgent.indexOf("Firefox") != -1 || userAgent.indexOf("Chrome") !=-1) {
            window.location.href = "Login.aspx";
        } else {
            window.opener = null;
            window.open("", "_self");
            window.close();
        }
    }
</script>
