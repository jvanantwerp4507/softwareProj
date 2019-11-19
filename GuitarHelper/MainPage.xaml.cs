using GuitarHelper.musicLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GuitarHelper.Classes;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GuitarHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Click.Initialize();
            //Notification();    NOTIFICATION TESTING -- Love oscar <3
        }

        private void Tuner_Click(object sender, RoutedEventArgs e)
        {
            Click.playClick();
            this.Frame.Navigate(typeof(Tuner.BlankPage1));
        }

        private void Metronome_Click(object sender, RoutedEventArgs e)
        {
            Click.playClick();
            this.Frame.Navigate(typeof(Metronome.Metronome));
        }

        private void library_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(musicLib.SheetMusic));
        }

        private void Notification()
        {
            ToastBindingGeneric bindingGeneric = new ToastBindingGeneric()
            {
                Children =
                {
                    new AdaptiveText()
                    {
                        Text = "Hello there!'",
                        HintMaxLines = 1
                    },

                    new AdaptiveText()
                    {
                        Text = ";)"
                    },

                    new AdaptiveText()
                    {
                        Text = "Lol"
                    }
                }
            };

            ToastContent content = new ToastContent()
            {
                Launch = "NotificationClicked",
 
                Visual = new ToastVisual()
                {
                    BindingGeneric = bindingGeneric
                },
 
                Actions = new ToastActionsCustom() {},
 
                Audio = new ToastAudio() { Src = new Uri("ms-appx:///Assets/youGotmail.mp3") }
            };

            var toast = new ToastNotification(content.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
