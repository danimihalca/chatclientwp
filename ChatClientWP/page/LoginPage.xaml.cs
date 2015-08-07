using ChatClientWP.Common;
using ChatClientWP.controller;
using ChatClientWP.Model;
using ChatClientWP.Utils;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ChatClientWP.page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page, IChatClientListener
    {
        private NavigationHelper navigationHelper;

        private IChatClientController m_controller;

        public LoginPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            m_controller = (Application.Current as App).GetController();
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
            if (e.PageState != null)
            {
                if (e.PageState.ContainsKey("userName"))
                {
                    userNameInput.Text = e.PageState["userName"] as String;
                }
                if (e.PageState.ContainsKey("password"))
                {
                    passwordInput.Password = e.PageState["password"] as String;
                }
                if (e.PageState.ContainsKey("rememberMe"))
                {
                    rememberMeCheckbox.IsChecked = e.PageState["rememberMe"] as bool?;
                }
            }
            else
            {
                m_controller.AddListener(this);
            }
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
            if (rememberMeCheckbox.IsChecked == true)
            {
                e.PageState["userName"] = userNameInput.Text;
                e.PageState["password"] = passwordInput.Password;
                e.PageState["rememberMe"] = rememberMeCheckbox.IsChecked;
            }
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
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            String userName = userNameInput.Text;
            String password = passwordInput.Password;

            m_controller.SetServerProperties("192.168.0.3", 9003);
            m_controller.Login(userName, password);
        }

        public void OnConnected()
        {
        }

        public async void OnDisconnected() 
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
                PopupDisplayer.DisplayPopup("Disconnected");
            });
        }

        public async void OnLoginSuccessful()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                Frame.Navigate(typeof(ContactListPage));
            });
        }

        public async void OnLoginFailed(string message)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                PopupDisplayer.DisplayPopup("Login failed");
            });
        }

        public void OnContactOnlineStatusChanged(Contact c)
        {
            Debug.WriteLine("O" + c.Id + ":" + c.IsOnline);
        }

        public async void OnConnectionError()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
                PopupDisplayer.DisplayPopup("Connection error");
            });
        }


        public void OnContactsReceived()
        {
        }


        public void OnMessageReceived(Message m)
        {
        }

        private void input_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
            }
        }
    }
}
