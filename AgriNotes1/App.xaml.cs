
namespace AgriNotes1
{
    public partial class App : Application
    {
        public DatabaseService _databaseService;

        public DatabaseService DatabaseService => _databaseService; // Proprietà pubblica per accedere al DatabaseService

        public App()
        {
            InitializeComponent();

            // Inizializza il DatabaseService
            _databaseService = new DatabaseService();

            // Inizializza il database in modo asincrono
            InitializeDatabase();

            MainPage = new AppShell(); // Usa AppShell per gestire la navigazione
        }

        private async void InitializeDatabase()
        {
            // Chiamata al metodo per inizializzare il database
            await _databaseService.InitializeDatabaseAsync();
        }
        
    }
}
