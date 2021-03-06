﻿using System;
using System.Collections.ObjectModel;
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
    public class RemoveMoneyPageViewModel : ViewModelBase
    {
        protected TransactionController TransactionController { get; }
        protected CurrencyController CurrencyController { get; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private ObservableCollection<CurrencyModel> _currencies;
        public ObservableCollection<CurrencyModel> Currencies
        {
            get => _currencies;
            set => SetProperty(ref _currencies, value);
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

        public RemoveMoneyPageViewModel(INavigationService navigationService,
                                     IUserDialogs userDialogs,
                                     TransactionController transactionController,
                                     CurrencyController currencyController) : base(navigationService, userDialogs)
        {
            TransactionController = transactionController;
            CurrencyController = currencyController;

            SaveCommand = new DelegateCommand(async () => await Save());
            CancelCommand = new DelegateCommand(async () => await Cancel());
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            ShowLoading();
            Currencies = new ObservableCollection<CurrencyModel>(await CurrencyController.LoadCurrenciesAsync());
            HideLoading();
        }

        private async Task Save()
        {
            if (Currency == null)
            {
                await UserDialogs.AlertAsync("Please select one Currency");
                return;
            }
            if (Amount == 0)
            {
                await UserDialogs.AlertAsync("Please fill the amount field");
                return;
            }

            var transaction = new TransactionModel()
            {
                Date = DateTime.Now,
                Symbol = Currency.Symbol,
                Amount = -Amount
            };

            var result = TransactionController.AddTransaction(transaction);
            await UserDialogs.AlertAsync("Transaction successfully registered!");
            await NavigationService.GoBackAsync();
        }

        private async Task Cancel()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
