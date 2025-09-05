using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;

namespace RE_SHMUP
{
    public class RE_SHMUPGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        private Texture2D moon;

        /// <summary>
        /// Constructor
        /// </summary>
        public RE_SHMUPGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Startup stuff
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Japanese mode
            Localization.SetLanguage("ja");

            //English mode
            //Localization.SetLanguage("en");

            moon = Content.Load<Texture2D>("Moon");

            _spriteFont = Content.Load<SpriteFont>("Meiryo");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            string startButtonString = Localization.GetText("StartButtonString");
            string quitButtonString = Localization.GetText("QuitButton");

            _spriteBatch.DrawString(_spriteFont, startButtonString, new Vector2(100, 100), Color.White);
            _spriteBatch.DrawString(_spriteFont, quitButtonString, new Vector2(100, 100), Color.White);

            _spriteBatch.Draw(moon, new Vector2(200, 200), new Rectangle(0, 0, 128, 128), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
