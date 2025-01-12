using SQLite;
using System.IO;

namespace TruthOrDrink
{
    public static class DatabaseConstants
    {
        private const string DBFileName = "TruthOrDrink.db";

        public const SQLiteOpenFlags DatabaseFlags =
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DBFileName); 
    }
}
