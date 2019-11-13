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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void play_Audio_Click(object sender, RoutedEventArgs e)
        {
            _ = Tuner.sound.playSoundAsync("bueller.wav");

        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {


            var file = await this.PickFileAsync();

            if (file != null)
            {
                var result = await AudioGraph.CreateAsync(
                       new AudioGraphSettings(AudioRenderCategory.Other));

                if (result.Status == AudioGraphCreationStatus.Success)
                {
                    this.graph = result.Graph;

                    var microphone = await DeviceInformation.CreateFromIdAsync(
                      MediaDevice.GetDefaultAudioCaptureId(AudioDeviceRole.Default));

                    // In my scenario I want 16K sampled, mono, 16-bit output
                    var outProfile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.Low);
                    outProfile.Audio = AudioEncodingProperties.CreatePcm(44100, 1, 16);

                    var outputResult = await this.graph.CreateFileOutputNodeAsync(file,
                      outProfile);

                    if (outputResult.Status == AudioFileNodeCreationStatus.Success)
                    {
                        this.outputNode = outputResult.FileOutputNode;

                        var inProfile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.High);

                        var inputResult = await this.graph.CreateDeviceInputNodeAsync(
                          MediaCategory.Speech,
                          inProfile.Audio,
                          microphone);

                        if (inputResult.Status == AudioDeviceNodeCreationStatus.Success)
                        {
                            inputResult.DeviceInputNode.AddOutgoingConnection(
                              this.outputNode);

                            this.graph.Start();
                        }
                    }
                }
            }
        }
        async void OnStop(object sender, RoutedEventArgs e)
        {
            if (this.graph != null)
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

            return (file);
        }
        AudioGraph graph;
        AudioFileOutputNode outputNode;
    }

}





