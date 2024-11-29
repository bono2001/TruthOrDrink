using Microsoft.Maui.Controls;

namespace TruthOrDrink
{
    public partial class RegisterPage : ContentPage
    {
        private int currentStep = 0;
        private string userName, gender;
        private int age;

        public RegisterPage()
        {
            InitializeComponent();
            ShowStep();
        }

        private void ShowStep()
        {
            // Wis de huidige inhoud
            DynamicLayout.Children.Clear();

            switch (currentStep)
            {
                case 0: // Vraag om naam
                    DynamicLayout.Children.Add(new Label
                    {
                        Text = "Wat is je naam?",
                        FontSize = 24,
                        HorizontalOptions = LayoutOptions.Center
                    });
                    var nameEntry = new Entry { Placeholder = "Naam" };
                    DynamicLayout.Children.Add(nameEntry);
                    DynamicLayout.Children.Add(CreateNextButton(() =>
                    {
                        if (string.IsNullOrWhiteSpace(nameEntry.Text))
                        {
                            DisplayAlert("Fout", "Voer een naam in.", "OK");
                            return;
                        }
                        userName = nameEntry.Text;
                        currentStep++;
                        ShowStep();
                    }));
                    break;

                case 1: // Vraag om leeftijd
                    DynamicLayout.Children.Add(new Label
                    {
                        Text = "Wat is je leeftijd?",
                        FontSize = 24,
                        HorizontalOptions = LayoutOptions.Center
                    });
                    var ageEntry = new Entry { Placeholder = "Leeftijd", Keyboard = Keyboard.Numeric };
                    DynamicLayout.Children.Add(ageEntry);
                    DynamicLayout.Children.Add(CreateNextButton(() =>
                    {
                        if (!int.TryParse(ageEntry.Text, out age) || age <= 0)
                        {
                            DisplayAlert("Fout", "Voer een geldige leeftijd in.", "OK");
                            return;
                        }
                        currentStep++;
                        ShowStep();
                    }));
                    break;

                case 2: // Vraag om geslacht
                    DynamicLayout.Children.Add(new Label
                    {
                        Text = "Wat is je geslacht?",
                        FontSize = 24,
                        HorizontalOptions = LayoutOptions.Center
                    });
                    var genderPicker = new Picker
                    {
                        ItemsSource = new List<string> { "Man", "Vrouw", "Anders" },
                        Title = "Kies je geslacht"
                    };
                    DynamicLayout.Children.Add(genderPicker);
                    DynamicLayout.Children.Add(CreateNextButton(() =>
                    {
                        if (genderPicker.SelectedItem == null)
                        {
                            DisplayAlert("Fout", "Selecteer een geslacht.", "OK");
                            return;
                        }
                        gender = genderPicker.SelectedItem.ToString();
                        currentStep++;
                        ShowStep();
                    }));
                    break;

                case 3: // Vraag om e-mailadres
                    DynamicLayout.Children.Add(new Label
                    {
                        Text = "Wat is je e-mailadres?",
                        FontSize = 24,
                        HorizontalOptions = LayoutOptions.Center
                    });
                    var emailEntry = new Entry { Placeholder = "E-mailadres", Keyboard = Keyboard.Email };
                    DynamicLayout.Children.Add(emailEntry);
                    DynamicLayout.Children.Add(CreateNextButton(() =>
                    {
                        if (emailEntry.Text != "test")
                        {
                            DisplayAlert("Fout", "Het e-mailadres moet 'test' zijn.", "OK");
                            return;
                        }
                        currentStep++;
                        ShowStep();
                    }));
                    break;

                case 4: // Vraag om wachtwoord
                    DynamicLayout.Children.Add(new Label
                    {
                        Text = "Kies een wachtwoord",
                        FontSize = 24,
                        HorizontalOptions = LayoutOptions.Center
                    });
                    var passwordEntry = new Entry { Placeholder = "Wachtwoord", IsPassword = true };
                    DynamicLayout.Children.Add(passwordEntry);
                    DynamicLayout.Children.Add(CreateNextButton(() =>
                    {
                        if (passwordEntry.Text != "test")
                        {
                            DisplayAlert("Fout", "Het wachtwoord moet 'test' zijn.", "OK");
                            return;
                        }
                        currentStep++;
                        ShowStep();
                    }));
                    break;

                case 5: // Overzicht en bevestiging
                    DynamicLayout.Children.Add(new Label
                    {
                        Text = "Bevestig je gegevens:",
                        FontSize = 24,
                        HorizontalOptions = LayoutOptions.Center
                    });
                    DynamicLayout.Children.Add(new Label { Text = $"Naam: {userName}" });
                    DynamicLayout.Children.Add(new Label { Text = $"Leeftijd: {age}" });
                    DynamicLayout.Children.Add(new Label { Text = $"Geslacht: {gender}" });
                    DynamicLayout.Children.Add(new Label { Text = $"E-mailadres: test" });
                    DynamicLayout.Children.Add(CreateNextButton(() =>
                    {
                        DisplayAlert("Succes", "Registratie voltooid!", "OK");
                        Navigation.PopAsync(); // Terug naar vorige pagina
                    }));
                    break;
            }
        }

        private Button CreateNextButton(Action onClickAction)
        {
            return new Button
            {
                Text = "Volgende",
                BackgroundColor = Colors.Blue,
                TextColor = Colors.White,
                CornerRadius = 10,
                Command = new Command(onClickAction)
            };
        }
    }
}
