using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();

        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Instellingen", "Instellingen openen...", "OK");
        }

        private async void OnAddQuestionsClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Eigen Vragen", "Pagina voor eigen vragen toevoegen...", "OK");
        }

        private async void OnPlayClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Spelen", "Game starten...", "OK");
        }


    }
}
