<%@ Page Title="STUDENT EXAM MAPPING" Language="C#" MasterPageFile="~/Pages/MasterPage.master" AutoEventWireup="true" CodeFile="StudentExamMapping.aspx.cs" Inherits="Pages_StudentExamMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .form-label {
            font-weight: bold;
            font-size: 13px;
            text-transform: uppercase;
        }
    </style>
      <script type="text/javascript">
  

  function ErrorModal() {
      var myModal = new bootstrap.Modal(document.getElementById('ErrorModal'), { keyboard: false });
      myModal.show();

          }

          window.onload = function () {
              var select_all_checkbox = document.querySelector(".chk-selectAll>input[type=checkbox]");
              console.log(select_all_checkbox);
              select_all_checkbox.onclick = function () {
                  var list_of_checkboxes = document.querySelectorAll(".table_student_list input[type=checkbox]");
                  console.log(list_of_checkboxes);
                  for (let i = 0; i < list_of_checkboxes.length; i++) {
                      if (select_all_checkbox.checked) {
                          list_of_checkboxes[i].checked = true;

                      } else {
                          list_of_checkboxes[i].checked = false;
                      }
                  }
              }
          }
    




      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        <div class="row mt-3 g-1">
            <div class="col-md-1"></div>
            <div class="col-md-3">
                <div class="list-group">
                    <div class="list-group-item bg-primary text-white fw-bold">MAP STUDENTS</div>
                    <div class="list-group-item">
                        <span class="form-label">ACADEMIC YEAR *</span>
                        <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-select form-select-sm border-1 border-primary"></asp:DropDownList>
                    </div>
                    <div class="list-group-item">
                        <span class="form-label">COURSE *</span>
                        <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" ID="ddlCourse" CssClass="form-select form-select-sm border-1 border-primary"></asp:DropDownList>
                    </div>
                    <div class="list-group-item">
                        <span class="form-label">DIVISION *</span>
                        <asp:DropDownList runat="server"  ID="ddlDivision" CssClass="form-select form-select-sm border-1 border-primary"></asp:DropDownList>
                    </div>
                    <div class="list-group-item">
                        <span class="form-label">COMBINATION *</span>
                        <asp:DropDownList runat="server" ID="ddlCombination" CssClass="form-select form-select-sm border-1 border-primary"></asp:DropDownList>
                    </div>
                    <div class="list-group-item">
                        <span class="form-label">EXAM *</span>
                        <asp:DropDownList runat="server" ID="ddlExam" CssClass="form-select form-select-sm border-1 border-primary"></asp:DropDownList>
                    </div>
                    <div class="list-group-item">
                        <asp:LinkButton runat="server" ID="btnGenerate" OnClick="btnGenerate_Click" CssClass="btn btn-sm btn-primary w-100">GENERATE</asp:LinkButton>
                    </div>
                    <div class="list-group-item">
                        <br />
                    </div>
                    <div class="list-group-item">
                        <span class="form-label"><asp:CheckBox runat="server" ID="chkSelectAll" CssClass="chk-selectAll" />  SELECT ALL</span>
                    </div>
                    <div class="list-group-item">
                        <asp:LinkButton runat="server" Id="btnSave" OnClick="btnSave_Click" Text="SAVE" CssClass="btn btn-success btn-sm w-100 "></asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="list-group">
                    <div class="list-group-item bg-primary text-white fw-bold">STUDENT LIST</div>
                    <div class="list-group-item" style="height:90vh;overflow:auto">
                        <asp:Repeater runat="server" ID="RptStudentList">
                            <HeaderTemplate>
                                <table class="table table-sm text-nowrap table-bordered table-condensed table_student_list" style="font-size:smaller">
                                    <tr>
                                        <th></th>
                                        <th class="text-center">RECORD NO</th>
                                        <th>STUDENT NAME</th>
                                        <th class="text-center">STUDENT ID NO</th>
                                        <th class="text-center">ROLL NO</th>


                                        <th class="text-center">DIVISION</th>
                                        <th class="text-center">COMBINATION</th>

                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <asp:Label runat="server" ID="lblSlno" Visible="false" Text='<%#Eval("slno") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblStudentIdNo" Visible="false" Text='<%#Eval("StudentIdNo") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblRollNo" Visible="false" Text='<%#Eval("RollNo") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblDivision" Visible="false" Text='<%#Eval("Division") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblacademicyear" Visible="false" Text='<%#Eval("academicyear") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblterm" Visible="false" Text='<%#Eval("term") %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblCombination" Visible="false" Text='<%#Eval("Combination") %>'></asp:Label>

                                    <td>
                                        <asp:CheckBox runat="server" ID="chkSelect" />
                                    </td>
                                    <td class="text-center"><%#Container.ItemIndex+1 %></td>
                                    <td><%#Eval("CandidateName") %></td>
                                    <td class="text-center"><%#Eval("StudentIdNo") %></td>
                                    <td class="text-center"><%#Eval("RollNo") %></td>
                                    <td class="text-center"><%#Eval("Division") %></td>
                                    <td class="text-center"><%#Eval("Combination") %></td>

                                </tr>

                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

