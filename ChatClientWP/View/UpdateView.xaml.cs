using ChatClientWP.ChatClient.Listener;
using ChatClientWP.ChatClient.Notifier;
using ChatClientWP.Common;
using ChatClientWP.controller;
using ChatClientWP.Model;
using ChatClientWP.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ChatClientWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateView : Page, IUpdateListener
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private User updatedUser;
        IChatClientController m_controller;

        public UpdateView()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            updatedUser = new User();


            m_controller = (Application.Current as App).GetController();
            m_controller.AddUpdateListener(this);

            User user = m_controller.GetUser();
            userNameInput.Text = user.UserName;
            passwordInput.Password = user.Password;
            firstNameInput.Text = user.FirstName;
            lastfNameInput.Text = user.LastName;

        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
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
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            updatedUser.UserName = userNameInput.Text;
            updatedUser.Password = passwordInput.Password;
            updatedUser.FirstName = firstNameInput.Text;
            updatedUser.LastName = lastfNameInput.Text;

            m_controller.UpdateUser(updatedUser);
            updateButton.IsEnabled = false;
        }

        private void input_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
            }
        }

        public void OnDisconnected()
        {
        }

        public async void onRegisterUpdateResponse(ChatClient.Notifier.REGISTER_UPDATE_USER_STATUS status)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
                updateButton.IsEnabled = true;
                PopupDisplayer.DisplayPopup(EnumCodePrettifier.Prettify(status));
                if (status == ChatClient.Notifier.REGISTER_UPDATE_USER_STATUS.USER_OK)
                {
                    m_controller.RemoveUpdateListener(this);
                    m_controller.SetUser(updatedUser);
                    Frame.GoBack();
                }
            });
        }

        public void OnContactsReceived()
        {
        }

        public async void OnContactStateChanged(Contact contact)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                PopupDisplayer.DisplayPopup(contact.FirstName + " is now " + contact.State.ToString());
            });
        }

        public async void OnMessageReceived(Message message)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                PopupDisplayer.DisplayPopup(message.Sender.FirstName + " send you a message");
            });
        }

        public bool OnAddRequest(string userName)
        {
            //if (m_isVisible)
            {
                AddRequestPrompt addRequestPrompt = null;

                IAsyncAction a = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    addRequestPrompt = new AddRequestPrompt(userName);
                    addRequestPrompt.Show();
                });
                a.AsTask().Wait();
                while (addRequestPrompt.IsOpen)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(100));
                }
                return addRequestPrompt.Accepted;
            }
            //return false;
        }

        public void OnAddContactResponse(string userName, ChatClient.Notifier.ADD_REQUEST_STATUS status)
        {
            //if (m_isVisible)
            {
                if (status == ADD_REQUEST_STATUS.ADD_YOURSELF)
                {
                    PopupDisplayer.DisplayPopup(EnumCodePrettifier.Prettify(status));
                }
                else
                {
                    PopupDisplayer.DisplayPopup(userName + " " + EnumCodePrettifier.Prettify(status));
                }
            }
        }

        public async void OnRemovedByContact(Contact contact)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                //if (m_isVisible)
                {
                    PopupDisplayer.DisplayPopup(contact.UserName + " has removed you");
                }
            });
        }
    }
}
