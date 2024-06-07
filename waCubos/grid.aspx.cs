using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace waCubos
{
    public partial class grid : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                bin bin = new bin(Empresa.SelectedValue);

                DataTable hoja = bin.Grid(TextBox1.Text, TextBox2.Text).Tables[0];

                if (hoja.Rows.Count == 0)
                {
                    tag.Text = "No se encontraron registros...";
                    return;
                }



                string sem0 = "", sem1 = "", sem2 = "", sem3 = "";

                try
                {
                    //---------Semanas----------//
                    DataRow semanas = bin.GridSemanas(TextBox1.Text, TextBox2.Text).Tables[0].Rows[0];
                    sem0 = semanas[0].ToString();
                    sem1 = semanas[1].ToString();
                    sem2 = semanas[2].ToString();
                    sem3 = semanas[3].ToString();
                }
                catch { }



                //---------Cabecera----------//
                DataColumnCollection cols = hoja.Columns;

                string tabla = Tabla.Head(
                    cols[3].ColumnName,
                    bin.serieNombre(TextBox2.Text),
                    sem0, sem1, sem2, sem3,
                    cols[4].ColumnName,
                    cols[5].ColumnName,
                    cols[14].ColumnName,
                    cols[15].ColumnName,
                    cols[16].ColumnName);

                //---------Cuerpo-----------//

                Dictionary<string, DatosTbl> datosTbl = new Dictionary<string, DatosTbl>();

                string seccionTmp = "";

                foreach (DataRow col in hoja.Rows)
                {
                    string seccion = col[1].ToString();
                    string concepto = col[3].ToString();



                    if (!datosTbl.ContainsKey(seccion))
                    {
                        if (seccionTmp != "" && seccionTmp != seccion)
                        {
                            //-----Totales-----//
                            tabla += Tabla.Totales(datosTbl[seccionTmp]);
                        }


                        //-----Secciones-----//
                        datosTbl.Add(seccion, new DatosTbl());
                        tabla += Tabla.Seccion(seccion);
                    }


                    DatosTbl tbl = new DatosTbl(
                        col[4], col[5], col[6], col[7], col[8], col[9], col[10],
                        col[11], col[12], col[13], col[14], col[15], col[16]);

                    //-----Detalle-----//
                    tabla += Tabla.Detalle(concepto, tbl);


                    //-----Suma Columnas-----//
                    datosTbl[seccion].Suma(tbl);


                    seccionTmp = seccion;
                }

                if (seccionTmp != "")
                {
                    //-----Totales-----//
                    tabla += Tabla.Totales(datosTbl[seccionTmp]);
                }

                //---------Footer-----------//
                tabla += Tabla.Footer();

                //*************************************************************************************//


                tag.Text = tabla;

                lblJavaScript.Text = @"
                <script type='text/javascript'>
                    $(document).ready(function () {

                    function gridviewScroll() {
                            gridView1 = $('#GridView1').gridviewScroll({
                                width: 912,
                                height: 550,
                                railcolor: '#F0F0F0',
                                barcolor: '#CDCDCD',
                                barhovercolor: '#606060',
                                bgcolor: '#F0F0F0',
                                freezesize: 1,
                                arrowsize: 30,
                                varrowtopimg: 'images/arrowvt.png',
                                varrowbottomimg: 'images/arrowvb.png',
                                harrowleftimg: 'images/arrowhl.png',
                                harrowrightimg: 'images/arrowhr.png',
                                headerrowcount: 2,
                                railsize: 16,
                                barsize: 15,
                                startVertical:0,
                                startHorizontal:0
                            });
                        }

                        gridviewScroll();
                        $('#GridView1Wrapper').css('height','auto');
                        
                    });
                   </script>";

            }
            catch (Exception ex)
            {
                tag.Text = ex.Message;
            }
        }
    }
}