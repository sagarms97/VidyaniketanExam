<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineExam.aspx.cs" Inherits="OnlineExam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            background-color: #f2f2f2 !important;
        }
    </style>
    <script>
        function myFunction() {
            const myWindow = window.open("", "", "width=300,height=300");
            myWindow.opener.document.getElementById("demo").innerHTML = "HELLO!";
        }
    </script>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/all.css" rel="stylesheet" />
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
        <div class="container-fluid">
            <div class="row mt-5">
                <div class="col-md-2"></div>
                <div class="col-12 col-lg-8 col-md-8">
                    <div class="list-group">
                        <div class="list-group-item bg-primary text-white text-uppercase">
                            <div class="row ">
                                <div class="col-md-6">
                                    <h6 class="title"><a href="Default.aspx" style="color: white!important"><i class="fa fa-arrows-left"></i></a>&nbsp;&nbsp;<i class="fa fa-graduation-cap"></i> &nbsp;Select Exam </h6>
                                </div>
                            </div>
                        </div>
                        <div class="list-group-item list">
                            <asp:Repeater ID="rep_Exams" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnSelect" CssClass="list-group-item list-group-item-action mt-2" CommandArgument='<%#Eval("exam_code") %>' OnClick="btnSelect_Click">
                                        <div class="row">
                                            <div class="col-8">
                                                <h6><i class="fa fa-user-graduate"></i>&nbsp;&nbsp; <%#Eval("exam_name") %><span style="float: right" class="pull-right"> </h6>

                                            </div>
                                            <div class="col-4">
                                                <asp:Label runat="server" ID="btnIcon" Text=""></asp:Label>
                                            </div>

                                        </div>
                                        <span style="font-size: 13px">EXAM DATE : <%#Eval("ExamDate","{0:dd-MM-yyyy}") %> &nbsp; TIME :<%#Eval("ExamTimingFrom","{0:hh:mm tt}") %> - <%#Eval("ExamTimingTo","{0:hh:mm tt}") %> </span>

                                        <asp:Label runat="server" ID="lblExamCode" Text='<%#Eval("exam_code") %>' Visible="false"></asp:Label>
                                    </asp:LinkButton>




                                    <asp:Label runat="server" ID="lblExamDate" Visible="false" Text='<%#Eval("ExamDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblExamTimeFrom" Visible="false" Text='<%#Eval("ExamTimingFrom","{0:yyyy-MM-dd hh:mm tt}") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblExamTimeTo" Visible="false" Text='<%#Eval("ExamTimingTo","{0:yyyy-MM-dd hh:mm tt}") %>'></asp:Label>

                                </ItemTemplate>

                            </asp:Repeater>
                        </div>
                        <div class="list-group-item">
                            <asp:Label runat="server" ID="lblNoRecords" Visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="alert alert-primary d-flex align-items-center" role="alert">
      <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" class="bi bi-info-circle-fill me-2" viewBox="0 0 16 16">
         <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16zm.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2z"/>
      </svg>
      <div>
         THERE ARE NO ACTIVE TESTS OR YOU'RE NOT ALLOWED ATTEND TEST
      </div>
   </div>

                                    </div>
                                </div>
                            </asp:Label>
                        </div>


                    </div>
                </div>

                <div class="col-md-2"></div>
                <div class="col-md-2"></div>
                <div class="col-12 col-lg-8 col-md-8 mt-3">

                    <div class="list-group">
                        <div class="list-group-item">
                            <div class="alert alert-primary" role="alert">
                                <i class="fa fa-bell-slash"></i>&nbsp;&nbsp;INFORMATION
                            </div>

                            <a class="btn btn-secondary"><i class="fa fa-exclamation-circle"></i></a>--> * Exam currently disabled (gray background indicates inactive status)."
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
