<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/all.css" rel="stylesheet" />
    <script src="assets/1.11.1jquery.min.js"></script>

    <script type="text/javascript">

        history.pushState("anything", "", "#1");
        window.onhashchange = function (event) {
            window.location.hash = "";
        };
    </script>
  

    <style>
        #bg {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            /*z-index: -1;*/
        }

            #bg canvas {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
            }

        .shadow-text {
            color: white;
            text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
        }

        .header {
            /*font-family: Helvetica Neue, Helvetica, Arial, sans-serif;*/
            /*font-size: 6rem;
	font-weight:  100;
	letter-spacing: 2px;
	text-align: center;*/
            /*color: #f35626;*/
            background-image: -webkit-linear-gradient(92deg, #f35626, #feab3a);
            /*-webkit-background-clip: text;*/
            -webkit-text-fill-color: transparent;
            -webkit-animation: hue 10s infinite linear;
        }

        @-webkit-keyframes hue {
            from {
                -webkit-filter: hue-rotate(0deg);
            }

            to {
                -webkit-filter: hue-rotate(-360deg);
            }
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        
        <div id="bg1" style="background-image: url(../css/images/bg.png)"; border-image-repeat: round; background-repeat: no-repeat; background-size: cover;">
            <div class="container-fluid">
                <div class="row  list-group-item">
                    <div class="col-12 d-flex  text-center text-truncate my-auto">
                        <h3 class="text-center text-muted fw-bold text-uppercase">
                            <img src="css/images/logo.jpg" style="height: 50px; width: 50px" />
                            Vidyaniketan PU Science College, Hubballi
                        </h3>
                    </div>
                </div>
                <div class="row" style="font-size: 20px; font-weight: 800; margin-bottom: 0px; position: sticky; z-index: 999">
                    <div class="col-md-3 col-sm-2    col-lg-4     "></div>
                    <div class="col-md-6 col-sm-8    col-lg-4    ">
                        <small style="color: #ba0c9b; letter-spacing: 2px">WELCOME TO,  </small>
                        <br />

                        <h4 class="text-white " style="color: white; margin-top: 0px; font-weight: 800"><span style="color: #ff004c">ONLINE EXAM CENTER</span></h4>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-2    col-lg-4     "></div>
                    <div class="col-md-6 col-sm-8    col-lg-4    ">

                        <div class="list-group">
                            <div class="list-group-item  " style="border: groove; border-color: #ffd800">

                                <br />
                                <div class="row">

                                    <div class="col-md-12">
                                        <h2 class="    text-primary  " style="font-weight: 700">SIGN IN</h2>
                                    </div>
                                </div>
                                <br />
                                <div class="row ">
                                    <div class=" col-12 col-md-12">
                                        <div class="form-floating">
                                            <asp:TextBox ID="txtID" runat="server" CssClass="form-control   " placeholder="User ID" AutoCompleteType="Disabled"></asp:TextBox>

                                            <label for="txtID" class="text-uppercase">Student ID</label>
                                        </div>
                                        <br />
                                        <div class="form-floating">
                                            <asp:TextBox ID="txtPWD" TextMode="Password" runat="server" CssClass="form-control  " placeholder="Password" AutoCompleteType="Disabled"></asp:TextBox><br />

                                            <label for="txtPWD" class="text-uppercase">Password</label>
                                        </div>

                                        <div class="form-floating">
                                            <asp:DropDownList runat="server" ID="ddlAcademicYear" CssClass="form-select">
                                                <asp:ListItem Value="23-24" Text="23-24"></asp:ListItem>
                                                <asp:ListItem Value="24-25" Text="24-25"></asp:ListItem>

                                            </asp:DropDownList>

                                            <label for="txtPWD" class="text-uppercase">Academic Year</label>
                                        </div>





                                    </div>
                                </div>
                                <br />



                                <div class="d-grid gap-2 col-12 mx-auto ">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary " OnClick="btnLogin_Click" Text="Login" OnClientClick="this.disabled = true; this.value = 'Authenticating...';" UseSubmitBehavior="false" />

                                </div>



                            </div>
                            <br />


                        </div>



                    </div>


                    <script type="text/javascript">
                        function ErrorModal() {
                            var myModal = new bootstrap.Modal(document.getElementById('ErrorModal'), { keyboard: false });
                            myModal.show();
                        }
                        function PaymentModel() {
                            var myModal = new bootstrap.Modal(document.getElementById('PaymentModel'), { keyboard: false });
                            myModal.show();
                        }

                    </script>
                    <div class="modal fade" id="ErrorModal" data-bs-backdrop="bounce" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">

                        <div class="modal-dialog ">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="text-primary">Message</h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">

                                    <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Black"></asp:Label>
                                </div>


                            </div>

                        </div>
                    </div>
                    <script type="text/javascript" language="javascript">
                        function DisableBackButton() {
                            window.history.forward()
                        }
                        DisableBackButton();
                        window.onload = DisableBackButton;
                        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
                        window.onunload = function () { void (0) }
                    </script>

                    <script src="assets/js/bootstrap.js"></script>
                    <script src="assets/custom_js/jquery-1.4.1.min.js"></script>




                </div>

            </div>
        </div>

    </form>
</body>
</html>
