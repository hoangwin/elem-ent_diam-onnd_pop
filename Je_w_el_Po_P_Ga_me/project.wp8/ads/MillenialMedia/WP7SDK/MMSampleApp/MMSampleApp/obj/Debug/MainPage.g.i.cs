﻿#pragma checksum "d:\project_android\pikachu\Gameloft_AI_Template_2013_V106\serveroffline\Can_dy_Ga_me\project.wp8\ads\MillenialMedia\WP7SDK\MMSampleApp\MMSampleApp\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3DC5445DE01967A64CC92FEE8621D317"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using MillennialMedia.WP7.SDK;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MMSampleApp {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid DCGrid;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Button btnRefresh;
        
        internal System.Windows.Controls.StackPanel ContentPanel;
        
        internal MillennialMedia.WP7.SDK.MMBannerAdView bannerAd;
        
        internal System.Windows.Controls.Image dcImage;
        
        internal System.Windows.Controls.StackPanel nyContentPanel;
        
        internal System.Windows.Controls.TextBlock nyPageTitle;
        
        internal System.Windows.Controls.Image nyImage;
        
        internal System.Windows.Controls.StackPanel sfContentPanel;
        
        internal System.Windows.Controls.TextBlock sfPageTitle;
        
        internal System.Windows.Controls.Button btnFetchInterstitial;
        
        internal System.Windows.Controls.Button btnDisplayInterstitial;
        
        internal System.Windows.Controls.Image sfImage;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MMSampleApp;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.DCGrid = ((System.Windows.Controls.Grid)(this.FindName("DCGrid")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.btnRefresh = ((System.Windows.Controls.Button)(this.FindName("btnRefresh")));
            this.ContentPanel = ((System.Windows.Controls.StackPanel)(this.FindName("ContentPanel")));
            this.bannerAd = ((MillennialMedia.WP7.SDK.MMBannerAdView)(this.FindName("bannerAd")));
            this.dcImage = ((System.Windows.Controls.Image)(this.FindName("dcImage")));
            this.nyContentPanel = ((System.Windows.Controls.StackPanel)(this.FindName("nyContentPanel")));
            this.nyPageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("nyPageTitle")));
            this.nyImage = ((System.Windows.Controls.Image)(this.FindName("nyImage")));
            this.sfContentPanel = ((System.Windows.Controls.StackPanel)(this.FindName("sfContentPanel")));
            this.sfPageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("sfPageTitle")));
            this.btnFetchInterstitial = ((System.Windows.Controls.Button)(this.FindName("btnFetchInterstitial")));
            this.btnDisplayInterstitial = ((System.Windows.Controls.Button)(this.FindName("btnDisplayInterstitial")));
            this.sfImage = ((System.Windows.Controls.Image)(this.FindName("sfImage")));
        }
    }
}

