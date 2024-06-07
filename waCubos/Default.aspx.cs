using DataBaseConnector;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using waCubos.Properties;
using static System.Net.WebRequestMethods;

namespace waCubos
{
    public partial class _Default : Page
    {
        public DataSet _dataSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            Button3.Visible = false;
            Button4.Visible = false;    
            btnupdate.Visible = false;
            btnupdateD.Visible = false;
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

            string jsond = getJsonData(TextBox1.Text, Grupo.SelectedValue, Empresa.SelectedValue, 0);
            if (jsond.Equals(""))
            {
                _dataSet = bin.HojaCalculoTotales(TextBox1.Text, Grupo.SelectedValue, Empresa.SelectedValue);
                btnupdate.Visible = false;
                btnupdateD.Visible = false;
            }
            else 
            {
                btnupdate.Visible = true;
                btnupdateD.Visible = false;
                _dataSet = JsonConvert.DeserializeObject<DataSet>(jsond);
            }

            

            foreach (DataTable table in _dataSet.Tables)
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
                                    titulos.Add(num, titulo);
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

            ViewState["MiVariable"] = _dataSet;
            Button3.Visible = true;
            Button4.Visible = false;
        }


        protected void obtenerDetallesDelivery(object sender, EventArgs e)
        {

            bin bin = new bin(Empresa.SelectedValue);
            Label label1 = new Label();
            label1.Text = "";
            label1.Text += "<div style=\"overflow:scroll\"><table><tr>";

            bool totales = false;
            var titulos = new Dictionary<int, string>();

            string jsond = getJsonData(TextBox1.Text, Grupo.SelectedValue, Empresa.SelectedValue, 1);
            if (jsond.Equals(""))
            {
                _dataSet = bin.DetallesDelivery(TextBox1.Text, Grupo.SelectedValue, Empresa.SelectedValue);
                btnupdateD.Visible = false;
                btnupdate.Visible = false;
            }
            else
            {
                btnupdateD.Visible = true;
                btnupdate.Visible = false;
                _dataSet = JsonConvert.DeserializeObject<DataSet>(jsond);
            }
            

            foreach (DataTable table in _dataSet.Tables)
            {
                label1.Text += "<td valign=\"top\">";
                label1.Text += "<table class=\"savanaSucursal\" >";
                int num = 0;
                string nombreapp = "";
                
                foreach (DataRow row in table.Rows)
                {
                    if (num == 0)
                    {
                        label1.Text += "<th colspan=2>" + row.ItemArray[0].ToString() + "</th>";
                        num++; 
                    }

                        if (!nombreapp.Equals(row.ItemArray[1].ToString())) 
                        {
                            label1.Text += "<tr><th colspan=2>" + row.ItemArray[1].ToString().Replace(" /",":") + "</th></tr>";
                            nombreapp = row.ItemArray[1].ToString();
                        }
                        double tkpromedio;
                        if (double.Parse(row.ItemArray[4].ToString()) == 0) 
                        {
                            tkpromedio = 0; 
                        } else { tkpromedio = double.Parse(row.ItemArray[4].ToString()) / int.Parse(row.ItemArray[2].ToString()); }

                        label1.Text += "<tr><td class=\"ssTi\">VENTA "+nombreapp.Substring(nombreapp.LastIndexOf("/")+1).Trim()+"</><td class=\"ssVa\">" + double.Parse(row.ItemArray[4].ToString()).ToString("N2") + "</td></tr>";
                        label1.Text += "<tr><td class=\"ssTi\">COMENSALES " + nombreapp.Substring(nombreapp.LastIndexOf("/") + 1).Trim() + "</><td class=\"ssVa\">" + int.Parse(row.ItemArray[3].ToString()) + "</td></tr>";
                        label1.Text += "<tr><td class=\"ssTi\">TOTAL TICKETS " + nombreapp.Substring(nombreapp.LastIndexOf("/") + 1).Trim() + "</><td class=\"ssVa\">" + int.Parse(row.ItemArray[2].ToString()) + "</td></tr>";
                        label1.Text += "<tr><td class=\"ssTi\">TICKET PROMEDIO " + nombreapp.Substring(nombreapp.LastIndexOf("/") + 1).Trim() + "</><td class=\"ssVa\">" + tkpromedio.ToString("N2") + "</td></tr>";
                        
                }

                label1.Text += "</table>";
                label1.Text += "</td>";
            }
            label1.Text += "</tr></table></div>";
            tag.Text = label1.Text;

            ViewState["MiVariable"] = _dataSet;
            Button3.Visible = false;
            Button4.Visible = true;
        }

