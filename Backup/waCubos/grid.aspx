<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="grid.aspx.cs" Inherits="waCubos.grid" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href='<%= this.ResolveUrl("~/Styles/Tabla.css") %>' rel="stylesheet" type="text/css" />
    <%--<link href='<%= this.ResolveUrl("~/Styles/GridviewScroll.css") %>' rel="stylesheet" type="text/css" />--%>

    
    <script src='<%= this.ResolveUrl("~/Scripts/1.9.0.jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= this.ResolveUrl("~/Scripts/jquery-ui-1.9.2.custom.min.js") %>' type="text/javascript"></script>
    <script src='<%= this.ResolveUrl("~/Scripts/jquery.ui.touch-punch.min.js") %>' type="text/javascript"></script>

    <script src='<%= this.ResolveUrl("~/Scripts/gridviewScroll.min.js") %>' type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent"> 

    <table>
        <tr>
            <td>
            </td>
            <td colspan="2">&nbsp;(aaaaa-mm-dd)
            </td>
        </tr>
        <tr>
            <td>Fecha
            </td>
            <td><asp:TextBox ID="TextBox1" runat="server" MaxLength="10">2015-05-20</asp:TextBox>
            </td>
            <td><asp:Button ID="Button1" runat="server" Text="Calcular" onclick="Button1_Click" 
                    PostBackUrl="~/grid.aspx" /></td>
        </tr>
        <tr>
            <td>Serie</td>
            <td colspan="2">
                <asp:TextBox ID="TextBox2" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Empresa</td>
            <td colspan="2">
                <asp:DropDownList ID="Empresa" runat="server">
                    <asp:ListItem Value="BD1" Selected="True">Queretaro</asp:ListItem>
                    <asp:ListItem Value="BD2">Mexico</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Grupo</td>
            <td colspan="2">
                <asp:DropDownList ID="Grupo" runat="server" Visible="False">
                    <asp:ListItem Value="WA" Selected="True">WA</asp:ListItem>
                    <asp:ListItem Value="GAD">GAD</asp:ListItem>
                    <asp:ListItem Value="GLOTONERIA">GLT</asp:ListItem>
		    <asp:ListItem Value="HELOTES">HLT</asp:ListItem> 
                </asp:DropDownList>
            </td>
        </tr>
    </table>


    <asp:Label ID="tag"  runat="server"></asp:Label>
    <asp:Label ID="lblJavaScript" runat="server"></asp:Label>
    

</asp:Content>
