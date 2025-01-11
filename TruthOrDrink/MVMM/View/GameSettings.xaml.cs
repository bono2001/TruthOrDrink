using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using TruthOrDrink.Models;

namespace TruthOrDrink
{
    public partial class GameSetting : ContentPage
    {
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();
        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Difficulties { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> RoundsOptions { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> QuestionOptions { get; set; } = new ObservableCollection<string>();

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                }
            }
        }

        private string _selectedDifficulty;
        public string SelectedDifficulty
        {
            get => _selectedDifficulty;
            set
            {
                if (_selectedDifficulty != value)
                {
                    _selectedDifficulty = value;
                    OnPropertyChanged(nameof(SelectedDifficulty));
                }
            }
        }

        private string _selectedRounds;
        public string SelectedRounds
        {
            get => _selectedRounds;
            set
            {
                if (_selectedRounds != value)
                {
                    _selectedRounds = value;
                    OnPropertyChanged(nameof(SelectedRounds));
                }
            }
        }

        private string _selectedQuestionOption;
        public string SelectedQuestionOption
        {
            get => _selectedQuestionOption;
            set
            {
                if (_selectedQuestionOption != value)
                {
                    _selectedQuestionOption = value;
                    OnPropertyChanged(nameof(SelectedQuestionOption));
                }
            }
        }

        public GameSetting(ObservableCollection<Player> players)
        {
            InitializeComponent();

            if (players == null || players.Count == 0)
            {
                Console.WriteLine("[DOTNET] Geen spelers beschikbaar. Initialisatie geannuleerd.");
                DisplayAlert("Fout", "Er moeten spelers worden toegevoegd voordat je de instellingen kunt aanpassen.", "OK");
                return;
            }

            Players = players;

            // Vul de opties
            LoadHardcodedOptions();

            // Instellen van de BindingContext
            BindingContext = this;
        }

        private void LoadHardcodedOptions()
        {
            // Vul de categorieën
            Categories.Clear();
            Categories.Add("Vrienden");
            Categories.Add("Grappig");
            Categories.Add("Spicy Koppel");
            Categories.Add("Serieus");

            // Vul de moeilijkheden
            Difficulties.Clear();
            Difficulties.Add("Makkelijk");
            Difficulties.Add("Gemiddeld");
            Difficulties.Add("Moeilijk");

            // Vul de rondes
            RoundsOptions.Clear();
            RoundsOptions.Add("5 Rondes");
            RoundsOptions.Add("10 Rondes");
            RoundsOptions.Add("15 Rondes");
            RoundsOptions.Add("20 Rondes");

            // Vul de vragenopties
            QuestionOptions.Clear();
            QuestionOptions.Add("Default Vragen");
            QuestionOptions.Add("Eigen Vragen");
            QuestionOptions.Add("Gemixt");

            // Zorg ervoor dat geen standaardwaarden geselecteerd zijn
            SelectedCategory = null;
            SelectedDifficulty = null;
            SelectedRounds = null;
            SelectedQuestionOption = null;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DOTNET] Fout bij terugnavigeren: {ex.Message}");
                await DisplayAlert("Fout", "Kan niet terugkeren naar de vorige pagina.", "OK");
            }
        }

        private async void OnConfirmClicked(object sender, EventArgs e)
        {
            try
            {
                // Valideer invoer
                if (string.IsNullOrEmpty(SelectedCategory))
                {
                    await DisplayAlert("Fout", "Selecteer een categorie voordat je doorgaat.", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedDifficulty))
                {
                    await DisplayAlert("Fout", "Selecteer een moeilijkheid voordat je doorgaat.", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedRounds))
                {
                    await DisplayAlert("Fout", "Selecteer het aantal rondes voordat je doorgaat.", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedQuestionOption))
                {
                    await DisplayAlert("Fout", "Selecteer een vragenoptie voordat je doorgaat.", "OK");
                    return;
                }

                if (!int.TryParse(SelectedRounds.Split(' ')[0], out int totalRounds))
                {
                    await DisplayAlert("Fout", "Ongeldig aantal rondes geselecteerd.", "OK");
                    return;
                }

                // Debug-log voor navigatie
                Console.WriteLine("[DOTNET] Navigeren naar GamePage met:");
                Console.WriteLine($"- Categorie: {SelectedCategory}");
                Console.WriteLine($"- Moeilijkheid: {SelectedDifficulty}");
                Console.WriteLine($"- Rondes: {totalRounds}");
                Console.WriteLine($"- Vragenoptie: {SelectedQuestionOption}");
                Console.WriteLine($"- Aantal spelers: {Players.Count}");

                // Navigeren naar GamePage
                await Navigation.PushAsync(new GamePage(Players, totalRounds, SelectedCategory, SelectedDifficulty, SelectedQuestionOption));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DOTNET] Fout bij bevestigen van instellingen: {ex.Message}");
                await DisplayAlert("Fout", $"Er is een onverwachte fout opgetreden: {ex.Message}", "OK");
            }
        }

    }
}
