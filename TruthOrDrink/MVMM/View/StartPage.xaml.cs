using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Devices;
using System;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;

namespace TruthOrDrink
{
    public partial class StartPage : ContentPage
    {
        private readonly Random _random = new Random(); // Willekeurige generator
        private bool _isAlertDisplayed = false; // Controleert of er al een pop-up open is

        public StartPage()
        {
            InitializeComponent();
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
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

        private async void OnFriendsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendPage());
        }

        // Dobbelsteen klikken: Geef instructies om te schudden
        private void OnDiceClicked(object sender, EventArgs e)
        {
            DisplayAlert("Schudden", "Schud je telefoon om te dobbelen!", "Ga Terug");
        }

        // Schuddetectie: Toon een willekeurig getal van 1 tot 6 en laat de telefoon trillen
        private async void OnShakeDetected(object sender, EventArgs e)
        {
            // Controleer of er al een pop-up open is
            if (_isAlertDisplayed)
                return;

            _isAlertDisplayed = true; // Markeer dat er een pop-up wordt weergegeven

            // Telefoon laten trillen
            try
            {
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(300));
            }
            catch (FeatureNotSupportedException)
            {
                // Trillen wordt niet ondersteund, geen actie nodig
            }

            // Genereer een willekeurig getal tussen 1 en 6
            var diceNumber = _random.Next(1, 7);

            // Toon een pop-up met het resultaat
            await DisplayAlert("Dobbelsteen", $"Je hebt een {diceNumber} gegooid!", "Ga Terug");

            _isAlertDisplayed = false; // Pop-up is gesloten, markeer als niet meer weergegeven
        }

        // Start de accelerometer wanneer de pagina wordt weergegeven
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Abonneer je op het schud-evenement
            Accelerometer.ShakeDetected += OnShakeDetected;

            // Start de accelerometer
            Accelerometer.Default.Start(SensorSpeed.Game);
        }

        // Stop de accelerometer wanneer de pagina verdwijnt
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Verwijder de schud-eventhandler
            Accelerometer.ShakeDetected -= OnShakeDetected;

            // Stop de accelerometer
            Accelerometer.Default.Stop();
        }
    }
}
