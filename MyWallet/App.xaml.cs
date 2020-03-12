using Acr.UserDialogs;
using MyWallet.Providers;
using MyWallet.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;

namespace MyWallet
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"/{nameof(HomePage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterInstance(new DatabaseProvider("MyWalletDB"));

            // Pages
            containerRegistry.RegisterForNavigation<HomePage>();
            containerRegistry.RegisterForNavigation<AddMoneyPage>();
            containerRegistry.RegisterForNavigation<RemoveMoneyPage>();
            containerRegistry.RegisterForNavigation<DetailsPage>();
            containerRegistry.RegisterForNavigation<UpdateTransactionPage>();            
        }
    }
}