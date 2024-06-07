<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="waCubos.login" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table border="0">
        <tr>
            <td>Usuario</td>
            <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            <td rowspan="3" valign="top">
                <asp:Label ID="tag" runat="server" ForeColor="#CC0000"></asp:Label></td>
        </tr>
        <tr>
            <td>Password</td>
            <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="right"><asp:Button ID="Button1" runat="server" Text="Entrar" 
                                                      onclick="Button1_Click" /></td>
        </tr>
    </table>
</asp:Content>
