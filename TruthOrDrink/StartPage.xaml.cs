using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using TruthOrDrink.Models;

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
            // Navigeer naar de SettingsPage
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void OnAddQuestionsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QuestionPage());
        }

        private async void OnPlayClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayerPage());
        }

        private async void OnRulesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RulePage());
        }
    }
}
