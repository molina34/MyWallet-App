using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyWallet.Models;
using MyWallet.Providers;
using Xamarin.Forms;

namespace MyWallet.Controllers
{
    public class TransactionController
    {
        private readonly DatabaseProvider _dbProvider;

        public TransactionController(DatabaseProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }


        public bool AddTransaction(TransactionModel transaction)
        {
            if (transaction == null)
            {
                return false;
            }

            bool success = false;
            try
            {
                _dbProvider.DBConnection.Insert(transaction);
                success = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"AddTransaction error: {exception.Message}");
            }

            return success;
        }


        public bool UpdateTransaction(TransactionModel transaction)
        {
            if (transaction == null)
            {
                return false;
            }

            bool success = false;
            try
            {
                _dbProvider.DBConnection.Update(transaction);
                success = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"UpdateTransaction error: {exception.Message}");
            }

            return success;
        }


        public bool RemoveTransaction(TransactionModel transaction)
        {
            if (transaction == null)
            {
                return false;
            }

            bool success = false;
            try
            {
                _dbProvider.DBConnection.Delete(transaction);
                success = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"AddTransaction error: {exception.Message}");
            }

            return success;
        }

        public List<TransactionModel> LoadAllTransactions()
        {
            var transactions = new List<TransactionModel>();
            try
            {
                transactions = _dbProvider.DBConnection.Table<TransactionModel>().OrderBy(t => t.Date).ToList();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"LoadAllTransactionsAsync error: {exception.Message}");
            }

            return transactions;
        }
    }
}

