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
<<<<<<< Updated upstream
            //EmailAutista = reader["emailAutista"].ToString();
            //...
=======
            EmailAutista = reader["emailAutista"].ToString();
            Nome = reader["Nome"].ToString();
            Cognome = reader["Cognome"].ToString();
            DataNascita = (DateTime)(reader["DataNascita"]);
            NumPatente = int.Parse(reader["NumPatente"]);
            scadenzaPatente = (DateTime)(reader["ScadenzaPatente"]);
            Auto = reader["Auto"].ToString();
            Tel = reader["Telefono"].ToString();
            PhotoFileName = reader["PhotoFileName"].ToString();
>>>>>>> Stashed changes
        }

        public static List<Autista> SelectAllAutisti()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Autista;");
            return Database.GetObjectList<Autista>(cmd);
        }

        public static Autista SelectById(string email)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Autista WHERE emailAutista = @email");
            cmd.Parameters.AddWithValue("email", email);
            return Database.GetObject<Autista>(cmd);
        }
<<<<<<< Updated upstream
=======

        public static void InsertAutista(Autista autista)
        {
            string query = "INSERT INTO Autista VALUES (@EmailAutista, @Nome, @Cognome);";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("EmailAutista", autista.EmailAutista);
            cmd.Parameters.AddWithValue("Nome", autista.Nome);
            cmd.Parameters.AddWithValue("Cognome", autista.Cognome);
            cmd.Parameters.AddWithValue("DataNascita", autista.DataNascita);
            cmd.Parameters.AddWithValue("NumPatente", autista.NumPatente);
            cmd.Parameters.AddWithValue("ScadenzaPatente", autista.scadenzaPatente);
            cmd.Parameters.AddWithValue("Auto", autista.Auto);
            cmd.Parameters.AddWithValue("Telefono", autista.Tel);
            cmd.Parameters.AddWithValue("PhotoFileName", autista.PhotoFileName);

            try
            {
                ExecuteNonQuery(cmd);
                    
            }
            catch(Exception ex)
            {
                ViewBag.ErroreInsert = "Errore inserimento Passeggero" + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }   


>>>>>>> Stashed changes
    }
}