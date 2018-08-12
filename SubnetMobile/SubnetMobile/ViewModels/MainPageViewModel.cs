using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubnetMobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private DelegateCommand _startCommand;
        public DelegateCommand StartCommand =>
            _startCommand ?? (_startCommand = new DelegateCommand(ExecuteStartCommand));

        public MainPageViewModel(INavigationService navigationService) : base (navigationService)
        {
            _navigationService = navigationService;
        }

        private async void ExecuteStartCommand()
        {
            await _navigationService.NavigateAsync("QuestionsPage");
        }
    }
}
