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

using ChatClientRC;

namespace ChatClientWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ChatClientRtC chatClientRtc;
        public MainPage()
        {
            this.InitializeComponent();

            chatClientRtc = new ChatClientRtC();
            chatClientRtc.initialize();
            chatClientRtc.startService();
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
            string dummy = "dummy";
            chatClientRtc.sendMessage(dummy);
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            //string dummy = "dummy";
            chatClientRtc.connect("192.168.0.3",1234);
        }
    }
}
