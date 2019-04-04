<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="YUNZHI.Management.Manage.Welcome" %>

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
    <link rel="stylesheet" type="text/css" href="static/h-ui/css/H-ui.min.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/H-ui.admin.css" />
    <link rel="stylesheet" type="text/css" href="lib/Hui-iconfont/1.0.8/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/skin/default/skin.css" id="skin" />
    <link rel="stylesheet" type="text/css" href="static/h-ui.admin/css/style.css" />

    <script type="text/javascript" src="lib/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="static/h-ui/js/H-ui.min.js"></script>

    <script type="text/javascript" src="highcharts/highcharts.js"></script>
    <script type="text/javascript" src="highcharts/exporting.js"></script>
    <script type="text/javascript" src="highcharts/drilldown.js"></script>
    <script type="text/javascript" src="highcharts/no-data-to-display.js"></script>

    <script type="text/javascript" src="lib/My97DatePicker/4.8/WdatePicker.js"></script>
    <!--[if IE 6]>
    <script type="text/javascript" src="lib/DD_belatedPNG_0.0.8a-min.js" ></script>
    <script>DD_belatedPNG.fix('*');</script>
    <![endif]-->
    <title>我的桌面</title>
</head>
<body>
    <form runat="server">
        <div class="page-container">
            <p class="f-20 text-success">欢迎使用云智后台！</p>
            <table class="table table-border table-bordered table-bg">
                <tr>
                    <td width="100%">查询日期:
                    <asp:TextBox ID="txtStartDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd' ,maxDate:'#F{$dp.$D(\'txtEndDate\')}'});" class="input-text Wdate" Style="width: 120px;24px !important" runat="server"></asp:TextBox>
                        -<asp:TextBox ID="txtEndDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd' ,minDate:'#F{$dp.$D(\'txtStartDate\')}'});" class="input-text Wdate" Style="width: 120px;24px !important" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="lbtnCheck" class="btn btn-success" style="float:right;margin-right:10px;"  runat="server" OnClick="lbtnCheck_Click">查询</asp:LinkButton>
                        <asp:LinkButton ID="btnReset" class="btn btn-success" style="float:right;margin-right:10px;" runat="server" OnClick="btnReset_Click">重置</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td width="50%">
                        <div id="healthChart">
                        </div>
                    </td>
                    <td width="50%">
                        <div id="deviceChart">
                        </div>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            $(function () {
                $('#healthChart').highcharts({
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: '各医院就诊人数统计'
                    },
                    subtitle: {
                        text: '云智平台'
                    },
                    xAxis: {
                        labels: {    
                            rotation: -45
                        },
                        categories: [
                            <%=HealthName %>
                        ],
                        crosshair: true
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: '就诊量(人)'
                        }
                    },
                    tooltip: {
                        // head + 每个 point + footer 拼接成完整的 table
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.0f} 人</b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    },
                    plotOptions: {
                        column: {
                            borderWidth: 0
                        }
                    },
                    series: [{
                        name: '就诊人数',
                        data: [<%=HealthCount %>]
                }]
                });
            });

            $('#deviceChart').highcharts({
                title: {
                    text: '设备使用情况统计'
                },
                tooltip: {
                    headerFormat: '{series.name}<br>',
                    pointFormat: '{point.name}: <b>{point.percentage:.2f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,  // 可以被选择
                        cursor: 'pointer',       // 鼠标样式
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.2f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        },
                        showInLegend: true // 设置饼图是否在图例中显示
                    }
                },
                series: [{
                    type: 'pie',
                    name: '设备使用情况占比',
                    data: [
                           <%=DeviceData %>
                    ]
                }]
            });
        </script>

        <%--<footer class="footer mt-20">
	        <div class="container">
		        <p>本后台系统由<a href="#" target="_blank" title="云智平台">云智平台</a>提供前端技术支持</p>
	        </div>
        </footer>--%>
    </form>
</body>
</html>
