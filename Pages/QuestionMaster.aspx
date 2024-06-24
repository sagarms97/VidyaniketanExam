<%@ Page Title="QuestionMaster" Language="C#" MasterPageFile="~/Pages/MasterPage.master" AutoEventWireup="true" CodeFile="QuestionMaster.aspx.cs" Inherits="Pages_QuestionMaster" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script type="text/javascript" src="https://code.jquery.com/jquery-3.1.1.min.js"></script>--%>
    <script type="text/javascript" src="https://cdn.ckeditor.com/4.9.2/standard/ckeditor.js"></script>

    <style>
        .bg {
            background: linear-gradient(180deg, #fbf1d5 0%, #ffffff 100%)
        }

        .form-label {
            text-transform: uppercase;
            font-weight: bold;
            font-size: 14px;
        }

        input[type=checkbox] {
            margin-right: 10px;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            var allCheckBoxes = document.querySelectorAll("input[type=checkbox]");
            for (let i = 0; i < allCheckBoxes.length; i++) {
                allCheckBoxes[i].classList.add("form-check-input");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager EnablePageMethods="true" ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <asp:Label ID="lblslno" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="ViewList" runat="server">

            <div class="list-group">
                <div class="list-group-item bg   bg-light-secondary   border-1 border-secondary rounded-0 border-1  p-2  text-dark">
                    <div class="row  ">
                        <div class="col-md-10  col-lg-10 col-sm-10 col-xl-10  col-8  my-auto ">
                            <span class="text-uppercase  fw-bold  ms-2 ">Questions List   </span>
                        </div>

                        <div class="col-md-2  col-lg-2 col-sm-2 col-xl-2  col-4 ">


                            <asp:LinkButton ID="btnViewNew" OnClick="btnViewNew_Click" runat="server" CssClass="  fw-semi-bold ms-2 me-2 text-decoration-none text-primary-hover float-end">New Question&nbsp;&nbsp;<i class="fa-solid fa-plus"></i></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>

            <div class=" container-fluid mt-2">
                <div class="row g-1">

                    <div class="col-md-12">
                        <div class="list-group">

                            <div class="list-group-item border-secondary border-1 w-100 overflow-auto text-nowrap">

                                <asp:Repeater ID="Rep_List" runat="server">
                                    <HeaderTemplate>


                                        <table id="myTable" class=" table table-sm table-condensed   table-responsive  ">
                                            <thead>

                                                <tr class="bg-gray-300">

                                                    <th></th>
                                                    <th>Code</th>
                                                    <th>question</th>
                                                     <th>Exam Code</th>
                                                    <th>Subject</th>
                                                    <th>Chapter</th>
                                                    <th>Level</th>



                                                    <th></th>
                                                </tr>
                                            </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>


                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="select" ToolTip="Select" OnClick="select_Click" runat="server"
                                                    CommandArgument='<%# Eval("question_code") %>' CssClass="btn btn-sm btn-outline-secondary">
     <i class="fa-regular fa-pen-to-square"></i>&nbsp;EDIT</asp:LinkButton>

                                            </td>
                                            <td>
                                                <asp:Label ID="lblcode" runat="server" Text='<%#Eval("question_code") %>'></asp:Label>
                                            </td>
                                            <td><%#Eval("question") %></td>
                                            <td><%#Eval("examcode") %></td>
                                            <td><%#Eval("name") %></td>
                                            <td><%#Eval("chapter") %></td>
                                            <td><%#Eval("difficulty_level") %></td>


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
            <div class="container-fluid">

                <div class="row ">
                    <div class="col-md-12">
                        <div class="list-group">
                            <div class="list-group-item bg  bg-light-secondary   border-1 border-secondary rounded-0   p-2">
                                <div class="row  ">

                                    <div class="col-12  my-auto ">
                                        <asp:LinkButton ID="btnViewList" OnClick="btnViewList_Click" runat="server" CssClass="  fw-semi-bold ms-2 text-decoration-none me-2 text-primary-hover"><i class="fa-solid fa-arrow-circle-left"></i>&nbsp;BACK</asp:LinkButton>&nbsp;|&nbsp;
                                        <span class="text-uppercase  fw-semi-bold  ">
                                            <asp:Label ID="lblCreate" runat="server" Text="Create"></asp:Label>
                                            QUESTION</span>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row g-1 mt-3">
                    <div class="col-md-2">
                        <div class="list-group">
                            <div class="list-group-item">
                                <br />
                            </div>
                            <div class="list-group-item bg-secondary text-white">
                                SELECT EXAMS
                            </div>
                            <div class="list-group-item ">
                                <asp:CheckBoxList ID="chk_Examlist" runat="server" CssClass="checkbox-primary">
                                </asp:CheckBoxList>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-10">
                        <div class="list-group">
                            <div class="list-group-item ">
                                <div class="row">
                                    <div class="col-md-8"></div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnCreate" runat="server" Text="SAVE Question" OnClick="btnCreate_Click" CssClass="btn btn-success text-uppercase  btn-sm w-100" ValidationGroup="Save" />

                                    </div>
                                </div>
                            </div>
                            <div class="list-group-item bg-secondary text-white">
                                ENTER QUESTIONS
                            </div>
                            <div class="list-group-item">
                                <div class="row g-1">
                                    <div class="col-md-3">
                                        <span class="form-label">Select Subject * </span>
                                        <asp:DropDownList ID="ddlsubject" runat="server" CssClass="form-select form-select-sm border-1 border-primary" required>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="form-label">Select Chapter</span>
                                        <asp:DropDownList ID="ddlchapter" runat="server" CssClass="form-select form-select-sm border-1 border-primary mb-2 text-dark">
                                            <asp:ListItem Value="1" Text="Chapter 1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Chapter 2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Chapter 3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Chapter 4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="Chapter 5"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Chapter 6"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="Chapter 7"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="Chapter 8"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="Chapter 9"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="Chapter 10"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="form-label">Marks *</span>
                                        <asp:TextBox ID="txtmarks" Text="1" runat="server" CssClass="form-control form-control-sm  border-1 border-primary"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required *" ID="val_Marks" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtmarks" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:FilteredTextBoxExtender runat="server" ID="filtr1" TargetControlID="txtmarks" FilterType="Numbers,Custom" ValidChars="."></asp:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-md-3">
                                        <span class="form-label">Negative marks *</span>
                                        <asp:TextBox ID="txtnegativemarks" Text="1" runat="server" CssClass="form-control form-control-sm  border-1 border-primary"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required *" ID="RequiredFieldValidator1" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtnegativemarks" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtnegativemarks" FilterType="Numbers,Custom" ValidChars="."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="list-group-item">
                                <span class="form-label">QUESTION *</span>
                                <CKEditor:CKEditorControl ID="txtCkEditorQue" BasePath="/ckeditor/" Height="80px" runat="server"></CKEditor:CKEditorControl>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="Required *" ID="RequiredFieldValidator7" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtCkEditorQue" ValidationGroup="Save"></asp:RequiredFieldValidator>

                            </div>
                            <div class="list-group-item">
                                <div class="row g-1">
                                    <div class="col-6">
                                        <span class="form-label">OPTION A *</span>
                                        <CKEditor:CKEditorControl ID="CKEditorobtA" BasePath="/ckeditor/" Height="80px" runat="server"></CKEditor:CKEditorControl>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required *" ID="RequiredFieldValidator2" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" ControlToValidate="CKEditorobtA" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="col-6">
                                        <span class="form-label">OPTION B *</span>
                                        <CKEditor:CKEditorControl ID="CKEditorobtB" BasePath="/ckeditor/" Height="80px" runat="server"></CKEditor:CKEditorControl>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required *" ID="RequiredFieldValidator3" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" ControlToValidate="CKEditorobtB" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="list-group-item">
                                <div class="row g-1">
                                    <div class="col-6">
                                        <span class="form-label">OPTION C *</span>
                                        <CKEditor:CKEditorControl ID="CKEditorobtC" BasePath="/ckeditor/" Height="80px" runat="server"></CKEditor:CKEditorControl>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required *" ID="RequiredFieldValidator4" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" ControlToValidate="CKEditorobtC" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="col-6">
                                        <span class="form-label">OPTION D *</span>
                                        <CKEditor:CKEditorControl ID="CKEditorobtD" BasePath="/ckeditor/" Height="80px" runat="server"></CKEditor:CKEditorControl>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required *" ID="RequiredFieldValidator5" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" ControlToValidate="CKEditorobtD" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="list-group-item">
                                <span class="form-label">HINT * </span>
                                <CKEditor:CKEditorControl ID="txtsolution" BasePath="/ckeditor/" Height="80px" runat="server"></CKEditor:CKEditorControl>
                            </div>
                            <div class="list-group-item">
                                <div class="row g-1">
                                    <div class="col-md-3">
                                        <span class="form-label">KEY ANSWER *</span>
                                        <asp:DropDownList runat="server" ID="ddlAnswer" CssClass="form-select form-select-sm border-1 border-primary">
                                            <asp:ListItem Value="" Text=""></asp:ListItem>
                                            <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                            <asp:ListItem Value="B" Text="B"></asp:ListItem>
                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Required *" ID="RequiredFieldValidator6" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" ControlToValidate="ddlAnswer" ValidationGroup="Save"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="col-md-1"></div>
                                    <div class="col-md-8">
                                        <span class="form-label">SELECT DIFFICULTY LEVEL *</span>
                                        <asp:RadioButtonList ID="rbtn_questionlevel" Font-Bold="true" runat="server" CssClass=" radio  radio-primary " RepeatDirection="Horizontal">


                                            <asp:ListItem style="color: #33e81f" Value="1" Selected="True"> &nbsp;Easy &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>

                                            <asp:ListItem style="color: #f5eb11" Value="2">&nbsp;Tough &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem style="color: red" Value="3">&nbsp;Toughest</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <asp:Label ID="lblquationcode" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lblAcademicYear" Text="" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lblExamSlno" Text="" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lblquestion_slno" Text="" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lblqpCode" Text="" Visible="false"></asp:Label>
            <asp:Label runat="server" ID="lblExamdate" Text="" Visible="false"></asp:Label>

            <asp:Label runat="server" ID="lblexamTimingFrom" Text="" Visible="false"></asp:Label>

            <asp:Label runat="server" ID="lblexamTimingTo" Text="" Visible="false"></asp:Label>





        </asp:View>


    </asp:MultiView>



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
</asp:Content>

