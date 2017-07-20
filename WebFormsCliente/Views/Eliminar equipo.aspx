<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Eliminar equipo.aspx.cs" Async="true"
    Inherits="WebFormsCliente.Views.Eliminar_equipo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Eliminar equipo</title>
    <style>
        .azul{
            color:blue;
            font-size:1.2em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" Text="Equipo" CssClass="azul" />
            <asp:TextBox ID="txbEquipo" runat="server" />
            <asp:Button runat="server" OnClick="Unnamed_Click"/>
        </div>
        <div id="resultadoEliminacion" runat="server"></div>
    </form>
</body>
</html>
