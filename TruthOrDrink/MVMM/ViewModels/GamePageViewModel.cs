using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace TruthOrDrink.ViewModels
{
    public class GamePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnGameOver; // Event for game over navigation

        public ObservableCollection<Player> Players { get; set; }
        public string CurrentPlayerName => Players[CurrentPlayerIndex].Name;
        public string CurrentQuestion => Questions[CurrentQuestionIndex];
        public string RoundDisplay => $"Ronde {CurrentRound}/{TotalRounds}";

        private int CurrentPlayerIndex { get; set; }
        private int CurrentQuestionIndex { get; set; }
        private int CurrentRound { get; set; }
        private int TotalRounds { get; set; }
        private List<string> Questions { get; set; }

        public GamePageViewModel()
        {
            Players = new ObservableCollection<Player>
            {
                new Player { Name = "Speler 1", Points = 0 },
                new Player { Name = "Speler 2", Points = 0 }
            };

            Questions = new List<string>
            {
                "Wat is je grootste geheim?",
                "Wie is je crush?",
                "Noem iets wat je nog nooit hebt verteld."
            };

            TotalRounds = 10;
            CurrentRound = 1;
            CurrentPlayerIndex = 0;
            CurrentQuestionIndex = 0;
        }

        public void FailQuestion()
        {
            NextTurn();
        }

        public void PassQuestion()
        {
            Players[CurrentPlayerIndex].Points += 5;
            OnPropertyChanged(nameof(Players));
            NextTurn();
        }

        private void NextTurn()
        {
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;

            if (CurrentPlayerIndex == 0)
            {
                CurrentRound++;
                if (CurrentRound > TotalRounds)
                {
                    OnGameOver?.Invoke(); // Trigger Game Over Event
                    return;
                }
            }

            CurrentQuestionIndex = (CurrentQuestionIndex + 1) % Questions.Count;
            OnPropertyChanged(nameof(CurrentPlayerName));
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(RoundDisplay));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Player : INotifyPropertyChanged
    {
        public string Name { get; set; }
        private int _points;
        public int Points
        {
            get => _points;
            set
            {
                _points = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Points)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
