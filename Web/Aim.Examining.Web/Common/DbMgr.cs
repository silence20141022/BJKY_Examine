using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Aim.Examining.Web
{
    public class DbMgr
    {
        private static readonly string ConnectionString = ConfigurationManager.AppSettings["connstr_base"];
        public static DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            SqlConnection orclCon = null;
            try
            {
                using (orclCon = new SqlConnection(ConnectionString))
                {
                    SqlCommand oc = orclCon.CreateCommand();
                    oc.CommandText = sql;
                    if (orclCon.State.ToString().Equals("Open"))
                    {
                        orclCon.Close();
                    }
                    orclCon.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = oc;
                    adapter.Fill(ds);
                }
            }
            catch (Exception e)
            {
                //log.Error(e.Message + e.StackTrace);
            }
            finally
            {
                orclCon.Close();
            }
            return ds;
        }

        public static DataTable GetDataTable(string sql)
        {
            DataSet ds = new DataSet();
            SqlConnection orclCon = null;
            try
            {
                using (orclCon = new SqlConnection(ConnectionString))
                {
                    SqlCommand oc = orclCon.CreateCommand();
                    oc.CommandText = sql;
                    if (orclCon.State.ToString().Equals("Open"))
                    {
                        orclCon.Close();
                    }
                    orclCon.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = oc;
                    adapter.Fill(ds);
                }
            }
            catch (Exception e)
            {
                //log.Error(e.Message + e.StackTrace);
            }
            finally
            {
                orclCon.Close();
            }
            return ds.Tables[0];
        }

        public static int ExecuteNonQuery(string sql)
        {
            int retcount = -1;
            SqlConnection orclCon = null;
            try
            {
                using (orclCon = new SqlConnection(ConnectionString))
                {
                    SqlCommand oc = new SqlCommand(sql, orclCon);
                    //oc.Parameters.AddRange(OraPara);, OracleParameter[] OraPara
                    if (orclCon.State.ToString().Equals("Open"))
                    {
                        orclCon.Close();
                    }
                    orclCon.Open();
                    retcount = oc.ExecuteNonQuery();
                    oc.Parameters.Clear();
                }
            }
            catch (Exception e)
            {
                //log.Error(e.Message + e.StackTrace);
            }
            finally
            {
                orclCon.Close();
            }
            return retcount;
        }
    }
}