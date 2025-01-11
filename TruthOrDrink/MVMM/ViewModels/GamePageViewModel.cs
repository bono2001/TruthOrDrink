using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TruthOrDrink.Models;

namespace TruthOrDrink.ViewModels
{
    public class GamePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnGameOver;

        public ObservableCollection<Player> Players { get; set; }
        public string CurrentPlayerName => Players.Count > 0 ? Players[CurrentPlayerIndex].Name : "Geen spelers beschikbaar";
        public string CurrentQuestion => Questions.Count > 0 ? Questions[CurrentQuestionIndex].PlayerQuestion : "Geen vragen beschikbaar";
        public string RoundDisplay => $"Ronde {CurrentRound}/{TotalRounds}";

        private int CurrentPlayerIndex { get; set; }
        private int CurrentQuestionIndex { get; set; }
        private int CurrentRound { get; set; }
        private int TotalRounds { get; set; }
        private List<Question> Questions { get; set; } = new();

        private readonly QuestionRepository _dbService;

        public GamePageViewModel(QuestionRepository dbService, int totalRounds, int selectedCategoryId, string selectedDifficulty)
        {
            _dbService = dbService;
            TotalRounds = totalRounds;

            Players = new ObservableCollection<Player>
            {
                new Player { Name = "Speler 1", Score = 0 },
                new Player { Name = "Speler 2", Score = 0 }
            };

            CurrentRound = 1;
            CurrentPlayerIndex = 0;
            CurrentQuestionIndex = 0;

            // Laad vragen asynchroon
            Task.Run(() => LoadQuestionsAsync(selectedCategoryId, selectedDifficulty));
        }

        private async Task LoadQuestionsAsync(int categoryId, string difficulty)
        {
            try
            {
                var allQuestions = await _dbService.GetQuestionsAsync();
                Questions = allQuestions
                    .Where(q => q.CategoryId == categoryId && q.Difficulty == difficulty)
                    .ToList();

                if (Questions.Count == 0)
                {
                    Console.WriteLine("[DOTNET] Geen vragen beschikbaar voor deze categorie en moeilijkheid.");
                }

                OnPropertyChanged(nameof(CurrentQuestion));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DOTNET] Fout bij het laden van vragen: {ex.Message}");
            }
        }

        public void FailQuestion()
        {
            if (!ValidateGameState())
                return;

            Console.WriteLine($"{CurrentPlayerName} heeft de vraag niet gehaald.");
            NextTurn();
        }

        public void PassQuestion()
        {
            if (!ValidateGameState())
                return;

            Console.WriteLine($"{CurrentPlayerName} heeft de vraag gehaald en 5 punten verdiend.");
            Players[CurrentPlayerIndex].Score += 5;
            OnPropertyChanged(nameof(Players));
            NextTurn();
        }

        private void NextTurn()
        {
            if (!ValidateGameState())
                return;

            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;

            if (CurrentPlayerIndex == 0)
            {
                CurrentRound++;
                if (CurrentRound > TotalRounds)
                {
                    Console.WriteLine("[DOTNET] Het spel is voorbij. Navigeren naar ScorePage...");
                    OnGameOver?.Invoke();
                    return;
                }
            }

            CurrentQuestionIndex = (CurrentQuestionIndex + 1) % Questions.Count;
            OnPropertyChanged(nameof(CurrentPlayerName));
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(RoundDisplay));
        }

        private bool ValidateGameState()
        {
            if (Players.Count == 0)
            {
                Console.WriteLine("[DOTNET] Geen spelers beschikbaar. Het spel kan niet doorgaan.");
                return false;
            }

            if (Questions.Count == 0)
            {
                Console.WriteLine("[DOTNET] Geen vragen beschikbaar. Het spel kan niet doorgaan.");
                return false;
            }

            return true;
        }

        public List<Player> GetPlayers() => Players.ToList();

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
