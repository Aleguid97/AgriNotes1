using AgriNotes1.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AgriNotes1
{
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<Cliente> Clienti { get; set; }

        public HomePage()
        {
            InitializeComponent();
            Clienti = new ObservableCollection<Cliente>();
            BindingContext = this;

            // Iscrizione al MessagingCenter per aggiungere nuovi clienti
            MessagingCenter.Subscribe<AddCustomer, Cliente>(this, "ClienteAggiunto", async (sender, cliente) =>
            {
                Clienti.Add(cliente); // Aggiunge il nuovo cliente
                await LoadClientiAsync(); // Forza l'aggiornamento della lista
            });

            // Carica i clienti iniziali
            LoadClientiAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Ricarica i clienti solo se non ci sono clienti presenti per evitare duplicazioni
            if (Clienti.Count == 0)
            {
                await LoadClientiAsync();
            }
        }

        private async Task LoadClientiAsync()
        {
            var app = Application.Current as App;
            if (app != null)
            {
                var clienti = await app.DatabaseService.GetClientiAsync();
                Clienti.Clear(); // Pulisce per evitare duplicazioni
                foreach (var cliente in clienti)
                {
                    Clienti.Add(cliente);
                }
            }
        }

        private async void OnAddCustomerClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AddCustomer");
        }

        private async void OnEliminaButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var clienteId = (int)button.CommandParameter;

            var app = Application.Current as App;
            if (app != null)
            {
                bool confirm = await DisplayAlert("Conferma Eliminazione", "Sei sicuro di voler eliminare questo cliente?", "Sì", "No");
                if (confirm)
                {
                    var clienteToDelete = await app.DatabaseService.GetClienteByIdAsync(clienteId);
                    if (clienteToDelete != null)
                    {
                        await app.DatabaseService.DeleteClienteAsync(clienteToDelete);
                        Clienti.Remove(clienteToDelete);
                        await DisplayAlert("Successo", "Cliente eliminato con successo.", "OK");
                        await Shell.Current.GoToAsync("HomePage");


                    }
                }
            }
        }

        private async void OnDettagliButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var clienteId = (int)button.CommandParameter;

            var app = Application.Current as App;
            if (app != null)
            {
                var cliente = await app.DatabaseService.GetClienteByIdAsync(clienteId);
                if (cliente != null)
                {
                    await DisplayAlert("Dettagli Cliente", $"Nome: {cliente.Nome}\nCognome: {cliente.Cognome}\nContrada: {cliente.Contrada}\nCellulare: {cliente.Cellulare}\nCultivar: {cliente.Cultivar}", "OK");
                }
            }
        }
    }
}
