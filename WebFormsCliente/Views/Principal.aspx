<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Async="true"
    Inherits="WebFormsCliente.Views.Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Principal</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" ID="btn_GetEquiposAsync" Text="Obtener Lista Equipos" OnClick="btn_GetEquiposAsync_Click"/>
        </div>
        <div>
            <asp:PlaceHolder runat="server" ID="marcadoListaEquipos"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
