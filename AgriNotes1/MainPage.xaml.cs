namespace AgriNotes1
{
    public partial class MainPage : ContentPage
    {
        private DatabaseService _databaseService;

        public MainPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService(); // Inizializzazione DatabaseService
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("LoginPage");
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("RegistrationPage");
        }

       

        private async Task SaveUtente(Models.Utente utente)
        {
            int result = await _databaseService.SaveUtenteAsync(utente);
            if (result > 0)
                await DisplayAlert("Successo", "Utente salvato correttamente!", "OK");
            else
                await DisplayAlert("Errore", "Salvataggio non riuscito", "OK");
        }

        private async Task GetUtenti()
        {
            List<Models.Utente> utenti = await _databaseService.GetUtentiAsync();
            foreach (var utente in utenti)
            {
                Console.WriteLine($"ID: {utente.Id}, Nome: {utente.Nome}");
            }
        }

        //private async void OnSaveButtonClicked(object sender, EventArgs e)
        //{
        //    var nuovoUtente = new Models.Utente { Nome = "Mario Rossi", Email = "mario.rossi@example.com" };
        //    await SaveUtente(nuovoUtente);
        //}

    }

}
