using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TruthOrDrink.Models;
using TruthOrDrink.MVMM.Models;

namespace TruthOrDrink
{
    public partial class QuestionPage : ContentPage
    {
        private readonly QuestionRepository _dbService;

        public ObservableCollection<Question> Questions { get; } = new();

        public ICommand DeleteQuestionCommand { get; }
        public ICommand UpdateQuestionCommand { get; }

        public QuestionPage()
        {
            InitializeComponent();

            _dbService = App.Services.GetService<QuestionRepository>();

            if (_dbService == null)
            {
                Console.WriteLine("Fout: QuestionRepository kon niet worden opgehaald.");
                return;
            }

            DeleteQuestionCommand = new Command<Question>(DeleteQuestion);
            UpdateQuestionCommand = new Command<Question>(UpdateQuestion);

            BindingContext = this;

            LoadQuestions();
            LoadCategories();
        }

        private async void LoadQuestions()
        {
            try
            {
                var storedQuestions = await _dbService.GetQuestionsAsync();
                var categories = await _dbService.GetCategoriesAsync();

                // Filter alleen player-created vragen
                var playerQuestions = storedQuestions
                    .Where(q => !string.IsNullOrEmpty(q.PlayerQuestion))
                    .ToList();

                // Wijs categorieën toe aan vragen
                foreach (var question in playerQuestions)
                {
                    question.Category = categories.FirstOrDefault(c => c.Id == question.CategoryId);
                }

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

        private async void LoadCategories()
        {
            try
            {
                var categories = await _dbService.GetCategoriesAsync();

                // Voeg standaardcategorieën toe als er geen categorieën zijn
                if (categories == null || !categories.Any())
                {
                    await _dbService.AddCategoryAsync(new Category { Name = "Vrienden" });
                    await _dbService.AddCategoryAsync(new Category { Name = "Spicy Koppel" });
                    await _dbService.AddCategoryAsync(new Category { Name = "Grappig" });
                    await _dbService.AddCategoryAsync(new Category { Name = "Serieus" });

                    categories = await _dbService.GetCategoriesAsync();
                }

                CategoryPicker.ItemsSource = categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het laden van categorieën: {ex.Message}");
            }
        }

        private async void CreateQuestion(object sender, EventArgs e)
        {
            string questionText = QuestionEntry.Text;
            string difficulty = DifficultyPicker.SelectedItem as string;
            var selectedCategory = CategoryPicker.SelectedItem as Category;

            if (string.IsNullOrEmpty(questionText) || string.IsNullOrEmpty(difficulty) || selectedCategory == null)
            {
                await DisplayAlert("Fout", "Alle velden moeten worden ingevuld.", "OK");
                return;
            }

            var newQuestion = new Question
            {
                Difficulty = difficulty,
                CategoryId = selectedCategory.Id,
                PlayerQuestion = questionText
            };

            try
            {
                await _dbService.AddQuestionAsync(newQuestion);
                Questions.Add(newQuestion);

                // Maak invoervelden leeg
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

        private async void DeleteQuestion(Question question)
        {
            if (question == null) return;

            bool isConfirmed = await DisplayAlert(
                "Bevestigen",
                $"Weet je zeker dat je deze vraag wilt verwijderen?\n\nVraag: {question.PlayerQuestion}",
                "Ja",
                "Nee"
            );

            if (!isConfirmed) return;

            try
            {
                await _dbService.DeleteQuestionAsync(question);
                Questions.Remove(question);

                await DisplayAlert("Succes", "De vraag is verwijderd!", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het verwijderen van een vraag uit de database: {ex.Message}");
                await DisplayAlert("Fout", "Er is iets misgegaan bij het verwijderen van de vraag.", "OK");
            }
        }

        private async void UpdateQuestion(Question question)
        {
            if (question == null) return;

            string newQuestionText = await DisplayPromptAsync(
                "Wijzig Vraag",
                "Pas de vraag aan:",
                initialValue: question.PlayerQuestion
            );

            if (string.IsNullOrEmpty(newQuestionText)) return;

            try
            {
                question.PlayerQuestion = newQuestionText;
                await _dbService.UpdateQuestionAsync(question);

                LoadQuestions(); // Herlaad de lijst
                await DisplayAlert("Succes", "De vraag is gewijzigd!", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het wijzigen van een vraag: {ex.Message}");
                await DisplayAlert("Fout", "Er is iets misgegaan bij het wijzigen van de vraag.", "OK");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
