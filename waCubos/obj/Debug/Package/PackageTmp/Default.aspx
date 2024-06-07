<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="waCubos._Default" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" />
    <style>
         .btn-icono::before {
     content: '\f021'; /* Código del icono de Font Awesome */
     font-family: 'Font Awesome 5 Free';
     margin-right: 5px; /* Ajusta el espaciado entre el icono y el texto según sea necesario */
 }
    </style>
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
            <td>Fecha
            </td>
            <td><asp:TextBox ID="TextBox1" runat="server" MaxLength="10"></asp:TextBox>
            </td>
            <td><asp:Button ID="Button1" runat="server" Text="Calcular" onclick="Button1_Click" 
                            PostBackUrl="~/Default.aspx" /></td>
              <td><asp:Button ID="Button2" runat="server" Text="Detalles delivery" onclick="obtenerDetallesDelivery" 
                  PostBackUrl="~/Default.aspx" /></td>

        </tr>
        <tr>
            <td>Empresa </td>
            <td colspan="2">
                <asp:DropDownList ID="Empresa" runat="server" onselectedindexchanged="Empresa_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="BD1" Selected="True">Queretaro</asp:ListItem>
                    <asp:ListItem Value="BD2">Mexico</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Grupo</td>
            <td colspan="2">
                <asp:DropDownList ID="Grupo" runat="server">
                    <asp:ListItem Value="WA" Selected="True">WA</asp:ListItem>
                    <asp:ListItem Value="GAD">GAD</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>

    <div style="margin-top: 10px;">
        <asp:Button ID="Button3" runat="server" Text=" Exportar a excel" onclick="exportarExcel2" PostBackUrl="~/Default.aspx" style="background-color:#22b136; color:white; padding:10px; border-radius: 5px; border:none;"/>
        <asp:Button ID="Button4" runat="server" Text="Exportar a excel" onclick="exportarExcel" PostBackUrl="~/Default.aspx" style="background-color:#22b136; color:white; padding:10px; border-radius: 5px; border:none;" />
    </div>
    
   <asp:Label ID="tag" runat="server" Text=""></asp:Label>
    <div>
            <%--<asp:Literal ID="litRefreshIcon" runat="server" Text='<i class="fas fa-sync-alt"></i>'></asp:Literal>--%>
            <asp:Button type="button"  runat="server" onclick="updateData" id="btnupdate" PostBackUrl="~/Default.aspx" OnClientClick="return confirmarAccion();" Text="RECALCULAR" style="background-color:darkred; color:white; margin-top:20px; padding:10px; border-radius:5px; border:none;" />
         <asp:Button type="button"  runat="server" onclick="updateDataD" id="btnupdateD" PostBackUrl="~/Default.aspx" OnClientClick="return confirmarAccion();" Text="RECALCULAR"   style="background-color:darkred; color:white; margin-top:20px; padding:10px; border-radius:5px; border:none;" />
    </div>

 

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

            <%--$("#<%=TextBox1.ClientID %>").datepicker();--%>
           <%-- $("#<%=TextBox1.ClientID %>").datepicker("option", "showAnim", "");--%>
            $("#<%=TextBox1.ClientID %>").datepicker({
                onSelect: function (dateText, inst) {
                    // Obtener la fecha seleccionada en formato Date
                    var selectedDate = $(this).datepicker('getDate');

                    // Obtener la fecha actual
                    var currentDate = new Date();

                    // Restar un día a la fecha actual
                    currentDate.setDate(currentDate.getDate() - 1);

                    // Verificar si la fecha seleccionada es mayor que la fecha actual menos un día
                    if (selectedDate > currentDate) {
                        alert("No puede seleccionar una fecha mayor al corte del dia de ayer");
                        $(this).val(""); // Limpiar el valor del datepicker
                    }
                }
            });
            
        });


        function confirmarAccion() {
            return confirm('Este proceso calculará todos los datos nuevamente lo cual podría tardar más de 5 minutos \n ¿Está seguro que desea continuar?');
        }

    </script>  
   
</asp:Content>
