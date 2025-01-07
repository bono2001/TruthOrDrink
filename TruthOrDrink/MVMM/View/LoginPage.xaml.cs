using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Controleer gebruikersnaam en wachtwoord
            if (username == "test" && password == "test")
            {
                // Navigeer naar de StartPage na succesvolle login
                await Navigation.PushAsync(new StartPage());
            }
            else
            {
                // Toon foutmelding als de logingegevens onjuist zijn
                ErrorLabel.Text = "Ongeldige gebruikersnaam of wachtwoord.";
                ErrorLabel.IsVisible = true;
            }
        }
    }
}
