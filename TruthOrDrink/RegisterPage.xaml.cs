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
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White
                    });

                    var nameEntry = CreateStyledEntry("Naam");
                    DynamicLayout.Children.Add(CreateStyledFrame(nameEntry));

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
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White
                    });

                    var ageEntry = CreateStyledEntry("Leeftijd");
                    ageEntry.Keyboard = Keyboard.Numeric;
                    DynamicLayout.Children.Add(CreateStyledFrame(ageEntry));

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
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White
                    });

                    var genderPicker = new Picker
                    {
                        ItemsSource = new List<string> { "Man", "Vrouw", "Anders" },
                        Title = "Kies je geslacht",
                        BackgroundColor = Colors.White,
                        TextColor = Colors.Black
                    };
                    DynamicLayout.Children.Add(CreateStyledFrame(genderPicker));

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
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White
                    });

                    var emailEntry = CreateStyledEntry("E-mailadres");
                    emailEntry.Keyboard = Keyboard.Email;
                    DynamicLayout.Children.Add(CreateStyledFrame(emailEntry));

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
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White
                    });

                    var passwordEntry = CreateStyledEntry("Wachtwoord");
                    passwordEntry.IsPassword = true;
                    DynamicLayout.Children.Add(CreateStyledFrame(passwordEntry));

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
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Colors.White
                    });

                    DynamicLayout.Children.Add(new Label { Text = $"Naam: {userName}", TextColor = Colors.White });
                    DynamicLayout.Children.Add(new Label { Text = $"Leeftijd: {age}", TextColor = Colors.White });
                    DynamicLayout.Children.Add(new Label { Text = $"Geslacht: {gender}", TextColor = Colors.White });
                    DynamicLayout.Children.Add(new Label { Text = $"E-mailadres: test", TextColor = Colors.White });

                    DynamicLayout.Children.Add(CreateNextButton(() =>
                    {
                        DisplayAlert("Succes", "Registratie voltooid!", "OK");
                        Navigation.PopAsync(); // Terug naar vorige pagina
                    }));
                    break;
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
