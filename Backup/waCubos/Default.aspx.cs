using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace waCubos
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string Etiqueta(string valor, string formato)
        {
            try
            {
                if (formato != "")
                    return Convert.ToDecimal(valor).ToString(formato, new CultureInfo("Es-MX"));
                return valor;
            }
            catch (Exception ex)
            {
                return valor;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bin bin = new bin(Empresa.SelectedValue);
            Label label1 = new Label();
            label1.Text = "";
            label1.Text += "<div style=\"overflow:scroll\"><table><tr>";

            bool totales = false;
            var titulos = new Dictionary<int, string>();

            foreach (DataTable table in bin.HojaCalculoTotales(TextBox1.Text, Grupo.SelectedValue).Tables)
            {
                label1.Text += "<td valign=\"top\">";
                label1.Text += "<table class=\"savanaSucursal\" >";
                int num = 0;
                
                foreach (DataRow row in table.Rows)
                {
                    if (num == 0)
                    {

                        if (row.ItemArray[2].ToString() == "TOTALES")
                        {
                            totales = true;
                            label1.Text += "<tr>";
                            label1.Text += "<th>" + row.ItemArray[2].ToString() + "</th>";
                            label1.Text += "</tr>";
                        }
                        else
                        {

                            label1.Text += "<tr>";
                            label1.Text += "<th colspan=2>" + row.ItemArray[2].ToString() + "</th>";
                            label1.Text += "</tr>";
                        }
                    }


                    if (num >= 1 && num <= 27)
                    {

                        string str1 = row.ItemArray[1].ToString();
                        label1.Text += "<tr>";
                        if (num <= 7)
                        {
                            if (str1 != "")
                            {
                                label1.Text += "<td class=\"presupuestoTi\">" + str1 + "</td>";

                            }
                            label1.Text += "<td class=\"presupuestoVa\">" + Etiqueta(row.ItemArray[2].ToString(), row.ItemArray[3].ToString()) + "</td>";

                        }
                        else
                        {
                            if (str1 != "")
                            {
                                label1.Text += "<td class=\"ssTi\">" + str1 + "</td>";
                            }

                            label1.Text += "<td class=\"ssVa\">" +
                                           Etiqueta(row.ItemArray[2].ToString(), row.ItemArray[3].ToString()) + "</td>";

                        }

                        label1.Text += "</tr>";
                    }


                    if (num >= 28)
                    {
                        string str1 = row.ItemArray[1].ToString();
                        label1.Text += "<tr>";


                        if (str1.StartsWith("VENTA ", true, CultureInfo.InvariantCulture))
                        {
                            var titulo = str1.Substring("VENTA ".Length);

                            label1.Text += "<tr>";
                            label1.Text += "<th colspan=2>" + titulo + "</th>";
                            label1.Text += "</tr>";

                            if (!titulos.ContainsKey(num))
                            {
                                titulos.Add(num,titulo);
                            }
                        }

                        if (totales && titulos.ContainsKey(num))
                        {
                            label1.Text += "<tr>";
                            label1.Text += "<th>" + titulos[num] + "</th>";
                            label1.Text += "</tr>";
                            
                        }


                        if (str1 != "")
                        {
                            label1.Text += "<td class=\"ssTi\">" + row.ItemArray[1] + "</td>";
                        }

                        label1.Text += "<td class=\"ssVa\">" +
                                       Etiqueta(row.ItemArray[2].ToString(), row.ItemArray[3].ToString()) + "</td>";



                        label1.Text += "</tr>";

                    }


                    ++num;
                }
                label1.Text += "</table>";
                label1.Text += "</td>";
            }
            label1.Text += "</tr></table></div>";
            tag.Text = label1.Text;
        }

        protected void Empresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataTable table in new bin(Empresa.SelectedValue).Grupos().Tables)
            {
                Grupo.Items.Clear();
                foreach (DataRow row in table.Rows)
                    Grupo.Items.Add(new ListItem(row.ItemArray[0].ToString(), row.ItemArray[0].ToString()));
            }
        }


    }
}
