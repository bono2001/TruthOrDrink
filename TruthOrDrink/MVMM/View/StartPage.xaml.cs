using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Devices;
using System;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using Plugin.AudioRecorder;
using System.Linq; // Voor audio-analyse
using System.Threading.Tasks; // Voor async functionaliteiten

namespace TruthOrDrink
{
    public partial class StartPage : ContentPage
    {
        private readonly Random _random = new Random(); // Willekeurige generator
        private bool _isAlertDisplayed = false; // Controleert of er al een pop-up open is
        private AudioRecorderService _recorder; // Voor de blaastest

        public StartPage()
        {
            InitializeComponent();
            _recorder = new AudioRecorderService
            {
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(3), // Luister 3 seconden
                AudioSilenceTimeout = TimeSpan.FromMilliseconds(500), // Stop na stilte
                PreferredSampleRate = 44100 // Hoge kwaliteit audio
            };
        }

        private async Task CheckAndRequestMicrophonePermissionAsync()
        {
            // Controleer of de permissie al is verleend
            var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();

            if (status != PermissionStatus.Granted)
            {
                // Vraag de permissie aan
                status = await Permissions.RequestAsync<Permissions.Microphone>();
            }

            // Controleer opnieuw de status
            if (status != PermissionStatus.Granted)
            {
                // Als de gebruiker de permissie weigert
                await DisplayAlert("Permissie nodig", "Microfoontoegang is nodig voor de blaastest.", "OK");
            }
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

        private async void OnBlowTestClicked(object sender, EventArgs e)
        {
            // Controleer en vraag permissie aan
            await CheckAndRequestMicrophonePermissionAsync();

            // Controleer of permissie is verleend
            var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (status != PermissionStatus.Granted)
            {
                // Als de permissie niet is verleend, stop hier
                return;
            }

            try
            {
                // Start opnemen
                await DisplayAlert("Blaastest", "Blaas nu in de microfoon!", "KLik hier om te beginnen!");
                _recorder.StartRecording(); // Start de opname

                // Wacht tot opname voltooid is
                await Task.Delay(3000);
                await _recorder.StopRecording(); // Stop de opname correct met 'await'

                // Verkrijg het bestandspad van de opname
                string audioResult = _recorder.GetAudioFilePath();

                // Controleer of opname correct is voltooid
                if (!string.IsNullOrEmpty(audioResult) && System.IO.File.Exists(audioResult))
                {
                    var audioData = System.IO.File.ReadAllBytes(audioResult);
                    double peakVolume = CalculatePeakVolume(audioData);
                    double averageVolume = CalculateAverageVolume(audioData);

                    // Debugging: Toon het piek- en gemiddelde volume
                    await DisplayAlert("Volume Debug", $"Peak Volume: {peakVolume}\nAverage Volume: {averageVolume}", "OK");

                    // Stel aangepaste drempelwaarden in
                    if (peakVolume > 0.4 || averageVolume > 0.2) // Hard blazen
                    {
                        await DisplayAlert("Resultaat", "Je hebt hard geblazen! Je bent nuchter!", "Sluiten");
                    }
                    else if (peakVolume > 0.2 || averageVolume > 0.1) // Zacht blazen
                    {
                        await DisplayAlert("Resultaat", "Je hebt te zacht geblazen! Je bent dronken!", "Sluiten");
                    }
                    else
                    {
                        await DisplayAlert("Resultaat", "Geen blaasgeluid gedetecteerd. Probeer opnieuw.", "Sluiten");
                    }
                }
                else
                {
                    await DisplayAlert("Fout", "Geen blaasgeluid gedetecteerd. Probeer opnieuw.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fout", $"Er is een probleem opgetreden: {ex.Message}", "OK");
            }
            finally
            {
                // Herinitialiseer de recorder voor een nieuwe test
                _recorder = new AudioRecorderService
                {
                    StopRecordingAfterTimeout = true,
                    TotalAudioTimeout = TimeSpan.FromSeconds(3), // Luister 3 seconden
                    AudioSilenceTimeout = TimeSpan.FromMilliseconds(500), // Stop na stilte
                    PreferredSampleRate = 44100 // Hoge kwaliteit audio
                };
            }
        }

        private double CalculatePeakVolume(byte[] audioData)
        {
            // Converteer de audiogegevens naar een bereik van 0-1
            var normalizedData = audioData.Select(b => Math.Abs((double)b / byte.MaxValue)).ToArray();

            // Retourneer de piekwaarde (hoogste amplitude)
            return normalizedData.Max();
        }

        private double CalculateAverageVolume(byte[] audioData)
        {
            // Converteer de audiogegevens naar een bereik van 0-1
            var normalizedData = audioData.Select(b => Math.Abs((double)b / byte.MaxValue)).ToArray();

            // Retourneer het gemiddelde volume
            return normalizedData.Average();
        }
    }
}
