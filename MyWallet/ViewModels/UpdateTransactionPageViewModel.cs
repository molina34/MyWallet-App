using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MyWallet.Controllers;
using MyWallet.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MyWallet.ViewModels
{
    public class UpdateTransactionPageViewModel : ViewModelBase
    {
        protected TransactionController _transactionController { get; }
        protected CurrencyController _currencyController { get; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }

        private ObservableCollection<CurrencyModel> _currencies;
        public ObservableCollection<CurrencyModel> Currencies
        {
            get => _currencies;
            set => SetProperty(ref _currencies, value);
        }

        private TransactionModel _transactionModel;
        public TransactionModel TransactionModel
        {
            get => _transactionModel;
            set => SetProperty(ref _transactionModel, value);
        }

        private double _amount;
        public double Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        private CurrencyModel _currency;
        public CurrencyModel Currency
        {
            get => _currency;
            set => SetProperty(ref _currency, value);
        }

        public UpdateTransactionPageViewModel(INavigationService navigationService,
                                     IUserDialogs userDialogs,
                                     TransactionController transactionController,
                                     CurrencyController currencyController) : base(navigationService, userDialogs)
        {
            _transactionController = transactionController;
            _currencyController = currencyController;

            SaveCommand = new DelegateCommand(async () => await Save());
            CancelCommand = new DelegateCommand(async () => await Cancel());
            DeleteCommand = new DelegateCommand(async () => await Delete());
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters is null ||
                !parameters.TryGetValue<TransactionModel>(nameof(TransactionModel), out var transactionModel))
            {
                return;
            }

            ShowLoading();
            Currencies = new ObservableCollection<CurrencyModel>(await _currencyController.LoadCurrenciesAsync());
            TransactionModel = transactionModel;

            Amount = TransactionModel.Amount;
            Currency = Currencies.FirstOrDefault(c => c.Symbol == transactionModel.Symbol);
            HideLoading();
        }

        private async Task Save()
        {
            if (Currency == null)
            {
                Console.WriteLine("Please select one Currency");
                return;
            }
            if (Amount == 0)
            {
                Console.WriteLine("Please fill the amount field");
                return;
            }

            TransactionModel.Symbol = Currency.Symbol;
            TransactionModel.Amount = Amount;

            var result = _transactionController.UpdateTransaction(TransactionModel);
            await UserDialogs.AlertAsync("Transaction successfully updated!");
            await NavigationService.GoBackAsync();
        }

        private async Task Delete()
        {
            if (TransactionModel == null)
            {
                await UserDialogs.AlertAsync("Transaction not detected!");
                return;
            }

            _transactionController.RemoveTransaction(TransactionModel);
            await UserDialogs.AlertAsync("Transaction successfully removed!");
            await NavigationService.GoBackAsync();
        }

        private async Task Cancel()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
