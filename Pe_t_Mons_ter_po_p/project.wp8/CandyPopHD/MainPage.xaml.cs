using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

using UnityApp = UnityPlayer.UnityApp;
using UnityBridge = WinRTBridge.WinRTBridge;

using Inneractive.Nokia.Ad;
namespace CandyPopHD
{
	public partial class MainPage : PhoneApplicationPage
	{
		private bool _unityStartedLoading;

		// Constructor
		public MainPage()
		{
			var bridge = new UnityBridge();
			UnityApp.SetBridge(bridge);
			InitializeComponent();
			bridge.Control = DrawingSurfaceBackground;
		}
       
		private void DrawingSurfaceBackground_Loaded(object sender, RoutedEventArgs e)
		{
            
			if (!_unityStartedLoading)
			{
				_unityStartedLoading = true;

				UnityApp.SetLoadedCallback(() => { Dispatcher.BeginInvoke(Unity_Loaded); });

				var content = Application.Current.Host.Content;
				var width = (int)Math.Floor(content.ActualWidth * content.ScaleFactor / 100.0 + 0.5);
				var height = (int)Math.Floor(content.ActualHeight * content.ScaleFactor / 100.0 + 0.5);

				UnityApp.SetNativeResolution(width, height);
				UnityApp.SetRenderResolution(width, height);
				UnityPlayer.UnityApp.SetOrientation((int)Orientation);

				DrawingSurfaceBackground.SetBackgroundContentProvider(UnityApp.GetBackgroundContentProvider());
				DrawingSurfaceBackground.SetBackgroundManipulationHandler(UnityApp.GetManipulationHandler());
			}
		}

		private void Unity_Loaded()
		{

		}

		private void PhoneApplicationPage_BackKeyPress(object sender, CancelEventArgs e)
		{
			e.Cancel = UnityApp.BackButtonPressed();
		}

		private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
		{
			UnityApp.SetOrientation((int)e.Orientation);
		}


        /*
         * 
         *         <mobfox:AdControl 
                Name="mobFoxadControl"                
                PublisherID="921bb609c6eed77679352276b51a9cd9"
                TestMode="True"
              
                VerticalAlignment="Bottom" />
         */
        /*
        void ShowMobfoxAds()
        {
            MobFox.Ads.AdControl adview = new MobFox.Ads.AdControl();
            adview.Name = "mobFoxadControl";
            adview.NoAd +=noAdsMobfox;
            adview.PublisherID = "921bb609c6eed77679352276b51a9cd9";
            adview.VerticalAlignment = VerticalAlignment.Bottom;               
        }
        */
        void noAdsMobfox(object sender, MobFox.Ads.NoAdEventArgs args)
        {

            //InneractiveXamlAd.IsEnabled = true;

            string AppID="LavaGame_CandyPopHD_WP";
            Dictionary<InneractiveAd.IaOptionalParams, string> optionalParams = new Dictionary<InneractiveAd.IaOptionalParams, string>();
            //optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Age, "25");
            //optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Gender, "m");
            //optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Keywords, "test,inneractive");
            //optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Gps_Coordinates, "53.5422,-2.2396");
            //optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Location, "US,NY,NY");
            optionalParams.Add(InneractiveAd.IaOptionalParams.Key_Ad_Alignment, InneractiveAd.IaAdAlignment.BOTTOM_CENTER.ToString());
            optionalParams.Add(InneractiveAd.IaOptionalParams.Key_OptionalAdWidth, "480");
            optionalParams.Add(InneractiveAd.IaOptionalParams.Key_OptionalAdHeight, "50");
            InneractiveAd iaBanner = new InneractiveAd(AppID, InneractiveAd.IaAdType.IaAdType_Banner, 60, optionalParams);
            DrawingSurfaceBackground.Children.Add(iaBanner);
        }

        void ShowAdsMobfox(object sender, MobFox.Ads.NoAdEventArgs args)
        {

            int i;
            i = 0;
        }
        
	}
}