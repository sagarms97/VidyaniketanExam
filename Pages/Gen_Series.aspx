<%@ Page Title="GENERATE SERIES" Language="C#" MasterPageFile="~/Pages/MasterPage.master" AutoEventWireup="true" CodeFile="Gen_Series.aspx.cs" Inherits="Pages_Gen_Series" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .form-label{
            font-weight:700;
            text-transform:uppercase;
        }
    </style>
     <script type="text/javascript">
  

     function ErrorModal() {
         var myModal = new bootstrap.Modal(document.getElementById('ErrorModal'), { keyboard: false });
         myModal.show();
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
    <div class="container">
        <div class="row mt-4">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header">
                        <h6 class="text-uppercase fw-bold">GENERATE QUESTION PAPER SERIES</h6>
                    </div>
                    <div class="card-body">
                        <div class="row g-1">
                            <div class="col-md-12">
                                <span class="form-label"><i class="fa fa-receipt"></i>&nbsp;SELECT EXAM</span>
                                <asp:DropDownList runat="server" ID="ddlExam" CssClass="form-select  border-1 border-primary"></asp:DropDownList>
                            </div>
                            <div class="col-md-12 mt-2">

                                <asp:LinkButton runat="server" OnClick="btnGenerate_Click" ID="btnGenerate" CssClass="btn d-block w-100 btn-sm btn-primary">GENERATE SERIES</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="col-md-12">
                            <a target="_blank" class="btn btn-100 btn-secondary btn-sm w-100" href="SeriesWiseReport.aspx"><i class="fa fa-file-arrow-up"></i>&nbsp;&nbsp;VIEW REPORTS</a>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>
       
</asp:Content>

