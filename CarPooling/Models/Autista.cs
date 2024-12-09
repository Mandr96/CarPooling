using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace CarPooling.Models
{
    public class Autista : DatabaseObject
    {
        public string EmailAutista {  get; set; }
        public string Nome {  get; set; }
        public string Cognome { get; set; }
        public void BuildFromReader(SqlDataReader reader)
        {
            
        }

        public static List<DatabaseObject> SelectAllAutisti()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Autista;");
            return Database.GetObjectList<Autista>(cmd);
        }

        public static DatabaseObject SelectById(string email)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Autista WHERE emailAutista = @email");
            cmd.Parameters.AddWithValue("email", email);
            return Database.GetObject<Autista>(cmd);
        }
    }
}