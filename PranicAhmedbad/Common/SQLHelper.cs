using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PranicAhmedbad.Common
{
    public enum StoredProcedures
    {
        USP_Check_Login,
        USP_Insert_Modules_Error_Log
    }
    public class SQLHelper
    {
        public static IConfiguration Configuration { get; private set; }

        public static void InitializeConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static string Connectionstring
        {
            get
            {
                return Configuration["ConnectionStrings:DefaultConnection"].ToString();
            }
        }
        public static DataSet GetData(StoredProcedures eStoredProcedure, object[] ParamName = null, object[] ParamVal = null)
        {
            DataSet dsTable = new DataSet();
            SqlConnection sqlconne = null;
            SqlDataAdapter sqladp = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (sqlconne = new SqlConnection(Connectionstring))
                    {
                        using (sqladp = new SqlDataAdapter(eStoredProcedure.ToString(), sqlconne))
                        {
                            //sqlconne.Open();
                            sqladp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            sqladp.SelectCommand.CommandTimeout = 0;
                            if (ParamName != null && ParamName.Length > 0)
                            {
                                for (int i = 0; i < ParamName.Length; i++)
                                {
                                    sqladp.SelectCommand.Parameters.AddWithValue(ParamName[i].ToString(), ParamVal[i]);
                                }
                            }
                            sqladp.Fill(dsTable);

                        }
                    }
                    scope.Complete();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlconne.Close();
                sqladp.Dispose();
            }


            return dsTable;
        }
        public static int ExecuteQuery(StoredProcedures eStoredProcedure, object[] ParamName = null, object[] ParamVal = null)
        {
            int iRetVal = 0;
            SqlConnection sqlconne = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (sqlconne = new SqlConnection(Connectionstring))
                    {
                        using (SqlDataAdapter sqladp = new SqlDataAdapter(eStoredProcedure.ToString(), sqlconne))
                        {
                            sqlCommand = new SqlCommand(eStoredProcedure.ToString(), sqlconne);
                            if (sqlconne.State != ConnectionState.Open)
                                sqlconne.Open();

                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqladp.InsertCommand = sqlCommand;
                            if (ParamName != null && ParamName.Length > 0)
                            {
                                for (int i = 0; i < ParamName.Length; i++)
                                {
                                    sqladp.InsertCommand.Parameters.AddWithValue(ParamName[i].ToString(), ParamVal[i]);
                                }
                            }
                            iRetVal = sqladp.InsertCommand.ExecuteNonQuery();

                        }
                        sqlconne.Close();
                        sqlCommand.Dispose();
                    }
                    scope.Complete();
                }
            }
            catch  {
                throw;
            }
            finally
            {
                sqlconne.Close();
                sqlCommand.Dispose();
            }
            return iRetVal;
        }


        public static DataSet executeQuery(string sqlQry)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlconne = null;
            SqlCommand sqlCommand = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (sqlconne = new SqlConnection(Connectionstring))
                    {
                        sqlCommand = new SqlCommand(sqlQry.ToString(), sqlconne);
                        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);

                        if (sqlconne.State != ConnectionState.Open)
                            sqlconne.Open();

                        int tableNameIndex = sqlQry.ToUpper().IndexOf("FROM");
                        String tableName = sqlQry.Substring(tableNameIndex + 4).Trim();
                        tableNameIndex = tableName.IndexOf(" ");
                        if (tableNameIndex == -1)
                            tableNameIndex = tableName.Length;
                        tableName = tableName.Substring(0, tableNameIndex);
                        tableNameIndex = tableName.IndexOf(",");
                        if (tableNameIndex == -1)
                            tableNameIndex = tableName.Length;
                        tableName = tableName.Substring(0, tableNameIndex).Trim();

                        if (tableName.Length == 0)
                            tableName = "DATASOURCE";

                        sqlAdapter.Fill(ds, tableName);

                        sqlconne.Close();
                        sqlCommand.Dispose();
                    }
                    scope.Complete();
                }
                return ds;
            }
            catch
            {
                throw;
            }
        }

        public static void writeException(Exception ex)
        {
            string str = Environment.CurrentDirectory + "\\log\\";
            string strpath = DateTime.Now.Ticks.ToString() + ".txt";
            string strFinal = str + strpath;

            if (!System.IO.Directory.Exists(str))
            {
                System.IO.Directory.CreateDirectory(str);
            }

            using (Stream stream = File.Create(strFinal))
            {
                TextWriter tw = new StreamWriter(stream); /* this is where the problem was */
                tw.WriteLine(ex.Message + " - " + Environment.NewLine + ex.StackTrace);
                tw.Close();
            }
        }

    }
}
