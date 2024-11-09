using SQLite;
using SQLitePCL;

namespace AgriNotes1
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

      
            public DatabaseService()
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Agrinotes.db3");
                _database = new SQLiteAsyncConnection(dbPath);
                InitializeDatabaseAsync(); // Non bloccare il thread principale
            }

        

        // Metodo per inizializzare il database e creare le tabelle necessarie
        public async Task InitializeDatabaseAsync()
        {
            // Crea le tabelle se non esistono già
            await _database.CreateTableAsync<Models.Utente>();
            await _database.CreateTableAsync<Models.Cliente>(); // Assicurati che Cliente sia definito
            await _database.CreateTableAsync<Models.Concimazione>(); // Assicurati che Concimazione sia definito
        }

        // Metodo per salvare un utente
        public Task<int> SaveUtenteAsync(Models.Utente utente)
        {
            return _database.InsertAsync(utente);
        }

        // Metodo per ottenere tutti gli utenti
        public Task<List<Models.Utente>> GetUtentiAsync()
        {
            return _database.Table<Models.Utente>().ToListAsync();
        }

        // Metodo per ottenere un utente per email
        public async Task<Models.Utente> GetUserAsync(string email)
        {
            return await _database.Table<Models.Utente>().FirstOrDefaultAsync(u => u.Email == email);
        }

        // Metodo per ottenere tutti i clienti
        public async Task<List<Models.Cliente>> GetClientiAsync()
        {
            return await _database.Table<Models.Cliente>().ToListAsync();
        }



        // Metodo per salvare un cliente
        public Task<int> SaveClienteAsync(Models.Cliente cliente)
        {
            return _database.InsertAsync(cliente);
        }


        // Metodo per ottenere un cliente per ID
        public async Task<Models.Cliente> GetClienteByIdAsync(int id)
        {
            return await _database.Table<Models.Cliente>().FirstOrDefaultAsync(c => c.Id == id);
        }


        // Metodo per eliminare un cliente
        public async Task<int> DeleteClienteAsync(Models.Cliente cliente)
        {
            return await _database.DeleteAsync(cliente);
        }

        //Metodo per ricevere i dettagli di un cliente

        public async Task<List<Models.Cliente>> GetDettagli(int clienteId)
        {
            // Usa 'Id' invece di 'ClienteId', che è il nome corretto del campo
            return await _database.Table<Models.Cliente>().Where(c => c.Id == clienteId).ToListAsync();
        }








    }
}
