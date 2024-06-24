<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MasterPage.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="Admin_CreateUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .hidden {
            display: none;
        }

        .heading {
            color: Black;
        }
        .list-group{
             box-shadow: 0 2px 2px 0 aliceblue, 0 2px 2px 0 rgba(0, 0, 0, 0.1);
        }
        .list-group-item{
            background-color:white;
        }
        label{
            font-size:13px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div class="container-fluid">
        <br />
       
        <div class="row">
            <%--  <div class="col-md-2">
                  </div>--%>
           <%--  <div class="row">--%>
                            <div class="col-md-2 pull-left">
                                 <asp:Image ID="img_user" runat="server" class="  center-block" ImageUrl="Images/User.jpg"
                                    Width="100px" Height="100px" />
                                &nbsp;
                                <asp:FileUpload ID="avatarUpload" runat="server" CssClass="form-control" onchange='previewFile()' AllowMultiple="true" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Photo is required"
                                    Display="None" ControlToValidate="avatarUpload" SetFocusOnError="true" ValidationGroup="Imageupload"></asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" TargetControlID="RequiredFieldValidator20"
                                    PopupPosition="BottomLeft">
                                </asp:ValidatorCalloutExtender>
                            </div>
                        
            <div class="col-md-5">
                <div class="list-group " style="border:none;">
                    <div class="list-group-item" style="background-color:white;">                        
                        <div class="row">
                            <div class="col-md-2 pull-right">
                                <asp:LinkButton ID="btnUser" runat="server" CssClass="btn btn-default pull-right icon btn-sm  btn-success" ValidationGroup="Save" OnClick="btnSave_Click"><i class="fa fa-check" aria-hidden="true"></i>&nbsp;Save</asp:LinkButton>
                                 <asp:LinkButton ID="btn_update" runat="server" CssClass="btn btn-default pull-right icon btn-sm  btn-success" Visible="false"  OnClick="btn_update_Click"><i class="fa fa-check" aria-hidden="true"></i>&nbsp;Update</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="list-group-item  ">
                        <br />
                        <div class="row">
                            <div class="col-md-12">

                                <div class="row">
                                    
                                      
                                    

                                    <div class="col-md-12 hidden">
                                        <label>
                                            Department</label>
                                        <asp:DropDownList ID="ddlDeprtmnt" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                       

                                    </div>
                                </div>
                                <div class="row" style="margin-top: 15px;">
                                     <div class="col-md-12">
                                         <label>
                                            UserName</label>
                                              <asp:TextBox ID="txtslno" runat="server" CssClass="form-control input-sm" Visible="false" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter User Name"
                                            Display="None" ControlToValidate="txtUserName" ValidationGroup="Save" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                                        </asp:ValidatorCalloutExtender>
                                    </div>
                                       
                                  
                                </div>
                                <div class="row" style="margin-top: 15px;">
                                     
                                         <div class="col-md-12">
                                         <label>
                                            User Id *</label>
                                        <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control input-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter User Id"
                                            Display="None" ControlToValidate="txtUserId" ValidationGroup="Save" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                        </asp:ValidatorCalloutExtender>

                                    </div>
                                    
                                    
                                </div>
                                 <div class="row" style="margin-top: 15px;">
                                     
                                         <div class="col-md-12">
                                         <label>
                                            Password *</label>
                                        <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control input-sm" AutoCompleteType="Disabled" type="password" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Password"
                                            Display="None" ControlToValidate="txtpassword" ValidationGroup="Save" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3">
                                        </asp:ValidatorCalloutExtender>

                                    </div>
                                    
                                    
                                </div>
                            </div>


                        </div>
                    </div>

                 </div>
     
                </div>


           
            <div class="col-md-5">
                  <div class="list-group " style="border:none;">
                <div class="list-group-item heading" style="background-color: white;">
                    <center><label>Users</label></center>
                </div>

                <div class="list-group-item  ">

                    <div class="row">
                        <div class="col-md-12 ">

                            <asp:GridView ID="Gridusers" runat="server" AutoGenerateColumns="false" Style="max-height: 180px; margin-top: -8px; border-left: 1px solid black; overflow: auto; border-color: aliceblue; width: 350px; font-size:13px; margin-bottom: 5px;"
                                CssClass="table table-hover" DataKeyNames="userid">
                                <Columns>

                                    <asp:BoundField DataField="username" HeaderText="User Name" />
                                    
                                    

                                    <asp:BoundField DataField="userid" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSelect"  runat="server" CommandArgument='<%# Eval("userid")%>'
                                                OnClick="Select_OnClick"> <i class="fa fa-trash-o" aria-hidden="true"></i>delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    

                                </Columns>

                            </asp:GridView>




                        </div>
                    </div>
                </div>
                      </div>
            </div>

             </div>


            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>







 

    <div id="ErrorModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h3>Message
                        <button type="button" class="close" data-dismiss="modal"><i class="fa fa-close "></i></button>
                    </h3>
                </div>
                <div class="modal-body">
                    <h4>
                        <asp:Label ID="lblErrorMsg2" runat="server" Text=""></asp:Label></h4>
                </div>

            </div>

        </div>
    </div>
      <script type="text/javascript">

        function previewFile() {
            var preview = document.querySelector('#<%=img_user.ClientID %>');
            var file = document.querySelector('#<%=avatarUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {

                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
        

    </script>
   
</asp:Content>

