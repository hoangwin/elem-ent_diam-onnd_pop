Imports MillennialMedia.WP7.SDK

Partial Public Class MainPage
    Inherits PhoneApplicationPage

    Private _InterstitialAd As MMInterstitialAd

    ' Constructor
    Public Sub New()
        InitializeComponent()
        _InterstitialAd = New MMInterstitialAd()
    End Sub

    Public Sub Refresh_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        BannerAdView.GetAd("28911")
    End Sub

    Public Sub Fetch_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        _InterstitialAd.Fetch("28911",
                             Sub(adView As MMAdView) OutputTextBlock.Text = "Fetch Success!",
                             Sub(adView As MMAdView, exception As MMException) OutputTextBlock.Text = "Fetch Failure!")
    End Sub

    Public Sub Display_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        _InterstitialAd.Display("28911")
    End Sub

End Class
