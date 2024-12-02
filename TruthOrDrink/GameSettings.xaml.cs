using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class GameSettings : ContentPage
    {
        public GameSettings()
        {
            InitializeComponent();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Ga terug naar de vorige pagina
        }

        private async void OnConfirmClicked(object sender, EventArgs e)
        {
            var vm = BindingContext as ViewModels.GameSettingsViewModel;

            // Toon de geselecteerde instellingen
            await DisplayAlert("Instellingen Geselecteerd",
                $"Gewaagdheid: {vm.SelectedBoldness}\n" +
                $"Categorie: {vm.SelectedCategory}\n" +
                $"Vragenlijst: {vm.SelectedQuestionList}\n" +
                $"Rondes: {vm.SelectedRounds}",
                "OK");

            // Navigeer naar volgende pagina of gebruik de instellingen
        }
    }
}
