using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarPooling.Models
{
    public class Prenotazione : DatabaseObject
    {
        public int IdPrenotazione { get; set; }
        public string EmailPasseggero { get; set; }
        public int IdViaggio { get; set; }
        public DateTime DataOra { get; set; }

        public void BuildFromReader(SqlDataReader reader)
        {
            IdPrenotazione = int.Parse(reader["IdPrenotazione"].ToString());
            EmailPasseggero = reader["fk_emailPasseggero"].ToString();
            IdViaggio = int.Parse(reader["fk_IdViaggio"].ToString());
            DataOra = (DateTime)reader["DataOra"];
        }

        public static void InsertPrenotazione(Prenotazione pren)
        {
            string query = "INSERT INTO Prenotazione VALUES (@email, @idViaggio, @dataOra);";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("email", pren.EmailPasseggero);
            cmd.Parameters.AddWithValue("idViaggio", pren.IdViaggio);
            cmd.Parameters.AddWithValue("dataOra", pren.DataOra);
            Database.ExecuteNonQuery(cmd);
        }

        public static List<Prenotazione> SelectByIdViaggio(int idViaggio)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Prenotazione WHERE fk_IdViaggio = @id");
            cmd.Parameters.AddWithValue("id", idViaggio);
            return Database.GetObjectList<Prenotazione>(cmd);
        }
    }
}