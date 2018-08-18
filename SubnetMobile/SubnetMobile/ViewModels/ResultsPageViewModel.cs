using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SubnetMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SubnetMobile.ViewModels
{
    public class ResultsPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        private bool _hasResults = false;
        private string _btnText;

        public string BtnText
        {
            get { return _btnText; }
            set { SetProperty(ref _btnText, value); }
        }

        private DelegateCommand mainBtnCommand;
        public DelegateCommand MainBtnCommand =>
            mainBtnCommand ?? (mainBtnCommand = new DelegateCommand(ExecuteMainBtnCommand));

        public ResultsPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            if (_hasResults)
            {
                BtnText = "AGAIN";
            }
            else
            {
                BtnText = "No IP yet, let's start!";
            }
        }

        private async void ExecuteMainBtnCommand()
        {
            await _navigationService.NavigateAsync("QuestionsPage");
        }
    }
}