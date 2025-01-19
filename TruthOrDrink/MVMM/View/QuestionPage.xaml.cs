using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TruthOrDrink.Models;
using TruthOrDrink.MVMM.Models;
using TruthOrDrink.Services;

namespace TruthOrDrink
{
    public partial class QuestionPage : ContentPage
    {
        private readonly QuestionRepository _dbService;
        private readonly QuotesService _quotesService;

        public ObservableCollection<Question> Questions { get; } = new();

        public ICommand DeleteQuestionCommand { get; }
        public ICommand UpdateQuestionCommand { get; }

        public QuestionPage()
        {
            InitializeComponent();

            _dbService = App.Services.GetService<QuestionRepository>();
            _quotesService = new QuotesService(); // Initialiseer QuotesService

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

                var playerQuestions = storedQuestions
                    .Where(q => !string.IsNullOrEmpty(q.PlayerQuestion))
                    .ToList();

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
                Console.WriteLine($"Fout bij het laden van vragen: {ex.Message}");
            }
        }

        private async void LoadCategories()
        {
            try
            {
                var categories = await _dbService.GetCategoriesAsync();

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

                QuestionEntry.Text = string.Empty;
                DifficultyPicker.SelectedItem = null;
                CategoryPicker.SelectedItem = null;

                await DisplayAlert("Succes", "De vraag is toegevoegd!", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het toevoegen van een vraag: {ex.Message}");
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij verwijderen: {ex.Message}");
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
                LoadQuestions();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij wijzigen: {ex.Message}");
            }
        }

        private async void FetchRandomQuote(object sender, EventArgs e)
        {
            Console.WriteLine("FetchRandomQuote werd aangeroepen.");

            try
            {
                Console.WriteLine("Probeer een quote op te halen via QuotesService...");
                string quote = await _quotesService.FetchRandomQuoteAsync();
                Console.WriteLine($"Quote succesvol opgehaald: {quote}");

                if (string.IsNullOrEmpty(quote))
                {
                    Console.WriteLine("De opgehaalde quote is leeg of null!");
                    await DisplayAlert("Fout", "De opgehaalde quote is leeg.", "OK");
                    return;
                }

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (QuestionEntry == null)
                    {
                        Console.WriteLine("QuestionEntry is null! Controleer je XAML.");
                        return;
                    }

                    QuestionEntry.Text = quote;
                    Console.WriteLine("Quote succesvol in QuestionEntry geplaatst.");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout opgetreden: {ex.Message}");
                await DisplayAlert("Fout", ex.Message, "OK");
            }
        }







        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
