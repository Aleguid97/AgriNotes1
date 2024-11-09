using AgriNotes1;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace AgriNotes1
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;

            // Verifica che i campi email e password non siano vuoti
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Errore", "Tutti i campi sono obbligatori", "OK");
                return;
            }

            // Controlla se l'utente esiste e la password è corretta
            var user = await GetUserAsync(email, password);

            if (user != null)
            {
                await DisplayAlert("Login", "Accesso effettuato con successo!", "OK");

                // Naviga alla HomePage dopo un accesso riuscito
                await Shell.Current.GoToAsync("HomePage");
            }
            else
            {
                // Messaggio d'errore se email o password non sono corretti
                LoginMessage.Text = "Email o password errati.";
                LoginMessage.IsVisible = true;
            }
        }

        // Funzione per ottenere l'utente dal database e verificare la password
        private async Task<Models.Utente> GetUserAsync(string email, string password)
        {
            var app = Application.Current as App;
            if (app == null)
            {
                return null;
            }
             
            // Recupera l'utente dal database tramite email
            var user = await app.DatabaseService.GetUserAsync(email);

            // Verifica se l'utente esiste e la password corrisponde
            if (user != null && user.Password == password)
            {
                return user; // Ritorna l'utente se le credenziali sono corrette
            }

            return null; // Ritorna null se l'utente non esiste o la password è errata
        }
    }
}
