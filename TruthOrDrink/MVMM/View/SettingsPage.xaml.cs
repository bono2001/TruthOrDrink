using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;

namespace TruthOrDrink
{
    public partial class SettingsPage : ContentPage
    {
        private bool _isFlashlightOn = false; // Status van de zaklamp bijhouden

        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            // Navigeren naar de StartPage
            await Navigation.PushAsync(new StartPage());
        }

        private void OnVibrateClicked(object sender, EventArgs e)
        {
            // Telefoon laten trillen
            try
            {
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(500));
            }
            catch (FeatureNotSupportedException)
            {
                DisplayAlert("Fout", "Trillen wordt niet ondersteund op dit apparaat.", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Fout", $"Er is een onverwachte fout opgetreden: {ex.Message}", "OK");
            }
        }

        private async void OnFlashlightClicked(object sender, EventArgs e)
        {
            // Zaklamp in-/uitschakelen
            try
            {
                if (_isFlashlightOn)
                {
                    await Flashlight.Default.TurnOffAsync(); // Zaklamp uitzetten
                    _isFlashlightOn = false; // Status bijwerken
                }
                else
                {
                    await Flashlight.Default.TurnOnAsync(); // Zaklamp aanzetten
                    _isFlashlightOn = true; // Status bijwerken
                }
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Fout", "De zaklamp wordt niet ondersteund op dit apparaat.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fout", $"Er is een fout opgetreden: {ex.Message}", "OK");
            }
        }
    }
}
