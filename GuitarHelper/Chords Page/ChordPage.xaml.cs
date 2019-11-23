using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GuitarHelper.Chords_Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        //api key
        //ea06206162bd4e5e807cf1417cda143503fe8e69 
        

        public BlankPage1()
        {
            this.InitializeComponent();
            //testing();
            var a = setChoords();
            flyouts(a);
        }

        private dynamic api(string search)
        {
            string songJson = "";
            // Create the web request 
            HttpWebRequest request = WebRequest.Create($"http://api.guitarparty.com/v2/chords/?query={search}") as HttpWebRequest;
            request.Headers["Guitarparty-Api-Key"] = "ea06206162bd4e5e807cf1417cda143503fe8e69";
            // Get the response
            dynamic stuff = null;
            string a = null;
            try
            {

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream 
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    // save the JSON data to a string
                    songJson = reader.ReadToEnd();
                     stuff = JsonConvert.DeserializeObject(songJson);
                   
                   
                    string name = stuff.objects[0].image_url;
                    chordChart.Source = new BitmapImage(new Uri(name));
            a = stuff.objects[0].uri;
                }
            }
            catch (Exception ex)
            {
                TextBlock error = new TextBlock();
                error.Text = "somethin done broke but dylan cant tell if it did or didnt./n Dylan cant read this";
                multipleChords.Children.Add(error);
            }
                    return a;
        }

        private void api_get_varitations(string ChordID)
        {
            string songJson = "";
            // Create the web request 
            HttpWebRequest request = WebRequest.Create($"http://api.guitarparty.com{ChordID}?variations=true") as HttpWebRequest;
            request.Headers["Guitarparty-Api-Key"] = "ea06206162bd4e5e807cf1417cda143503fe8e69";
            // Get the response
            try
            {

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream 
                StreamReader reader = new StreamReader(response.GetResponseStream());
                // save the JSON data to a string
                songJson = reader.ReadToEnd();
                dynamic stuff = JsonConvert.DeserializeObject(songJson);
                    int i = 0;
                    foreach (var item in stuff.objects)
                    {
                        Image a = new Image();
 string chord = stuff.objects[i].image_url;
                        a.Source = new BitmapImage(new Uri(chord));
                        multipleChords.Children.Add(a);
                        a.Height = 550;
                        //a.Stretch = Stretch.UniformToFill;
                        i++;

                    }
                  
            }
            } catch (Exception ex){

            }
        }

        #region dropdown of chords
        public List<String> SetKeys(bool? IWantMinorKeys)
        {
            if (IWantMinorKeys == true)
            {
                List<String> MinorKeysList = new List<String>
            {
               "C Minor",
              "Db Minor (Db)",
              "D Minor",
              "D# Minor (Eb)",
              "E Minor",
              "F Minor",
              "F# Minor (Gb)",
              "G Minor",
              "G# Minor (Ab)",
              "A Minor",
              "Bb Minor (A#)",
              "B Minor"

            };
                return MinorKeysList;
            } else if (IWantMinorKeys == null)
            {
                List<String> openList = new List<String>
            {
              "C",
              "B",
              "G",
              "D",
              "A",
              "E"
                };
                return openList;
            }  else
            {

            List<String> MajorKeysList = new List<String>
            {
              "C Major",
              "Db Major (C#)",
              "D Major",
              "Eb Major (D#)",
              "E Major",
              "F Major",
              "F# Major",
              "G Major",
              "Ab Major",
              "A Major",
              "Bb Major",
              "B Major"
             
            };
            return MajorKeysList;
            }
        }

        public List<Choord> setChoords()
        {
            List<Choord> choordList = new List<Choord>
            {
               //new Choord("C", "open", null, "Chords Page/Guitar Choords/Choords/C choord.png", )
            };
            return choordList;
        }

       
        public void flyouts(List<Choord> choordList)
        {
            MenuFlyoutSubItem major = new MenuFlyoutSubItem();
            MenuFlyoutSubItem minor = new MenuFlyoutSubItem();
            MenuFlyoutSubItem open = new MenuFlyoutSubItem();
            major.Text = "Major Keys";
            minor.Text = "Minor Keys";
            open.Text = "Open";
            Flyout.Items.Add(major);
            Flyout.Items.Add(open);
            Flyout.Items.Add(minor);
            int i = 0;
            //for all the major keys
            foreach (String Key in SetKeys(false))
            {
            MenuFlyoutSubItem key = new MenuFlyoutSubItem();
                key.Text = Key;

                major.Items.Add(key);
            

            }
            foreach (String Key in SetKeys(true))
            {
                MenuFlyoutSubItem key = new MenuFlyoutSubItem();
                key.Text = Key;

                minor.Items.Add(key);


            }

            foreach (String Key in SetKeys(null))
            {
                MenuFlyoutItem key = new MenuFlyoutItem();
                key.Text = Key;
                open.Items.Add(key);
            }


            foreach (Choord c in choordList)
            {
                if (c.IsMinor == null)
                {
                   
                MenuFlyoutItem item = new MenuFlyoutItem();
                item.Text = $"{c.Choordname} in {c.KeyName} key";
                    open.Items.Add(item);
                
                } 
            }
        }

        #endregion

        private void search_Tapped(object sender, TappedRoutedEventArgs e)
        {
            multipleChords.Children.Clear();
            var a = api(chordSearch.Text);
            if (a != null)
            {

        api_get_varitations(a);
            }

        }
    }

    public class Choord
    {
        public String Choordname { get; set; }
        public String KeyName { get; set; }

        public bool? IsMinor { get; set; }

        public Choord(string choordname, string keyName, bool? isMinor, String ImagePath, String AudioPath)
        {
            Choordname = choordname;
            KeyName = keyName;
            isMinor = IsMinor;
        }
    }
}
