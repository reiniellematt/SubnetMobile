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
    public class QuestionsPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPageDialogService _dialogService;

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

        public QuestionsPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
        }

        private async void ExecuteNextCommand()
        {
            string validateForm = ValidateForm();

            if (!string.IsNullOrWhiteSpace(validateForm))
            {
                await _dialogService.DisplayAlertAsync("Form error!", validateForm, "OK");
                return;
            }

            IpAddress ip = new IpAddress(int.Parse(FirstOctetEntry),
                                         int.Parse(SecondOctetEntry),
                                         int.Parse(ThirdOctetEntry),
                                         int.Parse(FourthOctetEntry));

            SubnetQuery subnetQuery = new SubnetQuery { NumberOfSubnets = int.Parse(NumberOfSubnetsEntry), StartingIpAddress = new CompleteIpInfo(ip) };

            _eventAggregator.GetEvent<SubnetQueryEvent>().Publish(subnetQuery);

            await _navigationService.GoBackAsync();
        }

        private string ValidateForm()
        {
            bool firstOctetValid = int.TryParse(FirstOctetEntry, out int firstOctet);
            bool secondOctetValid = int.TryParse(SecondOctetEntry, out int secondOctet);
            bool thirdOctetValid = int.TryParse(ThirdOctetEntry, out int thirdOctet);
            bool fourthOctetValid = int.TryParse(FourthOctetEntry, out int fourthOctet);

            if (!int.TryParse(NumberOfSubnetsEntry, out int numOfSubnets))
            {
                return "Enter a valid number of subnets.";
            }

            if (!firstOctetValid || !secondOctetValid || !thirdOctetValid || !fourthOctetValid)
            {
                return "Enter a valid IP address.";
            }

            if (firstOctet < 0 || firstOctet > 255)
            {
                return "The value of the first octet is not valid.";
            }

            if (secondOctet < 0 || secondOctet > 255)
            {
                return "The value of the second octet is not valid.";
            }

            if (thirdOctet < 0 || thirdOctet > 255)
            {
                return "The value of the third octet is not valid.";
            }

            if (fourthOctet < 0 || fourthOctet > 255)
            {
                return "The value of the fourth octet is not valid.";
            }

            return string.Empty;
        }
    }
}