using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GuitarHelper.musicLib
{

    public sealed partial class SheetMusic : Page
    {


        public SheetMusic()
        {
            this.InitializeComponent();
        }

        private PdfDocument _myDocument { get; set; }

        private async Task OpenPDFAsync(StorageFile file)
        {
            if (file == null) throw new ArgumentNullException();

            _myDocument = await PdfDocument.LoadFromFileAsync(file);
        }


        private async void Button_Click_Open_PDF(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".pdf");

            var file = await picker.PickSingleFileAsync();

            if (file == null) return;

            await OpenPDFAsync(file);
            await OpenPDFAsync(file);
            await DisplayPage(0);
        }
        private async Task DisplayPage(uint pageIndex)
        {
            if (_myDocument == null)
            {
                throw new Exception("No document open.");
            }

            if (pageIndex >= _myDocument.PageCount)
            {
                throw new ArgumentOutOfRangeException($"Document has only {_myDocument.PageCount} pages.");
            }


            var page = _myDocument.GetPage(pageIndex);


            var image = new BitmapImage();

            using (var stream = new InMemoryRandomAccessStream())
            {
                await page.RenderToStreamAsync(stream);
                await image.SetSourceAsync(stream);


                PdfImage.Source = image;
            }
        }
        private void back_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void ctlImage_PointerWheelChanged(object sender, PointerRoutedEventArgs e)

        {

            double dblDelta_Scroll = -1 * e.GetCurrentPoint(PdfImage).Properties.MouseWheelDelta;
            dblDelta_Scroll = (dblDelta_Scroll > 0) ? 0.8 : 0.4;
            double new_ScaleX = this.image_Transform.ScaleX * dblDelta_Scroll;

            double new_ScaleY = this.image_Transform.ScaleY * dblDelta_Scroll;

            this.image_Transform.ScaleX = new_ScaleX;

            this.image_Transform.ScaleY = new_ScaleY;

        }


    }
}

