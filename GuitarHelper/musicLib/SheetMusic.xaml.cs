using System;
using System.Collections.Generic;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SheetMusic : Page
    {
       /* private StorageFile file;

        public SheetMusic()
        {
            this.InitializeComponent();

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));

        }

        private async void ButtonClickOpenPDF(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".pdf");

            var file = await picker.PickSingleFileAsync();

            if (file == null) return;

        }
        private PdfDocument myDocument { get; set; }

        private async Task OpenPDFAsync(StorageFile file)
        {
            if (file == null) throw new ArgumentNullException();

            myDocument = await PdfDocument.LoadFromFileAsync(file);

            await OpenPDFAsync(file);
        }
        private async Task DisplayPage(uint pageIndex)
        {
            if (myDocument == null)
            {
                throw new Exception("No document open.");
            }

            if (pageIndex >= myDocument.PageCount)
            {
                throw new ArgumentOutOfRangeException($"Document has only {myDocument.PageCount} pages.");
            }


            var page = myDocument.GetPage(pageIndex);


            var image = new BitmapImage();

            using (var stream = new InMemoryRandomAccessStream())
            {
                await page.RenderToStreamAsync(stream);
                await image.SetSourceAsync(stream);
                await OpenPDFAsync(file);
                await DisplayPage(0);
            }
        }*/


    }
}
