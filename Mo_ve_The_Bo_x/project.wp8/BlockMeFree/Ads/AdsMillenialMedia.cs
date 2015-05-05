using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillennialMedia.WP7.SDK;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using System.Windows;
using System.Security.Cryptography;
namespace BlockMeFree.Ads
{
    class AdsMillenialMedia
    {        
        public static MMBannerAdView mmAdView = null;
        public static WebBrowser AdView;
        public static bool isFistLoaded = false;
        public static bool isAdsLoadOK = false;
        
        public static void ShowMillenialMedia(DrawingSurfaceBackgroundGrid DrawingSurfaceBackground)
        {
            isFistLoaded = true;
            //MMSDK.DEFAULT_APID;
            //<mmi:MMBannerAdView x:Name="mmAdView" Height="60" Width="480" VerticalAlignment ="Bottom" />   
            mmAdView.GetAd(AdsManager.STR_PUBLISH_ID_MILLENIAL_MEDIA);
            mmAdView.RefreshApid = AdsManager.STR_PUBLISH_ID_MILLENIAL_MEDIA;
            mmAdView.RefreshTimer = 40;
            MMSDK.MMAdEvents += mmSdkOnMmAdEvents;            
        }

        public static void ShowMillenialMediaNoSDK(DrawingSurfaceBackgroundGrid DrawingSurfaceBackground)
        {
            SHA1Managed sha1gen = new SHA1Managed();
            byte[] auid = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
            //string adRequest = "http://ads.mp.mydas.mobi/getAd?apid="+AdsManager.STR_PUBLISH_ID_MILLENIAL_MEDIA +"&hsht=60&hswd=480&hdid=" + sha1gen.ComputeHash(auid);

            string hex = BitConverter.ToString(sha1gen.ComputeHash(auid));
            hex = hex.Replace("-", "");
            string adRequest = "http://ads.mp.mydas.mobi/getAd?apid=140549&hsht=60&hswd=480&hdid=" + hex;             

            //string adRequest = "http://ads.mp.mydas.mobi/getAd?apid=140736&hsht=60&hswd=480&hdid=c3e9e6cc280a1ef75301d14f793184f9629f3200";
           // string adRequest = "http://www.google.com.vn";
            AdView.Source = new Uri(adRequest);
            AdView.LoadCompleted += CompletedEventHandler;
        }
        static void CompletedEventHandler(object sender, NavigationEventArgs e)
        {
            if (AdView.SaveToString().Length == 0)
            {
              //  AdView.Visiblity = Visibility.Collapsed;
            }
            else
            {

            }
        }
        public static void mmSdkOnMmAdEvents(object sender, MMEventArgs e)
        {
            if (e.Type == MMEventType.OverlayOpened)
            {

            }
            else if (e.Type == MMEventType.OverlayClosed)
            {

            }
            else if (e.Type == MMEventType.TaskStarted)
            {
            }
            else if (e.Type == MMEventType.GetAdSuccess)
            {
                isAdsLoadOK = true;
                AdsManager.ChooseDisplayAds();
            }
            else if (e.Type == MMEventType.GetAdFailure)
            {
                isAdsLoadOK = false;
                
              //  AdsManager.getRequestNextAds(AdsManager.INDEX_INNER_ACTIVE);
                
            }
            else if (e.Type == MMEventType.DisplayStarted)
            {
            }
            else if (e.Type == MMEventType.FetchStartedCaching)
            {


            }
            else if (e.Type == MMEventType.FetchFinishedCaching)
            {

            }
        }
        public static void enableDisplayAds(bool isdisplay)
        {
            if (mmAdView == null)
                return;
            if (isdisplay)
            {
                mmAdView.Visibility = Visibility.Visible;
                mmAdView.IsEnabled = true;
            }
            else
            {
                mmAdView.Visibility = Visibility.Collapsed;
                mmAdView.IsEnabled = false;
            }
        }
    }
}
