using DataBaseConnector;
using FluentScheduler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.UI.WebControls;
using System.Xml;
using waCubos.Properties;

namespace waCubos
{
    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            // Configura una tarea para ejecutarse todos los días a las 5 am
            Schedule<MyJob>().ToRunEvery(1).Days().At(16, 31);
        }
    }

    public class MyJob : IJob
    {
        public void Execute()
        {
            DataSet _dataSet;
            bin bin = new bin("BD2");
            _dataSet = bin.HojaCalculoTotales(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"),"WA", "BD2");
            // Convertir DataSet a cadena JSON
            string jsonResult = ConvertirDataSetAJson(_dataSet);
            saveData(jsonResult, DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "WA", "BD2",0);

            _dataSet = bin.HojaCalculoTotales(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "WA", "BD1");
            jsonResult = ConvertirDataSetAJson(_dataSet);
            saveData(jsonResult, DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "WA", "BD1",0);
            Console.WriteLine("");

            //DETALLES DELIVERY 
            _dataSet = bin.DetallesDelivery(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "WA", "BD2");
            // Convertir DataSet a cadena JSON
            jsonResult = ConvertirDataSetAJson(_dataSet);
            saveData(jsonResult, DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "WA", "BD2", 1);

            _dataSet = bin.DetallesDelivery(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "WA", "BD1");
            jsonResult = ConvertirDataSetAJson(_dataSet);
            saveData(jsonResult, DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "WA", "BD1", 1);
            Console.WriteLine("");

        }

        static string ConvertirDataSetAJson(DataSet dataSet)
        {
            // Convertir DataSet a JSON usando la biblioteca Newtonsoft.Json
            string jsonResult = JsonConvert.SerializeObject(dataSet, Newtonsoft.Json.Formatting.Indented);

            return jsonResult;
        }

        public void saveData(string jsondata, string fecha, string grupo, string ciudad,int detdel) 
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
                Coll.addParameter(new SqlParameter("@JSONDATA", jsondata));
                Coll.addParameter(new SqlParameter("@DETALLESDELIVERY", detdel));

                conn = new SqlConnection(Settings.Default.Cadena.Replace("%bd%", "BD2"));
                conn.Open();
                comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandTimeout = Settings.Default.TimeOut;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "WAS_INSERT_REPORTE_TEMP";
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

    }

}
