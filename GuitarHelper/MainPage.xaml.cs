using GuitarHelper.musicLib;
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
using GuitarHelper.Classes;

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
    }
}
