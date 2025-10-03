using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;

namespace RE_SHMUP.Scenes
{
    public class TitleScene : Scene
    {
        #region For_Calcs
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

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.Back) || Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
                Core.Instance.Exit();

            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.A) || Core.Input.Keyboard.WasKeyJustPressed(Keys.Space))
                Core.ChangeScene(new InstructionsScene());

            // TODO: Add your update logic here
            languageButton.Update(gameTime);
            quitButton.Update(gameTime);
            startButton.Update(gameTime);

            _cometPosition += _cometVelocity;

            base.Update(gameTime);
        }

        public override void LoadContent()
        {
            //Japanese mode
            //Localization.SetLanguage("ja");

            //English mode
            Localization.SetLanguage("en");

            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }

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
                float x = random.Next(0, Core.Graphics.PreferredBackBufferWidth);
                float y = random.Next(0, Core.Graphics.PreferredBackBufferHeight);
                starPlacements.Add(new Vector2(x, y));
            }

            menuButtonTexture = Content.Load<Texture2D>("MenuButton-Smaller");

            _spriteFont = Content.Load<SpriteFont>("ArkPixel");

            startButton = new Button(_spriteFont, menuButtonTexture);
            startButton.buttonPosition = new Vector2(650, 260);
            startButton._buttonText = Localization.GetText("StartButton");
            startButton.Click += StartButton_Click;

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

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            Core.SpriteBatch.Begin();

            foreach (Vector2 pos in starPlacements)
            {
                Core.SpriteBatch.Draw(basicStar, pos, Color.White);
                Core.SpriteBatch.Draw(basicStar, 
                    pos, 
                    null, 
                    Color.White * 0.6f, 
                    0, 
                    new Vector2(0, 0), 
                    1, 
                    SpriteEffects.None, 
                    2f);
            }

            startButton.Draw(gameTime, Core.SpriteBatch);
            languageButton.Draw(gameTime, Core.SpriteBatch);
            quitButton.Draw(gameTime, Core.SpriteBatch);

            Core.SpriteBatch.Draw(comet, 
                _cometPosition, 
                null, 
                Color.White, 
                0, 
                new Vector2(0, 0), 
                1f, 
                SpriteEffects.None, 
                0);

            Core.SpriteBatch.Draw(jupiter, 
                new Vector2(-150, 0), 
                null, 
                Color.White, 
                0, 
                new Vector2(64, 64), 
                1f, 
                SpriteEffects.None, 
                1);

            Core.SpriteBatch.Draw(colony, 
                new Vector2(200, 330), 
                null, 
                Color.White, 
                0, 
                new Vector2(64, 64), 
                0.4f, 
                SpriteEffects.None, 
                0);

            Core.SpriteBatch.Draw(moon, 
                new Vector2(100, 220), 
                null, 
                Color.White, 
                0, 
                new Vector2(64, 64), 
                0.5f, 
                SpriteEffects.None, 
                0);

            Core.SpriteBatch.Draw(mousePointer, 
                new Vector2(Core.Input.Mouse.Position.X, Core.Input.Mouse.Position.Y), 
                null, 
                Color.White, 
                0, 
                new Vector2(0, 0), 
                2f, 
                SpriteEffects.None, 
                0f);

            Core.SpriteBatch.DrawString(_spriteFont, 
                Localization.GetText("TempTitleString"), 
                new Vector2(350, 80), 
                Color.Red, 
                0f, 
                new Vector2(0, 0), 
                3f, 
                SpriteEffects.None, 
                0f);

            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }

        #region Event handling
        /// <summary>
        /// Sets the game state to the game scene
        /// </summary>
        /// <param name="sender">The object signaling the event</param>
        /// <param name="e">Information about the event</param>
        private void StartButton_Click(object sender, System.EventArgs e)
        {
            Core.ChangeScene(new InstructionsScene());
        }

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
            Core.Instance.Exit();
        }
        #endregion
    }
}
