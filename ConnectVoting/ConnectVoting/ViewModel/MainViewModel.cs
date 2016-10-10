using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using ConnectVoting.Model;

namespace ConnectVoting.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		public const string WelcomeTitlePropertyName = "WelcomeTitle";
		public const string AvailableElectionsPropertyName = "AvailableElections";
		public const string CreateElectionKey = "CreateElection";

		private readonly IDataService _dataService;
		private readonly INavigationService _navigationService;
		private int _counter;
		private RelayCommand _createElectionCommand;
		private RelayCommand _sendMessageCommand;
		private RelayCommand _showDialogCommand;
		private string _welcomeTitle = string.Empty;
		private List<string> availableElections = new List<string>();

		public RelayCommand SendMessageCommand
		{
			get
			{
				return _sendMessageCommand
					?? (_sendMessageCommand = new RelayCommand(
					() =>
					{
						Messenger.Default.Send(
							new NotificationMessageAction<string>(
								"Testing",
								reply =>
								{
									WelcomeTitle = reply;
								}));
					}));
			}
		}

		public RelayCommand ShowDialogCommand
		{
			get
			{
				return _showDialogCommand
					   ?? (_showDialogCommand = new RelayCommand(
						   async () =>
						   {
							   var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
							   await dialog.ShowMessage("Hello Universal Application", "it works...");
						   }));
			}
		}

		public RelayCommand CreateElectionCommand
		{
			get
			{
				return _createElectionCommand
					   ?? (_createElectionCommand = new RelayCommand(
						   () => _navigationService.NavigateTo(ViewModelLocator.CreateElectionKey),
						   () => true));
			}
		}

		public string WelcomeTitle
		{
			get
			{
				return _welcomeTitle;
			}

			set
			{
				Set(ref _welcomeTitle, value);
			}
		}


		public List<string> AvailableElections
		{
			get { return _dataService.AvailableElections; }
			set { Set(AvailableElectionsPropertyName, ref availableElections, value); }
		}

		public MainViewModel(
			IDataService dataService,
			INavigationService navigationService)
		{
			_dataService = dataService;
			_navigationService = navigationService;
			Initialize();
		}

		private async Task Initialize()
		{
			try
			{
				var item = await _dataService.GetData();
				this.availableElections = _dataService.AvailableElections;
			}
			catch (Exception ex)
			{
				// Report error here
				WelcomeTitle = ex.Message;
			}
		}
	}
}