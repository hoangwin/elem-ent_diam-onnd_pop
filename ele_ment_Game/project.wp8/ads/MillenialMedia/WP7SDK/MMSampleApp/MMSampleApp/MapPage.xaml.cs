using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MMSampleApp
{
    public partial class MapPage
    {
        private Image _imageContent;

        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string name;
            string image;

            NavigationContext.QueryString.TryGetValue("name", out name);
            NavigationContext.QueryString.TryGetValue("image", out image);

            PageTitle.Text = name;

            Uri uri = new Uri(image, UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            _imageContent = new Image();
            _imageContent.SetValue(Image.SourceProperty, bitmapImage);
            MapScrollViewer.Content = _imageContent;

            
        }
    }
}