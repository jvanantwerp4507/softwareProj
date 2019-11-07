using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls;

namespace GuitarHelper.Tuner
{
    internal class sound : IMediaPlaybackSource
    {
       public static  MediaPlayer a = new MediaPlayer();
       

        public static async Task playSoundAsync(string soundFile)
        {

            MediaElement mysong = new MediaElement();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Tuner");
            Windows.Storage.StorageFile file = await folder.GetFileAsync(soundFile);
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            mysong.SetSource(stream, file.ContentType);
            mysong.Play();
        }

      
    }
}
