using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using TruthOrDrink;



namespace TruthOrDrink
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }
        public App(IServiceProvider services)
        {
            InitializeComponent();
            Services = services;


            //MainPage = new NavigationPage(new MainPage());  //haal deze comment weg als je login scherm wilt zien
            MainPage = new NavigationPage(new StartPage());
        }
    }
}
