using Microsoft.Maui.Controls;
using TruthOrDrink.ViewModels;

namespace TruthOrDrink
{
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
            if (BindingContext is GamePageViewModel vm)
            {
                vm.OnGameOver += NavigateToScorePage;
            }
        }

        private async void NavigateToScorePage()
        {
            await Navigation.PushAsync(new ScorePage());
        }

        private void OnFailedClicked(object sender, EventArgs e)
        {
            if (BindingContext is GamePageViewModel vm)
            {
                vm.FailQuestion();
            }
        }

        private void OnSuccessClicked(object sender, EventArgs e)
        {
            if (BindingContext is GamePageViewModel vm)
            {
                vm.PassQuestion();
            }
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Verlaat Spel", "Weet je zeker dat je het spel wilt verlaten?", "Ja", "Nee");
            if (confirm)
            {
                await Navigation.PushAsync(new StartPage());
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
