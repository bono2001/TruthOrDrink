using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using TruthOrDrink.ViewModels;

namespace TruthOrDrink
{
    public partial class ScorePage : ContentPage
    {
        public ScorePage()
        {
            InitializeComponent();
            BindingContext = new GamePageViewModel();
        }

        private async void OnBackToStartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartPage());
        }

        private async void OnShareClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SharePage());
        }
    }
}
