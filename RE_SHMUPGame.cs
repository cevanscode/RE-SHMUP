using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RE_SHMUP
{
    public class RE_SHMUPGame : Game
    {
        //MouseState _currentMouseState;
        //MouseState _priorMouseState;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        #region Textures

        private Texture2D moon;
        private Texture2D colony;
        private Texture2D langButtonTexture;
        private Texture2D jupiter;
        private Texture2D menuButtonTexture;
        #endregion

        #region Buttons
        public Button languageButton;
        public Button quitButton;
        #endregion

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
            //Localization.SetLanguage("ja");

            //English mode
            Localization.SetLanguage("en");

            moon = Content.Load<Texture2D>("Moon");
            colony = Content.Load<Texture2D>("SpaceColony");
            jupiter = Content.Load<Texture2D>("jupiter-512x512");
            menuButtonTexture = Content.Load<Texture2D>("MenuButton-Smaller");
            langButtonTexture = Content.Load<Texture2D>("LangButton");

            _spriteFont = Content.Load<SpriteFont>("Meiryo");

            languageButton = new Button(_spriteFont, menuButtonTexture);
            languageButton.buttonPosition = new Vector2(650, 330);
            languageButton._buttonText = Localization.GetText("LanguageLabel");
            languageButton.Click += LanguageButton;

            quitButton = new Button(_spriteFont, menuButtonTexture);
            quitButton.buttonPosition = new Vector2(650, 400);
            quitButton._buttonText = Localization.GetText("QuitButton");
            quitButton.Click += QuitButton_Click;

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            languageButton.Update(gameTime);
            quitButton.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            languageButton.Draw(gameTime, _spriteBatch);

            //string startButtonString = Localization.GetText("StartButtonString");
            //string quitButtonString = Localization.GetText("QuitButton");

            languageButton.Draw(gameTime, _spriteBatch);
            quitButton.Draw(gameTime, _spriteBatch);

            _spriteBatch.Draw(jupiter, new Vector2(-150, 0), null, Color.White, 0, new Vector2(64, 64), 1f, SpriteEffects.None, 1);
            _spriteBatch.Draw(colony, new Vector2(200, 330), null, Color.White, 0, new Vector2(64, 64), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(moon, new Vector2(200, 220), null, Color.White, 0, new Vector2(64, 64), 1f, SpriteEffects.None, 0f);



            //_spriteBatch.DrawString(_spriteFont, startButtonString, new Vector2(100, 100), Color.White);
            //_spriteBatch.DrawString(_spriteFont, quitButtonString, new Vector2(100, 100), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void LanguageButton(object sender, System.EventArgs e)
        {
            if (Localization.CurrentCulture.TwoLetterISOLanguageName == "ja")
            {
                Localization.SetLanguage("en");
                languageButton._buttonText = Localization.GetText("LanguageLabel");
            }
            else
            {
                Localization.SetLanguage("ja");
                languageButton._buttonText = Localization.GetText("LanguageLabel");
            }
            // Update button text so it refreshes immediately
            quitButton._buttonText = Localization.GetText("QuitButton");
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }
    }
}
