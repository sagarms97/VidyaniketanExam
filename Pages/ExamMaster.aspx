<%@ Page Title="ExamMater" Language="C#" MasterPageFile="~/Pages/MasterPage.master" AutoEventWireup="true" CodeFile="ExamMaster.aspx.cs" Inherits="Pages_ExamMaster" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/flatpickr/4.6.13/flatpickr.min.js" integrity="sha512-K/oyQtMXpxI4+K0W7H25UopjM8pzq0yrVdFdG21Fh5dBe91I40pDd9A4lzNlHPHBIP2cwZuoxaUSX0GJSObvGA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/flatpickr/4.6.13/flatpickr.min.css" integrity="sha512-MQXduO8IQnJVq1qmySpN87QQkiR1bZHtorbJBD0tzy7/0U9+YIC93QWHeGTEoojMVHWWNkoCp8V6OzVSYrX0oQ==" crossorigin="anonymous" referrerpolicy="no-referrer" />
       <link href="../css/toster.css" rel="stylesheet" />
    <script src="../assets/js/toster.js"></script>
     <script>
         function success(msg) {
             //         toastr.options.positionClass = "toast-bottom-left";
             toastr.options.positionClass = "toast-bottom-right";
             toastr.success(msg);
         }
         function warning(msg) {
             //        toastr.options.timeOut = 1500; // 1.5s
             toastr.options.positionClass = "toast-bottom-right";
             toastr.warning(msg);
         }
         function info(msg) {
             //        toastr.options.timeOut = 1500; // 1.5s
             toastr.options.positionClass = "toast-bottom-right";
             toastr.info(msg);
         }
         function error(msg) {
             //        toastr.options.timeOut = 1500; // 1.5s
             toastr.options.positionClass = "toast-top-right";
             toastr.error(msg);
         }



         function calculateDuration() {
             // Convert time strings to Date objects
             var formTime_ = document.querySelector('.txt_FromTime');
             var toTime_=document.querySelector('.txt_ToTime');

             var duration_textBox = document.querySelector('.txt_Duration');

             var fromDate = new Date("2000-01-01 " + formTime_.value);
             var toDate = new Date("2000-01-01 " + toTime_.value);

             // Calculate the time difference in milliseconds
             var timeDifference = toDate - fromDate;

             // Convert milliseconds to minutes
             var durationInMinutes = timeDifference / (1000 * 60);


             duration_textBox.value = durationInMinutes;
            
         }

         window.onload = function () {
             flatpickr(".datetimePicker", {
                 enableTime: true,
                 noCalendar: true,
                 dateFormat: "h:i K",
                 time_24hr: false
                 


             });

             flatpickr(".datePicker", {
                 enableTime: false,
                 dateFormat: "d-m-Y",
                 time_24hr: false


             });
         }
     </script>
    <style>
        .form-label{
           font-weight:bold;
           font-size:13px;
           text-transform:uppercase;
        }

        #cke_1_contents{
            height:296px !important;
        }

        .ckEditor{
            height:300px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ToolkitScriptManager EnablePageMethods="true" ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="ViewList" runat="server">
            <div class="list-group">

                <div class="list-group-item bg  bg-light-secondary   border-1 border-secondary rounded-0 border-1  p-2  text-dark">
                    <div class="row  ">
                        <div class="col-md-10  col-lg-10 col-sm-10 col-xl-10  col-8  my-auto ">
                            <span class="text-uppercase  fw-bold  ms-2 ">Exam List   </span>
                        </div>

                        <div class="col-md-2  col-lg-2 col-sm-2 col-xl-2  col-4 ">


                            <asp:LinkButton ID="btnViewNew" OnClick="btnViewNew_Click" runat="server" CssClass="  fw-semi-bold ms-2 me-2 text-primary-hover text-decoration-none float-end">New Exam&nbsp;&nbsp;<i class="fa-solid fa-plus"></i></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>
            <div class="container mt-2">
                <div class="row g-1">

                    <div class="col-md-12">
                        <div class="list-group">

                            <div class="list-group-item border-secondary border-1 w-100 overflow-auto text-nowrap">

                                <asp:Repeater ID="Rep_List" runat="server">
                                    <HeaderTemplate>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <input type="text" id="myInput" onkeyup="myFunction()" tabindex="1" class="form-control input-sm" style="height: 38px" autofocus="autofocus" autocomplete="off" placeholder="Search..." title="Type in a name" />
                                            </div>
                                        </div>

                                        <table id="myTable" class=" table table-sm table-condensed   ">
                                            <thead>

                                                <tr>


                                                    <th>Code</th>
                                                    <th>Exam Name</th>
                                                    <th>Duration</th>
                                                    <th>No of Questions</th>


                                                    <th></th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>


                                        <tr>


                                            <td>
                                                <asp:Label ID="lblcode" runat="server" Text='<%#Eval("exam_code") %>'></asp:Label>
                                            </td>
                                            <td><%#Eval("exam_name") %></td>
                                            <td><%#Eval("duration") %></td>
                                            <td><%#Eval("no_of_qustions") %></td>
                                            <td>
                                                <asp:LinkButton ID="select" ToolTip="Select" OnClick="select_Click" runat="server"
                                                    CommandArgument='<%# Eval("exam_code") %>' CssClass="text-primary">
<i class="fa-regular fa-pen-to-square"></i>&nbsp;EDIT</asp:LinkButton>
                                            </td>

                                        </tr>


                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                         <asp:Label ID="Label1" runat="server" CssClass="list-group-item list-group-item-warning text-center"
                             Visible='<%# Rep_List.Items.Count==0 %>' Text="No Records found">
                                  <h3 class="text-center"><i class="fa-solid fa-road-barrier"></i>
                                      <br /><br />No Records found
                                  </h3>
                         </asp:Label>
                                    </FooterTemplate>

                                </asp:Repeater>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="ViewCreate" runat="server">
            <div class="list-group">

                <div class="list-group-item bg  bg-light-secondary   border-1 border-secondary rounded-0   p-2">
                    <div class="row  ">

                        <div class="col-12  my-auto ">
                            <asp:LinkButton ID="btnViewList" OnClick="btnViewList_Click" runat="server" CssClass="  fw-semi-bold ms-2 me-2 text-decoration-none text-primary-hover"><i class="fa-solid fa-arrow-circle-left"></i>&nbsp;BACK</asp:LinkButton>&nbsp;|&nbsp;
          <span class="text-uppercase  fw-semi-bold  ">
              <asp:Label ID="lblCreate" runat="server" Text="Create"></asp:Label>
              EXAM</span>
                        </div>


                    </div>
                </div>
            </div>
             <asp:Label ID="lblslno" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
            <div class="container-fluid">
                <div class="row mt-4 g-1">
                   
                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                        <div class="list-group">
                            <div class="list-group-item list-group-item-action bg-primary text-white">
                                CREATE EXAMS 
                            </div>
                            <div class="list-group-item">
                                <span class="form-label">EXAM NAME * </span>
                                <asp:TextBox ID="txtexamname" runat="server"  CssClass="form-control form-control-sm   mb-2"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="val_name" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Exam" ErrorMessage="Required * Please Enter Exam Name" ControlToValidate="txtexamname" CssClass="text-danger"></asp:RequiredFieldValidator>

                            </div>
                            <div class="list-group-item">
                                <div class="row g-1">
                                    <div class="col-md-4">
                                        <span class="form-label">Easy</span>
                                        <asp:TextBox ID="txtnoofeasy" runat="server" TextMode="Number" onkeyup="sum();"   Text="0" CssClass="form-control form-control-sm  border-1 border-dark-secondary mb-2"></asp:TextBox>

                                    </div>

                                    <div class="col-md-4">
                                        <span class="form-label">Tough</span>
                                        <asp:TextBox ID="txtnooftough" runat="server" TextMode="Number" onkeyup="sum();" placeholder="Enter No of Question" Text="0" CssClass="form-control form-control-sm  border-1 border-dark-secondary mb-2"></asp:TextBox>


                                    </div>


                                    <div class="col-md-4">
                                        <span class="form-label">Toughest</span>
                                        <asp:TextBox ID="txtnoofToughest" runat="server" TextMode="Number" onkeyup="sum();" placeholder="Enter No of Question" Text="0" CssClass="form-control form-control-sm  border-1 border-dark-secondary mb-2"></asp:TextBox>
                                      

                                    </div>
                                </div>
                            </div> 
                            <div class="list-group-item">
                                <div class="row g-1">
                                    <div class="col-md-6">
                                        <span class="form-label">TOTAL QUESTIONS</span>
                                        <asp:TextBox ID="txtnoofquestion" runat="server" CssClass="form-control form-control-sm   mb-2"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <span class="form-label">NO OF SERIES *</span>
                                        <asp:TextBox ID="txt_series_no" runat="server" CssClass="form-control form-control-sm   mb-2"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" Display="Dynamic" ValidationGroup="Exam" SetFocusOnError="true" ErrorMessage="Required * Please Enter No of Series" ControlToValidate="txt_series_no" CssClass="text-danger"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                              


                            </div>
                            <div class="list-group-item">
                                <span class="form-label">EXAM DATE *</span>
                                <asp:TextBox runat="server" ID="txt_ExamDate" CssClass="form-control form-control-sm datePicker" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="Exam" SetFocusOnError="true" ErrorMessage="Required * Please Enter Exam Date" ControlToValidate="txt_ExamDate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                <asp:CalendarExtender runat="server" CssClass="z-depth-1" ID="cal_ExamDate" Format="dd-MM-yyyy" TargetControlID="txt_ExamDate"></asp:CalendarExtender>
                                <asp:FilteredTextBoxExtender runat="server" ID="filterExamDate" TargetControlID="txt_ExamDate" FilterType="Numbers,Custom" ValidChars="-" ></asp:FilteredTextBoxExtender>
                               
                            </div>
                            <div class="list-group-item">
                                <div class="row g-1">
                                    <div class="col-md-6">
                                        <span class="form-label">FROM TIME *</span>
                                        <asp:TextBox runat="server" onkeyup="calculateDuration()" onkeydown="calculateDuration()" ID="txt_FromTime" CssClass="form-control form-control-sm datetimePicker txt_FromTime"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="Exam" SetFocusOnError="true" ErrorMessage="Required * Please Enter Exam Time" ControlToValidate="txt_FromTime" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txt_FromTime" FilterType="Numbers,Custom" ValidChars="-,:,A,P,M, " ></asp:FilteredTextBoxExtender>


                                    </div>
                                    <div class="col-md-6">
                                        <span class="form-label">TO TIME *</span>
                                        <asp:TextBox runat="server" ID="txt_ToTime" onkeyup="calculateDuration()" onkeydown="calculateDuration()" CssClass="form-control form-control-sm datetimePicker txt_ToTime"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" Display="Dynamic" ValidationGroup="Exam" SetFocusOnError="true" ErrorMessage="Required * Please Enter Exam Time" ControlToValidate="txt_ToTime" CssClass="text-danger"></asp:RequiredFieldValidator>
                                       <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2" TargetControlID="txt_ToTime" FilterType="Numbers,Custom" ValidChars="-,:,A,P,M, " ></asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                            </div>
                            <div class="list-group-item">
                                <div class="row g-1">
                                    <div class="col-md-6">
                                        <span class="form-label">EXAM DURATION *(in Minutes)</span>
                                        <asp:TextBox ID="txtduration" runat="server"   CssClass="form-control form-control-sm txt_Duration  border-1 border-dark-secondary mb-2"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" TargetControlID="txtduration" FilterType="Numbers" ValidChars="-" ></asp:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" Display="Dynamic" ValidationGroup="Exam" SetFocusOnError="true" ErrorMessage="Required * Please Enter Exam Duration" ControlToValidate="txtduration" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        
                                    </div>
                                    <div class="col-md-6">
                                        <span class="form-label">HAS NEGATIVE MARKS</span>
                                        <asp:DropDownList ID="ddlnegativemarks" runat="server" CssClass="form-select  form-select-sm  border-1 border-dark-secondary mb-2">
                                            <asp:ListItem Value="1" Text="True"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="False"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="list-group-item">
                                <asp:Button ID="btnCreate" runat="server" Text="Create Exam" OnClick="btnCreate_Click" ValidationGroup="Exam" CssClass="btn btn-success text-uppercase btn-sm w-100" />

                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <span class="form-label">INSTRUCTIONS*</span>
                        <CKEditor:CKEditorControl ID="txtexaminstruction" CssClass="form-control ckEditor"  BasePath="/ckeditor/" Height="80px" runat="server"></CKEditor:CKEditorControl>

                    </div>
                </div>
            </div>

          <asp:Label ID="lblExamcode" runat="server" Text="" Visible="False"></asp:Label>


       
           

            <script type="text/javascript">


                function ErrorModal() {
                    var myModal = new bootstrap.Modal(document.getElementById('ErrorModal'), { keyboard: false });
                    myModal.show();
                }



            </script>



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


        </asp:View>
    </asp:MultiView>
     <script type="text/javascript">
         function sum() {
             var txtFirstNumberValue = document.getElementById('<%= txtnoofeasy.ClientID %>').value;
             var txtSecondNumberValue = document.getElementById('<%= txtnooftough.ClientID %>').value;
             var txtthirdNumberValue = document.getElementById('<%= txtnoofToughest.ClientID %>').value;
             var result = parseInt(txtFirstNumberValue) + parseInt(txtSecondNumberValue) + parseInt(txtthirdNumberValue) ;
             if (!isNaN(result)) {
                 document.getElementById('<%= txtnoofquestion.ClientID %>').value = result;
             }
         }

     </script>
</asp:Content>

