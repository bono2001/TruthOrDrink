using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();

            AnimateLabel();
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

        private async void AnimateLabel()
        {
            while (true)
            {
                await WelkomLabel.ScaleTo(1.2, 500, Easing.CubicInOut);
                await WelkomLabel.ScaleTo(1.0, 500, Easing.CubicInOut);
            }
        }

    }
}
