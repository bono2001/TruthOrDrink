using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace TruthOrDrink
{
    public partial class RegisterPage : ContentPage
    {
        private int currentStep = 0;
        private string userName, gender, email;
        private int age;
        private bool isRegistrationComplete = false;

        public RegisterPage()
        {
            InitializeComponent();
            ShowStep(); // Zorg dat de eerste stap wordt weergegeven bij het laden van de pagina
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Ga terug naar de vorige pagina
        }

        private async void OnConfirmClicked(object sender, EventArgs e)
        {
            if (isRegistrationComplete)
            {
                // Laat een bevestigingsbericht zien
                await DisplayAlert("Registratie Voltooid", $"Welkom, {userName}!\nGeslacht: {gender}\nLeeftijd: {age}", "OK");

                // Navigeren naar MainPage
                await Navigation.PushAsync(new StartPage());
            }
            else
            {
                // Geef een waarschuwing als de registratie niet compleet is
                await DisplayAlert("Niet Compleet", "Voltooi alle stappen om je registratie te bevestigen.", "OK");
            }
        }

        private void ShowStep()
        {
            // Maak de dynamische inhoud leeg voordat nieuwe inhoud wordt toegevoegd
            DynamicLayout.Children.Clear();

            if (currentStep == 0)
            {
                // Stap 1: Vraag de naam van de gebruiker
                var nameEntry = CreateStyledEntry("Voer je naam in");
                DynamicLayout.Children.Add(CreateStyledFrame(nameEntry));

                DynamicLayout.Children.Add(CreateNextButton(() =>
                {
                    userName = nameEntry.Text; // Sla de ingevoerde naam op
                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        DisplayAlert("Fout", "Naam mag niet leeg zijn.", "OK");
                    }
                    else
                    {
                        currentStep++;
                        ShowStep(); // Toon de volgende stap
                    }
                }));
            }
            else if (currentStep == 1)
            {
                // Stap 2: Vraag het geslacht van de gebruiker
                var genderPicker = new Picker
                {
                    Title = "Selecteer je geslacht",
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 250,
                    BackgroundColor = Colors.White,
                    TextColor = Colors.Black,
                    Margin = new Thickness(0, 10, 0, 10)
                };

                // Voeg de opties toe aan de Picker
                genderPicker.Items.Add("Man");
                genderPicker.Items.Add("Vrouw");
                genderPicker.Items.Add("Anders");

                // Voeg de Picker toe aan de layout
                DynamicLayout.Children.Add(CreateStyledFrame(genderPicker));

                DynamicLayout.Children.Add(CreateNextButton(() =>
                {
                    if (genderPicker.SelectedIndex != -1) // Controleer of een optie is geselecteerd
                    {
                        gender = genderPicker.Items[genderPicker.SelectedIndex]; // Haal de geselecteerde waarde op
                        currentStep++;
                        ShowStep(); // Toon de volgende stap
                    }
                    else
                    {
                        DisplayAlert("Fout", "Selecteer een geslacht.", "OK");
                    }
                }));
            }
            else if (currentStep == 2)
            {
                // Stap 3: Vraag de leeftijd van de gebruiker
                var ageEntry = CreateStyledEntry("Voer je leeftijd in");
                DynamicLayout.Children.Add(CreateStyledFrame(ageEntry));

                DynamicLayout.Children.Add(CreateNextButton(() =>
                {
                    if (int.TryParse(ageEntry.Text, out age) && age > 0)
                    {
                        currentStep++;
                        isRegistrationComplete = true; // Markeer de registratie als compleet
                        ShowStep(); // Toon de volgende stap
                    }
                    else
                    {
                        DisplayAlert("Fout", "Voer een geldige leeftijd in.", "OK");
                    }
                }));
            }
            else
            {
                // Stap 4: Laat een bevestigingsbericht zien
                DynamicLayout.Children.Add(CreateStyledFrame(new Label
                {
                    Text = "Druk op de bevestig-knop om je registratie af te ronden.",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }));
            }
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
