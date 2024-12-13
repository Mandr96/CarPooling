using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace CarPooling.Models
{
    public class Passeggero : DatabaseObject
    {
        [Required(ErrorMessage ="Campo obbligatorio")]
        public string EmailPasseggero { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string CartaIdentita { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Telefono { get; set; }

        public Utente Credenziali { get; set; }



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
            string query = "InsertPasseggero";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("emailP", p.EmailPasseggero);
            cmd.Parameters.AddWithValue("nomeP", p.Nome);
            cmd.Parameters.AddWithValue("cognomeP", p.Cognome);
            cmd.Parameters.AddWithValue("cartaId", p.CartaIdentita);
            cmd.Parameters.AddWithValue("tel", p.Telefono);
            cmd.Parameters.AddWithValue("user", p.Credenziali.Username);
            cmd.Parameters.AddWithValue("pwd", p.Credenziali.Password);
            cmd.Parameters.AddWithValue("role", p.Credenziali.Ruolo);

            var ResultQuery = new SqlParameter("ResultQuery", System.Data.SqlDbType.NVarChar, 200);
            ResultQuery.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(ResultQuery);

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
                Database.Connection.Open();
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


        public static void EditPasseggero(Passeggero p)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Passeggero SET Nome=@nom, Cognome=@cog, CartaIdentita=@car, Telefono=@tel WHERE email=@ema");
            cmd.Parameters.AddWithValue("nom", p.Nome);
            cmd.Parameters.AddWithValue("cog", p.Cognome);
            cmd.Parameters.AddWithValue("car", p.CartaIdentita);
            cmd.Parameters.AddWithValue("tel", p.Telefono);
            cmd.Parameters.AddWithValue("ema", p.EmailPasseggero);
            Database.ExecuteNonQuery(cmd);
        }

    }
}