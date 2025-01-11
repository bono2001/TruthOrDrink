using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using TruthOrDrink.Models;
using TruthOrDrink.MVMM.Models;

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
            try
            {
                string name = await DisplayPromptAsync("Nieuwe Speler", "Voer de naam in:");
                if (string.IsNullOrWhiteSpace(name))
                {
                    await DisplayAlert("Fout", "De naam van een speler mag niet leeg zijn.", "OK");
                    return;
                }

                string gender = await DisplayActionSheet("Kies het geslacht", "Annuleren", null, "Man", "Vrouw", "Anders");

                if (gender == "Annuleren")
                {
                    return; // Annuleer toevoegen van speler
                }

                // Speler toevoegen
                Players.Add(new Player { Name = name, Gender = gender });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DOTNET] Fout bij het toevoegen van een speler: {ex.Message}");
                await DisplayAlert("Fout", "Er is een onverwachte fout opgetreden bij het toevoegen van de speler.", "OK");
            }
        }

        // Methode voor Terug knop
        private async void OnBackClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync(); // Terug naar de vorige pagina
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DOTNET] Fout bij terugnavigeren: {ex.Message}");
                await DisplayAlert("Fout", "Kan niet terugkeren naar de vorige pagina.", "OK");
            }
        }

        // Methode voor Bevestigen knop
        private async void OnConfirmClicked(object sender, EventArgs e)
        {
            try
            {
                if (Players.Count == 0)
                {
                    await DisplayAlert("Fout", "Voeg minstens één speler toe voordat je verder gaat.", "OK");
                    return;
                }

                // Navigeren naar GameSetting zonder database
                await Navigation.PushAsync(new GameSetting(Players));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DOTNET] Fout bij bevestigen: {ex.Message}");
                await DisplayAlert("Fout", $"Er is een onverwachte fout opgetreden: {ex.Message}", "OK");
            }
        }
    }
}
