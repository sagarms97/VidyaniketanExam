<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeriesWiseReport.aspx.cs" Inherits="Pages_SeriesWiseReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SERIES REPORT</title>

    <link href="../assets/css/all.css" rel="stylesheet" />
    <link href="../../assets/css/bootstrap.css" rel="stylesheet" />
    <link href="../assets/custom_css/components.css" rel="stylesheet" />
    <script src="../../assets/js/bootstrap.js"></script>

    <script src="../assets/1.11.1jquery.min.js"></script>

    <link href="../css/toster.css" rel="stylesheet" />
    <script src="../assets/js/toster.js"></script>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:wght@800&display=swap" rel="stylesheet" />
       <script src="../Assets/js/bootstrap.js"></script>


    <script type="text/javascript">
        window.onload = function () {
            var columnCount = document.querySelectorAll(".column-count>*").length;
          
            var headerRow = document.querySelector(".header-list");
            headerRow.setAttribute("colspan", columnCount);
        }
    </script>
    <style>

        * {
            -webkit-print-color-adjust: exact !important;
        }

          .form-label{
      font-weight:700;
      text-transform:uppercase;
      font-size:14px;
  }



        body {
            background: rgb(204,204,204);
            counter-reset: chapternum figurenum;
            font-family: "Trebuchet MS", "Lucida Grande", "Lucida Sans Unicode", "Lucida Sans", Tahoma, sans-serif;
            line-height: 1.5;
            font-size: 10pt;
        }

        page {
            background: white;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
        }

            page[size="A4"] {
                width: 21cm;
                padding: 5px;
            }


        thead > tr > th {
            font-size: 0.7rem !important;
        }

        page[size="A4"][layout="landscape"] {
            width: 29.7cm;
        }

        page[size="A3"] {
            width: 29.7cm;
            height: 42cm;
        }

            page[size="A3"][layout="landscape"] {
                width: 42cm;
                height: 29.7cm;
            }

        page[size="A5"] {
            width: 14.8cm;
            height: 21cm;
        }

            page[size="A5"][layout="landscape"] {
                width: 21cm;
                height: 14.8cm;
            }

        .table td, .table th {
            vertical-align: middle;
            font-family: "Trebuchet MS", "Lucida Grande", "Lucida Sans Unicode", "Lucida Sans", Tahoma, sans-serif;
            font-size: 10pt;
        }

        .trow th {
            background-color: #e3e3e3 !important;
            color: #000;
        }

        .titem td {
            background-color: #f2f2f2 !important;
            color: #000;
        }

        @media print {
            * {
                -webkit-print-color-adjust: exact !important;
            }

            .titem td {
                background-color: #f2f2f2 !important;
                color: #000;
            }

            #gridFeedback {
                -webkit-print-color-adjust: exact;
            }
               .cl-mg-img{
               position:absolute;
               top:25px;
               left:2vw;

            }


            body {
                font-family: "Trebuchet MS", "Lucida Grande", "Lucida Sans Unicode", "Lucida Sans", Tahoma, sans-serif;
                line-height: 1.5;
                font-size: 11pt;
            }

            .no-print {
                display: block !important;
            }

            .hide-print {
                display: none !important;
            }

            .trow th {
                background-color: #f2f2f2 !important;
                color: #000;
                -webkit-print-color-adjust: exact;
            }





            .table {
                border-collapse: collapse;
                border: 2px solid rgb(200, 200, 200);
            }

                .table td,
                .table th {
                    border: 1px solid rgb(190, 190, 190);
                    padding: 5px 10px;
                    vertical-align: middle;
                }





            .trow th {
                background-color: #e3e3e3 !important;
                color: #000;
                -webkit-print-color-adjust: exact;
            }
        }
    </style>
     <script type="text/javascript">
         function ErrorModal() {
             var myModal = new bootstrap.Modal(document.getElementById('exampleModal'), { keyboard: false });
             myModal.show();
         }

     </script>
</head>

<body>
    <form id="form1" runat="server">
       

     
    <div id="ErrorModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">

                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <asp:Label ID="lblErrorMsg" runat="server" CssClass="text-uppercase" Text=""></asp:Label>
                </div>

            </div>

        </div>
    </div>
        
        <div class="list-group navbar-fixed-top hide-print bg-light-dark">
            <div class="list-group-item" style="border-radius: 0px">
                <div class="row">
                    <div class="col-md-9" style="font-size: 20px;">
                        <div class="row g-1">
                            <div class="col-md-4">
                                <span class="form-label">SELECT EXAM *</span>
                                <asp:DropDownList runat="server" ID="ddlExam" CssClass="form-select form-select-sm border-1 border-primary"></asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <span class="form-label">SELECT SERIES *</span>
                                <asp:DropDownList runat="server" ID="ddlSeries" CssClass="form-select form-select-sm border-1 border-primary"></asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <br />
                                <asp:LinkButton runat="server" ID="btnGenerate" OnClick="btnGenerate_Click" CssClass="btn btn-primary btn-sm ">GENERATE</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row g-1">
                            <div class="col-md-6">
                                  <br />
                                <a onclick="window.print()" class="btn btn-success btn-sm btn-block w-100 ">&#x2399;&nbsp;Print / PDF</a>
                            </div>
                            <div class="col-md-6">
                                  <br />
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Export_Click_Click" CssClass="btn btn-danger btn-sm btn-block w-100 ">Excel </asp:LinkButton>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>

           <div class="hide-print">
            <br />
            <br />
            <br />
        </div>
        <div id="ExportDiv" runat="server">

            <page size="A4">

                <table class="table table-sm table-bordered" border="1">
                    <thead>
                        <tr>
                            <th class="header-list">
                                <asp:Label runat="server" ID="lblHeader"></asp:Label>
                            </th>
                        </tr>
                        <tr class="column-count">
                            <th>SLNO </th>
                            <th>SUBJECT</th>
                            <th>QUESTION </th>
                            <th class="text-center">OPTION A</th>
                            <th class="text-center">OPTINO B </th>
                            <th class="text-center">OPTION C </th>
                            <th class="text-center">OPTION D </th>
                         
                               <th class="text-center">ANSWER </th>
                        </tr>
                    </thead>

                    <asp:Repeater runat="server" ID="rptReportContent">
                        <HeaderTemplate>
                            <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("Seriesquestion_code") %></td>
                                <td><%#Eval("name")%></td>
                                <td><%#Eval("question")%></td>
                                <td class="text-center"><%#Eval("option_a")%></td>
                                <td class="text-center"><%#Eval("option_b")%></td>
                                <td class="text-center"><%#Eval("option_c")%></td>
                                <td class="text-center"><%#Eval("option_d")%></td>
                                <td class="text-center"><%#Eval("answer")%></td>

                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>



            </page>

        </div>



      


       
    </form>
</body>
</html>
