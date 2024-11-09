using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriNotes1.Models
{
    public class Concimazione
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public double Superficie { get; set; } // ad esempio, superficie in metri quadrati
        public double Grammi { get; set; }
        public double Millilitri { get; set; }
        public string Note { get; set; }

        // Chiave esterna per collegare Concimazione a Cliente
        public int ClienteId { get; set; }

        [Ignore] // Questa proprietà non sarà memorizzata nel database
        public Cliente Cliente { get; set; }
    }
}
