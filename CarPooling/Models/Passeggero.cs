using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace CarPooling.Models
{
    public class Passeggero : DatabaseObject
    {
        public string EmailPasseggero { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CartaIdentita { get; set; }
        public string Telefono { get; set; }



        public void BuildFromReader(SqlDataReader reader)
        {
            EmailPasseggero = reader["EmailPasseggero"].ToString();
            Nome = reader["Nome"].ToString();
            Cognome = reader["Cognome"].ToString();
            CartaIdentita = reader["CartaIdentita"].ToString();
            Telefono = reader["Telefono"].ToString();
        }

        public static List<Passeggero> SelectAllPasseggeri()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Passeggero;");
            return Database.GetObjectList<Passeggero>(cmd);
        }

        public static Passeggero SelectById(string email)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Passeggero WHERE EmailPasseggero= @email;");
            cmd.Parameters.AddWithValue("email", email);
            return Database.GetObject<Passeggero>(cmd);
        }

        public static void InsertPasseggero(Passeggero p)
        {
            string query = "INSERT INTO Passeggero VALUES (@EmailPasseggero, @Nome, @Cognome, @CartaIdentita, @Telefono);";
            SqlCommand cmd = new SqlCommand(query);


            cmd.Parameters.AddWithValue("EmailPasseggero", p.EmailPasseggero);
            cmd.Parameters.AddWithValue("Nome", p.Nome);
            cmd.Parameters.AddWithValue("Cognome", p.Cognome);
            cmd.Parameters.AddWithValue("CartaIdentita", p.Cognome);
            cmd.Parameters.AddWithValue("Telefono", p.Cognome);

            Database.ExecuteNonQuery(cmd);
        }

        public static List<Viaggio> GetViaggiByPasseggero(string emailPasseggero)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Passeggero pa JOIN Prenotazione pr ON pa.EmailPasseggero=pr.fk_EmailPasseggero " +
                "JOIN Viaggio v ON pr.fk_IdViaggio=v.IdViaggio WHERE fk_EmailPasseggero = @email;");
            cmd.Parameters.AddWithValue("email", emailPasseggero);
            return Database.GetObjectList<Viaggio>(cmd);
        }

        public static int CountPrenotazioniTotali()
        {
            int conto = 0;
            SqlCommand cmd = new SqlCommand("SELECT COUNT(IdPrenotazione) AS Conto FROM Prenotazione", Database.Connection);
            try
            {
                Database.Connection.Open(); //non funziona
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    conto = (int)reader["Conto"];
                }
                reader.Close();
            }
            finally { Database.Connection.Close(); }
            return conto;
        }

    }
}