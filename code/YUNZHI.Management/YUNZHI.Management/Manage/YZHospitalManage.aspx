<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/List.Master" AutoEventWireup="true" CodeBehind="YZHospitalManage.aspx.cs" Inherits="YUNZHI.Management.Manage.YZHospitalManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <link rel="stylesheet" href="css/normalize3.0.2.min.css" />
    <link rel="stylesheet" href="css/css.css?v=27" />
    <link href="css/frame.css" rel="stylesheet" />
    <link href="css/frame1.css" rel="stylesheet" />
    <link href="css/frame2.css" rel="stylesheet" />

    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
            list-style: none;
        }

        body {
            font-size: 12px;
        }

        .main {
            width: 500px;
            height: 100%;
            position: fixed;
            top: 0px;
            border: 1px solid #ccc;
            z-index: 10000;
            opacity: 0.9;
            background-color: #EFEEF0;
            right: -502px;
        }

        *html .main {
            position: absolute;
            top: expression(eval(document.documentElement.scrollTop));
            margin-top: 320px;
        }

        .main2 {
            width: 480px;
            height: 100%;
            position: relative;
            padding: 10px;
        }



        .bar {
            width: 25px;
            height: 105px;
            position: absolute;
            left: -25px;
            top: -1px;
            background: url(images/mini_bg.png) no-repeat;
            display: block;
            background-position: -25px 0px;
        }

        .WB_row_r4 li {
            width: 33.3%;
        }

        .line {
            font-size: 10px;
        }

        .WB_feed_detail {
            padding: 10px;
        }

        .tabs {
            width: 460px;
            float: none;
            list-style: none;
            position: relative;
            text-align: left;
        }

            .tabs li {
                float: left;
                display: block;
            }

            .tabs input[type="radio"] {
                position: absolute;
                top: -9999px;
                left: -9999px;
            }

            .tabs label {
                display: block;
                padding: 8px 40px;
                border-radius: 2px 2px 0 0;
                font-size: 16px;
                font-weight: normal;
                text-transform: uppercase;
                background: #f9f4f4;
                cursor: pointer;
                position: relative;
                top: 4px;
                -webkit-transition: all 0.2s ease-in-out;
                -moz-transition: all 0.2s ease-in-out;
                -o-transition: all 0.2s ease-in-out;
                transition: all 0.2s ease-in-out;
            }

                .tabs label:hover {
                    background: #a9a8a8;
                }

            .tabs .tab-content {
                z-index: 2;
                display: none;
                width: 100%;
                height: 400px;
                font-size: 12px;
                line-height: 25px;
                padding: 10px;
                position: absolute;
                top: 36px;
                left: 0;
                background: #dcdcdc;
            }

            .tabs [id^="tab"]:checked + label {
                top: 0;
                padding-top: 10px;
                background: #dcdcdc;
            }

            .tabs [id^="tab"]:checked ~ [id^="tab-content"] {
                display: block;
            }

        .select {
            width: 48% !important;
        }
    </style>

    <script type="text/javascript">
        function removeIframe1() {
            debugger;
            var topWindow = $(window.parent.document);
            var iframe = topWindow.find('#iframe_box .show_iframe');
            var tab = topWindow.find(".acrossTab li");
            var showTab = topWindow.find(".acrossTab li.active");
            var showBox = topWindow.find('.show_iframe:visible');
            var i = showTab.index();
            tab.eq(i - 1).addClass("active");
            iframe.eq(i - 1).show();
            tab.eq(i).remove();
            iframe.eq(i).remove();
        }
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <article class="page-container">
        <div class="row cl">
            <label class="form-label col-xs-4 col-sm-2">机构代码<span class="c-red">*</span>：</label>
            <div class="formControls col-xs-8 col-sm-9">
                <asp:TextBox ID="txtHCode" runat="server" CssClass="input-text radius txtproblem" datatype="*" nullmsg="机构代码不能为空"></asp:TextBox>
            </div>
        </div>
        <div class="row cl">
            <label class="form-label col-xs-4 col-sm-2">机构名称<span class="c-red">*</span>：</label>
            <div class="formControls col-xs-8 col-sm-9">
                <asp:TextBox ID="txtHName" runat="server" CssClass="input-text radius txtproblem" datatype="*" nullmsg="机构名称不能为空"></asp:TextBox>
            </div>
        </div>
        <div class="row cl">
            <label class="form-label col-xs-4 col-sm-2">区域：</label>
            <div class="formControls col-xs-8 col-sm-9">
                <%--  <asp:DropDownList ID="ddlAreaType" AutoPostBack="true" runat="server" CssClass="select-box select radius" size="1"></asp:DropDownList>--%>

                <asp:DropDownList ID="ddlProvince" runat="server" CssClass="select-box select radius" size="1" Style="width: 120px" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="ddlUrban" runat="server" CssClass="select-box select radius" size="1" Style="width: 120px"></asp:DropDownList>

            </div>
        </div>
        <div class="row cl">
            <div class="col-xs-8 col-sm-9 col-xs-offset-4 col-sm-offset-2">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary radius" Text="提交" OnClick="btnSave_Click" />
                <asp:Label ID="lbText" runat="server" Style="color: red;" Visible="false" CssClass="ml-10" Text=""></asp:Label>
                <button onclick="removeIframe1();" class="btn btn-default radius" style="display: none" type="button">&nbsp;&nbsp;取消&nbsp;&nbsp;</button>
            </div>
        </div>

    </article>
    <!--请在下方写此页面业务相关的脚本-->

    <script type="text/javascript" src="lib/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="lib/webuploader/0.1.5/webuploader.min.js"></script>
    <script type="text/javascript" src="lib/ueditor/1.4.3/ueditor.config.js"></script>
    <script type="text/javascript" src="lib/ueditor/1.4.3/ueditor.all.min.js"> </script>
    <script type="text/javascript" src="lib/ueditor/1.4.3/lang/zh-cn/zh-cn.js"></script>

    <script type="text/javascript">

        $(function () {
            $.Huihover('.page-container');
        });
        function ClosePage(val) {
            parent.RefreshWeb(); //刷新父页面

            var str = "";
            if (val == 1) {
                str = "保存成功";
            }
            if (val == 2) {
                str = "修改成功";
            }
            if (val == 3) {
                str = "终止成功";
            }
            alert(str);
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
            layer.msg(str, { icon: 6, time: 2000 });
        }
    </script>
    <!--/请在上方写此页面业务相关的脚本-->
</asp:Content>
