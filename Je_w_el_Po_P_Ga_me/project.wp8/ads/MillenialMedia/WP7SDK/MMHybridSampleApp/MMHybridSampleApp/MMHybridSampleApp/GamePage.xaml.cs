using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MillennialMedia.WP7.SDK;

namespace MMHybridSampleApp
{
    /// <summary>
    /// Sample XNA game with Millennial Silverlight SDK
    /// </summary>
    public partial class GamePage
    {
        #region Sample App Locals

        /// <summary>
        /// Value: "Millennial Media: "
        /// </summary>
        private const String LogPrefix = "Millennial Media: ";

        private readonly ContentManager _contentManager;
        private readonly GameTimer _timer;
        private readonly IList<Ball> _balls = new List<Ball>();

        private Texture2D _ballTexture;
        private UIElementRenderer _elementRenderer;
        private SpriteFont _messageFont;
        private SpriteBatch _spriteBatch;
        private bool _touching;

        #endregion

        // Instantiate this object for the purposes of displaying interstitial ads at some point.
        private readonly MMInterstitialAd _interstitialAd = new MMInterstitialAd();

        public GamePage()
        {
            #region Sample App Construction

            InitializeComponent();

            // Get the content manager from the application
            _contentManager = ((App)Application.Current).Content;

            // Create a timer for this page
            _timer = new GameTimer { UpdateInterval = TimeSpan.FromTicks(333333) };
            _timer.Update += onUpdate;
            _timer.Draw += onDraw;

            LayoutUpdated += gamePageLayoutUpdated;

            #endregion

            // Required call to display the initial banner ad. Replace "MMSDK.DEFAULT_APID" with your own APID.
            bannerAdView.GetAd(MMSDK.DEFAULT_APID);
            // Also set the the RefreshApid property if you plan on having the banner ad automatically refresh itself.
            bannerAdView.RefreshApid = MMSDK.DEFAULT_APID;

            // See the markup in GamePage.xaml for an example of how to integrate a banner ad in your layout.

            // Register a delegate with this event handler to capture detailed events from the SDK
            MMSDK.MMAdEvents += mmSdkOnMmAdEvents;
        }

        #region Event Handlers

        /// <summary>
        /// Handles detailed ad events raised by the SDK, including pausing the game if necessary.
        /// </summary>
        /// <param name="sender">The ad object which caused the event to be raised.</param>
        /// <param name="e">The event descriptor</param>
        private void mmSdkOnMmAdEvents(object sender, MMEventArgs e)
        {
            if (e.Type == MMEventType.OverlayOpened)
            {
                // A full-screen ad is being shown. Pause the game state until after the ad is dismissed.
                _timer.Stop();
                SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);
            }
            else if (e.Type == MMEventType.OverlayClosed)
            {
                // The full-screen ad has been dismissed. Resume the game state.
                SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);
                _timer.Start();
            }
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

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("{0}Refreshing ad.", LogPrefix);
            bannerAdView.GetAd(MMSDK.DEFAULT_APID);
        }

        private void fetchButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("{0}Fetching interstitial ad.", LogPrefix);
            _interstitialAd.Fetch(MMSDK.DEFAULT_APID);
        }

        private void displayButton_Click(object sender, RoutedEventArgs e)
        {
            // Displays the interstitial ad, if available. Display() will return true if an ad was available and is now
            // being displayed on the screen, otherwise false will be returned.
            Boolean outcome = _interstitialAd.Display();

            Debug.WriteLine(outcome ? "{0}Cached ad is displaying..." : "{0}Cached ad is not ready.", LogPrefix);

        }

        #endregion

        #region Sample App Implementation

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);
            _ballTexture = _contentManager.Load<Texture2D>(@"Images/Ball");
            _messageFont = _contentManager.Load<SpriteFont>(@"Fonts/Arial");

            // Start the timer
            _timer.Start();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the timer since this page is no longer active.
            // This logic is important if a video ad is show, for example.
            _timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }

        void gamePageLayoutUpdated(object sender, EventArgs e)
        {
            // Create the UIElementRenderer to draw the XAML page to a texture.

            // Check for 0 because when we navigate away the LayoutUpdate event
            // is raised but ActualWidth and ActualHeight will be 0 in that case.
            if ((ActualWidth > 0) && (ActualHeight > 0))
            {
                SharedGraphicsDeviceManager.Current.PreferredBackBufferWidth = (int)ActualWidth;
                SharedGraphicsDeviceManager.Current.PreferredBackBufferHeight = (int)ActualHeight;
            }

            if (null == _elementRenderer)
                _elementRenderer = new UIElementRenderer(this, (int)ActualWidth, (int)ActualHeight);
        }

        private Ball generateBall()
        {
            // Create a random seed suited for high frequency sequential loops
            var randomProvider = new RNGCryptoServiceProvider();
            var seedBytes = new byte[4];
            randomProvider.GetBytes(seedBytes);

            Random random = new Random(BitConverter.ToInt32(seedBytes, 0));

            Color ballColor = new Color(random.Next(255), random.Next(255), random.Next(255));
            Vector2 velocity = new Vector2((random.NextDouble() > .5 ? -1 : 1) * random.Next(9), (random.NextDouble() > .5 ? -1 : 1) * random.Next(9)) + Vector2.UnitX + Vector2.UnitY;
            Vector2 center = new Vector2((float)SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.Width / 2, (float)SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.Height / 2);
            float radius = 25f * (float)random.NextDouble() + 5f;

            return new Ball(this, ballColor, _ballTexture, center, velocity, radius);
        }

        private void handleTouches()
        {
            TouchCollection touches = TouchPanel.GetState();
            if (!_touching && touches.Count > 0)
            {
                _touching = true;

                foreach (TouchLocation touch in touches)
                {
                    if ((touch.State == TouchLocationState.Pressed))
                    {
                        if (touch.Position.Y < GameRectangle.ActualHeight)
                        {
                            _balls.Add(generateBall());
                        }
                    }
                }
            }
            else if (touches.Count == 0)
            {
                _touching = false;
            }
        }

        /// <summary>
        /// Allows the page to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        private void onUpdate(object sender, GameTimerEventArgs e)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                NavigationService.GoBack();

            // Start off with three balls
            if (_balls.Count < 3)
                _balls.Add(generateBall());


            // Intercept touch events
            handleTouches();

            // Update the balls
            foreach (var ball in _balls)
                ball.Update();
        }

        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void onDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.Black);

            _elementRenderer.Render();
            _spriteBatch.Begin();

            // Using the texture from the UIElementRenderer, 
            // draw the Silverlight controls to the screen.
            _spriteBatch.Draw(_elementRenderer.Texture, Vector2.Zero, Color.White);

            //If you are using the third-party tools suite, don't have the SDK draw the ad. Otherwise, uncomment the line below.
            _spriteBatch.DrawString(_messageFont, "Touch to add bouncing balls...", new Vector2(75, 400), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            foreach (var ball in _balls)
            {
                ball.Draw(_spriteBatch);
            }
            _spriteBatch.End();
        }

        #endregion
    }
}