        protected void Empresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (DataTable table in new bin(Empresa.SelectedValue).Grupos().Tables)
            //{
            //    Grupo.Items.Clear();
            //    foreach (DataRow row in table.Rows)
            //        Grupo.Items.Add(new ListItem(row.ItemArray[0].ToString(), row.ItemArray[0].ToString()));
            //}
            if (Empresa.SelectedValue.Equals("BD1"))
            {
                Grupo.Items.Clear();
                Grupo.Items.Add(new ListItem("WA","WA"));
                Grupo.Items.Add(new ListItem("GAD","GAD"));
            }
            else 
            {
                Grupo.Items.Clear();
                Grupo.Items.Add(new ListItem("WA", "WA"));
            }
        }

        public void exportarExcel(object sender, EventArgs e) 
        {
            _dataSet = ViewState["MiVariable"] as DataSet;
            // Obtener la ruta del directorio actual
            string directorioActual = Server.MapPath("~");

            // Puedes concatenar la ruta con subdirectorios específicos si es necesario
            string rutaCompleta = Path.Combine(directorioActual, "DetallesDelivery.xlsx");


            bin bin = new bin(Empresa.SelectedValue);
            if (_dataSet == null) 
            {
                _dataSet = bin.DetallesDelivery(TextBox1.Text, Grupo.SelectedValue, Empresa.SelectedValue);
            } 
            // Convertir DataSet a archivo Excel
            byte[] excelFile = ConvertDataSetToExcel(_dataSet);

            // Guardar el archivo Excel en el disco
            System.IO.File.WriteAllBytes(rutaCompleta, excelFile);

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename= DetallesDelivery.xlsx");
            Response.TransmitFile(rutaCompleta);
            Response.End();
        }

        public void exportarExcel2(object sender, EventArgs e)
        {
            _dataSet = ViewState["MiVariable"] as DataSet;
            // Obtener la ruta del directorio actual
            string directorioActual = Server.MapPath("~");

            // Puedes concatenar la ruta con subdirectorios específicos si es necesario
            string rutaCompleta = Path.Combine(directorioActual, "CalculosTotales.xlsx");


            bin bin = new bin(Empresa.SelectedValue);
            if (_dataSet == null)
            {
                _dataSet = bin.HojaCalculoTotales(TextBox1.Text, Grupo.SelectedValue, Empresa.SelectedValue);
            }
            // Convertir DataSet a archivo Excel
            byte[] excelFile = ConvertDataSetToExcel2(_dataSet);

            // Guardar el archivo Excel en el disco
            System.IO.File.WriteAllBytes(rutaCompleta, excelFile);

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename= CalculosTotales.xlsx");
            Response.TransmitFile(rutaCompleta);
            Response.End();
        }

        public static byte[] ConvertDataSetToExcel(DataSet dataSet)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Hoja1");

                int startColumn = 1;
                int num = 0;
                string nombreapp = "";
                int startRow = 1;

                var celda = worksheet.Cells["A1"];
                // Agregar estilos a la celda
                var estiloCeldaT = celda.Style;

                // Iterar a través de las tablas en el DataSet
                foreach (DataTable table in dataSet.Tables)
                {   
                    startRow =1;
                    num = 0;    
                    foreach (DataRow row in table.Rows)
                    {

                        if (num == 0)
                        {
                            var mergedCells = worksheet.Cells[startRow, startColumn, startRow, startColumn+1];
                            mergedCells.Merge = true;
                            worksheet.Cells[startRow, startColumn].Value = row.ItemArray[0].ToString();
                            celda = worksheet.Cells[startRow, startColumn];
                            // Agregar estilos a la celda
                            estiloCeldaT = celda.Style;
                            estiloCeldaT.Font.Bold = true; // Texto en negrita
                            estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                            estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(176, 20, 20)); // Color de fondo
                            estiloCeldaT.Font.Color.SetColor(System.Drawing.Color.White);
                            estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);
                            num++;

                        }

                        if (!nombreapp.Equals(row.ItemArray[1].ToString()))
                        {
                            if (num > 0) { startRow++; }
                           
                            var mergedCells = worksheet.Cells[startRow, startColumn, startRow, startColumn + 1];
                            mergedCells.Merge = true;
                            worksheet.Cells[startRow, startColumn].Value = row.ItemArray[1].ToString().Replace(" /", ":");
                            celda = worksheet.Cells[startRow, startColumn];
                            // Agregar estilos a la celda
                            estiloCeldaT = celda.Style;
                            estiloCeldaT.Font.Bold = true; // Texto en negrita
                            estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                            estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(176, 20, 20)); // Color de fondo
                            estiloCeldaT.Font.Color.SetColor(System.Drawing.Color.White);
                            estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);
                            nombreapp = row.ItemArray[1].ToString();
                            startRow++;
                        } else { startRow++; }
                        double tkpromedio;
                        if (double.Parse(row.ItemArray[4].ToString()) == 0)
                        {
                            tkpromedio = 0;
                        }
                        else { tkpromedio = double.Parse(row.ItemArray[4].ToString()) / int.Parse(row.ItemArray[2].ToString()); }

                        worksheet.Cells[startRow, startColumn].Value = "VENTA " + nombreapp.Substring(nombreapp.LastIndexOf("/") + 1).Trim();
                        celda = worksheet.Cells[startRow, startColumn];
                        // Agregar estilos a la celda
                        estiloCeldaT = celda.Style;
                        estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                        estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                        estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);

                        startRow++;

                        worksheet.Cells[startRow, startColumn].Value = "COMENSALES " + nombreapp.Substring(nombreapp.LastIndexOf("/") + 1).Trim();
                        celda = worksheet.Cells[startRow, startColumn];
                        // Agregar estilos a la celda
                        estiloCeldaT = celda.Style;
                        estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                        estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                        estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);

                        startRow++;
                        
                        worksheet.Cells[startRow, startColumn].Value = "TOTAL TICKETS " + nombreapp.Substring(nombreapp.LastIndexOf("/") + 1).Trim();
                        celda = worksheet.Cells[startRow, startColumn];
                        // Agregar estilos a la celda
                        estiloCeldaT = celda.Style;
                        estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                        estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                        estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);

                        startRow++;

                        worksheet.Cells[startRow, startColumn].Value = "TICKET PROMEDIO " + nombreapp.Substring(nombreapp.LastIndexOf("/") + 1).Trim();
                        celda = worksheet.Cells[startRow, startColumn];
                        // Agregar estilos a la celda
                        estiloCeldaT = celda.Style;
                        estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                        estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                        estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);

                        startColumn++;
                        startRow = startRow -3;



                        worksheet.Cells[startRow, startColumn].Value = double.Parse(row.ItemArray[4].ToString()).ToString("N2");
                        celda = worksheet.Cells[startRow, startColumn];
                        // Agregar estilos a la celda
                        estiloCeldaT = celda.Style;
                        estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                        estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                        estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);

                        startRow++;

                        worksheet.Cells[startRow, startColumn].Value = int.Parse(row.ItemArray[3].ToString());
                        celda = worksheet.Cells[startRow, startColumn];
                        // Agregar estilos a la celda
                        estiloCeldaT = celda.Style;
                        estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                        estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                        estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);

                        startRow++;

                        worksheet.Cells[startRow, startColumn].Value = int.Parse(row.ItemArray[2].ToString());
                        celda = worksheet.Cells[startRow, startColumn];
                        // Agregar estilos a la celda
                        estiloCeldaT = celda.Style;
                        estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                        estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                        estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);

                        startRow++;

                        worksheet.Cells[startRow, startColumn].Value = tkpromedio.ToString("N2");
                        celda = worksheet.Cells[startRow, startColumn];
                        // Agregar estilos a la celda
                        estiloCeldaT = celda.Style;
                        estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                        estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                        estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);

                        startColumn--;
                        
                    }
                    startColumn = startColumn + 2;
                }



                // Autoajustar el ancho de las columnas
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Convertir el paquete de Excel a un array de bytes
                return package.GetAsByteArray();
            }
        }


        public static byte[] ConvertDataSetToExcel2(DataSet dataSet)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Hoja1");

                int startColumn = 1;
                int num = 0;
                string nombreapp = "";
                int startRow = 1;
                var celda = worksheet.Cells[startRow, startColumn];
                var estiloCeldaT = celda.Style;
                estiloCeldaT.Font.Bold = true; // Texto en negrita
                estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(176, 20, 20)); // Color de fondo
                estiloCeldaT.Font.Color.SetColor(System.Drawing.Color.White);
                estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);
                string valor = "";

                Boolean totales = false;
                // Iterar a través de las tablas en el DataSet
                foreach (DataTable table in dataSet.Tables)
                {
                    startRow = 1;

                    foreach (DataRow row in table.Rows) 
                    {
                        if (row.ItemArray[0].ToString().Equals("0.00"))
                        {
                            if (row[2].ToString().Equals("TOTALES"))
                            {
                                totales = true;

                                worksheet.Cells[startRow, startColumn].Value = row[2].ToString();
                                celda = worksheet.Cells[startRow, startColumn];
                                // Agregar estilos a la celda
                                estiloCeldaT = celda.Style;
                                estiloCeldaT.Font.Bold = true; // Texto en negrita
                                estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                                estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(176, 20, 20)); // Color de fondo
                                estiloCeldaT.Font.Color.SetColor(System.Drawing.Color.White);
                                estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);
                                startRow++;
                            }
                            else 
                            {
                                var mergedCells = worksheet.Cells[startRow, startColumn, startRow, startColumn + 1];
                                mergedCells.Merge = true;
                                worksheet.Cells[startRow, startColumn].Value = row[2].ToString();
                                celda = worksheet.Cells[startRow, startColumn];
                                // Agregar estilos a la celda
                                estiloCeldaT = celda.Style;
                                estiloCeldaT.Font.Bold = true; // Texto en negrita
                                estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                                estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(176, 20, 20)); // Color de fondo
                                estiloCeldaT.Font.Color.SetColor(System.Drawing.Color.White);
                                estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);
                                startRow++;
                            }

                          
                           
                        }
                        else 
                        {
                            if (row.ItemArray[0].ToString().Equals("28.00") || row.ItemArray[0].ToString().Equals("32.00")
                                || row.ItemArray[0].ToString().Equals("36.00") || row.ItemArray[0].ToString().Equals("40.00")
                                || row.ItemArray[0].ToString().Equals("44.00"))
                            {

                                if (totales)
                                {
                                    worksheet.Cells[startRow, startColumn].Value = worksheet.Cells[startRow, startColumn-2].Value;
                                    celda = worksheet.Cells[startRow, startColumn];
                                    // Agregar estilos a la celda
                                    estiloCeldaT = celda.Style;
                                    estiloCeldaT.Font.Bold = true; // Texto en negrita
                                    estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                                    estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(176, 20, 20)); // Color de fondo
                                    estiloCeldaT.Font.Color.SetColor(System.Drawing.Color.White);
                                    estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);
                                    startRow++;
                                }
                                else 
                                {
                                    var mergedCells = worksheet.Cells[startRow, startColumn, startRow, startColumn + 1];
                                    mergedCells.Merge = true;
                                    worksheet.Cells[startRow, startColumn].Value = row[1].ToString().Substring(6);
                                    celda = worksheet.Cells[startRow, startColumn];
                                    // Agregar estilos a la celda
                                    estiloCeldaT = celda.Style;
                                    estiloCeldaT.Font.Bold = true; // Texto en negrita
                                    estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                                    estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(176, 20, 20)); // Color de fondo
                                    estiloCeldaT.Font.Color.SetColor(System.Drawing.Color.White);
                                    estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);
                                    startRow++;
                                }
                            }

                            if (totales)
                            {
                                if (row[0].ToString().Equals("12.00") || row[0].ToString().Equals("13.00") ||
                                    row[0].ToString().Equals("14.00") || row[0].ToString().Equals("16.00"))
                                {
                                    valor = (Double.Parse(row[2].ToString()) * 100).ToString("N2") + " %";
                                }
                                else { valor = row[2].ToString().Contains(".") ? Double.Parse(row[2].ToString()).ToString("N2") : row[2].ToString(); }
                                worksheet.Cells[startRow, startColumn].Value = valor;
                                celda = worksheet.Cells[startRow, startColumn];
                                // Agregar estilos a la celda
                                estiloCeldaT = celda.Style;
                                estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                                estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                                estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);
                            }
                            else 
                            {
                                if (row[0].ToString().Equals("12.00") || row[0].ToString().Equals("13.00") ||
                                    row[0].ToString().Equals("14.00") || row[0].ToString().Equals("16.00"))
                                {
                                    valor = (Double.Parse(row[2].ToString()) * 100).ToString("N2") + " %";
                                }
                                else { valor = row[2].ToString().Contains(".") ? Double.Parse(row[2].ToString()).ToString("N2") : row[2].ToString(); }
                                worksheet.Cells[startRow, startColumn].Value = row[1].ToString();
                                celda = worksheet.Cells[startRow, startColumn];
                                // Agregar estilos a la celda
                                estiloCeldaT = celda.Style;
                                estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                                estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                                estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells[startRow, startColumn + 1].Value = valor;
                                celda = worksheet.Cells[startRow, startColumn+1];
                                // Agregar estilos a la celda
                                estiloCeldaT = celda.Style;
                                estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                                estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 204, 153)); // Color de fondo
                                estiloCeldaT.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);
                            }
                          

                            startRow++;

                        }
                      
                    }

                    startColumn = startColumn + 2;

                }

       
                celda = worksheet.Cells[2,1,8,startColumn-2];
                // Agregar estilos a la celda
                estiloCeldaT = celda.Style;
                estiloCeldaT.Fill.PatternType = ExcelFillStyle.Solid; // Relleno sólido
                estiloCeldaT.Fill.BackgroundColor.SetColor(Color.FromArgb(102, 102, 51)); // Color de fondo
                estiloCeldaT.Font.Color.SetColor(Color.White);
                estiloCeldaT.Border.BorderAround(ExcelBorderStyle.Thin);

                // Autoajustar el ancho de las columnas
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Convertir el paquete de Excel a un array de bytes
                return package.GetAsByteArray();
            }
        }


        public string getJsonData(string fecha, string grupo, string ciudad, int detdel)
        {
            try
            {
                Database dbc = null;
                SqlConnection conn;
                SqlCommand comm;
                DataBaseConnector.ParameterCollection Coll = new DataBaseConnector.ParameterCollection();
                Coll.addParameter(new SqlParameter("@FECHA", fecha));
                Coll.addParameter(new SqlParameter("@GRUPO", grupo));
                Coll.addParameter(new SqlParameter("@CIUDAD", ciudad));
                Coll.addParameter(new SqlParameter("@DETALLESDELIVERY", detdel));

                conn = new SqlConnection(Settings.Default.Cadena.Replace("%bd%", "BD2"));
                conn.Open();
                comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandTimeout = Settings.Default.TimeOut;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "WAS_GET_REPORTE_TEMP";
                comm.Parameters.Clear();
                for (int i = 1; i <= Coll.Parameters.Count; i++)
                {
                    comm.Parameters.Add(Coll.Parameters[i]);
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                comm.Dispose();
                conn.Close();
                conn.Dispose();

                return dt.Rows[0]["JSONDATA"].ToString();

            }
            catch (Exception ex)
            {
                return "";
            }

        }


        public void updateData(object sender, EventArgs e)
        {
            try
            {

                string fecha = TextBox1.Text;
                string grupo = Grupo.SelectedValue;
                string ciudad = Empresa.SelectedValue;
                string jsondata = "";
                DataSet _dataSet;
                bin bin = new bin("BD2");
                _dataSet = bin.HojaCalculoTotales(fecha, grupo, ciudad);
                //Convertir DataSet a cadena JSON
               jsondata = ConvertirDataSetAJson(_dataSet);

                int detdel = 0;
                Database dbc = null;
                SqlConnection conn;
                SqlCommand comm;
                DataBaseConnector.ParameterCollection Coll = new DataBaseConnector.ParameterCollection();
                Coll.addParameter(new SqlParameter("@FECHA", fecha));
                Coll.addParameter(new SqlParameter("@GRUPO", grupo));
                Coll.addParameter(new SqlParameter("@CIUDAD", ciudad));
                Coll.addParameter(new SqlParameter("@JSONDATA", jsondata));
                Coll.addParameter(new SqlParameter("@DETALLESDELIVERY", detdel));

                conn = new SqlConnection(Settings.Default.Cadena.Replace("%bd%", "BD2"));
                conn.Open();
                comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandTimeout = Settings.Default.TimeOut;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "WAS_UPDATE_REPORTE_TEMP";
                comm.Parameters.Clear();
                for (int i = 1; i <= Coll.Parameters.Count; i++)
                {
                    comm.Parameters.Add(Coll.Parameters[i]);
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                comm.Dispose();
                conn.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void updateDataD(object sender, EventArgs e)
        {
            try
            {

                string fecha = TextBox1.Text;
                string grupo = Grupo.SelectedValue;
                string ciudad = Empresa.SelectedValue;
                string jsondata = "";
                DataSet _dataSet;
                bin bin = new bin("BD2");
                _dataSet = bin.DetallesDelivery(fecha, grupo, ciudad);
                // Convertir DataSet a cadena JSON
                jsondata = ConvertirDataSetAJson(_dataSet);

                int detdel = 1;
                Database dbc = null;
                SqlConnection conn;
                SqlCommand comm;
                DataBaseConnector.ParameterCollection Coll = new DataBaseConnector.ParameterCollection();
                Coll.addParameter(new SqlParameter("@FECHA", fecha));
                Coll.addParameter(new SqlParameter("@GRUPO", grupo));
                Coll.addParameter(new SqlParameter("@CIUDAD", ciudad));
                Coll.addParameter(new SqlParameter("@JSONDATA", jsondata));
                Coll.addParameter(new SqlParameter("@DETALLESDELIVERY", detdel));

                conn = new SqlConnection(Settings.Default.Cadena.Replace("%bd%", "BD2"));
                conn.Open();
                comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandTimeout = Settings.Default.TimeOut;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "WAS_UPDATE_REPORTE_TEMP";
                comm.Parameters.Clear();
                for (int i = 1; i <= Coll.Parameters.Count; i++)
                {
                    comm.Parameters.Add(Coll.Parameters[i]);
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                comm.Dispose();
                conn.Close();
                conn.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static string ConvertirDataSetAJson(DataSet dataSet)
        {
            // Convertir DataSet a JSON usando la biblioteca Newtonsoft.Json
            string jsonResult = JsonConvert.SerializeObject(dataSet, Newtonsoft.Json.Formatting.Indented);

            return jsonResult;
        }

    }

}
