using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TruthOrDrink.Models;
using TruthOrDrink.MVMM.Models;

namespace TruthOrDrink.ViewModels
{
    public class GameSettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly QuestionRepository _dbService;

        // Dropdown-opties
        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<string> DifficultyOptions { get; set; } = new();
        public ObservableCollection<string> RoundsOptions { get; set; } = new();

        // Geselecteerde waarden
        private Category _selectedCategory;
        public Category SelectedCategory
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

        // Constructor
        public GameSettingsViewModel(QuestionRepository dbService)
        {
            _dbService = dbService;

            // Laad opties
            LoadCategories();
            LoadDifficultyOptions();
            LoadRoundsOptions();
        }

        // Categorieën laden
        private async void LoadCategories()
        {
            if (_dbService == null)
            {
                Console.WriteLine("[DOTNET] Database niet beschikbaar. Hardcoded categorieën worden geladen.");
                UseHardcodedCategories();
                return;
            }

            try
            {
                var categories = await _dbService.GetCategoriesAsync();

                if (categories == null || categories.Count == 0)
                {
                    Console.WriteLine("[DOTNET] Geen categorieën gevonden in de database. Hardcoded categorieën worden geladen.");
                    UseHardcodedCategories();
                }
                else
                {
                    Categories.Clear();
                    foreach (var category in categories)
                    {
                        Categories.Add(category);
                    }
                }

                SelectedCategory = null; // Geen standaardcategorie
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DOTNET] Fout bij het laden van categorieën: {ex.Message}");
                UseHardcodedCategories();
            }
        }

        private void UseHardcodedCategories()
        {
            Categories.Clear();
            Categories.Add(new Category { Name = "Vrienden" });
            Categories.Add(new Category { Name = "Grappig" });
            Categories.Add(new Category { Name = "Spicy Koppel" });
            Categories.Add(new Category { Name = "Serieus" });
        }

        // Moeilijkheidsopties instellen
        private void LoadDifficultyOptions()
        {
            DifficultyOptions.Clear();
            DifficultyOptions.Add("Makkelijk");
            DifficultyOptions.Add("Gemiddeld");
            DifficultyOptions.Add("Moeilijk");

            SelectedDifficulty = null; // Geen standaard selectie
        }

        // Rondes instellen
        private void LoadRoundsOptions()
        {
            RoundsOptions.Clear();
            RoundsOptions.Add("5 Rondes");
            RoundsOptions.Add("10 Rondes");
            RoundsOptions.Add("15 Rondes");
            RoundsOptions.Add("20 Rondes");

            SelectedRounds = null; // Geen standaard selectie
        }

        // PropertyChanged helper
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
