using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using MonoGameLibrary;
using RE_SHMUP.Scenes;

namespace RE_SHMUP
{
    public class RE_SHMUPGame : Core
    {
        #region For_Calcs
        MouseState _currentMouseState;
        MouseState _priorMouseState;
        private Vector2 _cometPosition;
        private Vector2 _cometVelocity;
        #endregion

        #region Textures
        private SpriteFont _spriteFont;
        private Texture2D moon;
        private Texture2D colony;
        private Texture2D jupiter;
        private Texture2D basicStar;
        private List<Vector2> starPlacements;
        private Texture2D menuButtonTexture;
        private Texture2D mousePointer;
        private Texture2D comet;
        #endregion

        #region Buttons
        public Button languageButton;
        public Button quitButton;
        public Button startButton;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public RE_SHMUPGame() : base("RE_SHMUP", 800, 480, false)
        {
            IsMouseVisible = false;
        }

        /// <summary>
        /// Startup stuff
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            ChangeScene(new TitleScene());
        }

        /// <summary>
        /// Loads content
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

            //Japanese mode
            //Localization.SetLanguage("ja");

            //English mode
            Localization.SetLanguage("en");

            moon = Content.Load<Texture2D>("Moon");
            colony = Content.Load<Texture2D>("SpaceColony");
            jupiter = Content.Load<Texture2D>("jupiter-512x512");
            basicStar = Content.Load<Texture2D>("BasicStar");
            comet = Content.Load<Texture2D>("Comet");

            _cometPosition = new Vector2(-10, 140);
            _cometVelocity = new Vector2(5, 0);

            mousePointer = Content.Load<Texture2D>("Pointer");

            starPlacements = new List<Vector2>();

            Random random = new Random();

            //Randomly create star coordinates
            for (int i = 0; i < 200; i++)
            {
                float x = random.Next(0, Graphics.PreferredBackBufferWidth);
                float y = random.Next(0, Graphics.PreferredBackBufferHeight);
                starPlacements.Add(new Vector2(x, y));
            }

            menuButtonTexture = Content.Load<Texture2D>("MenuButton-Smaller");

            _spriteFont = Content.Load<SpriteFont>("ArkPixel");

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

        /// <summary>
        /// Updates the game state repeatedly
        /// </summary>
        /// <param name="gameTime"></param>
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

            _cometPosition += _cometVelocity;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws onto the game canvas
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            SpriteBatch.Begin();

            foreach (Vector2 pos in starPlacements)
            {
                SpriteBatch.Draw(basicStar, pos, Color.White);
                SpriteBatch.Draw(basicStar, pos, null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 2f);
            }

            startButton.Draw(gameTime, SpriteBatch);
            languageButton.Draw(gameTime, SpriteBatch);
            quitButton.Draw(gameTime, SpriteBatch);

            SpriteBatch.Draw(comet, _cometPosition, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);

            SpriteBatch.Draw(jupiter, new Vector2(-150, 0), null, Color.White, 0, new Vector2(64, 64), 1f, SpriteEffects.None, 1);
            SpriteBatch.Draw(colony, new Vector2(200, 330), null, Color.White, 0, new Vector2(64, 64), 0.4f, SpriteEffects.None, 0);
            SpriteBatch.Draw(moon, new Vector2(100, 220), null, Color.White, 0, new Vector2(64, 64), 0.5f, SpriteEffects.None, 0);

            SpriteBatch.Draw(mousePointer, new Vector2(_currentMouseState.X, _currentMouseState.Y), null, Color.White, 0, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);
            SpriteBatch.DrawString(_spriteFont, Localization.GetText("TempTitleString"), new Vector2(400, 100), Color.Red, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);

            SpriteBatch.End();

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
