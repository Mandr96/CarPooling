using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CarPooling.Models
{
    public class Viaggio : DatabaseObject
    {
        public int IdViaggio { get; set; }
        [Required(ErrorMessage ="Campo obbligatorio")]
        [DisplayName("Citta di partenza")]
        public string CittaPartenza { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DisplayName("Citta di destinazione")]
        public string CittaArrivo { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DisplayName("Data e ora di partenza")]
        public DateTime DataOraPartenza { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DisplayName("Quota richiesta ai passeggeri")]
        public double Costo { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DisplayName("Data e ora di arrivo")]
        public DateTime DataOraArrivo { get; set; }
        public bool Disponibile { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DisplayName("Ammessi animali: ")]
        public bool Animali { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DisplayName("Ammessi bagagli: ")]
        public bool Bagagli { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DisplayName("Soste previste durante il viaggio")]
        public int SostePreviste { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string FK_EmailAutista { get; set; }

        public List<Prenotazione> Prenotazioni { get; set; }    

        public void BuildFromReader(SqlDataReader reader)
        {
            IdViaggio = int.Parse(reader["IdViaggio"].ToString());
            CittaPartenza = reader["CittaPartenza"].ToString();
            CittaArrivo = reader["CittaArrivo"].ToString();
            DataOraPartenza = (DateTime)reader["DataOraPartenza"];
            Costo = double.Parse(reader["Costo"].ToString());
            DataOraArrivo = (DateTime)reader["DataOraArrivo"];
            Disponibile = bool.Parse(reader["Disponibile"].ToString());
            Animali = bool.Parse(reader["Animali"].ToString());
            Bagagli = bool.Parse(reader["Bagagli"].ToString());
            SostePreviste = int.Parse(reader["SostePreviste"].ToString());
            FK_EmailAutista = reader["fk_EmailAutista"].ToString();
        }

        public static void InsertViaggio(Viaggio viaggio)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Viaggio VALUES (@partenza, @arrivo, @dataOra, @costo, @oraArrivo, @disp, @animali, @bagagli, @soste, @fk_EmailAutista);");
            cmd.Parameters.AddWithValue("partenza", viaggio.CittaPartenza);
            cmd.Parameters.AddWithValue("arrivo", viaggio.CittaArrivo);
            cmd.Parameters.AddWithValue("dataOra", viaggio.DataOraPartenza);
            cmd.Parameters.AddWithValue("costo", viaggio.Costo);
            cmd.Parameters.AddWithValue("oraArrivo", viaggio.DataOraArrivo);
            cmd.Parameters.AddWithValue("disp", true);
            cmd.Parameters.AddWithValue("animali", viaggio.Animali);
            cmd.Parameters.AddWithValue("bagagli", viaggio.Bagagli);
            cmd.Parameters.AddWithValue("soste", viaggio.SostePreviste);
            cmd.Parameters.AddWithValue("fk_EmailAutista", viaggio.FK_EmailAutista);
            Database.ExecuteNonQuery(cmd);
        }
        public static Viaggio SelectById(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Viaggio WHERE IdViaggio = @id");
            cmd.Parameters.AddWithValue("id", id);
            return Database.GetObject<Viaggio>(cmd);
        }
        public static List<Viaggio> SelectByAutista(string emailAutista)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Viaggio WHERE fk_EmailAutista = @email;");
            cmd.Parameters.AddWithValue("email", emailAutista);
            return Database.GetObjectList<Viaggio>(cmd);
        }


        public static List<Viaggio> SelectByTratta(string cittaPartenza, string cittaArrivo)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Viaggio WHERE CittaPartenza = @partenza AND CittaArrivo = @arrivo");
            cmd.Parameters.AddWithValue("partenza", cittaPartenza);
            cmd.Parameters.AddWithValue("arrivo", cittaArrivo);
            return Database.GetObjectList<Viaggio>(cmd);
        }
        public static List<Viaggio> SelectByTratta(DateTime dataPartenza, string cittaPartenza, string cittaArrivo)
        {
            DateTime dayEnd = dataPartenza.AddDays(1);
            string startDate = dataPartenza.Year + "-" + dataPartenza.Day + "-" + dataPartenza.Month;
            string endDate = dayEnd.Year + "-" + dayEnd.Day + "-" + dayEnd.Month;
            string query = "SELECT * FROM Viaggio WHERE CittaPartenza = @partenza AND CittaArrivo = @arrivo AND "+
                                            "DataOraPartenza > @start AND DataOraPartenza < @end;";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("partenza", cittaPartenza);
            cmd.Parameters.AddWithValue("arrivo", cittaArrivo);
            cmd.Parameters.AddWithValue("start", startDate);
            cmd.Parameters.AddWithValue("end", endDate);
            return Database.GetObjectList<Viaggio>(cmd);
        }

        public static void UpdateDisponibilita(int id)
        {
            string query = "UPDATE viaggio SET Disponibile = 0 WHERE IdViaggio = @id";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("id", id);
            Database.ExecuteNonQuery(cmd);   

        }

        public static List<Passeggero> GetPasseggeriByViaggio(int id)
        {
            List<Passeggero> p = new List<Passeggero>();
            string query = "SELECT COGNOME, NOME FROM PASSEGGERO AS P " +
                "JOIN PRENOTAZIONE AS PR ON P.EMAILPASSEGGERO = PR.FK_EMAILPASSEGGERO " +
                "WHERE PR.FK_IDVIAGGIO = @id";
            

            try
            {
                Database.Connection.Open(); 
                SqlCommand cmd = new SqlCommand(query, Database.Connection);
                cmd.Parameters.AddWithValue("id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Passeggero pas = new Passeggero();
                    pas.Nome= (string)reader["Nome"];
                    pas.Cognome= (string)reader["Cognome"];
                    p.Add(pas);
                }
                reader.Close();
            }
            finally { Database.Connection.Close(); }
            return p;

        }
    }
}