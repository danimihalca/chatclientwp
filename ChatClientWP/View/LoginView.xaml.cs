using ChatClientWP.ChatClient.Listener;
using ChatClientWP.ChatClient.Notifier;
using ChatClientWP.Common;
using ChatClientWP.controller;
using ChatClientWP.Model;
using ChatClientWP.Utils;
using Coding4Fun.Toolkit.Controls;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ChatClientWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page, ILoginListener
    {
        private NavigationHelper navigationHelper;

        private IChatClientController m_controller;

        private string m_userName;
        private string m_password;
        private USER_STATE m_state;

        private bool m_performLogin;
        private bool m_isVisible;
        private RelayCommand GoBackCommand;

        public LoginView()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.NavigationCacheMode = NavigationCacheMode.Required;
            GoBackCommand = new RelayCommand(GoBackAction);
            this.navigationHelper.GoBackCommand = GoBackCommand;

            m_controller = (Application.Current as App).GetController();
            m_controller.AddLoginListener(this);

            m_performLogin = false;
            m_isVisible = true;
        }
        private void GoBackAction()
        {
            App.Current.Exit();
        }


        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            m_isVisible = true;
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            m_isVisible = false;
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            m_userName = userNameInput.Text;
            m_password = passwordInput.Password;
            loginButton.IsEnabled = false;
            m_state = ((bool) invisibleBox.IsChecked)? USER_STATE.INVISIBLE : USER_STATE.ONLINE;
            m_controller.Login(m_userName,m_password,m_state);
        }

        public async void OnDisconnected() 
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
                loginButton.IsEnabled = true;
                PopupDisplayer.DisplayPopup("Disconnected");
            });
        }

        public async void OnLoginSuccessful(UserDetails userDetails)
        {
            User user = new User();
            user.UserName = m_userName;
            user.Password = m_password;
            user.Details = userDetails;
            m_controller.SetUser(user);
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                loginButton.IsEnabled = true;
                Frame.Navigate(typeof(ContactListView));
            });
            m_controller.RequestContacts();
        }

        public async void OnLoginFailed(AUTHENTICATION_STATUS reason)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                loginButton.IsEnabled = true;
                PopupDisplayer.DisplayPopup(EnumCodePrettifier.Prettify(reason));
            });
        }


        public async void OnConnectionError()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
                loginButton.IsEnabled = true;
                PopupDisplayer.DisplayPopup("Connection error");
            });
        }

        private void input_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
            }
        }

        private async void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            var c = new SettingsDialog();
            await c.ShowAsync();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterView));

        }
    }
}
