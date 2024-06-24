<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Instraction.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Instraction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style>
        body
        {
            background-color:#f2f2f2!important;
        }
    </style>
      <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/all.css" rel="stylesheet" />
    <script src="../../assets/js/bootstrap.js"></script>
     <script type="text/javascript">


         function ErrorModal() {
             var myModal = new bootstrap.Modal(document.getElementById('ErrorModal'), { keyboard: false });
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

        <div class="container-fluid mt-5">
            <div class="row">
                <div class="col-12 offset-lg-2 offset-md-2 col-lg-8 col-md-8">
                    <div class="list-group">
                        <div class="list-group-item list-group-item-action text-uppercase text-white  bg-primary "  role="alert">Exam  instructions</div>
                        <asp:Repeater runat="server" ID="rep_Examinstruction">
                            <ItemTemplate>
                                <div class="list-group-item list-group-item-action">
                                    <p class="text-muted"><%#Eval("Exam_instractions") %><span style="float: right" class="pull-right"></span></p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="list-group-item">
                            <asp:CheckBox runat="server" ID="chkSelection" AutoPostBack="true" OnCheckedChanged="chkSelection_CheckedChanged" />  I hear by confirm to accept the exam instructions ...?
                          
                        </div>
                        <div class="list-group-item">
                            <asp:LinkButton ID="btnstartExam"  Enabled="false" runat="server" CssClass="btn btn-default  btn btn-secondary " ValidationGroup="req" OnClick="btnstartExam_Click" Text="Start Exam"></asp:LinkButton>

                        </div>
                     
                    </div>
                </div>
            </div>
        </div>


       
    </form>
</body>
</html>
