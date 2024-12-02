using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TruthOrDrink.ViewModels
{
    public class GamePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Eigenschappen
        public ObservableCollection<Player> Players { get; set; }
        public string CurrentPlayerName => Players[CurrentPlayerIndex].Name;
        public string CurrentQuestion => Questions[CurrentQuestionIndex];
        public string RoundDisplay => $"Ronde {CurrentRound}/{TotalRounds}";

        private int CurrentPlayerIndex { get; set; }
        private int CurrentQuestionIndex { get; set; }
        private int CurrentRound { get; set; }
        private int TotalRounds { get; set; }
        private List<string> Questions { get; set; }

        // Constructor
        public GamePageViewModel()
        {
            // Dummy spelers en vragen
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

        // Methode: Vraag niet gehaald
        public void FailQuestion()
        {
            // Alleen naar de volgende beurt gaan
            NextTurn();
        }

        // Methode: Vraag gehaald
        public void PassQuestion()
        {
            // Speler krijgt punten
            Players[CurrentPlayerIndex].Points += 5;
            OnPropertyChanged(nameof(Players)); // Update de UI voor de puntenlijst
            NextTurn();
        }

        // Methode: Naar de volgende beurt
        private void NextTurn()
        {
            // Volgende speler
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;

            // Controleer of de ronde voorbij is
            if (CurrentPlayerIndex == 0)
            {
                CurrentRound++;
                if (CurrentRound > TotalRounds)
                {
                    // Spel beëindigen
                    GameOver();
                    return;
                }
            }

            // Volgende vraag
            CurrentQuestionIndex = (CurrentQuestionIndex + 1) % Questions.Count;

            // Update gebonden eigenschappen
            OnPropertyChanged(nameof(CurrentPlayerName));
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(RoundDisplay));
        }

        // Methode: Spel beëindigen
        private async void GameOver()
        {
            string winner = Players.OrderByDescending(p => p.Points).First().Name;
            await App.Current.MainPage.DisplayAlert("Spel Beëindigd", $"Gefeliciteerd {winner}, je hebt gewonnen!", "OK");
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        // Helper voor PropertyChanged
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Player klasse
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
