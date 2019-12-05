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
                error.Text = "Not a valid chord try again";
                error.FontSize = 42;
                error.TextAlignment = TextAlignment.Center;
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



        public List<String> SetKeys(bool? IWantMinorKeys)
        {
            if (IWantMinorKeys == true)
            {
                List<String> MinorKeysList = new List<String>
            {
               "C Minor",
               "C# Minor",
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
            else if (IWantMinorKeys == false)
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
            else if (IWantMinorKeys == null)
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
            }
            return null;
        }
        #region Populating the dropdown 
        public List<Choord> setChoords()
        {
            List<Choord> choordList = new List<Choord>
            {
                new Choord("Cm","C Minor",true),
                new Choord("Cm7","C Minor",true),
                new Choord("Ddim","C Minor",true), //test this one
                new Choord("Dm7b5","C Minor",true),
                new Choord("Ebmaj","C Minor",true),
                new Choord("Ebmaj7","C Minor",true),
                new Choord("Fm","C Minor",true),
                new Choord("Fm7","C Minor",true),
                new Choord("Gm","C Minor",true),
                new Choord("Gm7","C Minor",true),
                new Choord("Abmaj","C Minor",true),
                new Choord("Abmaj7","C Minor",true),
                new Choord("Bbmaj","C Minor",false),
                new Choord("Bb7","C Minor",true),
                new Choord("C#m","C# Minor",true),
                new Choord("C#m7","C# Minor",true),
                new Choord("D#dim","C# Minor",true),
                new Choord("D#m7b5","C# Minor",true),
                new Choord("Emaj","C# Minor",true),
                new Choord("Emaj7","C# Minor",true),
                new Choord("F#m","C# Minor",true),
                new Choord("F#m7","C# Minor",true),
                new Choord("G#m","C# Minor",true),
                new Choord("G#m7","C# Minor",true),
                new Choord("Amaj","C# Minor",true),
                new Choord("Amaj7","C# Minor",true),
                new Choord("Bmaj","C# Minor",true),
                new Choord("B7","C# Minor",true),
                new Choord("C#m","C# Minor",true),
                new Choord("C#m","C# Minor",true),
                new Choord("C#m","C# Minor",true),
                new Choord("C#m","C# Minor",true),

                new Choord("C#min","Db Minor",true),
                new Choord("C#min7","Db Minor",true),
                new Choord("D#dim","Db Minor",true),
                new Choord("D#m7b5","Db Minor",true),
                new Choord("Emaj","Db Minor",true),
                new Choord("Emaj7","Db Minor",true),
                new Choord("F#min","Db Minor",true),
                new Choord("F#min7","Db Minor",true),
                new Choord("G#min","Db Minor",true),
                new Choord("G#min7","Db Minor",true),
                new Choord("Amaj","Db Minor",true),
                new Choord("Amaj7","Db Minor",true),
                new Choord("Bmaj","Db Minor",true),
                new Choord("B7","Db Minor",true),
                new Choord("Dmin","D Minor",true),
                new Choord("Dmin7","D Minor",true),
                new Choord("Emin","D Minor",true),
                new Choord("Em7b5","D Minor",true),
                new Choord("Fmaj","D Minor",true),
                new Choord("Fmaj7","D Minor",true),
                new Choord("Gmin","D Minor",true),
                new Choord("Gmin7","D Minor",true),
                new Choord("Amin","D Minor",true),
                new Choord("Amin7","D Minor",true),
                new Choord("Bbmaj","D Minor",true),
                new Choord("Bbmaj7","D Minor",true),
                new Choord("Cbmaj","D Minor",true),
                new Choord("C7","D Minor",true),

                new Choord("Ebmin","D# Minor",true),
                new Choord("Ebmin7","D# Minor",true),
                new Choord("Fdim","D# Minor",true),
                new Choord("Fm7b5","D# Minor",true),
                new Choord("Gbmaj","D# Minor",true),
                new Choord("Gbmaj7","D# Minor",true),
                new Choord("Abmin","D# Minor",true),
                new Choord("Abmin7","D# Minor",true),
                new Choord("Bbmin","D# Minor",true),
                new Choord("Bbmin7","D# Minor",true),
                new Choord("Cbmaj","D# Minor",true),
                new Choord("Cbmaj7","D# Minor",true),
                new Choord("Dbmaj","D# Minor",true),
                new Choord("Db7","D# Minor",true),


                new Choord("Emin","E Minor",true),
                new Choord("Emin7","E Minor",true),
                new Choord("F#dim","E Minor",true),
                new Choord("F#m7b5","E Minor",true),
                new Choord("Gmaj","E Minor",true),
                new Choord("Gmaj7","E Minor",true),
                new Choord("Amin","E Minor",true),
                new Choord("Amin7","E Minor",true),
                new Choord("Bmin","E Minor",true),
                new Choord("Bmin7","E Minor",true),
                new Choord("Cmaj","E Minor",true),
                new Choord("Cmaj7","E Minor",true),
                new Choord("Dmaj","E Minor",true),
                new Choord("D7","E Minor",true),

                new Choord("Fmin","F Minor",true),
                new Choord("Fmin7","F Minor",true),
                new Choord("Gdim","F Minor",true),
                new Choord("Gm7b5","F Minor",true),
                new Choord("Abmaj","F Minor",true),
                new Choord("Bbmin","F Minor",true),
                new Choord("Bbmin","F Minor",true),
                new Choord("Cmin","F Minor",true),
                new Choord("Cmin7","F Minor",true),
                new Choord("Dbmaj","F Minor",true),
                new Choord("Dbmaj7","F Minor",true),
                new Choord("Ebmaj","F Minor",true),
                new Choord("Eb7","F Minor",true),

                new Choord("F#min","F# Minor",true),
                new Choord("F#min7","F# Minor",true),
                new Choord("G#dim","F# Minor",true),
                new Choord("G#m7b5","F# Minor",true),
                new Choord("Amaj","F# Minor",true),
                new Choord("Amaj7","F# Minor",true),
                new Choord("Bmin","F# Minor",true),
                new Choord("Bmin7","F# Minor",true),
                new Choord("C#min","F# Minor",true),
                new Choord("C#min7","F# Minor",true),
                new Choord("Dmaj","F# Minor",true),
                new Choord("Dmaj7","F# Minor",true),
                new Choord("Emaj","F# Minor",true),
                new Choord("E7","F# Minor",true),


                new Choord("Gmin","G Minor",true),
                new Choord("Gmin7","G Minor",true),
                new Choord("Adim","G Minor",true),
                new Choord("Am7b5","G Minor",true),
                new Choord("Bbmaj","G Minor",true),
                new Choord("Bbmaj7","G Minor",true),
                new Choord("Cmin","G Minor",true),
                new Choord("Cmin7","G Minor",true),
                new Choord("Dmin","G Minor",true),
                new Choord("Dmin7","G Minor",true),
                new Choord("Ebmaj","G Minor",true),
                new Choord("Ebmaj7","G Minor",true),
                new Choord("Fmaj","G Minor",true),
                new Choord("F7","G Minor",true),

                new Choord("Abmin","G# Minor",true),
                new Choord("Abmin7","G# Minor",true),
                new Choord("Bbdim","G# Minor",true),
                new Choord("Bbm7b5","G# Minor",true),
                new Choord("Cbmaj","G# Minor",true),
                new Choord("Cbmaj7","G# Minor",true),
                new Choord("Dbmin","G# Minor",true),
                new Choord("Dbmin7","G# Minor",true),
                new Choord("Ebmin","G# Minor",true),
                new Choord("Ebmin7","G# Minor",true),
                new Choord("Gbmaj","G# Minor",true),
                new Choord("Fbmaj7","G# Minor",true),
                new Choord("Gbmaj","G# Minor",true),
                new Choord("Gb7","G# Minor",true),
                new Choord("G#min","G# Minor",true),
                new Choord("A#dim","G# Minor",true),
                new Choord("A#m7b5","G# Minor",true),
                new Choord("Bmaj","G# Minor",true),
                new Choord("Bmaj7","G# Minor",true),
                new Choord("C#min","G# Minor",true),
                new Choord("C#min7","G# Minor",true),
                new Choord("D#min","G# Minor",true),
                new Choord("D#min7","G# Minor",true),
                new Choord("Emaj","G# Minor",true),
                new Choord("Emaj7","G# Minor",true),
                new Choord("F#maj7","G# Minor",true),
                new Choord("F#7","G# Minor",true),


                new Choord("Amin","A Minor",true),
                new Choord("Amin7","A Minor",true),
                new Choord("Bdim","A Minor",true),
                new Choord("Bm7b5","A Minor",true),
                new Choord("Cmaj","A Minor",true),
                new Choord("Cmaj7","A Minor",true),
                new Choord("Dmin","A Minor",true),
                new Choord("Dmin7","A Minor",true),
                new Choord("Emin","A Minor",true),
                new Choord("Emin7","A Minor",true),
                new Choord("Fmaj","A Minor",true),
                new Choord("Fmaj7","A Minor",true),
                new Choord("Gmaj","A Minor",true),
                new Choord("G7","A Minor",true),

                new Choord("Bbmin","A# Minor",true),
                new Choord("Bbmin7","A# Minor",true),
                new Choord("Cdim","A# Minor",true),
                new Choord("Cm7b5","A# Minor",true),
                new Choord("Dbmaj","A# Minor",true),
                new Choord("Dbmaj7","A# Minor",true),
                new Choord("Ebmin","A# Minor",true),
                new Choord("Ebmin7","A# Minor",true),
                new Choord("Fmin","A# Minor",true),
                new Choord("Fmin7","A# Minor",true),
                new Choord("Gbmaj","A# Minor",true),
                new Choord("Gbmaj7","A# Minor",true),
                new Choord("Abmaj","A# Minor",true),
                new Choord("Ab7","A# Minor",true),

                new Choord("A#min","A# Minor",true),
                new Choord("A#min7","A# Minor",true),
                new Choord("B#dim","A# Minor",true),
                new Choord("B#m7b5","A# Minor",true),
                new Choord("C#maj","A# Minor",true),
                new Choord("C#maj7","A# Minor",true),
                new Choord("D#min","A# Minor",true),
                new Choord("D#min7","A# Minor",true),
                new Choord("E#min","A# Minor",true),
                new Choord("E#min7","A# Minor",true),
                new Choord("F#maj","A# Minor",true),
                new Choord("F#maj7","A# Minor",true),
                new Choord("G#maj","A# Minor",true),
                new Choord("G#7","A# Minor",true),


                new Choord("Bmin","B Minor",true),
                new Choord("Bmin7","B Minor",true),
                new Choord("C#dim","B Minor",true),
                new Choord("C#m7b5","B Minor",true),
                new Choord("Dmaj","B Minor",true),
                new Choord("Dmaj7","B Minor",true),
                new Choord("Emin","B Minor",true),
                new Choord("Emin7","B Minor",true),
                new Choord("F#min","B Minor",true),
                new Choord("F#min7","B Minor",true),
                new Choord("Gmaj","B Minor",true),
                new Choord("Gmaj7","B Minor",true),
                new Choord("Amaj","B Minor",true),
                new Choord("A7","B Minor",true)
               


                
                
               
               

            };
            return choordList;
        }

        public void flyouts(List<Choord> choordList)
        {
            MenuFlyoutSubItem major = new MenuFlyoutSubItem();
            MenuFlyoutSubItem minor = new MenuFlyoutSubItem();
            MenuFlyoutSubItem open = new MenuFlyoutSubItem();
            open.Text = "Open";
            major.Text = "Major Keys";
            minor.Text = "Minor Keys";
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

            foreach (String Key in SetKeys(null))
            {
                MenuFlyoutItem key = new MenuFlyoutItem();
                key.Click += chord_clicked;
                key.Text = Key;
                open.Items.Add(key);
            }

            foreach (String Key in SetKeys(true))
            {
                MenuFlyoutSubItem key = new MenuFlyoutSubItem();
                key.Text = Key;

                minor.Items.Add(key);


            }

            foreach (Choord c in choordList)
            {
                MenuFlyoutItem mn = new MenuFlyoutItem();
                mn.Click += chord_clicked;
                //if (c.IsMinor)
                //{
                foreach (MenuFlyoutSubItem menuitem in minor.Items)
                {
                    if (menuitem.Text == "C Minor")
                    {


                        if (c.KeyName == "C Minor")
                        {
                            mn.Text = $"{c.Choordname}";
                            menuitem.Items.Add(mn);
                        }
                    }
                    if (menuitem.Text == "C# Minor")
                    {
                        if (c.KeyName == "C# Minor")
                        {
                            mn.Text = $"{c.Choordname}";
                            menuitem.Items.Add(mn);

                        }
                    }
                    if (menuitem.Text == "Db Minor (Db)")
                    {
                        if (c.KeyName == "Db Minor")
                        {
                            mn.Text = $"{c.Choordname}";
                            menuitem.Items.Add(mn);

                        }
                    }
                    if (menuitem.Text == "D Minor")
                    {
                        if (c.KeyName == "D Minor")
                        {
                            mn.Text = $"{c.Choordname}";
                            menuitem.Items.Add(mn);

                        }
                    }
                    if (menuitem.Text == "D# Minor (Eb)")
                    {
                        if (c.KeyName == "D# Minor")
                        {
                            mn.Text = $"{c.Choordname}";
                            menuitem.Items.Add(mn);

                        }
                    }
                    //}
                }
            }
        }
        #endregion 

        private void chord_clicked(object sender, RoutedEventArgs e)
        {
            multipleChords.Children.Clear();
            var a = api((sender as MenuFlyoutItem).Text);
            if (a != null)
            {

                api_get_varitations(a);
            }
        }

        private void Chord_Tapped(object sender, TappedRoutedEventArgs e)
        {
            multipleChords.Children.Clear();
            var a = api("Am");
            if (a != null)
            {

                api_get_varitations(a);
            }
        }


        public class Choord
        {
            public String Choordname { get; set; }
            public String KeyName { get; set; }

            public bool IsMinor { get; set; }

            public Choord(string choordname, string keyName, bool? isMinor)
            {
                Choordname = choordname;
                KeyName = keyName;
                isMinor = IsMinor;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}

