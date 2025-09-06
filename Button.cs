using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RE_SHMUP
{
    public class Button
    {
        #region Fields
        MouseState _currentMouseState;
        MouseState _priorMouseState;
        private SpriteFont _font;
        private bool _touch;
        private Texture2D _texture;
        #endregion

        #region Properties
        public event EventHandler Click;
        //public bool Clicked { get; private set; }

        public Color ButtonFontColor { get; set; }
        
        public Vector2 buttonPosition { get; set; }

        public Rectangle ButtonRect { get { return new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, _texture.Width, _texture.Height); } }

        public string _buttonText { get; set; }
        #endregion

        #region Methods

        public Button(SpriteFont font, Texture2D texture)
        {
            _font = font;
            _texture = texture;

            ButtonFontColor = Color.Black;
        }

        public void Update(GameTime gameTime)
        {
            _priorMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            var mouseRect = new Rectangle(_currentMouseState.X,
            _currentMouseState.Y, 1, 1);

            _touch = false;

            if (mouseRect.Intersects(ButtonRect))
            {
                _touch = true;

                //clicking here
                if (_currentMouseState.LeftButton == ButtonState.Released && _priorMouseState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this,new EventArgs());
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.White;

            if (_touch)
            {
                color = Color.Red;
            }

            spriteBatch.Draw(_texture, ButtonRect, color);

            if (!string.IsNullOrEmpty(_buttonText))
            {
                var x = ((ButtonRect.X + (ButtonRect.Width / 2)) - (_font.MeasureString(_buttonText).X / 2));
                var y = ((ButtonRect.Y + (ButtonRect.Height / 2)) - (_font.MeasureString(_buttonText).Y / 2));
                spriteBatch.DrawString(_font, _buttonText, new Vector2(x, y), Color.White);
            }
        }
        #endregion
    }
}
