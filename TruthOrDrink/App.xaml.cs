using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Stel LoginPage in als de hoofdpagina van de app
            //MainPage = new NavigationPage(new MainPage());  //haal deze comment weg als je login scherm wilt zien
            MainPage = new NavigationPage(new StartPage());
        }
    }
}
