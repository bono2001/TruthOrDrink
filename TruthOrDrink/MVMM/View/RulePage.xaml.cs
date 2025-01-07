using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class RulePage : ContentPage
    {
        public RulePage()
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
