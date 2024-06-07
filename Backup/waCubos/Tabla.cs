using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waCubos
{
    public static class Tabla
    {
        public static string Head(string concepto0, string serie1, string sem2, string sem3, string sem4,
            string sem5, string col6, string col7, string col8, string col9, string col10)
        {

            string head = @"
<table class='savanaSucursal2' style='margin-bottom:17px;'>
    <tr>
        <td>

<table id='GridView1' style='width:100%;border-collapse:collapse;' border='1' class='savanaSucursal2'>
    <tr>
        <th rowspan='2'>{0}</th>
        <th colspan='2'>{1}</th>
        <th colspan='2' class='ssGrid2'>{2}</th>
        <th colspan='2' class='ssGrid2'>{3}</th>
        <th colspan='2' class='ssGrid2'>{4}</th>
        <th colspan='2' class='ssGrid2'>{5}</th>
        <th colspan='3'>&nbsp;</th>
    </tr>
    <tr>
        <th>{6}</th>
        <th>{7}</th>
        <th>PAGO FISCAL</th>
        <th>PAGO NO FISCAL</th>
        <th>PAGO FISCAL</th>
        <th>PAGO NO FISCAL</th>
        <th>PAGO FISCAL</th>
        <th>PAGO NO FISCAL</th>
        <th>PAGO FISCAL</th>
        <th>PAGO NO FISCAL</th>
        <th>{8}</th>
        <th>{9}</th>
        <th>{10}</th>
    </tr>".Replace("'", "\"");


            return string.Format(head, concepto0, serie1, sem2, sem3, sem4, sem5, col6, col7, col8, col9, col10);
        }

        public static string Seccion(string seccion)
        {
            string fila = @"
    <tr>
   	    <td style='border-left-width:2px;background-color:#B9B9B9;text-align:right;'>
            <div class='cut'><strong>{0}</strong></div>
        </td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
    </tr>".Replace("'", "\"");

            return string.Format(fila, seccion);
        }



        public static string Detalle(string concepto, DatosTbl tbl)
        {

            string fila = @"
    <tr>
	    <td style='border-left-width:2px;padding-left:1px;background-color:#EBEBEB;text-align:right;'>
            <div style='border:1px #C8C8C8 solid;' class='cut'>{0}</div>
        </td>
	    <td style='text-align:right' class='ssGrid2'>{1}</td>
	    <td style='text-align:right' class='ssGrid2'>{2}</td>
	    <td style='text-align:right' class='ssGrid2'>{3}</td>
	    <td style='text-align:right' class='ssGrid2'>{4}</td>
	    <td style='text-align:right' class='ssGrid2'>{5}</td>
	    <td style='text-align:right' class='ssGrid2'>{6}</td>
	    <td style='text-align:right' class='ssGrid2'>{7}</td>
	    <td style='text-align:right' class='ssGrid2'>{8}</td>
	    <td style='text-align:right' class='ssGrid2'>{9}</td>
	    <td style='text-align:right' class='ssGrid2'>{10}</td>
	    <td class='{15}' style='background-color:{14};text-align:right'>{11}</td>
	    <td style='text-align:right' class='ssGrid2'>{12}</td>
	    <td style='text-align:right' class='ssGrid2'>{13}</td>
    </tr>".Replace("'", "\"");


            string semafor = "";
            string sem = "";

            if (tbl.Semaforo < 0)
            {
                semafor = "#FF5858";
                //sem = "semRed";
            }
            else
            {
                semafor = "#69FF69";
                //sem = "semGreen";
            }

            return string.Format(fila, concepto,
                tbl.Presupuesto.ToString("C2"),
                tbl.PorSobreVta.ToString("N2") + "%",
                tbl.Sem1F.ToString("C2"),
                tbl.Sem1Nf.ToString("C2"),
                tbl.Sem2F.ToString("C2"),
                tbl.Sem2Nf.ToString("C2"),
                tbl.Sem3F.ToString("C2"),
                tbl.Sem3Nf.ToString("C2"),
                tbl.Sem4F.ToString("C2"),
                tbl.Sem4Nf.ToString("C2"),
                tbl.Semaforo.ToString("C2"),
                tbl.TotalFiscal.ToString("C2"),
                tbl.TotalNoFiscal.ToString("C2"),
                semafor, sem);

        }

        public static string Totales(DatosTbl tbl)
        {
            string fila = @"
    <tr>
        <td style='border-left-width:2px;border-bottom-width:2px;background-color:#EBEBEB;text-align:right;'><strong>TOTAL</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{0}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{1}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{2}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{3}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{4}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{5}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{6}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{7}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{8}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{9}</strong></td>
	    <td class='{14}' style='border-bottom-width:2px;text-align:right;background-color:{13}'><strong>{10}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{11}</strong></td>
	    <td style='border-bottom-width:2px;text-align:right'><strong>{12}</strong></td>
    </tr>".Replace("'", "\"");

            string semafor = "";
            string sem = "";

            if (tbl.Semaforo < 0)
            {
                semafor = "#FF5858";
                //sem = "semRed";
            }
            else
            {
                semafor = "#69FF69";
                //sem = "semGreen";
            }

            return string.Format(fila,
                tbl.Presupuesto.ToString("C2"),
                tbl.PorSobreVta.ToString("N2") + "%",
                tbl.Sem1F.ToString("C2"),
                tbl.Sem1Nf.ToString("C2"),
                tbl.Sem2F.ToString("C2"),
                tbl.Sem2Nf.ToString("C2"),
                tbl.Sem3F.ToString("C2"),
                tbl.Sem3Nf.ToString("C2"),
                tbl.Sem4F.ToString("C2"),
                tbl.Sem4Nf.ToString("C2"),
                tbl.Semaforo.ToString("C2"),
                tbl.TotalFiscal.ToString("C2"),
                tbl.TotalNoFiscal.ToString("C2"),
                semafor, sem);
        }

        public static string Footer()
        {
            string footer = @"
            </table>
        </td>
    </tr>
</table>".Replace("'", "\"");

            return footer;
        }
    }
}