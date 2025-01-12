using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TruthOrDrink
{
    public partial class SettingsPage : ContentPage, INotifyPropertyChanged
    {
        private bool _isFlashlightOn = false; // Status van de zaklamp bijhouden
        private bool _isDarkMode; // Status van Dark Mode
        private Brush _backgroundBrush; // Achtergrond van de pagina

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                if (_isDarkMode != value)
                {
                    _isDarkMode = value;
                    OnPropertyChanged();
                    UpdateBackgroundBrush();
                }
            }
        }

        public Brush BackgroundBrush
        {
            get => _backgroundBrush;
            set
            {
                if (_backgroundBrush != value)
                {
                    _backgroundBrush = value;
                    OnPropertyChanged();
                }
            }
        }

        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = this;

            // Standaard achtergrond instellen
            BackgroundBrush = CreateDefaultBrush();
        }

        private Brush CreateDefaultBrush()
        {
            return new LinearGradientBrush(
                new GradientStopCollection
                {
                    new GradientStop(Colors.Blue, 0.0f),
                    new GradientStop(Colors.Red, 1.0f)
                },
                new Point(0, 0),
                new Point(0, 1));
        }

        private Brush CreateDarkModeBrush()
        {
            return new LinearGradientBrush(
                new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb("#001A33"), 0.0f), // Donkerblauw
                    new GradientStop(Color.FromArgb("#610e71"), 1.0f)  // Donkerpaars
                },
                new Point(0, 0),
                new Point(0, 1));
        }

        private void UpdateBackgroundBrush()
        {
            BackgroundBrush = IsDarkMode ? CreateDarkModeBrush() : CreateDefaultBrush();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Methode voor exit-knop
        private async void OnExitClicked(object sender, EventArgs e)
        {
            // Navigeren naar de StartPage
            await Navigation.PushAsync(new StartPage());
        }

        // Methode voor vibratieknop
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

        // Methode voor zaklampknop
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
