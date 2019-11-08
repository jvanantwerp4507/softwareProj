using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.MediaProperties;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using GuitarHelper.Classes;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GuitarHelper.Metronome
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Metronome : Page
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        public Metronome()
        {
            this.InitializeComponent();
            
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Tick.mp3"));
            DispatcherTimerSetup();
            BPM.Text = Classes.MetronomeHelper.bpm.ToString();
        }

        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 1;
        int timesToTick = 60 * 3;
        bool right = true;

        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan();
            //IsEnabled defaults to false
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            dispatcherTimer.Start();
            //IsEnabled should now be true after calling start
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            //Time since last tick should be very very close to Interval
            timesTicked++;
            if ( right )
            {
                Special.Angle += (Classes.MetronomeHelper.bpm / 90f) * (span.Milliseconds/1000f) * 60f;
                if ( Special.Angle >= 20 )
                {
                    Special.Angle = 20;
                    right = false;
                    try
                    {
                        mediaPlayer.Play();
                    }
                    catch ( Exception exception )
                    {
                        //Console.WriteLine(exception);
                    }
                }
            }
            else
            {
                Special.Angle -= (Classes.MetronomeHelper.bpm / 90f) * (span.Milliseconds/1000f) * 60f;
                if ( Special.Angle <= -20 )
                {
                    Special.Angle = -20;
                    right = true;
                    if ( mediaPlayer != null )
                    {
                        try
                        {
                            mediaPlayer.Play();
                        }
                        catch ( Exception exception )
                        {
                            //Console.WriteLine(exception);
                        }
                    }
                }
            }
           /* if (timesTicked > timesToTick)
            {
                stopTime = time;
                dispatcherTimer.Stop();
                //IsEnabled should now be false after calling stop
                span = stopTime - startTime;
            }*/
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Click.playClick();
            Classes.MetronomeHelper.bpm++;
            BPM.Text = Classes.MetronomeHelper.bpm.ToString();
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            Click.playClick();
            Classes.MetronomeHelper.bpm--;
            BPM.Text = Classes.MetronomeHelper.bpm.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click.playClick();
            mediaPlayer.Pause();
            this.Frame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo());
            mediaPlayer.Dispose();
        }
    }
}
