using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using SubnetMobile.Helpers;
using SubnetMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SubnetMobile.ViewModels
{
    public class QuestionsPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        private string _numberOfSubnetsEntry;
        private string _firstOctetEntry;
        private string _secondOctetEntry;
        private string _thirdOctetEntry;
        private string _fourthOctetEntry;

        private DelegateCommand _nextCommand;

        public string NumberOfSubnetsEntry
        {
            get { return _numberOfSubnetsEntry; }
            set { SetProperty(ref _numberOfSubnetsEntry, value); }
        }
        public string FirstOctetEntry
        {
            get { return _firstOctetEntry; }
            set { SetProperty(ref _firstOctetEntry, value); }
        }
        public string SecondOctetEntry
        {
            get { return _secondOctetEntry; }
            set { SetProperty(ref _secondOctetEntry, value); }
        }
        public string ThirdOctetEntry
        {
            get { return _thirdOctetEntry; }
            set { SetProperty(ref _thirdOctetEntry, value); }
        }
        public string FourthOctetEntry
        {
            get { return _fourthOctetEntry; }
            set { SetProperty(ref _fourthOctetEntry, value); }
        }

        public DelegateCommand NextCommand => _nextCommand ?? (_nextCommand = new DelegateCommand(ExecuteNextCommand));

        public QuestionsPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
        }

        private async void ExecuteNextCommand()
        {
            IpAddress ip = new IpAddress
            {
                FirstOctet = int.Parse(FirstOctetEntry),
                SecondOctet = int.Parse(SecondOctetEntry),
                ThirdOctet = int.Parse(ThirdOctetEntry),
                FourthOctet = int.Parse(FourthOctetEntry)
            };

            IpQuery ipQuery = new IpQuery
            {
                NumberOfSubnets = int.Parse(NumberOfSubnetsEntry),
                StartingIpAddress = ip
            };

            _eventAggregator.GetEvent<IpEntryEvent>().Publish(ipQuery);

            await _navigationService.GoBackAsync();
        }
    }
}