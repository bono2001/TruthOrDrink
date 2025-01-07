using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TruthOrDrink.ViewModels
{
    public class GameSettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Dropdown-opties
        public ObservableCollection<string> Categories { get; set; }
        public ObservableCollection<string> QuestionLists { get; set; }
        public ObservableCollection<string> RoundsOptions { get; set; }
        public ObservableCollection<string> BoldnessOptions { get; set; }

        // Geselecteerde waarden
        private ObservableCollection<string> _selectedBoldnessOptions = new ObservableCollection<string>();
        public ObservableCollection<string> SelectedBoldnessOptions
        {
            get => _selectedBoldnessOptions;
            set
            {
                if (_selectedBoldnessOptions != value)
                {
                    _selectedBoldnessOptions = value;
                    OnPropertyChanged(nameof(SelectedBoldnessOptions));
                }
            }
        }

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

        private string _selectedQuestionList;
        public string SelectedQuestionList
        {
            get => _selectedQuestionList;
            set
            {
                if (_selectedQuestionList != value)
                {
                    _selectedQuestionList = value;
                    OnPropertyChanged(nameof(SelectedQuestionList));
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
        public GameSettingsViewModel()
        {
            // Dropdown-opties initialiseren
            Categories = new ObservableCollection<string>
            {
                "Algemeen",
                "Feest",
                "Persoonlijk",
                "Intiem"
            };

            QuestionLists = new ObservableCollection<string>
            {
                "Standaard Vragen",
                "Eigen Vragen",
                "Gemengd"
            };

            RoundsOptions = new ObservableCollection<string>
            {
                "5 Rondes",
                "10 Rondes",
                "15 Rondes",
                "20 Rondes"
            };

            BoldnessOptions = new ObservableCollection<string>
            {
                "☆",
                "☆☆",
                "☆☆☆",
                "☆☆☆☆",
                "☆☆☆☆☆"
            };

            // Defaultwaarden instellen
            SelectedCategory = Categories[0];
            SelectedQuestionList = QuestionLists[0];
            SelectedRounds = RoundsOptions[0];
        }

        // Helper-methode voor PropertyChanged
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
