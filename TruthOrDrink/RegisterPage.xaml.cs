using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace TruthOrDrink
{
    public partial class RegisterPage : ContentPage
    {
        private int currentStep = 0;
        private string userName, gender, email;
        private int age;

        public RegisterPage()
        {
            InitializeComponent();
            ShowStep();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Ga terug naar de vorige pagina
        }

        private async void OnConfirmClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bevestigd", "Je hebt je registratie voltooid.", "OK");
        }

        private void ShowStep()
        {
            // Dynamische inhoud logica blijft hetzelfde
        }

        private Frame CreateStyledFrame(View content)
        {
            return new Frame
            {
                Content = content,
                BorderColor = Colors.Black,
                CornerRadius = 10,
                Padding = new Thickness(10),
                WidthRequest = 270,
                HorizontalOptions = LayoutOptions.Center,
                Shadow = new Shadow { Opacity = 0.2f, Offset = new Point(2, 2) }
            };
        }

        private Entry CreateStyledEntry(string placeholder)
        {
            return new Entry
            {
                Placeholder = placeholder,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 250,
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                Margin = new Thickness(0, 10, 0, 10)
            };
        }

        private Button CreateNextButton(Action onClickAction)
        {
            return new Button
            {
                Text = "Volgende",
                BackgroundColor = Colors.Blue,
                TextColor = Colors.White,
                CornerRadius = 10,
                Command = new Command(onClickAction),
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 200
            };
        }
    }
}
