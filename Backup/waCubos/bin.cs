using DataBaseConnector;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using waCubos.Properties;

namespace waCubos
{
    public class bin
    {
        public Database dbc = null;

        public bin(string empresa)
        {
            this.dbc = new Database(Settings.Default.Cadena.Replace("%bd%", empresa), Settings.Default.TimeOut);
        }

        public DataSet Series()
        {
            try
            {
                return this.dbc.loadDataSet("was_Series");
            }
            catch (Exception ex)
            {
                throw new Exception("Series: " + ex.Message);
            }
        }

        public DataSet Grupos()
        {
            try
            {
                return this.dbc.loadDataSet(CommandType.Text, "select distinct(grupo) from serie_an ORDER BY GRUPO");
            }
            catch (Exception ex)
            {
                throw new Exception("Grupos: " + ex.Message);
            }
        }

        public DataSet HojaCalculo(string serie, string fecha)
        {
            try
            {
                ParameterCollection Coll = new ParameterCollection();
                Coll.addParameter(new SqlParameter("@SERIE", serie));
                Coll.addParameter(new SqlParameter("@FECHA", fecha));

                return this.dbc.loadDataSet("was_HojaCalculo", Coll);
            }
            catch (Exception ex)
            {
                throw new Exception("HojaCalculo: " + ex.Message);
            }
        }

        public DataSet HojaCalculoTotales(string fecha, string grupo)
        {
            try
            {
                ParameterCollection Coll = new ParameterCollection();
                Coll.addParameter(new SqlParameter("@FECHA", fecha));
                Coll.addParameter(new SqlParameter("@GRUPO", grupo));
                
                return this.dbc.loadDataSet("was_HojaCalculoTotales", Coll);
            }
            catch (Exception ex)
            {
                throw new Exception("HojaCalculoTotales: " + ex.Message);
            }
        }

        public DataSet CostoPonderado(string fecha1, string fecha2)
        {
            try
            {
                ParameterCollection Coll = new ParameterCollection();
                Coll.addParameter(new SqlParameter("@FECHAINI", fecha1));
                Coll.addParameter(new SqlParameter("@FECHAFIN", fecha2));
                return this.dbc.loadDataSet("was_CostoPonderado", Coll);
            }
            catch (Exception ex)
            {
                throw new Exception("CostoPonderado: " + ex.Message);
            }
        }

        public DataSet Grid(string fecha, string serie)
        {
            try
            {
                ParameterCollection Coll = new ParameterCollection();
                Coll.addParameter(new SqlParameter("@FECHAU", fecha));
                Coll.addParameter(new SqlParameter("@SERIE", serie));
                return this.dbc.loadDataSet("was_PresupuestoVenta", Coll);
            }
            catch (Exception ex)
            {
                throw new Exception("Grid: " + ex.Message);
            }
        }

        public DataSet GridSemanas(string fecha, string serie)
        {
            try
            {
                ParameterCollection Coll = new ParameterCollection();
                Coll.addParameter(new SqlParameter("@FECHAU", fecha));
                Coll.addParameter(new SqlParameter("@SERIE", serie));
                return this.dbc.loadDataSet("was_Semanas", Coll);
            }
            catch (Exception ex)
            {
                throw new Exception("GridSemanas: " + ex.Message);
            }
        }

        public string serieNombre(string serie)
        {
            try
            {
                return this.dbc.loadDataSet(CommandType.Text, "SELECT DESCRIPCION FROM SERIES WHERE SERIE = '" + serie + "'").Tables[0].Rows[0].ItemArray[0].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("serieNombre: " + ex.Message);
            }
        }

        public string[] login(string usuario, string password)
        {
            try
            {
                ParameterCollection Coll = new ParameterCollection();
                Coll.addParameter(new SqlParameter("@U", usuario));
                Coll.addParameter(new SqlParameter("@P", password));
                DataSet dataSet = this.dbc.loadDataSet("was_login", Coll);


                if (dataSet.Tables.Count < 1)
                    return null;
                return new string[]
                {
                    dataSet.Tables[0].Rows[0].ItemArray[0].ToString(),
                    dataSet.Tables[0].Rows[0].ItemArray[1].ToString(),
                    dataSet.Tables[0].Rows[0].ItemArray[2].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("login: " + ex.Message);
            }
        }
    }
}