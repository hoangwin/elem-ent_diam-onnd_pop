using System;
using System.Device.Location;
using System.Diagnostics;
using System.Windows;
using MillennialMedia.WP7.SDK;

namespace MMSampleApp
{
    public partial class MainPage
    {
        #region Consts and Locals

        /// <summary>
        /// Your APID from the mMedia console. An APID is necessary to make any ad requests.
        /// </summary>
        private const String Apid = "28911";
        /// <summary>
        /// Object which will handle the retrieval and display of interstitial advertisements. Note that this object gets defined here in the codebehind and not in the XAML markup.
        /// </summary>
        private readonly MMInterstitialAd _interstitialAd = new MMInterstitialAd();

        // Sample app implementaton
        private const String LogPrefix = "Millennial Media: ";
        private readonly GeoCoordinateWatcher _geoWatcher;

        #endregion

        public MainPage()
        {
            InitializeComponent();

            #region Millennial SDK Required Operations

            // Request an initial banner ad for display
            bannerAd.GetAd(Apid);

            #endregion

            #region Millennial SDK Optional Operations

            // Optionally set any available demographic information
            MMSDK.Demographic = getUserDemographics();

            // Optionally set the user's location, if available
            _geoWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High) { MovementThreshold = 1000 };
            _geoWatcher.PositionChanged += (sender, args) => MMSDK.Location = args.Position.Location;
            _geoWatcher.Start();

            // Optionally define a delegate for handling detailed events raised by the SDK
            MMSDK.MMAdEvents += mmSdkOnMmAdEvents;

            #endregion

            // Also see the declarations for control mmi:MMAdView (bannerAd) in MainPage.xaml
        }

        #region Interstitial Ad Example

        private void btnFetchInterstitial_Click(object sender, RoutedEventArgs e)
        {
            // Request the interstitial to be ready to display it later. Write the outcome to the debug log.

            // In a real implementation, you will want to call Fetch() as soon as possible to give the SDK
            // time to download and cache the interstitial data. The ad will not be shown until you call Display() below.
            _interstitialAd.Fetch(Apid,
                view => Debug.WriteLine("{0}Interstitial successfully retrieved.", LogPrefix), // Success delegate
                (view, error) => Debug.WriteLine("{0}Interstitial failed to load. Details: {1}", LogPrefix, error)); //Failure delegate
        }

        private void btnDisplayInterstitial_Click(object sender, RoutedEventArgs e)
        {
            // Check to see if an interstitial ad is ready to be displayed, and if so, display it.
            if (_interstitialAd.IsAdAvailable())
                _interstitialAd.Display();
        }

        #endregion

        #region Optional SDK Operation Implementations

        /// <summary>
        /// A sample method to demonstrate accessing user demographic information
        /// </summary>
        /// <returns>A sample object containing some fixed demographic values</returns>
        private MMDemographic getUserDemographics()
        {
            // A sample value is returned for demonstration purposes
            return new MMDemographic { Gender = MMGender.Male, MaritalStatus = MMMaritalStatus.Married };
        }

        /// <summary>
        /// Event handler which demonsrates the various events that can be raised by the SDK.
        /// </summary>
        /// <param name="sender">The object which caused this event to be raised. Can be <value>null</value></param>
        /// <param name="e">The specific event being raised/</param>
        private void mmSdkOnMmAdEvents(object sender, MMEventArgs e)
        {
            if (e.Type == MMEventType.OverlayOpened)
                Debug.WriteLine("{0}Overlay opened.", LogPrefix);
            else if (e.Type == MMEventType.OverlayClosed)
                Debug.WriteLine("{0}Overlay closed.", LogPrefix);
            else if (e.Type == MMEventType.TaskStarted)
                Debug.WriteLine("{0}Task started.", LogPrefix);
            else if (e.Type == MMEventType.GetAdSuccess)
                Debug.WriteLine("{0}Ad successfuly retrieved.", LogPrefix);
            else if (e.Type == MMEventType.GetAdFailure)
                Debug.WriteLine("{0}Ad failed to be retrieved.", LogPrefix);
            else if (e.Type == MMEventType.DisplayStarted)
                Debug.WriteLine("{0}Display started.", LogPrefix);
            else if (e.Type == MMEventType.FetchStartedCaching)
                Debug.WriteLine("{0}Caching started", LogPrefix);
            else if (e.Type == MMEventType.FetchFinishedCaching)
                Debug.WriteLine("{0}Caching finished.", LogPrefix);
        }

        #endregion

        #region Sample App Implementation

        private void dcMap_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MapPage.xaml?image=img/dcmetro.jpg&name=dc metro", UriKind.Relative));
        }

        private void nyMap_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MapPage.xaml?image=img/manhattan.png&name=ny metro", UriKind.Relative));
        }

        private void sfMap_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MapPage.xaml?image=img/muni.png&name=muni", UriKind.Relative));
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            bannerAd.GetAd(Apid);
        }

        #endregion
    }
}