using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMHybridSampleApp
{
    internal class Ball
    {
        private readonly Color _color;
        private readonly GamePage _gamePage;
        private readonly float _radius;
        private readonly Texture2D _texture;
        private float _scale;
        private Vector2 _topLeft;
        private Vector2 _velocity;

        public Ball(GamePage gamePage, Color color, Texture2D texture, Vector2 center, Vector2 velocity, float radius)
        {
            _gamePage = gamePage;
            _color = color;
            _texture = texture;
            _topLeft = new Vector2(center.X - radius, center.Y - radius);
            _velocity = velocity;
            _radius = radius;
            calculateScale();
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(_texture, _topLeft, null, _color, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
        }

        public void Update()
        {
            bounceBall();
            _topLeft += _velocity;
        }

        private void bounceBall()
        {
            Vector2 newTopLeft = _topLeft + _velocity;
            float left = newTopLeft.X;
            float right = newTopLeft.X + (_radius*2);
            float top = newTopLeft.Y;
            float bottom = newTopLeft.Y + (_radius*2);

            if ((top < 0) || (bottom > _gamePage.GameRectangle.ActualHeight))
            {
                _velocity.Y *= -1;
            }

            if ((left < 0) || (right > SharedGraphicsDeviceManager.Current.GraphicsDevice.Viewport.Width))
            {
                _velocity.X *= -1;
            }
        }

        private void calculateScale()
        {
            float width = _texture.Bounds.Width;
            _scale = (_radius*2)/width;
        }
    }
}