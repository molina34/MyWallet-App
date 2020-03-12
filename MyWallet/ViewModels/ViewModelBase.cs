using System;
using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;

namespace MyWallet.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware
    {
        protected INavigationService NavigationService { get; private set; }
        protected IUserDialogs UserDialogs { get; }

        public ViewModelBase(INavigationService navigationService, IUserDialogs userDialogs)
        {
            NavigationService = navigationService;
            UserDialogs = userDialogs;
        }

        protected void ShowLoading(MaskType maskType = MaskType.Black) => UserDialogs.ShowLoading("Loading...", maskType);
        protected void HideLoading() => UserDialogs.HideLoading();

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }
        public virtual void OnNavigatedTo(INavigationParameters parameters) { }
    }
}
