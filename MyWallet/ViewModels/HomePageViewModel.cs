using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MyWallet.Controllers;
using MyWallet.Models;
using MyWallet.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace MyWallet.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        protected TransactionController TransactionController { get; }

        public ICommand AddMoneyCommand { get; }
        public ICommand RemoveMoneyCommand { get; }
        public ICommand SeeDetailsCommand { get; }

        private ObservableCollection<BalanceModel> _balances;
        public ObservableCollection<BalanceModel> Balances
        {
            get => _balances;
            set => SetProperty(ref _balances, value);
        }

        public HomePageViewModel(INavigationService navigationService,
                                 IUserDialogs userDialogs,
                                 TransactionController transactionController) : base(navigationService, userDialogs)
        {
            TransactionController = transactionController;

            AddMoneyCommand = new DelegateCommand(async () => await AddMoney());
            RemoveMoneyCommand = new DelegateCommand(async () => await RemoveMoney());
            SeeDetailsCommand = new DelegateCommand(async () => await SeeDetails());
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadBalances();
        }

        private void LoadBalances()
        {            
            Balances = new ObservableCollection<BalanceModel>();
            var transactions = TransactionController.LoadAllTransactions();

            foreach (var currency in transactions.GroupBy(t => t.Symbol))
            {
                var totalAmount = transactions.Where(t => t.Symbol == currency.Key).Sum(t => t.Amount);
                Balances.Add(new BalanceModel() { Symbol = currency.Key, Amount = totalAmount });
            }
        }

        private async Task AddMoney()
        {
            await NavigationService.NavigateAsync(nameof(AddMoneyPage));
        }

        private async Task RemoveMoney()
        {
            await NavigationService.NavigateAsync(nameof(RemoveMoneyPage));
        }

        private async Task SeeDetails()
        {
            await NavigationService.NavigateAsync(nameof(DetailsPage));
        }
    }
}
