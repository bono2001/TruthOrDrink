using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruthOrDrink.Models;
using TruthOrDrink.MVMM.Models;

namespace TruthOrDrink
{
    public class QuestionRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public QuestionRepository(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);

            // Maak de benodigde tabellen
            _connection.CreateTableAsync<Category>().Wait();
            _connection.CreateTableAsync<Question>().Wait();

            // Voeg standaarddata toe
            SeedDefaultData().Wait();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _connection.Table<Category>().ToListAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            if (category == null) return;

            // Controleer of de categorie al bestaat
            var existingCategory = await _connection.Table<Category>()
                .FirstOrDefaultAsync(c => c.Name == category.Name);

            if (existingCategory == null)
            {
                await _connection.InsertAsync(category);
            }
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _connection.Table<Question>().ToListAsync();
        }

        public async Task AddQuestionAsync(Question question)
        {
            if (question == null) return;
            await _connection.InsertAsync(question);
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            if (question == null) return;
            await _connection.UpdateAsync(question);
        }

        public async Task DeleteQuestionAsync(Question question)
        {
            if (question == null) return;
            await _connection.DeleteAsync(question);
        }

        private async Task SeedDefaultData()
        {
            // Voeg standaardcategorieën toe
            var existingCategories = await GetCategoriesAsync();
            if (!existingCategories.Any())
            {
                var defaultCategories = new List<Category>
                {
                    new Category { Name = "Vrienden" },
                    new Category { Name = "Grappig" },
                    new Category { Name = "Spicy Koppel" },
                    new Category { Name = "Serieus" }
                };

                await _connection.InsertAllAsync(defaultCategories);
            }

            // Voeg standaardvragen toe
            var existingQuestions = await GetQuestionsAsync();
            if (!existingQuestions.Any())
            {
                var categories = await GetCategoriesAsync();
                var defaultQuestions = new List<Question>
                {
                    new Question
                    {
                        DefaultQuestion = "Wat is je favoriete herinnering met een vriend?",
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Vrienden")?.Id ?? 0,
                        Difficulty = "Makkelijk"
                    },
                    new Question
                    {
                        DefaultQuestion = "Wat is het grappigste dat je ooit hebt gedaan?",
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Grappig")?.Id ?? 0,
                        Difficulty = "Gemiddeld"
                    },
                    new Question
                    {
                        DefaultQuestion = "Wat is de meest romantische daad die je hebt gedaan?",
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Spicy Koppel")?.Id ?? 0,
                        Difficulty = "Moeilijk"
                    }
                };

                await _connection.InsertAllAsync(defaultQuestions);
            }
        }
    }
}
