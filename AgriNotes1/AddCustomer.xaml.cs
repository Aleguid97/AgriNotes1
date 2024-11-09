using AgriNotes1.Models;
using Microsoft.Maui.Controls;
using System;

namespace AgriNotes1
{
    public partial class AddCustomer : ContentPage
    {
        public AddCustomer()
        {
            InitializeComponent();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Crea un nuovo oggetto Cliente con i dati inseriti
            Cliente nuovoCliente = new Cliente
            {
                Nome = NomeEntry.Text,
                Cognome = CognomeEntry.Text,
                Contrada = ContradaEntry.Text,
                Cellulare = CellulareEntry.Text,
                Cultivar = CultivarEntry.Text
            };

            var app = Application.Current as App;
            if (app != null)
            {
                await app.DatabaseService.SaveClienteAsync(nuovoCliente);
                await DisplayAlert("Successo", "Cliente aggiunto con successo!", "OK");

                // Invia il nuovo cliente alla HomePage tramite MessagingCenter
                MessagingCenter.Send(this, "ClienteAggiunto", nuovoCliente);

                // Naviga indietro alla pagina precedente
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Errore", "Impossibile accedere al servizio del database.", "OK");
            }
        }
    }
}
