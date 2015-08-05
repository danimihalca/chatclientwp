using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;


using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

using Windows.UI.Core;
using Windows.ApplicationModel.Core;

namespace ChatClientWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page//, IChatClientListener
    {
        Popup popup;
        private RCChatClientController chatClient;
        //ToastNotification toast;
        //ToastNotifier toastNotifier;
        public MainPage()
        {
            this.InitializeComponent();

            //ChatClientRC.AbstractRTChatClientListener asd = new AbstractRTChatClientListener();
            connectButton.IsEnabled = true;
            disconnectButton.IsEnabled = false;

            chatClient = new RCChatClientController();
            //chatClient.addListener(this);
 

        }

        private Popup CreatePopup()
        {
            // text
            TextBlock tb = new TextBlock();
            //tb.Foreground = (Brush)this.Resources["PhoneForegroundBrush"];
            //tb.FontSize = (double)this.Resources["PhoneFontSizeMedium"];
            //tb.Margin = new Thickness(24, 32, 24, 12);
            tb.Text = "Custom toast message";

            // grid wrapper
            Grid grid = new Grid();
            //grid.Background = (Brush)this.Resources["PhoneAccentBrush"];
            grid.Children.Add(tb);
            grid.Width = this.ActualWidth;

            // popup
            Popup popup = new Popup();
            popup.Child = grid;

            return popup;
        }

        // hides popup
        private void HidePopup()
        {
            //SystemTray.BackgroundColor = (Color)this.Resources["PhoneBackgroundColor"];
            this.popup.IsOpen = false;
        }

        // shows popup
        private void ShowPopup()
        {
            //SystemTray.BackgroundColor = (Color)this.Resources["PhoneAccentColor"];

            if (this.popup == null)
            {
                this.popup = this.CreatePopup();
            }

            this.popup.IsOpen = true;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            String message = messageTextBox.Text;

            //chatClient.sendMessage(message);
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            string address = addressTextBox.Text;
            UInt16 port = Convert.ToUInt16(portTextBox.Text);
            //chatClient.connect(address, port);
            connectButton.IsEnabled = false;
            disconnectButton.IsEnabled = true;

            //StandardPopup.IsOpen = true;
            //toastNotifier.Show(toast);
        }

        private void disconnectButton_Click(object sender, RoutedEventArgs e)
        {
            //chatClient.disconnect();
            connectButton.IsEnabled = true;
            disconnectButton.IsEnabled = false;

        }

         public async  void  onConnected()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                messagesTextBox.Text += "Connected\n";
            });

        }
         public async void onDisconnected()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                messagesTextBox.Text += "Disonnected\n";
            });
        }
        public async void onMessage(string message)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {      
                messagesTextBox.Text += message + "\n";
            });
        }

        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }

        // Handles the Click event on the Button on the page and opens the Popup. 
        private void ShowPopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            // open the Popup if it isn't open already 
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
        } 
    }
}
