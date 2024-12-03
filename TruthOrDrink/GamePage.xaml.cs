using Microsoft.Maui.Controls;
using TruthOrDrink.ViewModels;

namespace TruthOrDrink
{
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
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
                await Navigation.PopToRootAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent going back to InstructiePage
            return true;
        }
    }
}
