using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace RE_SHMUP
{
    public class RE_SHMUPGame : Game
    {
        MouseState _currentMouseState;
        MouseState _priorMouseState;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        #region Textures

        private Texture2D moon;
        private Texture2D colony;
        private Texture2D jupiter;
        private Texture2D basicStar;
        private List<Vector2> starPlacements;
        private Texture2D menuButtonTexture;
        private Texture2D mousePointer;
        #endregion

        #region Buttons
        public Button languageButton;
        public Button quitButton;
        public Button startButton;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public RE_SHMUPGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
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
            basicStar = Content.Load<Texture2D>("BasicStar");

            mousePointer = Content.Load<Texture2D>("Pointer");


            starPlacements = new List<Vector2>();

            Random random = new Random();

            //Randomly create star coordinates
            for (int i = 0; i < 200; i++)
            {
                float x = random.Next(0, _graphics.PreferredBackBufferWidth);
                float y = random.Next(0, _graphics.PreferredBackBufferHeight);
                starPlacements.Add(new Vector2(x, y));
            }

            menuButtonTexture = Content.Load<Texture2D>("MenuButton-Smaller");

            _spriteFont = Content.Load<SpriteFont>("Meiryo");

            startButton = new Button(_spriteFont, menuButtonTexture);
            startButton.buttonPosition = new Vector2(650, 260);
            startButton._buttonText = Localization.GetText("StartButton");
            //Do not have a game to start yet
            //startButton.Click += ;

            languageButton = new Button(_spriteFont, menuButtonTexture);
            languageButton.buttonPosition = new Vector2(650, 330);
            languageButton._buttonText = Localization.GetText("LanguageLabel");
            languageButton.Click += LanguageButton_Click;

            quitButton = new Button(_spriteFont, menuButtonTexture);
            quitButton.buttonPosition = new Vector2(650, 400);
            quitButton._buttonText = Localization.GetText("QuitButton");
            quitButton.Click += QuitButton_Click;

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _priorMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            languageButton.Update(gameTime);
            quitButton.Update(gameTime);
            startButton.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (Vector2 pos in starPlacements)
            {
                _spriteBatch.Draw(basicStar, pos, Color.White);
                _spriteBatch.Draw(basicStar, pos, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 2f);
            }

            startButton.Draw(gameTime, _spriteBatch);
            languageButton.Draw(gameTime, _spriteBatch);
            quitButton.Draw(gameTime, _spriteBatch);

            _spriteBatch.Draw(jupiter, new Vector2(-150, 0), null, Color.White, 0, new Vector2(64, 64), 1f, SpriteEffects.None, 1);
            _spriteBatch.Draw(colony, new Vector2(200, 330), null, Color.White, 0, new Vector2(64, 64), 0.4f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(moon, new Vector2(100, 220), null, Color.White, 0, new Vector2(64, 64), 0.5f, SpriteEffects.None, 0f);

            _spriteBatch.Draw(mousePointer, new Vector2(_currentMouseState.X, _currentMouseState.Y), null, Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        #region Event handling
        /// <summary>
        /// Changes the language when connected button is clicked
        /// </summary>
        /// <param name="sender">The object signaling the event</param>
        /// <param name="e">Information about the event</param>
        private void LanguageButton_Click(object sender, System.EventArgs e)
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
            startButton._buttonText = Localization.GetText("StartButton");
            quitButton._buttonText = Localization.GetText("QuitButton");
        }

        /// <summary>
        /// Quits the game when connected button is pressed
        /// </summary>
        /// <param name="sender">The object signaling the event</param>
        /// <param name="e">Information about the event</param>
        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }
        #endregion
    }
}
