using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using TruthOrDrink.Models;

namespace TruthOrDrink
{
    public partial class PlayerPage : ContentPage
    {
        // Eigenschappen voor spelers
        public ObservableCollection<Player> Players { get; set; }

        public PlayerPage()
        {
            InitializeComponent();

            // Spelerslijst initialiseren
            Players = new ObservableCollection<Player>();

            // Binding instellen
            BindingContext = this;
        }

        // Methode om een speler toe te voegen
        private async void OnAddPlayerClicked(object sender, EventArgs e)
        {
            string name = await DisplayPromptAsync("Nieuwe Speler", "Voer de naam in:");
            string gender = await DisplayActionSheet("Kies het geslacht", "Annuleren", null, "Man", "Vrouw", "Anders");

            if (!string.IsNullOrWhiteSpace(name) && gender != "Annuleren")
            {
                Players.Add(new Player { Name = name, Gender = gender });
            }
        }

        // Methode voor Terug knop
        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Terug naar de vorige pagina
        }

        // Methode voor Bevestigen knop
        private async void OnConfirmClicked(object sender, EventArgs e)
        {
            if (Players.Count == 0)
            {
                await DisplayAlert("Fout", "Voeg minstens één speler toe voordat je verder gaat.", "OK");
                return;
            }

            await Navigation.PushAsync(new GameSettings());
        }
    }
}
