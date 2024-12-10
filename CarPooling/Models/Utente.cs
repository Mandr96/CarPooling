using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarPooling.Models
{
    public class Utente : DatabaseObject
    {
        [Required(ErrorMessage = "Obbligatorio")]
        public string Email { get; set; }
     
        [Required(ErrorMessage = "Obbligatorio")]
        public string Password { get; set; }

        public string Username { get; set; }
        public string Ruolo {  get; set; }  

        public void BuildFromReader(SqlDataReader reader)
        {
            Email = (string)reader["Email"];
            Username = (string)reader["Username"];
            Password = (string)reader["Password"];
            Ruolo = (string)reader["Ruolo"];
        }

        public static Utente SelectUtenteById(string id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FORM Utente WHERE Email = @id");
            cmd.Parameters.AddWithValue("id", id);
            return Database.GetObject<Utente>(cmd);
        }
    }
}