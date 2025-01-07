using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            // Navigeren naar de StartPage
            await Navigation.PushAsync(new StartPage());
        }
    }
}
