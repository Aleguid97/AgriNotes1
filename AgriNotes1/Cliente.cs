using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AgriNotes1.Models
{
    public class Cliente
    {
        
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Contrada { get; set; }
        public string Cellulare { get; set; }
        public string Cultivar { get; set; }


        [Ignore]
        public List<Concimazione> Concimazioni { get; set; } = new List<Concimazione>();
    }
}