using Microsoft.Maui.Controls;
using TruthOrDrink.ViewModels;

namespace TruthOrDrink
{
    public partial class GamePageSettings : ContentPage
    {
        public GamePageSettings()
        {
            InitializeComponent();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            // Navigeer terug naar de vorige pagina
            await Navigation.PopAsync();
        }

        private async void OnConfirmClicked(object sender, EventArgs e)
        {
            // Haal de ViewModel op uit de BindingContext
            if (BindingContext is GameSettingsViewModel vm)
            {
                // Converteer geselecteerde gewaagdheidsopties naar een string
                string boldness = string.Join(", ", vm.SelectedBoldnessOptions);

                // Toon een overzicht van de geselecteerde instellingen
                await DisplayAlert("Instellingen Geselecteerd",
                    $"Gewaagdheid: {boldness}\n" +
                    $"Categorie: {vm.SelectedCategory}\n" +
                    $"Vragenlijst: {vm.SelectedQuestionList}\n" +
                    $"Rondes: {vm.SelectedRounds}",
                    "OK");

                // Navigeer naar de volgende pagina (GamePage)
                await Navigation.PushAsync(new GamePage());
            }
        }
    }
}
