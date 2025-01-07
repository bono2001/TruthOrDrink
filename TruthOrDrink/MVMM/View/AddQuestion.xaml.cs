using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class AddQuestionPage : ContentPage
    {
        public AddQuestionPage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Placeholder: Hier voeg je de code toe om de vraag op te slaan
            await DisplayAlert("Succes", "De vraag is toegevoegd!", "OK");
            

        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
