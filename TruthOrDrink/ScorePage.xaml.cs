using Microsoft.Maui.Controls;
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
            await Navigation.PopToRootAsync();
        }
    }
}
