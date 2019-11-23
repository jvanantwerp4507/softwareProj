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
using System.Net;
using Newtonsoft.Json;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GuitarHelper.Chords_Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {


        public BlankPage1()
        {
            this.InitializeComponent();

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
            }
            catch (Exception ex)
            {

            }
        }

        private void search_Tapped(object sender, TappedRoutedEventArgs e)
        {
            multipleChords.Children.Clear();
            var a = api(chordSearch.Text);
            if (a != null)
            {

                api_get_varitations(a);
            }

        }



        public List<String> SetKeys(bool IWantMinorKeys)
        {
            if (IWantMinorKeys)
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
            }
            else
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
                new Choord("C#","A",true),
                new Choord("D#","B",false),
                new Choord("E#","D",true),
                new Choord("F#","C",true),
                new Choord("H#","G",true)
            };
            return choordList;
        }

        public void flyouts(List<Choord> choordList)
        {
            MenuFlyoutSubItem major = new MenuFlyoutSubItem();
            MenuFlyoutSubItem minor = new MenuFlyoutSubItem();
            major.Text = "Major Keys";
            minor.Text = "Minor Keys";
            Flyout.Items.Add(major);
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


            foreach (Choord c in choordList)
            {
                if (c.IsMinor)
                {

                    MenuFlyoutItem mn = new MenuFlyoutItem();
                    mn.Text = $"{c.Choordname} in {c.KeyName} key";
                    var a = c.KeyName;


                }
                else
                {

                }
            }
        }

    }


    public class Choord
    {
        public String Choordname { get; set; }
        public String KeyName { get; set; }

        public bool IsMinor { get; set; }

        public Choord(string choordname, string keyName, bool isMinor)
        {
            Choordname = choordname;
            KeyName = keyName;
            isMinor = IsMinor;
        }
    }
}

