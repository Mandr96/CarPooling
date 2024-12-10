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
        public string Emailpasseggero { get; set; }
        public int IdViaggio { get; set; }
        public DateTime DataOra { get; set; }

        public void BuildFromReader(SqlDataReader reader)
        {
            
        }
    }
}