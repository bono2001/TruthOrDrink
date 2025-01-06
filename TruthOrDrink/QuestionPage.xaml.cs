using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TruthOrDrink.Models;

namespace TruthOrDrink
{
    public partial class QuestionPage : ContentPage
    {
        private readonly QuestionRepository _dbService;

        public ObservableCollection<Question> Questions { get; } = new();

        // Constructor zonder parameter. Haalt LocalDbService intern op via DI.
        public QuestionPage()
        {
            InitializeComponent();

            // Haal de LocalDbService op via Dependency Injection
            _dbService = App.Services.GetService<QuestionRepository>();

            if (_dbService == null)
            {
                Console.WriteLine("Fout: LocalDbService kon niet worden opgehaald.");
                return;
            }

            BindingContext = this;

            // Laad de vragen
            LoadQuestions();
        }

        private async void LoadQuestions()
        {
            try
            {
                // Haal vragen op uit de database
                var storedQuestions = await _dbService.GetQuestionsAsync();

                // Filter alleen de vragen van de speler (PlayerQuestion)
                var playerQuestions = storedQuestions.Where(q => !string.IsNullOrEmpty(q.PlayerQuestion)).ToList();

                Questions.Clear();
                foreach (var question in playerQuestions)
                {
                    Questions.Add(question);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het laden van vragen uit de database: {ex.Message}");
            }
        }

        private async void OnAddQuestionConfirmed(object sender, EventArgs e)
        {
            // Haal waarden uit het formulier
            string questionText = QuestionEntry.Text;
            string difficulty = DifficultyPicker.SelectedItem as string;
            string category = CategoryPicker.SelectedItem as string;

            // Validatie
            if (string.IsNullOrEmpty(questionText) || string.IsNullOrEmpty(difficulty) || string.IsNullOrEmpty(category))
            {
                await DisplayAlert("Fout", "Alle velden moeten worden ingevuld.", "OK");
                return;
            }

            // Maak een nieuwe vraag
            var newQuestion = new Question
            {
                Difficulty = difficulty,
                Category = category,
                DefaultQuestion = null, // Dit is een speler-vraag
                PlayerQuestion = questionText
            };

            try
            {
                // Voeg de vraag toe aan de database
                await _dbService.AddQuestionAsync(newQuestion);

                // Voeg de vraag toe aan de lijst die op de pagina wordt weergegeven
                Questions.Add(newQuestion);

                // Leeg de velden na toevoegen
                QuestionEntry.Text = string.Empty;
                DifficultyPicker.SelectedItem = null;
                CategoryPicker.SelectedItem = null;

                await DisplayAlert("Succes", "De vraag is toegevoegd!", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het toevoegen van een vraag aan de database: {ex.Message}");
                await DisplayAlert("Fout", "Er is iets misgegaan bij het opslaan van de vraag.", "OK");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            // Ga terug naar de vorige pagina
            await Navigation.PopAsync();
        }
    }
}
