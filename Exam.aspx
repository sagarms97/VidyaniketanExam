<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exam.aspx.cs" Inherits="Exam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        var timerInterval;

        function startTimer() {
            var hiddenField = document.getElementById('<%= hdnTimerSeconds.ClientID %>');

            // Get the current timer value from the hidden field or set a default value
            var seconds = parseInt(hiddenField.value) || 300; // 300 seconds = 5 minutes

            // Update the hidden field with the current timer value
            hiddenField.value = seconds;

            // Display the initial timer value
            updateTimerDisplay(seconds);

            // Start the timer interval
            timerInterval = setInterval(function () {
                seconds--;

                // Update the hidden field with the current timer value
                hiddenField.value = seconds;

                // Display the updated timer value
                updateTimerDisplay(seconds);

                // Check if the timer has reached 0
                if (seconds <= 0) {
                    clearInterval(timerInterval);
                    alert('Timer has reached 0!');
                }
            }, 1000); // Update every 1 second
        }

        function updateTimerDisplay(seconds) {
            // Display the timer value wherever you need it
            var timerDisplay = document.getElementById('timerDisplay');
            if (timerDisplay) {
                var minutes = Math.floor(seconds / 60);
                var remainingSeconds = seconds % 60;
                timerDisplay.innerHTML = minutes + ':' + (remainingSeconds < 10 ? '0' : '') + remainingSeconds;
            }
        }

        // Call startTimer when the page loads
        window.onload = startTimer;
    </script>
    <style>
        .answer {
            background: none repeat scroll 0 0 #008000;
            border: 2px solid #ffffff;
            border-radius: 50%;
            color: #fff;
            font-size: 28px;
            font-weight: bold;
            padding: 1px 15px;
        }

        .notanswer {
            background: none repeat scroll 0 0 #ea0622;
            border: 2px solid #ffffff;
            border-radius: 50%;
            color: #fff;
            font-size: 28px;
            font-weight: bold;
            padding: 1px 15px;
        }

        body {
            background-color: #f2f2f2 !important;
        }

        .bg {
            background: linear-gradient(180deg, #fbf1d5 0%, #ffffff 100%);
        }

        .bg1 {
            background: linear-gradient(225deg, #d7f3fc 0%, #fbdfd6 100%);
            /*background-image: url(css/images/bg.png)*/
        }
    </style>
    <link href="assets/custom_css/components.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/all.css" rel="stylesheet" />
    <script src="assets/1.11.1jquery.min.js"></script>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

     <script type="text/javascript">
         function callPageMethod() {
             PageMethods.FetchRepeaterData(<%:rep_List as Repeater%>,<%:lblTotalRecords as Label%>, onSuccess, onError);
         }

         function onSuccess(result) {
             alert(result);
         }

         function onError(error) {
             alert('An error occurred: ' + error.get_message());
         }
     </script>

</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager runat="server" EnablePageMethods="true" ></asp:ScriptManager>
        <div class="list-group">
            <div class="list-group-item bg">
                <small style="float: left">
                    <asp:Image ID="Image2" runat="server" ImageUrl="css/images/logo.jpg" Height="65px" Width="65px" /></small>
                <small style="float: right">
                    <asp:Image ID="Image1" runat="server" ImageUrl="assets/images/NoImage.png" Height="65px" Width="65px" /></small>

                <h5 class="text-center text-uppercase" style="color: GrayText; font-weight: bold; text-align: center; font-size: 14px; line-height: normal">CES Foundation <small style="float:right;margin-right:5px">  Time remaining : <div style="text-align: right;margin-right:5px" id="timerDisplay"></div></small></h5>
                <h3 class="text-center text-uppercase" style="color: #e60000; font-weight: 700; line-height: 18px; letter-spacing: 0px">Exam of NEET - 2024</h3>
              

            </div>
            <div class="list-group-item text-uppercase" style="background-image: url(css/images/bg.png)">
                <small class="text-left text-uppercase " style="float: left; color: purple; font-size: 14px; font-weight: 600">
                     <b style="color:cadetblue"> Student Name : Vishnu G Pawar</b></small>
                <small class="text-left " style="float: right; color: purple; text-align: center; font-size: 14px; font-weight: 600"><b style="color:cadetblue"> Student Id No : 501CS14031</b></small>

                <br />

                <small style="float: left; color: purple; font-size: 14px; font-weight: 600"><b style="color:cadetblue"> HallTicket No : 501CS14031</b></small>
                <small style="float: right; color: purple; font-size: 14px; font-weight: 600;"><b style="color:cadetblue"> Roll Number : 20242515</b></small>

            </div>
             <div class="list-group-item">
                  <asp:Repeater ID="Rep_SubjectsSection" runat="server">
                                <ItemTemplate>
                                  
                                     <asp:LinkButton ID="btn_selectSubject" runat="server" Text='<%#Eval("name")+" (SECTIONI)" %>' CommandArgument='<%# Eval("code") %>' 
                                        CssClass='<%# Eval("code").ToString()==lblCurrentPage.Text ? "btn btn-sm btn-outline-success mx-1 col-md-2" :" btn mx-1 btn-sm btn-outline-secondary col-md-2"%>'
                                        OnClick="btn_selectSubject_Click"></asp:LinkButton>
                                      
                                    </ItemTemplate>
                      </asp:Repeater>
                 <h5 class="text-uppercase" style="float:right;color:#b41bb4;font-weight:800;margin-right:35px">Objective Questions</h5>

                 </div>
            <div class="list-group-item">
                <div class="row">
                    <div class="col-md-9">
                        <table class="list-group">
                            <asp:Repeater ID="Rep_Exam" runat="server">

                                <ItemTemplate>

                                    <tr>
                                        <td>
                                            <h5 class="d-flex">
                                               <%#Container.ItemIndex+1 %> : <%#Eval("question") %> 
                                            </h5>
                                        </td>
                                    </tr>
                                  
                                    
                                    <tr>
                                        <td> <asp:RadioButton GroupName="qp1" ID="rbnOptionA" runat="server" CssClass="radio radio-primary" Text='<%#Eval("option_a") %>' /></td>
                                    </tr>
                                      <tr>
                                        <td> <asp:RadioButton GroupName="qp1" ID="rbnOptionB" runat="server" CssClass="radio radio-primary" Text='<%#Eval("option_b") %>' /></td>
                                    </tr>
                                      <tr>
                                        <td> <asp:RadioButton GroupName="qp1" ID="rbnOptionC" runat="server" CssClass="radio radio-primary" Text='<%#Eval("option_c") %>' /></td>
                                    </tr>
                                      <tr>
                                        <td> <asp:RadioButton GroupName="qp1" ID="rbnOptionD" runat="server" CssClass="radio radio-primary" Text='<%#Eval("option_d") %>' /></td>
                                    </tr>


                                </ItemTemplate>

                            </asp:Repeater>

                        </table>

                    </div>
                    <div class="col-md-3">
                        <div class="row g-1">
                            <asp:Repeater ID="rptPager" runat="server">

                                <ItemTemplate>
                                   
                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                        CssClass='<%# Eval("Value").ToString()==lblCurrentPage.Text ? "btn btn-sm btn-primary mx-1 col-md-2" :" btn mx-1 btn-sm btn-secondary col-md-2"%>'
                                        OnClick="lnkPage_Click"></asp:LinkButton>
                                    
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                  
                        <asp:Label runat="server" Visible="false" ID="lblTotalRecords" Text=""></asp:Label>
                       <asp:Label runat="server" Visible="false" ID="lblCurrentPage" Text="1"></asp:Label>     
                    </div>
                </div>
            </div>
             <div class="list-group-item">
                 <asp:Repeater ID="rep_List" runat="server">

                                <ItemTemplate>
                                   
                                    <table>
                                        <tr>
                                            <td><%#("question_code")%></td>
                                            <td><%#("questionname")%></td>
                                        </tr>
                                    </table>
                                    
                                </ItemTemplate>
                            </asp:Repeater>
             </div>
        </div>
    
      <asp:HiddenField ID="hdnTimerSeconds" runat="server" />
         <asp:HiddenField ID="hfobjecttive_que" runat="server"  />
    </form>


</body>
</html>
