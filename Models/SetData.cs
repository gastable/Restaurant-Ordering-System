using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace R.Models
{
    public class SetData
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantConnection"].ConnectionString);

        SqlCommand cmd = new SqlCommand("", conn);

        public void executeSql(string sql, List<SqlParameter> list)
        {
            cmd.CommandText = sql;

            foreach (var p in list)
            {
                cmd.Parameters.Add(p);
            }

            conn.Open();
            cmd.ExecuteNonQuery();//Only execute. No query from database
            cmd.Dispose(); 
            conn.Close();
        }

        public void executeSql(string sql)
        {
            cmd.CommandText = sql;

            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose(); 
            conn.Close();
        }

        public void executeSP(string sql, List<SqlParameter> para)
        {
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter p in para)
            {
                cmd.Parameters.Add(p);
            }
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        public void executeSP(string sql)
        {
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
    }
}