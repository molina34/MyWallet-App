using System;
using System.IO;
using MyWallet.Models;
using SQLite;

namespace MyWallet.Providers
{
    public class DatabaseProvider
    {
        public string DatabasePath { get; set; }
        public string DatabaseName { get; set; }
        private SQLiteConnection _dbConnection { get; set; }

        public DatabaseProvider(string dbName)
        {
            DatabaseName = dbName;
            InitDatabase();
        }

        public SQLiteConnection DBConnection => _dbConnection;

        private void InitDatabase()
        {
            try
            {
                DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);
                _dbConnection = new SQLiteConnection(DatabasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InitDatabase error {ex.Message}");
            }
            CreateAllTables();
        }

        public void CreateAllTables()
        {
            //create tables if they don't exist
            try
            {
                CreateTableResult result = _dbConnection.CreateTable<TransactionModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InitDatabase {ex.Message}");
                throw new MissingMemberException("Could not initialize the sqLite database.", ex);
            }
        }
    }
}
