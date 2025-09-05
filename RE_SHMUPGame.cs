using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RE_SHMUP
{
    public class RE_SHMUPGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        public RE_SHMUPGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Localization.SetLanguage("ja");

            //Localization.SetLanguage("en");

            //_spriteFont = Content.Load<SpriteFont>("Yu Gothic");
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            string startTest = Localization.GetText("StartButtonString");

            _spriteBatch.DrawString(_spriteFont, startTest, new Vector2(100, 100), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
