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


        private async void OnPlayClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayerPage());
        }

        private async void OnAddQuestionsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QuestionPage());
        }




    }
}
