using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SubnetMobile.Helpers;
using SubnetMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms.Internals;

namespace SubnetMobile.ViewModels
{
    public class ResultsPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPageDialogService _dialogService;

        private bool _hasResults = false;
        private string _subnetMask;
        private string _prefixLength;
        private ObservableCollection<IpAddress> _subnetworkAddresses = new ObservableCollection<IpAddress>();
        private ObservableCollection<string> _subnetHostAddresses = new ObservableCollection<string>();
        private ObservableCollection<IpAddress> _subnetBroadcastAddresses = new ObservableCollection<IpAddress>();

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
        public string SubnetMask
        {
            get { return _subnetMask; }
            set { SetProperty(ref _subnetMask, value); }
        }
        public string PrefixLength
        {
            get { return _prefixLength; }
            set { SetProperty(ref _prefixLength, value); }
        }
        public ObservableCollection<IpAddress> SubnetworkAddresses
        {
            get { return _subnetworkAddresses; }
            set { SetProperty(ref _subnetworkAddresses, value); }
        }
        public ObservableCollection<string> SubnetHostAddresses
        {
            get { return _subnetHostAddresses; }
            set { SetProperty(ref _subnetHostAddresses, value); }
        }
        public ObservableCollection<IpAddress> SubnetBroadcastAddresses
        {
            get { return _subnetBroadcastAddresses; }
            set { SetProperty(ref _subnetBroadcastAddresses, value); }
        }

        private DelegateCommand mainBtnCommand;
        public DelegateCommand MainBtnCommand =>
            mainBtnCommand ?? (mainBtnCommand = new DelegateCommand(ExecuteMainBtnCommand));

        public ResultsPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
        }

        private async void ExecuteMainBtnCommand()
        {
            _eventAggregator.GetEvent<SubnetQueryEvent>().Subscribe(OnSubnetQueryChanged);

            await _navigationService.NavigateAsync("QuestionsPage");
        }

        private void OnSubnetQueryChanged(SubnetQuery subnetQuery)
        {
            _hasResults = true;
            RaisePropertyChanged("BtnText");

            SubnetworkAddresses.Clear();
            SubnetHostAddresses.Clear();
            SubnetBroadcastAddresses.Clear();

            _eventAggregator.GetEvent<SubnetQueryEvent>().Unsubscribe(OnSubnetQueryChanged);

            CalculateSubnets(subnetQuery);
        }

        private void CalculateSubnets(SubnetQuery subnetQuery)
        {
            // Yeah, this needs some refactoring.
            // TASK: REFACTOR THIS

            int bits = Convert.ToString(subnetQuery.NumberOfSubnets, 2).Length;
            int interval = GetInterval(bits);
            IpAddress startIp = subnetQuery.StartingIpAddress.MainIp;
            IpAddress snAdd = startIp;
            IpAddress sbAdd = startIp;

            PrefixLength = $"{subnetQuery.StartingIpAddress.PrefixLength + bits}";
            SubnetMask = subnetQuery.StartingIpAddress.SubnetMask.DisplayName;

            for (int i = 0; i < subnetQuery.NumberOfSubnets; i++)
            {
                SubnetworkAddresses.Add(snAdd);
                snAdd.FourthOctet += 1;
                SubnetHostAddresses.Add($"{snAdd.DisplayName} - ");
                snAdd.FourthOctet -= 1;
                snAdd.FourthOctet += interval;
            }

            sbAdd.FourthOctet = interval - 1;

            for (int i = 0; i < subnetQuery.NumberOfSubnets; i++)
            {
                SubnetBroadcastAddresses.Add(sbAdd);

                sbAdd.FourthOctet -= 1;
                SubnetHostAddresses[i] += $"{sbAdd.DisplayName}";
                sbAdd.FourthOctet += 1;

                sbAdd.FourthOctet += interval;
            }
        }

        private int GetInterval(int bits)
        {
            int interval = 0;

            switch (bits)
            {
                case 1:
                    interval = 128;
                    break;

                case 2:
                    interval = 64;
                    break;

                case 3:
                    interval = 32;
                    break;

                case 4:
                    interval = 16;
                    break;

                case 5:
                    interval = 8;
                    break;

                case 6:
                    interval = 4;
                    break;

                case 7:
                    interval = 2;
                    break;

                case 8:
                    interval = 1;
                    break;

                default:
                    interval = 0;
                    break;
            }

            return interval;
        }
    }
}