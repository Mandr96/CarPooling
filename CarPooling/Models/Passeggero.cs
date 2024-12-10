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
            cmd.Parameters.AddWithValue("EmailPasseggero", p.EmailAutista);
            cmd.Parameters.AddWithValue("Nome", p.Nome);
            cmd.Parameters.AddWithValue("Cognome", p.Cognome);
            cmd.Parameters.AddWithValue("CartaIdentita", p.Cognome);
            cmd.Parameters.AddWithValue("Telefono", p.Cognome);


            try
            {
                ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                ViewBag.ErroreInsert = "Errore inserimento Passeggero" + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}