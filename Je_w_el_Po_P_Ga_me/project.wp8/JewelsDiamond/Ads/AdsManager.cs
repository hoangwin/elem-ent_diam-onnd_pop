using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MillennialMedia.WP7.SDK;
namespace JewelsDiamond
{
    class AdsManager
    {
        public static DrawingSurfaceBackgroundGrid DrawingSurfaceBackground;
        //publish ID
        public static string STR_PUBLISH_ID_MOBFOX = "a705cd0092baccdb00b971a098b953f2";//HOANG.NGUYENMAU@HOTMAIL
        public static string STR_PUBLISH_ID_MILLENIAL_MEDIA = "xxx";//HOANG.NGUYENMAU@HOTMAIL
        public static string STR_PUBLISH_ID_INMOBI = "d1449b9a52ec4d059078e7cc561cee9b";//mobilepuzzle&gmail
        public static string STR_PUBLISH_ID_INNER_ACTIVE = "LavaGame_JewelsAndDiamonds_WP";//HOANG.NGUYENMAU@HOTMAIL

        public const int INDEX_MOBFOX = 0;
        public const int INDEX_INMOBI = 1;
        public const int INDEX_MILLENIAL_MEDIA = 2;
        public const int INDEX_INNER_ACTIVE = 3;
        
        public static int mcurrentIndexShow = -1;

        
        public static void showAds(DrawingSurfaceBackgroundGrid _DrawingSurfaceBackground,int index)
        {
            DrawingSurfaceBackground = _DrawingSurfaceBackground;            
            switch (index)
            {
                case INDEX_MOBFOX:
                    Ads.AdsMobfox.ShowMobfoxAds(_DrawingSurfaceBackground);
                    break;
                case INDEX_INMOBI:
                  //  Ads.AdsInmobi.ShowInmobi(_DrawingSurfaceBackground);
                    break;
                case INDEX_INNER_ACTIVE:
                    Ads.AdsInnerActive.ShowInnerActive(_DrawingSurfaceBackground);
                    break;
                case INDEX_MILLENIAL_MEDIA:
                    Ads.AdsMillenialMedia.ShowMillenialMedia(_DrawingSurfaceBackground);
                    break;
            }
        }
        public static void getRequestNextAds(int index)
        {
           
            switch (index)
            {
                case INDEX_MOBFOX:
                                                       
                    if(Ads.AdsInmobi.AdView == null)//khi chua co khoi tao thi khoi tao
                    {
                        AdsManager.showAds(AdsManager.DrawingSurfaceBackground, AdsManager.INDEX_MOBFOX);
                    }

                    break;
               
                case INDEX_INMOBI:
                    
                    if (Ads.AdsInnerActive.iaBanner == null)
                    {
                        AdsManager.showAds(AdsManager.DrawingSurfaceBackground, AdsManager.INDEX_INMOBI);

                    }
                    break;
                case INDEX_INNER_ACTIVE:
                    if (Ads.AdsInnerActive.iaBanner == null)
                    {
                        AdsManager.showAds(AdsManager.DrawingSurfaceBackground, AdsManager.INDEX_INNER_ACTIVE);
                    }
                 
                    break;
                case INDEX_MILLENIAL_MEDIA:


                    if (Ads.AdsMillenialMedia.mmAdView == null)
                    {
                        AdsManager.showAds(AdsManager.DrawingSurfaceBackground, AdsManager.INDEX_MILLENIAL_MEDIA);

                    }

                    break;
            }
        }

        public static void ChooseDisplayAds()
        {
           if(Ads.AdsMobfox.isAdsLoadOK)
           {
                  Ads.AdsMobfox.enableDisplayAds(true);
                  Ads.AdsMillenialMedia.enableDisplayAds(false);
                  Ads.AdsInmobi.enableDisplayAds(false);
                  Ads.AdsInnerActive.enableDisplayAds(false);
           }
           else if(Ads.AdsMillenialMedia.isAdsLoadOK)
           {
                  Ads.AdsMobfox.enableDisplayAds(false);
                  Ads.AdsMillenialMedia.enableDisplayAds(true);
                  Ads.AdsInmobi.enableDisplayAds(false);
                  Ads.AdsInnerActive.enableDisplayAds(false);
           }
           else if(Ads.AdsInmobi.isAdsLoadOK)
           {
                  Ads.AdsMobfox.enableDisplayAds(false);
                  Ads.AdsMillenialMedia.enableDisplayAds(false);
                  Ads.AdsInmobi.enableDisplayAds(true);
                  Ads.AdsInnerActive.enableDisplayAds(false);
           }else if(Ads.AdsInnerActive.isAdsLoadOK)
           {
                  Ads.AdsMobfox.enableDisplayAds(false);
                  Ads.AdsMillenialMedia.enableDisplayAds(false);
                  Ads.AdsInmobi.enableDisplayAds(false);
                  Ads.AdsInnerActive.enableDisplayAds(true);
           }
        }

        public static void init(MMBannerAdView _mmAdView)
        {
            Ads.AdsMillenialMedia.mmAdView = _mmAdView;


        }
    }
}
