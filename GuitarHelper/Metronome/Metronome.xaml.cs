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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GuitarHelper.Metronome
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Metronome : Page
    {
        public Metronome()
        {
            this.InitializeComponent();
            DispatcherTimerSetup();
        }

        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 1;
        int timesToTick = 60 * 3;
        int bpm = 60;
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
                Special.Angle += (bpm / 90f) * (span.Milliseconds/1000f) * 60f;
                if ( Special.Angle >= 20 )
                {
                    Special.Angle = 20;
                    right = false;
                }
            }
            else
            {
                Special.Angle -= (bpm / 90f) * (span.Milliseconds/1000f) * 60f;
                if ( Special.Angle <= -20 )
                {
                    Special.Angle = -20;
                    right = true;
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
            bpm++;
            BPM.Text = bpm.ToString();
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            bpm--;
            BPM.Text = bpm.ToString();
        }
    }
}
