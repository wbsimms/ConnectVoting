using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Navigation;
using ConnectVoting.ViewModel;

namespace ConnectVoting
{
	public sealed partial class MainPage
	{
		public MainViewModel Vm => (MainViewModel)DataContext;

		public MainPage()
		{
			InitializeComponent();

			SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManagerBackRequested;
			Loaded += (s, e) =>
			{
				if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
				{
					ApplicationView.GetForCurrentView().Title = "Connect Voting";
				}
			};
		}

		private void SystemNavigationManagerBackRequested(object sender, BackRequestedEventArgs e)
		{
			if (Frame.CanGoBack)
			{
				e.Handled = true;
				Frame.GoBack();
			}
		}

		protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
			base.OnNavigatingFrom(e);
		}
	}
}
