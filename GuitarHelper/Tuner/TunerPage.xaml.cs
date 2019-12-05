using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Audio;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.Media.Playback;
using Windows.Media.Render;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GuitarHelper.Tuner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void play_Audio_Click( object sender, RoutedEventArgs e )
        {
            _ = Tuner.sound.playSoundAsync("bueller.wav");

        }

        private async void Button_Click_1( object sender, RoutedEventArgs e )
        {


            var file = await this.PickFileAsync();

            if ( file != null )
            {
                var result = await AudioGraph.CreateAsync(
                    new AudioGraphSettings(AudioRenderCategory.Other));

                if ( result.Status == AudioGraphCreationStatus.Success )
                {
                    this.graph = result.Graph;

                    var microphone = await DeviceInformation.CreateFromIdAsync(
                        MediaDevice.GetDefaultAudioCaptureId(AudioDeviceRole.Default));

                    // In my scenario I want 16K sampled, mono, 16-bit output
                    var outProfile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.Low);
                    outProfile.Audio = AudioEncodingProperties.CreatePcm(44100, 1, 16);

                    var outputResult = await this.graph.CreateFileOutputNodeAsync(file,
                        outProfile);

                    if ( outputResult.Status == AudioFileNodeCreationStatus.Success )
                    {
                        this.outputNode = outputResult.FileOutputNode;

                        var inProfile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.High);

                        var inputResult = await this.graph.CreateDeviceInputNodeAsync(
                            MediaCategory.Speech,
                            inProfile.Audio,
                            microphone);

                        if ( inputResult.Status == AudioDeviceNodeCreationStatus.Success )
                        {
                            inputResult.DeviceInputNode.AddOutgoingConnection(
                                this.outputNode);

                            this.graph.Start();
                        }
                    }
                }
            }
        }

        async void OnStop( object sender, RoutedEventArgs e )
        {
            if ( this.graph != null )
            {
                this.graph?.Stop();

                await this.outputNode.FinalizeAsync();

                // assuming that disposing the graph gets rid of the input/output nodes?
                this.graph?.Dispose();

                this.graph = null;
            }
        }

        async Task<StorageFile> PickFileAsync()
        {
            FileSavePicker picker = new FileSavePicker();
            picker.FileTypeChoices.Add("Wave File (PCM)", new List<string> { ".wav" });
            picker.SuggestedStartLocation = PickerLocationId.Desktop;

            var file = await picker.PickSaveFileAsync();

            return ( file );
        }

        AudioGraph graph;
        AudioFileOutputNode outputNode;

        private string one = "GuitarSounds\\BottomLeft\\E.mp3";
        private string two = "GuitarSounds\\MiddleLeft\\A.mp3";
        private string three = "GuitarSounds\\TopLeft\\D.mp3";
        private string four = "GuitarSounds\\TopRight\\G.mp3";
        private string five = "GuitarSounds\\MiddleRight\\B.mp3";
        private string six = "GuitarSounds\\BottomRight\\E.mp3";

        private void ChangeLetters( string letters )
        {
            string[] strings = letters.Split(',');
            for ( int i = 0; i < 6; i++ )
            {
                Button button;
                foreach ( var a in mainGrid.Children )
                {
                    if ( a.GetValue(NameProperty).Equals("Tuner" + ( i + 1 )) )
                    {
                        ( a as Button ).Content = strings[i];
                        break;
                    }
                }

                switch ( i )
                {
                    case 0:
                        one = "GuitarSounds\\BottomLeft\\" + helper(strings[0]);
                        break;
                    case 1:
                        two = "GuitarSounds\\MiddleLeft\\" + helper(strings[1]);
                        break;
                    case 2:
                        three = "GuitarSounds\\TopLeft\\" + helper(strings[2]);
                        break;
                    case 3:
                        four = "GuitarSounds\\TopRight\\" + helper(strings[3]);
                        break;
                    case 4:
                        five = "GuitarSounds\\MiddleRight\\" + helper(strings[4]);
                        break;
                    case 5:
                        six = "GuitarSounds\\BottomRight\\" + helper(strings[5]);
                        break;
                }
            }
        }

        private string helper( string word )
        {
            string result = word[0].ToString();

            if ( word.Length > 1 )
            {
                if ( word[1] == '#' )
                {
                    result += "Sharp";
                }else if ( word[1] == '♭' )
                {
                    result += "Flat";
                }
            }

            result += ".mp3";

            return result;
        }

        #region Options

        private void StandardButton_Click( object sender, RoutedEventArgs e )
        {
            ChangeLetters("E,A,D,G,B,E");
        }

        private void DropDButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("D,A,D,G,B,E");
        }

        private void DropCSharpButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("C#,A,D,G,B,E");
        }

        private void DropCButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("C,G,C,F,A,D");
        }

        private void DropBButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("B,G♭,B,E,A♭,D♭");
        }

        private void DropAButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("A,E,A,D,G♭,B");
        }

        private void DADGADButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("D,A,D,G,A,D");
        }

        private void HalfStepDownButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("E♭,A♭,D♭,G♭,B♭,E♭");
        }

        private void FullStepDownButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("D,G,C,F,A,D");
        }

        private void HalfStepUpButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("F,A#,D#,G#,C,F");
        }

        private void OpenCButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("C,G,C,G,C,E");
        }

        private void OpenDButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("D,A,D,F#,A,D");
        }

        private void OpenEButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("E,B,E,G#,B,E");
        }

        private void OpenFButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("F,A,C,F,C,F");
        }

        private void OpenGButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("D,G,D,G,B,D");
        }

        private void OpenAButton_OnClick( object sender, RoutedEventArgs e )
        {
            ChangeLetters("E,A,E,A,C#,E");
        }
        #endregion{

        #region TunerButtons

        private void Tuner1_OnClick( object sender, RoutedEventArgs e )
        {
            _ = Tuner.sound.playSoundAsync(one);
        }

        private void Tuner2_OnClick( object sender, RoutedEventArgs e )
        {
            _ = Tuner.sound.playSoundAsync(two);
        }

        private void Tuner3_OnClick( object sender, RoutedEventArgs e )
        {
            _ = Tuner.sound.playSoundAsync(three);
        }

        private void Tuner4_OnClick( object sender, RoutedEventArgs e )
        {
            _ = Tuner.sound.playSoundAsync(four);
        }

        private void Tuner5_OnClick( object sender, RoutedEventArgs e )
        {
            _ = Tuner.sound.playSoundAsync(five);
        }

        private void Tuner6_OnClick( object sender, RoutedEventArgs e )
        {
            _ = Tuner.sound.playSoundAsync(six);
        }

        #endregion

    
    }

}





