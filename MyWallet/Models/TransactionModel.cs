using System;
using SQLite;

namespace MyWallet.Models
{
    public class TransactionModel
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Symbol { get; set; }

        public double Amount { get; set; }
    }
}
