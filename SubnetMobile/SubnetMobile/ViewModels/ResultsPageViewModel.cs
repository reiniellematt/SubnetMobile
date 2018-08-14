using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using SubnetMobile.Events;
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

        public ResultsPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<IpResultsEvent>().Subscribe(IpResultsEventChanged);
        }

        private void IpResultsEventChanged(IpAddress message)
        {
            throw new NotImplementedException();
        }
    }
}