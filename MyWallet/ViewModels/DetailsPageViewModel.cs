using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MyWallet.Controllers;
using MyWallet.Models;
using MyWallet.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MyWallet.ViewModels
{
    public class DetailsPageViewModel : ViewModelBase
    {
        protected TransactionController _transactionController { get; }

        public ICommand GoBackCommand { get; }
        public ICommand EditTransactionCommand { get; }

        private ObservableCollection<TransactionModel> _transactions;
        public ObservableCollection<TransactionModel> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }

        private TransactionModel _transactionSelected;
        public TransactionModel TransactionSelected
        {
            get => _transactionSelected;
            set => SetProperty(ref _transactionSelected, value);
        }
        
        public DetailsPageViewModel(INavigationService navigationService,
                                    IUserDialogs userDialogs,
                                    TransactionController transactionController) : base(navigationService, userDialogs)
        {
            _transactionController = transactionController;

            GoBackCommand = new DelegateCommand(async () => await GoBack());
            EditTransactionCommand = new DelegateCommand(async () => await EditTransaction());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            Transactions = new ObservableCollection<TransactionModel>(_transactionController.LoadAllTransactions());
        }

        private async Task EditTransaction()
        {
            if(TransactionSelected == null)
            {
                return;
            }

            var navParams = new NavigationParameters { { nameof(TransactionModel), TransactionSelected } };
            await NavigationService.NavigateAsync(nameof(UpdateTransactionPage), navParams);
        }

        private async Task GoBack()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
