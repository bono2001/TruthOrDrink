using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruthOrDrink.Models;

namespace TruthOrDrink
{
    public class LocalDbService
    {
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<Question>().Wait();
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
