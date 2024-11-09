using System;
using Microsoft.Maui.Controls;

namespace AgriNotes1
{
    public partial class RegistrationPage : ContentPage
    {
        public DatabaseService _databaseService;

        public RegistrationPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService(); // Inizializzazione DatabaseService
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Creo un nuovo utente utilizzando i valori inseriti nei campi
            var nuovoUtente = new Models.Utente
            {
                Nome = NameEntry.Text,
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text
            };

            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Errore", "Registrazione non riuscita: tutti i campi sono obbligatori", "OK");
                return; // Esce dalla funzione per evitare di proseguire con la registrazione
            }



            // Salvo l'utente nel database
            int result = await _databaseService.SaveUtenteAsync(nuovoUtente);
            if (result > 0)
            {
                await DisplayAlert("Successo", "Utente registrato con successo!", "OK");
                await Shell.Current.GoToAsync(".."); // Torna alla pagina precedente (MainPage)
            }
            else if (result == 0)
            {
                await DisplayAlert("Errore", "Utente già registrato", "OK");
            }

            else
            {
                await DisplayAlert("Errore", "Registrazione non riuscita", "OK");
            }
        }
    }
}
