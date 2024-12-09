using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace CarPooling.Models
{
    public class Database
    {
        public static SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionDB"].ConnectionString);
        //Metodo da usare se la query restituisce un solo record
        public static Type GetObject<Type>(SqlCommand cmd) where Type : DatabaseObject, new()
        {
            cmd.Connection = Connection;

            Type obj = new Type();
            SqlDataReader reader = null;
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                {
                    Connection.Open();
                }
                reader = cmd.ExecuteReader();
                reader.Read();
                obj.BuildFromReader(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return obj;
        }
        //Metodo da usare se la query restituisce più record
        public static List<Type> GetObjectList<Type>(SqlCommand cmd) where Type : DatabaseObject, new()
        {
            cmd.Connection = Connection;

            List<Type> result = new List<Type>();

            SqlDataReader reader = null;
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                {
                    Connection.Open();
                }
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Type obj = new Type();
                    obj.BuildFromReader(reader);
                    result.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
            return result;
        }
        //Metodo da utilizzare per INSERT e UPDATE
        public static void ExecuteNonQuery(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = Connection;

                if (Connection.State == System.Data.ConnectionState.Closed)
                {
                    Connection.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
    }

    public interface DatabaseObject
    {
        void BuildFromReader(SqlDataReader reader);
    }
}