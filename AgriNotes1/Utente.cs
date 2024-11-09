using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriNotes1.Models
{
    public class Utente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

       
        public string Nome { get; set; }
    
        public  string Email { get; set; }

        public string Password { get; set; } // Cambia o aggiungi altri campi se necessario
    }
}
