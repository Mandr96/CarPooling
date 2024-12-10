using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarPooling.Models
{
    public class Viaggio : DatabaseObject
    {
        public int IdViaggio { get; set; }
        public string CittaPartenza { get; set; }
        public string CittaArrivo { get; set; }
        public DateTime DataOraPartenza { get; set; }
        public double Costo { get; set; }
        public DateTime DataOraArrivo { get; set; }
        public bool Disponibile { get; set; }
        public bool Animali { get; set; }
        public bool Bagagli { get; set; }
        public int SostePreviste { get; set; }

        public void BuildFromReader(SqlDataReader reader)
        {
            IdViaggio = int.Parse(reader["IdViaggio"].ToString());
            CittaPartenza = reader["CittaPartenza"].ToString();
            CittaPartenza = reader["CittaArrivo"].ToString();
            DataOraPartenza = (DateTime)reader["DataOraPartenza"];
            Costo = double.Parse(reader["Costo"].ToString());
            DataOraArrivo = (DateTime)reader["DataOraArrivo"];
            Disponibile = bool.Parse(reader["Disponibile"].ToString());
            Disponibile = bool.Parse(reader["Animali"].ToString());
            Disponibile = bool.Parse(reader["Bagagli"].ToString());
            SostePreviste = int.Parse(reader["SostePreviste"].ToString());
        }

        public static void InsertViaggio(Viaggio viaggio)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Viaggio VALUES (@partenza, @arrivo, @dataOra, @costo, @oraArrivo, @disp, @animali, @bagagli, @soste);");
            cmd.Parameters.AddWithValue("partenza", viaggio.CittaPartenza);
            cmd.Parameters.AddWithValue("arrivo", viaggio.CittaArrivo);
            cmd.Parameters.AddWithValue("dataOra", viaggio.DataOraPartenza);
            cmd.Parameters.AddWithValue("costo", viaggio.Costo);
            cmd.Parameters.AddWithValue("oraArrivo", viaggio.DataOraArrivo);
            cmd.Parameters.AddWithValue("disp", true);
            cmd.Parameters.AddWithValue("animali", viaggio.Animali);
            cmd.Parameters.AddWithValue("bagagli", viaggio.Bagagli);
            cmd.Parameters.AddWithValue("soste", viaggio.SostePreviste);
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
            SqlCommand cmd = new SqlCommand("SELECT * FROM Viaggio WHERE emailAutista = @email;");
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
    }
}