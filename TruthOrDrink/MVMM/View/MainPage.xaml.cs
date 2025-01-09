using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);


           
        }


        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Navigeren naar de loginpagina
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
