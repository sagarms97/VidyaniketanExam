<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo.aspx.cs" Inherits="Demo" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://cdn.ckeditor.com/ckeditor5/40.2.0/classic/ckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                                      
        <div id="editor" runat="server"></div>
        
   
                <script type="text/javascript">
                        ClassicEditor
                                .create( document.querySelector( '#editor' ) )
                                .then( editor => {
                                    console.log( editor );
                                } )
                                .catch( error => {
                                        console.error( error )
                                } );
                </script>
         

      
    </div>
      <asp:Button ID="btnsubmit" OnClick="btnsubmit_Click" runat="server" />
        <p>
 <asp:TextBox ID="txtname" runat="server" CssClass="editor"></asp:TextBox>

    </form>
</body>
</html>
