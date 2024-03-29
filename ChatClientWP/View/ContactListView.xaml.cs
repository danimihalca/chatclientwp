﻿using ChatClientWP.ChatClient.Listener;
using ChatClientWP.ChatClient.Notifier;
using ChatClientWP.Common;
using ChatClientWP.controller;
using ChatClientWP.Model;
using ChatClientWP.Utils;
using Coding4Fun.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ChatClientWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContactListView : Page, IRuntimeListener
    {
        private NavigationHelper navigationHelper;

        private IChatClientController m_controller;
        ObservablePropertyCollection<Contact> m_contactCollection;

        private bool m_isVisible;

        private RelayCommand GoBackCommand;
        private bool goBackPressed;

        public ContactListView()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            GoBackCommand = new RelayCommand(GoBackAction);
            goBackPressed = false;
            this.navigationHelper.GoBackCommand = GoBackCommand;

            m_controller = (Application.Current as App).GetController();
            m_controller.AddRuntimeListener(this);
            //m_controller.RequestContacts();
  

            m_isVisible = true;
        }

        private void GoBackAction()
        {
            if (!goBackPressed)
            {
                goBackPressed = true;
                PopupDisplayer.DisplayPopup("Press again to log out");
            }
            else
            {
                m_controller.RemoveRuntimeListener(this);
                m_controller.Disconnect();
                this.NavigationCacheMode = NavigationCacheMode.Disabled;
                //navigationHelper.GoBack();
                Frame.GoBack();
            }
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
            Debug.WriteLine("CL:NAVIGATEDFROM");
            m_isVisible = true;
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Debug.WriteLine("CL:NAVIGATEDTO");
            m_isVisible = false;
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


        public void OnDisconnected()
        {
            m_controller.RemoveRuntimeListener(this);
            IAsyncAction action = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                this.NavigationCacheMode = NavigationCacheMode.Disabled; 
                navigationHelper.GoBack();
            });
            action.AsTask().Wait();
        }


        public async void OnContactsReceived()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                IList<Contact> contacts = m_controller.GetContacts();
                m_contactCollection = new ObservablePropertyCollection<Contact>(contacts);
                ContactsView.ItemsSource = m_contactCollection;
            });
        }

        private void ContactsView_ItemClick(object sender, ItemClickEventArgs e)
        {
        }


        public void OnMessageReceived(Message m)
        {
        }


        public void OnContactStateChanged(Contact contact)
        {
        }

        private void ContactsView_Holding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == Windows.UI.Input.HoldingState.Started)
            {
                Debug.WriteLine("HOLDING:" + ((sender as ListViewItem).DataContext as Contact).FirstName);
                MenuFlyout mf = (Application.Current.Resources["FlyoutBase1"] as MenuFlyout);
                MenuFlyoutItem removeContactItem = mf.Items[0] as MenuFlyoutItem;
                removeContactItem.Click += RemoveContactFlyoutItem_Click;
                mf.ShowAt((FrameworkElement)sender);

            }
        }


        private void RemoveContactFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(((e.OriginalSource as MenuFlyoutItem).DataContext as Contact).FirstName);
            Contact contact = (e.OriginalSource as MenuFlyoutItem).DataContext as Contact;
            m_controller.RemoveContact(contact, true);
            m_contactCollection.Remove(contact);
            (e.OriginalSource as MenuFlyoutItem).Click -= RemoveContactFlyoutItem_Click;
        }

        private void ListViewItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ConversationView), (sender as ListViewItem).DataContext as Contact);
        }


        public bool OnAddRequest(string userName)
        {
            if (m_isVisible)
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
            return false;
        }


        private void AddContact_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                m_controller.AddContact(e.Result);
            }
        }

        public async void OnAddContactResponse(string userName, ADD_REQUEST_STATUS status)
        {
            if (m_isVisible)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    if (status == ADD_REQUEST_STATUS.ADD_YOURSELF)
                    {
                        PopupDisplayer.DisplayPopup(EnumCodePrettifier.Prettify(status));

                    }
                    else
                    {
                        PopupDisplayer.DisplayPopup(userName+" "+EnumCodePrettifier.Prettify(status));
                    }
                });
            }
        }

        public async void OnRemovedByContact(Contact contact)
        {

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                if (m_isVisible)
                {
                   PopupDisplayer.DisplayPopup(contact.UserName + " has removed you");
                }
                m_contactCollection.Remove(contact);
            });
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            InputPrompt input = new InputPrompt();
            input.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(AddContact_Completed);
            input.Title = "Add contact";
            input.Message = "Please enter the username";
            input.IsCancelVisible = true;
            input.Show();
        }

        private void r_Checked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine((e.OriginalSource as RadioButton).Content);
            USER_STATE newState = USER_STATE.ONLINE;
            switch ((e.OriginalSource as RadioButton).Content as string)
            {
                case "ONLINE":
                    {
                        newState = USER_STATE.ONLINE;
                        break;
                    }
                case "IDLE":
                    {
                        newState = USER_STATE.IDLE;
                        break;
                    }
                    case "BUSY":
                        {
                            newState = USER_STATE.BUSY;
                            break;
                        }
                case "INVISIBLE":
                        {
                            newState = USER_STATE.INVISIBLE;
                            break;
                        }
            }
            m_controller.ChangeState(newState);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateView));
        }

        private void ChangeStateButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyout menu = ChangeStateButton.Flyout as MenuFlyout;
            for (int i = 0; i < menu.Items.Count; ++i )
            {
                menu.Items[i].Background = new SolidColorBrush(Colors.White);
            }
            USER_STATE state = m_controller.GetState();
            switch (state)
            {
                case USER_STATE.ONLINE:
                    menu.Items[0].Background = new SolidColorBrush(Colors.Gray);
                    break;
                case USER_STATE.IDLE:
                    menu.Items[1].Background = new SolidColorBrush(Colors.Gray);
                    break;
                case USER_STATE.BUSY:
                    menu.Items[2].Background = new SolidColorBrush(Colors.Gray);
                    break;
                case USER_STATE.INVISIBLE:
                    menu.Items[3].Background = new SolidColorBrush(Colors.Gray);
                    break;
                default:
                    break;
            }
        }

        private void ToggleMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = e.OriginalSource as MenuFlyoutItem;
            MenuFlyout menu = ChangeStateButton.Flyout as MenuFlyout;
            int index = menu.Items.IndexOf(item);
            switch (index)
            {
                case 0:
                    USER_STATE state = USER_STATE.ONLINE;
                    m_controller.ChangeState(state);
                    break;
                case 1:
                    state = USER_STATE.IDLE;
                    m_controller.ChangeState(state);
                    break;
                case 2:
                    state = USER_STATE.BUSY;
                    m_controller.ChangeState(state);
                    break;
                case 3:
                    state = USER_STATE.INVISIBLE;
                    m_controller.ChangeState(state);
                    break;
                default:
                    break;
            }
        }
    }
}
