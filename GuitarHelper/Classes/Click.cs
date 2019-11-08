using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace GuitarHelper.Classes
{
    public static class Click
    {
        private static MediaPlayer mediaPlayer;

        public static void Initialize()
        {
            if ( mediaPlayer == null )
            {
                mediaPlayer = new MediaPlayer();
                mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Click.mp3"));
            }
        }

        public static void playClick()
        {
            stopClick();
            mediaPlayer.Play();
        }

        public static void stopClick()
        {
            mediaPlayer.Pause();
            mediaPlayer.Position = TimeSpan.Zero;
        }
    }
}
