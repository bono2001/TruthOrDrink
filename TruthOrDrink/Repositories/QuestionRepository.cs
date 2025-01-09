using SQLite;
using System.Collections.Generic;
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
            _connection.CreateTableAsync<Category>().Wait();
            _connection.CreateTableAsync<Question>().Wait();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _connection.Table<Category>().ToListAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _connection.InsertAsync(category);
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _connection.Table<Question>().ToListAsync();
        }

        public async Task AddQuestionAsync(Question question)
        {
            await _connection.InsertAsync(question);
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            await _connection.UpdateAsync(question);
        }

        public async Task DeleteQuestionAsync(Question question)
        {
            await _connection.DeleteAsync(question);
        }
    }
}
