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

        // Geselecteerde waarden
        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCategory)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedQuestionList)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedRounds)));
                }
            }
        }

        private double _selectedBoldness;
        public double SelectedBoldness
        {
            get => _selectedBoldness;
            set
            {
                if (_selectedBoldness != value)
                {
                    _selectedBoldness = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedBoldness)));
                }
            }
        }

        // Constructor
        public GameSettingsViewModel()
        {
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

            SelectedBoldness = 3; 
        }
    }
}
