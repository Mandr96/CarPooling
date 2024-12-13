using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;


namespace CarPooling.Models
{
    public class Autista : DatabaseObject
    {
        [Required(ErrorMessage ="Campo obbligatorio")]
        [DisplayName("Email")]
        public string EmailAutista {  get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Nome {  get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [DisplayName("Data di nascita")]
        public DateTime DataNascita { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [DisplayName("Num. Patente")]
        public int NumPatente { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [DisplayName("Scadenza Patente")]
        public DateTime ScadenzaPatente { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Auto { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Telefono { get; set; }

        public string PhotoFileName { get; set; }

        public HttpPostedFileBase File { get; set; }
        public string PathFile { get; set; }

        public Utente Credenziali { get; set; }

        public void BuildFromReader(SqlDataReader reader)
        {


            EmailAutista = reader["emailAutista"].ToString();
            Nome = reader["Nome"].ToString();
            Cognome = reader["Cognome"].ToString();
            DataNascita = (DateTime)reader["DataNascita"];
            NumPatente = int.Parse(reader["NumPatente"].ToString());
            ScadenzaPatente = (DateTime)reader["ScadenzaPatente"];
            Auto = reader["Auto"].ToString();
            Telefono = reader["Telefono"].ToString();
            PhotoFileName = reader["PhotoFileName"].ToString();
            Credenziali.Email = reader["emailAutista"].ToString();

        }

        public static List<Autista> SelectAllAutisti()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Autista;");
            return Database.GetObjectList<Autista>(cmd);
        }

        public static Autista SelectById(string email)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Autista WHERE EmailAutista = @email");
            cmd.Parameters.AddWithValue("email", email);
            return Database.GetObject<Autista>(cmd);
        }


        public static void InsertAutista(Autista autista)
        {
            string query = "InsertAutista";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("emailA", autista.EmailAutista);
            cmd.Parameters.AddWithValue("nomeA", autista.Nome);
            cmd.Parameters.AddWithValue("cognomeA", autista.Cognome);
            cmd.Parameters.AddWithValue("dataN", autista.DataNascita);
            cmd.Parameters.AddWithValue("numP", autista.NumPatente);
            cmd.Parameters.AddWithValue("scadP", autista.ScadenzaPatente);
            cmd.Parameters.AddWithValue("auto", autista.Auto);
            cmd.Parameters.AddWithValue("tel", autista.Telefono);
            cmd.Parameters.AddWithValue("photo", autista.PhotoFileName);
            cmd.Parameters.AddWithValue("user", autista.Credenziali.Username);
            cmd.Parameters.AddWithValue("pwd", autista.Credenziali.Password);
            cmd.Parameters.AddWithValue("role", autista.Credenziali.Ruolo);

            var ResultQuery = new SqlParameter("ResultQuery", System.Data.SqlDbType.NVarChar, 200);
            ResultQuery.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(ResultQuery);

            Database.ExecuteNonQuery(cmd);
        }   

    }
}