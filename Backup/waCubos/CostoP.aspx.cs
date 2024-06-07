using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace waCubos
{
    public partial class CostoP : Page
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


            bool totales = false;
            var titulos = new Dictionary<int, string>();

            //presupuestoTi
            //ssTi


            foreach (DataTable table in bin.CostoPonderado(TextBox1.Text,TextBox2.Text).Tables)
            {
                label1.Text += "<table class=\"savanaSucursal\" >";
                
                
                label1.Text += "<tr>";
                label1.Text += "<th style='width:140px;'>SUCURSAL</th>";
                label1.Text += "<th style='width:165px;'>DEPARTAMENTO</th>";
                label1.Text += "<th style='width:115px;'>VENTA TOTAL</th>";
                label1.Text += "<th style='width:115px;'>IMPORTE COSTO PONDERADO</th>";
                label1.Text += "<th style='width:115px;'>% COSTO PONDERADO VENTA</th>";
                label1.Text += "<th style='width:115px;'>IMPORTE COSTO COMPRAS</th>";
                label1.Text += "<th style='width:115px;'>% COSTO COMPRAS</th>";
                label1.Text += "</tr>";


                label1.Text += "<tr><td colspan='7' style='padding:0px;'>";
                label1.Text += "<div style=\"overflow:scroll;height:495px;\">";
                label1.Text += "<table cellpadding='0' cellspacing ='0'>";

                int num = 0;
                int fil = 0;

                var clas = "ssTi";
                foreach (DataRow row in table.Rows)
                {

                    if (fil == 4)
                    {
                        fil = 0;
                    }

                    if (fil == 0 || fil == 1)
                    {
                        clas = "ssTi";
                    }

                    if(fil==2 || fil==3)
                    {
                        clas = "presupuestoTi";
                    }


                    label1.Text += "<tr>";
                    if (num % 2==0)
                    {
                        label1.Text += "<td style=\"min-width: 134px;\" class=\"" + clas + "\" rowspan=\"2\" align=\"center\">" + row.ItemArray[1] + "</td>";
                    }


                    label1.Text += "<td style=\"min-width: 160px;\" class=\"" + clas + "\">" + row.ItemArray[2] + "</td>";
                    label1.Text += "<td style=\"min-width: 109px;\" class=\"" + clas + "\" align=\"right\">" + Convert.ToDouble(row.ItemArray[3]).ToString("N2") + "</td>";
                    label1.Text += "<td style=\"min-width: 113px;\" class=\"" + clas + "\" align=\"right\">" + Convert.ToDouble(row.ItemArray[4]).ToString("N2") + "</td>";
                    label1.Text += "<td style=\"min-width: 111px;\" class=\"" + clas + "\" align=\"right\">" + Math.Round(Convert.ToDouble(row.ItemArray[5]), 2) + "%</td>";
                    label1.Text += "<td style=\"min-width: 110px;\" class=\"" + clas + "\" align=\"right\">" + Convert.ToDouble(row.ItemArray[6]).ToString("N2") + "</td>";
                    label1.Text += "<td style=\"min-width: 93px;\" class=\"" + clas + "\" align=\"right\">" + Math.Round(Convert.ToDouble(row.ItemArray[7]), 2) + "%</td>";
                    label1.Text += "</tr>";

                    

                    ++num;
                    ++fil;
                }
                label1.Text += "</table>";

                label1.Text += "</div></td></tr>";
                label1.Text += "</table>";

            }

            tag.Text = label1.Text;
        }

        protected void Empresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (DataTable table in new bin(Empresa.SelectedValue).Grupos().Tables)
            //{
            //    Grupo.Items.Clear();
            //    foreach (DataRow row in table.Rows)
            //        Grupo.Items.Add(new ListItem(row.ItemArray[0].ToString(), row.ItemArray[0].ToString()));
            //}
        }


    }
}
