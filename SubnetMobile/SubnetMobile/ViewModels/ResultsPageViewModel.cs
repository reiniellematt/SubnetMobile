using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SubnetMobile.Helpers;
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

        public string BtnText
        {
            get
            {
                if (_hasResults)
                {
                    return "AGAIN";
                }
                else
                {
                    return "No IP yet, let's start!";
                }
            }
        }

        private DelegateCommand mainBtnCommand;
        public DelegateCommand MainBtnCommand =>
            mainBtnCommand ?? (mainBtnCommand = new DelegateCommand(ExecuteMainBtnCommand));

        public ResultsPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            

            _eventAggregator.GetEvent<SubnetQueryEvent>().Subscribe(OnSubnetQueryChanged);
        }

        private async void ExecuteMainBtnCommand()
        {
            await _navigationService.NavigateAsync("QuestionsPage");
        }

        private void OnSubnetQueryChanged(SubnetQuery subnetQuery)
        {
            _hasResults = true;
            RaisePropertyChanged("BtnText");
        }
    }
}