<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CostoP.aspx.cs" Inherits="waCubos.CostoP" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>
            </td>
            <td colspan="2">&nbsp;(dd/mm/aaaa)
            </td>
        </tr>
        <tr>
            <td>Fecha Inicial
            </td>
            <td><asp:TextBox ID="TextBox1" runat="server" MaxLength="10"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        
        <tr>
            <td>Fecha Final
            </td>
            <td><asp:TextBox ID="TextBox2" runat="server" MaxLength="10"></asp:TextBox>
            </td>
            <td></td>
        </tr>

        <tr>
            <td>Empresa </td>
            <td colspan="2">
                <asp:DropDownList ID="Empresa" runat="server" onselectedindexchanged="Empresa_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="_BD1" Selected="True">Queretaro</asp:ListItem>
                    <asp:ListItem Value="_BD2">Mexico</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
       
        <tr>
            <td> 
            </td>
            <td> <asp:Button ID="Button1" runat="server" Text="Calcular" onclick="Button1_Click" 
                             PostBackUrl="~/CostoP.aspx" />
            </td>
            <td></td>
        </tr>
    </table>

    <asp:Label ID="tag" runat="server" Text=""></asp:Label>
    

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            $.datepicker.regional['es'] = {
                closeText: 'Cerrar',
                prevText: '< Ant',
                nextText: 'Sig >',
                currentText: 'Hoy',
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            };
            $.datepicker.setDefaults($.datepicker.regional['es']);

            $("#<%=TextBox1.ClientID %>").datepicker();
            $("#<%=TextBox1.ClientID %>").datepicker("option", "showAnim", "");


            $("#<%=TextBox2.ClientID %>").datepicker();
            $("#<%=TextBox2.ClientID %>").datepicker("option", "showAnim", "");
        });
    </script>  
    

</asp:Content>
