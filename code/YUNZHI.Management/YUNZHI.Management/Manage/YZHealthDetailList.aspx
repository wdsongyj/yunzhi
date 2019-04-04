<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/List.Master" AutoEventWireup="true" CodeBehind="YZHealthDetailList.aspx.cs" Inherits="YUNZHI.Management.Manage.YZHealthDetailList" %>

<%@ Register Assembly="AspNetPager, Version=7.4.2.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    患者管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">

    <link rel="stylesheet" href="css/normalize3.0.2.min.css" />
    <link rel="stylesheet" href="css/css.css?v=24" />
    <style type="text/css">
        .text-serch {
            margin-left: 10px;
            margin-right: 5px;
        }

        .text-serch-f {
            margin-right: 5px;
        }
    </style>

    <script type="text/javascript">
        (function () {
            $('.table-sort').dataTable({
                "aaSorting": [[1, "desc"]],//默认第几个排序
                "bStateSave": true,//状态保存
                "aoColumnDefs": [
                  //{"bVisible": false, "aTargets": [ 3 ]} //控制列的隐藏显示
                  { "orderable": false, "aTargets": [0, 8] }// 不参与排序的列
                ]
            });
        });

        /*添加*/
        function article_add(title, url, w, h) {
            var index = layer.open({
                type: 2,
                title: title,
                content: url + "?IsEdit=1"
            });
            layer.full(index);
        }
        /*编辑*/
        function article_edit(title, url, id, w, h) {
            var index = layer.open({
                type: 2,
                title: title,
                content: url + "?ID=" + id + "&IsEdit=2"
            });
            layer.full(index);
        }

        /*查看*/
        function article_view(title, url, id, w, h) {
            var index = layer.open({
                type: 2,
                title: title,
                content: url + "?ID=" + id + "&IsEdit=3"
            });
            layer.full(index);
        }

        /*删除*/
        function article_del(obj, id) {
            if (!confirm('确认要删除吗？')) {
                return false;
            }
            return true;
        }


        function GetSelectAllCheckBox(obj) {
            var value = $('#<%=chdSelectedItems.ClientID%>').val();;
            var cbxList = document.getElementsByTagName("input");
            for (var i = 0; i < cbxList.length; i++) {
                var tmpCB = cbxList[i];
                if (tmpCB.type == "checkbox") {
                    if (tmpCB.id == "cbId") {
                        if (obj.checked) {
                            tmpCB.checked = true;
                            if (value.indexOf(tmpCB.getAttribute("ubi") + ";") < 0) {
                                value += tmpCB.getAttribute("ubi") + ";"
                            }
                        } else {
                            tmpCB.checked = false;
                            if (value.indexOf(tmpCB.getAttribute("ubi") + ";") >= 0) {
                                value = value.replace(tmpCB.getAttribute("ubi") + ";", "");
                            }
                        }
                    }
                }
            }
            $('#<%=chdSelectedItems.ClientID%>').val(value);
            return value;
        }
        function GetSelectCheckBox(obj) {

            var value = $('#<%=chdSelectedItems.ClientID%>').val();
            if (obj.checked) {
                if (value.indexOf(obj.getAttribute("ubi") + ";") < 0) {
                    value += obj.getAttribute("ubi") + ";"
                }


            } else {
                if (value.indexOf(obj.getAttribute("ubi") + ";") >= 0) {
                    value = value.replace(obj.getAttribute("ubi") + ";", "");
                }
            }
            $('#<%=chdSelectedItems.ClientID%>').val(value);
        }

        function checkBatchDelete() {

            if ($('#<%=chdSelectedItems.ClientID%>').val() == "") {
                alert("请选择要批量删除的问题！");
                return false;
            }
            if (!confirm('确认要批量删除当前选中的问题，批量删除后将不能再次显示？')) {
                return false;
            }
            return true;
        }

        //刷新本页面
        function RefreshWeb() {
            location.replace(location.href);
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 患者管理 <a class="btn btn-success radius r" style="line-height: 1.6em; margin-top: 3px" href="javascript:location.replace(location.href);" title="刷新"><i class="Hui-iconfont">&#xe68f;</i></a></nav>
    <div class="page-container">
        <div class="text-c">
            <span class="text-serch-f">患者姓名：</span><asp:TextBox ID="txtPTName" runat="server" placeholder=" " Style="width: 250px" class="input-text"></asp:TextBox>
            <span class="text-serch-f">患者身份证号：</span><asp:TextBox ID="txtIDNumber" runat="server" placeholder=" " Style="width: 250px" class="input-text"></asp:TextBox>
            <span class="text-serch" style="display:none;">医疗机构：</span><asp:DropDownList ID="ddlHName" runat="server" CssClass="select-box select radius" size="1" Style="width: 120px;display:none;"></asp:DropDownList>
            <span class="text-serch">看病时间：</span>
            <asp:TextBox ID="txtStartDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" class="input-text Wdate" Style="width: 120px;" runat="server"></asp:TextBox>
            -<asp:TextBox ID="txtEndDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" class="input-text Wdate" Style="width: 120px;" runat="server"></asp:TextBox>
            <asp:LinkButton ID="lbtnCheck" class="btn btn-success" runat="server" OnClick="lbtnCheck_Click"><i class="Hui-iconfont">&#xe665;</i> 查询</asp:LinkButton>
        </div>

        <div class="cl pd-5 bg-1 bk-gray mt-20">
            <span class="l">
                <asp:HiddenField ID="chdSelectedItems" runat="server" />
                <%-- <a class="btn btn-primary radius ml-5" onclick="article_add('添加','YZDeviceManage.aspx')" href="javascript:;" title="添加"><i class="Hui-iconfont">&#xe600;</i> 添加设备</a>--%>
            </span>
            <span class="l ml-10">
                <%--   <asp:LinkButton ID="lbBatchDelete" class="btn btn-primary ml-10 radius" runat="server" OnClientClick="return checkBatchDelete();" OnClick="lbBatchDelete_Click"><i class="Hui-iconfont">&#xe6e2;</i> 批量删除</asp:LinkButton>&nbsp;--%>
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                </asp:DropDownList>
            </span>
        </div>

        <div class="mt-20">
            <table class="table table-border table-bordered table-bg table-hover table-sort">
                <thead>
                    <tr class="text-c">
                        <th width="5%">
                            <input type="checkbox" name="" onclick="GetSelectAllCheckBox(this);" value=""></th>
                        <th width="15%">姓名</th>
                        <th width="5%">性别</th>
                        <th width="15%">身份证号</th>
                        <th width="15%">出生年月</th>
                        <th width="20%">家庭住址</th>
                        <th width="10%">联系电话</th>
                        <th width="15%">就医时间</th>
          
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RepLetter" runat="server">
                        <ItemTemplate>
                            <tr class="text-c">
                                <td>
                                    <input type="checkbox" value="" id="cbId" ubi="<%# Eval("PTID") %>" onclick="GetSelectCheckBox(this);" name="ubiid"></td>
                                <td>
                                    <%# Eval("PTName") %>
                                </td>
                                <td><%# Eval("PTSex") %></td>
                                <td><%# Eval("PTIDNumber") %></td>
                                <td><%# Eval("PTBrithday") %></td>
                                <td class="text-l" ><%# Eval("PTAddress") %></td>
                                <td><%# Eval("PTTelPhone") %></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "PTCreated","{0:yyyy-MM-dd HH:mm:ss}")%></td>
                                
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="jxbottom">
            <table width="100%">
                <tr align="center">
                    <td align="center" style="width: 100%"><%-- 当前页 %CurrentPageIndex%--%>
                        <webdiyer:AspNetPager ID="pagerbind" runat="Server" PageSize="10" OnPageChanged="pageBind_PageChange"
                            CustomInfoHTML="共 %PageCount% 页    共 %RecordCount% 条记录" ShowCustomInfoSection="Right"
                            LastPageText="最后一页" FirstPageText="第一页" PrevPageText="上一页" NextPageText="下一页"
                            ShowPageIndexBox="Always" AlwaysShow="true" SubmitButtonText="转到" AlwaysShowFirstLastPageNumber="True"
                            CssClass="pages" CurrentPageButtonClass="cpb" PageIndexBoxClass="textreport" SubmitButtonClass="buttonreport" CustomInfoClass="cis" NumericButtonCount="8">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
