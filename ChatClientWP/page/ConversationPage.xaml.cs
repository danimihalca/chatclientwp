﻿using ChatClientWP.Common;
using ChatClientWP.controller;
using ChatClientWP.Model;
using ChatClientWP.Utils;
using System;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
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
    public sealed partial class ConversationPage : Page, IChatClientListener
    {
        private NavigationHelper navigationHelper;
       
        private IChatClientController m_controller;
        private Contact m_contact;
        ObservablePropertyCollection<Message> m_messageCollection;


        public ConversationPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;


            m_controller = (Application.Current as App).GetController();
            m_controller.AddListener(this);
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
            m_contact = e.NavigationParameter as Contact;
            m_contact.UnreadMesssagesCount = 0;

            m_messageCollection = new ObservablePropertyCollection<Message>(m_controller.getMessages(m_contact.Id));
            MessageListView.ItemsSource = m_messageCollection;
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
            m_controller.RemoveListener(this);
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        public void OnConnected()
        {
        }

        public void OnDisconnected()
        {
            IAsyncAction action = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
                m_controller.RemoveListener(this);
                navigationHelper.GoBack();
            });
            action.AsTask().Wait();
        }


        public void OnLoginSuccessful()
        {
        }

        public void OnLoginFailed(string message)
        {
        }

        public async void OnContactOnlineStatusChanged(Contact c)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                PopupDisplayer.DisplayPopup(c.FullName + " is now " + (c.IsOnline ? "online" : "offline"));
            });
        }

        public async void OnConnectionError()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
                navigationHelper.GoBack();
            });
        }

        public void OnContactsReceived()
        {
        }

        public async void OnMessageReceived(Message m)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                if(m.Sender.Equals(m_contact))
                {
                    m_contact.UnreadMesssagesCount = 0;
                    m_messageCollection.Add(m);
                }
                else
                {
                    PopupDisplayer.DisplayPopup(m.Sender.FullName + " send you a message");
                }
            });
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void messageInput_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
            }
        }

        private void SendMessage()
        {
            Message message = new Message();
            message.Sender = m_controller.GetUser();
            message.MessageText = messageInput.Text;
            message.Receiver = m_contact;

            messageInput.Text = "";
            m_controller.SendMessage(message);

            m_messageCollection.Add(message);
        }
    }
}