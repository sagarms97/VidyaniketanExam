<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StartExam.aspx.cs" Inherits="StartExam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    

<!-- Bootstrap CSS -->

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
  <%--  <link href="assets/css/all.css" rel="stylesheet" />
 <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/custom_css/components.css" rel="stylesheet" />
<script src="assets/js/bootstrap.js"></script>   
    <script src="assets/1.11.1jquery.min.js"></script>     
    <link href="css/toster.css" rel="stylesheet" />--%>
     <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/all.css" rel="stylesheet" />
    <link href="assets/custom_css/components.css" rel="stylesheet" />
    <script src="assets/1.11.1jquery.min.js"></script>
    <script src="assets/js/bootstrap.js"></script>

   


    <script src="assets/js/toster.js"></script>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="list-group">
            <div class="list-group-item bg">
                <small style="float: left">
                    <asp:Image ID="Image2" runat="server" ImageUrl="css/images/logo.jpg" Height="65px" Width="65px" /></small>
                <small style="float: right">
                    <asp:Image ID="Image1" runat="server" ImageUrl="assets/images/NoImage.png" Height="65px" Width="65px" /></small>
               
                <h5 class="text-center text-uppercase" style="color: GrayText; font-weight: bold; text-align: center; font-size: 14px; line-height: normal">CES Foundation <small style="float:right;margin-right:5px">  Time remaining : <div style="text-align: right;margin-right:5px" id="timerDisplay"></div></small></h5>
                   <h3 class="text-center fw-bold text-uppercase">
                   Vidyaniketan PU Science College, Hubballi
            </h3>
                <h3 class="text-center text-uppercase" style="color: #e60000; font-weight: 700; line-height: 18px; letter-spacing: 0px">Examination of NEET - 2024</h3>
              

            </div>
            <div class="list-group-item text-uppercase" style="background-image: url(css/images/bg.png)">
                <small class="text-left text-uppercase " style="float: left; color: purple; font-size: 14px; font-weight: 600">
                     <b style="color:cadetblue"> Student Name : <asp:Label ID="lblstudentname" runat="server"></asp:Label></b></small>
                <small class="text-left " style="float: right; color: purple; text-align: center; font-size: 14px; font-weight: 600"><b style="color:cadetblue"> Student Id No : <asp:Label ID="lblstudentidno" runat="server"></asp:Label></b></small>

                <br />

                <small style="float: left; color: purple; font-size: 14px; font-weight: 600"><b style="color:cadetblue"> Course : <asp:Label ID="lblcourse" runat="server"></asp:Label></b></small>
                <small style="float: right; color: purple; font-size: 14px; font-weight: 600;"><b style="color:cadetblue"> Roll Number : <asp:Label ID="lblrollno" runat="server"></asp:Label></b></small>

            </div>
                                           
             <div class="list-group-item">
                  <asp:Repeater ID="Rep_SubjectsSection" runat="server">
                                <ItemTemplate>
                                 
                                     <asp:LinkButton ID="btn_selectSubject" runat="server" Text='<%#Eval("name") +" (SECTIONI)" %>' CommandArgument='<%# Eval("subjectcode") %>' 
                                        CssClass='<%# Eval("subjectcode")=="105" ? "btn btn-sm btn-outline-secondary mx-1 col-md-2" :" btn mx-1 btn-sm btn-secondary col-md-2"%>'
                                        OnClick="btn_selectSubject_Click"></asp:LinkButton>
                                      
                                    </ItemTemplate>
                      </asp:Repeater>
                 <h5 class="text-uppercase" style="float:right;color:#b41bb4;font-weight:800;margin-right:35px">Objective Questions</h5>

                 </div>
            <div class="list-group-item" style="background-image: url(css/images/bg.png)">

               

                <div class="row">
                    <div class="col-md-9">

                        <table class="list-group">


                            <asp:Repeater ID="Rep_Exam" runat="server">

                                <ItemTemplate>

                                    <tr>
                                        <td>
                                            <h5 class="d-flex">
                                               
                                                  <asp:Label ID="lblquestion" runat="server" Text='<%#Eval("Seriesquestion_code")  +":"+ Eval("question") %>' ></asp:Label>
                                                <asp:Label ID="lblorg_quecode" runat="server" Text=' <%#Eval("Orgquestion_code") %>' Visible="false" ></asp:Label>
                                                <asp:Label ID="lblorg_anscode" runat="server" Text=' <%#Eval("answer") %>' Visible="false"  ></asp:Label>
                                                
                                            </h5>
                                        </td>
                                    </tr>
                                  
                                    
                                    <tr>
                                       <td> A)<asp:RadioButton GroupName="grp1" ID="rbnOptionA" OnCheckedChanged="rbnOptionA_CheckedChanged" AutoPostBack="true"   value="A" runat="server" CssClass="radio radio-primary" Text='<%#Eval("option_a") %>' /></td>
                                    </tr>
                                      <tr>
                                        <td>  B)<asp:RadioButton GroupName="grp1" ID="rbnOptionB" OnCheckedChanged="rbnOptionB_CheckedChanged" value="B" runat="server" AutoPostBack="true"  CssClass="radio radio-primary" Text='<%#Eval("option_b") %>' /></td>
                                    </tr>
                                      <tr>
                                        <td>  C)<asp:RadioButton GroupName="grp1" ID="rbnOptionC" OnCheckedChanged="rbnOptionC_CheckedChanged" value="C" runat="server" AutoPostBack="true"  CssClass="radio radio-primary" Text='<%#Eval("option_c") %>' /></td>
                                    </tr>
                                      <tr>
                                        <td> D)<asp:RadioButton GroupName="grp1" ID="rbnOptionD" OnCheckedChanged="rbnOptionD_CheckedChanged"  value="D" runat="server" AutoPostBack="true"  CssClass="radio radio-primary" Text='<%#Eval("option_d") %>' /></td>
                                    </tr>


                                </ItemTemplate>

                            </asp:Repeater>

                        </table>

                    </div>
                    <div class="col-md-3">
                        <div class="row g-1">
                            <asp:Repeater ID="rptPager" runat="server">

                                <ItemTemplate>

                                    
                                   
                                    <asp:LinkButton ID="lnkPage" runat="server"  Text='<%#Eval("Seriesquestion_code") %>' CommandArgument='<%# Eval("Seriesquestion_code") %>'
                                       CssClass='<%# Convert.ToInt32( Eval("Seriesquestion_code")) > Convert.ToInt32(Eval("Seriesquestion_code")) ? "btn btn-sm btn-primary mx-1 col-md-2" :" btn mx-1 btn-sm btn-secondary col-md-2"%>'
                                        OnClick="lnkPage_Click"></asp:LinkButton>
                                    
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                  
                        <asp:Label runat="server" Visible="false" ID="lblTotalRecords" Text=""></asp:Label>
                       <asp:Label runat="server" Visible="false" ID="lblCurrentPage" Text="1"></asp:Label>     
                    </div>
                </div>
            </div>


             <div class="list-group-item list-group-item-info ">
              

                  <asp:Repeater ID="Rep_ExamSheetList" runat="server" Visible="false">
                                    <HeaderTemplate>
                                     

                                        <table id="myTable" class=" table table-sm table-condensed   table-responsive  ">
                                            <thead>

                                                <tr>
                                                     <th>A.year</th>
                                                    <th>Slno</th> 
                                                    <th>subjectcode</th> 
                                                     <th>Examcode</th>
                                                     <th>ExamDate</th>                                                    
                                                    <th>SeriesCode</th>
                                                      <th>SeriesQueCode</th>
                                                    <th>SeriesorgQueCode</th>
                                                     <th>keyans</th>
                                                     <th>negmarks</th>
                                                     <th>marks</th>

                                                    <th>selectopt</th>
                                                    <th>negmarks</th>
                                                    <th>marks</th>
                                                    
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>


                                        <tr>
                                            <td>
                                                <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Academicyear") %>'></asp:Label>
                                            </td>
                                             <td>
                                                <asp:Label ID="lblslno" runat="server" Text='<%#Eval("Slno") %>'></asp:Label>
                                            </td>
                                              <td>
                                                <asp:Label ID="lblsubjectcode" runat="server" Text='<%#Eval("subjectcode") %>'></asp:Label>
                                            </td>
                                             
                                            <td>
                                                <asp:Label ID="lblexamcode" runat="server" Text='<%#Eval("examcode") %>'></asp:Label>
                                            </td>
                                              <td>
                                                <asp:Label ID="lblexamdate" runat="server" Text='<%#Eval("ExamDate") %>'></asp:Label>
                                            </td>
                                            
                                            <td>
                                                <asp:Label ID="lblSeriesCode" runat="server" Text='<%#Eval("SeriesCode") %>'></asp:Label>
                                            </td>
                                             <td>
                                                <asp:Label ID="lblSeriesquestion_code" runat="server" Text='<%#Eval("Seriesquestion_code") %>'></asp:Label>
                                            </td>
                                             <td>
                                                <asp:Label ID="lblOrgquestion_code" runat="server" Text='<%#Eval("Orgquestion_code") %>'></asp:Label>
                                            </td>
                                              <td>
                                                <asp:Label ID="lblanswer" runat="server" Text='<%#Eval("answer") %>'></asp:Label>
                                            </td>
                                              <td>
                                                <asp:Label ID="lblNegativemarks" runat="server" Text='<%#Eval("Negativemarks") %>'></asp:Label>
                                            </td>
                                              <td>
                                                <asp:Label ID="lblMarks" runat="server" Text='<%#Eval("Marks") %>'></asp:Label>
                                            </td>
                                             <td>
                                              <asp:TextBox ID="txtselectopt" runat="server"  Width="100px"></asp:TextBox>
                                                  
                                            </td>
                                             <td>
                                               <asp:TextBox ID="txtnegmarks" runat="server"  Text="-1"  Width="100px"></asp:TextBox>
                                            </td>
                                              <td>
                                               <asp:TextBox ID="txtmarks" runat="server"  Text="-1" Width="100px"></asp:TextBox>
                                            </td>
                                           
                                           

                                        </tr>


                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                            
                                    </FooterTemplate>

                                </asp:Repeater>
                
                 
                                          <asp:LinkButton ID="btn_submit" runat="server" Text="Exam Submitt" OnClick="btn_submit_Click" Style="float:right" CssClass="btn btn-sm btn-success text-uppercase mx-10  col-md-2 "></asp:LinkButton>

               
             </div>
        </div>
    
      <asp:HiddenField ID="hdnTimerSeconds" runat="server" />
         <asp:HiddenField ID="hfobjecttive_que" runat="server"  />
        <asp:HiddenField ID="btnsubject" runat="server" />
        <asp:HiddenField ID="hfselectedopt" Visible="true" runat="server" />
         <asp:HiddenField ID="hfnegmarks" Visible="true" runat="server" />
        <asp:HiddenField ID="hfmarks" Visible="true" runat="server" />


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
                    <div class="modal-header bg-primary text-white">
                        <h5 class="text-white">conform Message</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">

                        <asp:Label ID="lblErrorMsg" runat="server" Text=""  ForeColor="Black"></asp:Label>
                    </div>
                    <div class="modal-footer">
                                                <asp:LinkButton ID="LinkButton2" runat="server" Text="No"  class="btn-close" data-bs-dismiss="modal" aria-label="Close" Style="float: right" CssClass="btn btn-sm btn-outline-secondary text-uppercase mx-1  col-md-4 "></asp:LinkButton>

                        <asp:LinkButton ID="btn_result" runat="server" Text="Submitt" OnClick="btn_result_Click" Style="float: right" CssClass="btn btn-sm btn-outline-success text-uppercase mx-1  col-md-4 "></asp:LinkButton>

                    </div>


                </div>

            </div>
        </div>
    </form>
     <script src="assets/js/bootstrap.js"></script>
        <script src="assets/custom_js/jquery-1.4.1.min.js"></script>

</body>
</html>
