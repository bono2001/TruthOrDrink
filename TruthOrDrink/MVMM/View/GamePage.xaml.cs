using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using TruthOrDrink.Models;

namespace TruthOrDrink
{
    public partial class GamePage : ContentPage
    {
        public ObservableCollection<Player> Players { get; set; }
        public ObservableCollection<string> Questions { get; set; } = new();

        public int CurrentPlayerIndex { get; set; }
        public int CurrentRound { get; set; }
        public int TotalRounds { get; set; }

        public string CurrentPlayerName => Players[CurrentPlayerIndex].Name;
        public string CurrentQuestion => Questions.Count > 0 ? Questions[CurrentPlayerIndex % Questions.Count] : "Geen vragen beschikbaar.";
        public string RoundDisplay => $"Ronde {CurrentRound}/{TotalRounds}";

        public GamePage(ObservableCollection<Player> players, int totalRounds, string category, string difficulty, string questionOption)
        {
            InitializeComponent();

            if (players == null || players.Count == 0)
            {
                DisplayAlert("Fout", "Er zijn geen spelers toegevoegd.", "OK");
                Navigation.PopAsync();
                return;
            }

            Players = players;
            TotalRounds = totalRounds;

            // Logica om vragen op basis van de geselecteerde optie te simuleren
            Questions = GenerateQuestions(category, difficulty, questionOption);

            CurrentPlayerIndex = 0;
            CurrentRound = 1;

            BindingContext = this;
        }

        private ObservableCollection<string> GenerateQuestions(string category, string difficulty, string questionOption)
        {
            var questions = new ObservableCollection<string>();

            if (questionOption == "Default Vragen")
            {
                questions.Add($"Default Vraag 1 ({category} - {difficulty})");
                questions.Add($"Default Vraag 2 ({category} - {difficulty})");
                questions.Add($"Default Vraag 3 ({category} - {difficulty})");
            }
            else if (questionOption == "Eigen Vragen")
            {
                questions.Add($"Eigen Vraag 1 ({category} - {difficulty})");
                questions.Add($"Eigen Vraag 2 ({category} - {difficulty})");
                questions.Add($"Eigen Vraag 3 ({category} - {difficulty})");
            }
            else if (questionOption == "Gemixt")
            {
                questions.Add($"Default Vraag 1 ({category} - {difficulty})");
                questions.Add($"Eigen Vraag 1 ({category} - {difficulty})");
                questions.Add($"Default Vraag 2 ({category} - {difficulty})");
                questions.Add($"Eigen Vraag 2 ({category} - {difficulty})");
            }
            else
            {
                questions.Add("Geen geldige vragenoptie geselecteerd.");
            }

            return questions;
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Verlaat Spel", "Weet je zeker dat je het spel wilt verlaten?", "Ja", "Nee");
            if (confirm)
            {
                await Navigation.PopAsync(); // Ga terug naar de vorige pagina
            }
        }

        private void OnFailedClicked(object sender, EventArgs e)
        {
            Console.WriteLine($"{CurrentPlayerName} heeft de vraag niet gehaald.");
            NextTurn();
        }

        private void OnSuccessClicked(object sender, EventArgs e)
        {
            Players[CurrentPlayerIndex].Score += 5;
            Console.WriteLine($"{CurrentPlayerName} heeft de vraag gehaald en 5 punten verdiend.");
            NextTurn();
        }

        private void NextTurn()
        {
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;

            if (CurrentPlayerIndex == 0) // Nieuwe ronde begint
            {
                CurrentRound++;
                if (CurrentRound > TotalRounds)
                {
                    GameOver();
                    return;
                }
            }

            OnPropertyChanged(nameof(CurrentPlayerName));
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(RoundDisplay));
        }

        private async void GameOver()
        {
            Console.WriteLine("Het spel is voorbij!");
            await DisplayAlert("Game Over", "Het spel is voorbij! Bekijk de scores.", "OK");
            // Hier kun je navigeren naar een ScorePage
            await Navigation.PopToRootAsync(); // Terug naar de startpagina
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            OnPropertyChanged(nameof(CurrentPlayerName));
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(RoundDisplay));
        }
    }
}
