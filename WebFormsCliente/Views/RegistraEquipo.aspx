<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistraEquipo.aspx.cs" Async="true"
    Inherits="WebFormsCliente.Views.RegistraEquipo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registrar Equipo</title>
    <style>
        .inputTexto, .inputNumero{
            width:250px;
            border-color:blue;
            border-radius:10px;
        }
        .inputNumero{
            width:60px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="Nombre" Text="Nombre"></asp:Label>
            <asp:TextBox ID="txbNombre" runat="server" CssClass=".inputTexto"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" ID="Apodo" Text="Apodo"></asp:Label>
            <asp:TextBox ID="txbApodo" runat="server" CssClass=".inputTexto"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" ID="Fundacion" Text="Fundación"></asp:Label>
            <asp:Calendar runat="server" ID="calendarFundacion"></asp:Calendar>
        </div>
        <div>
            <asp:Label runat="server" ID="Campeonatos" Text="Número de campeonatos"></asp:Label>
            <asp:TextBox ID="txbCampeonatos" runat="server" CssClass=".inputNumero"></asp:TextBox>
        </div>
        <div id="registradoContenedor" runat="server"></div>
        <asp:Button runat="server" ID="btnPostEquipo" Text="Registrar" OnClick="btnPostEquipo_Click"/>
        <asp:Button runat="server" ID="btnActualizarEquipo" Text="Actualizar" OnClick="btnUpdateEquipo_Click"/>
    </form>
</body>
</html>
