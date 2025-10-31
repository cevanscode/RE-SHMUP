using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace RE_SHMUP.Scenes
{
    /// <summary>
    /// A scene which provides instructions on how to play the game
    /// </summary>
    public class InstructionsScene : Scene
    {
        private SpriteFont _spriteFont;

        public Game _theGame;

        private Texture2D menuButtonTexture;
        private Button[] _theButtons = new Button[2];
        private ButtonHelper _buttonHelper = new ButtonHelper();
        private float prevStickY = 0;
        private float currStickY = 0;

        #region Buttons
        public Button missileTestButton;
        public Button levelButton;
        #endregion

        /// <summary>
        /// Initializes content
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// The updater
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            currStickY = Core.Input.GamePads[0].LeftThumbStick.Y;

            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.Back) || Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
                Core.Instance.Exit();

            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.Y) ||
                Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
                Core.ChangeScene(new TitleScene());

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Down)
                || Core.Input.GamePads[0].WasButtonJustPressed(Buttons.DPadDown)
                || Core.Input.GamePads[0].WasButtonJustPressed(Buttons.DPadDown)
                || (currStickY < -0.5f && prevStickY >= -0.5f))
            {
                _buttonHelper.IncrementSelection();
            }

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Up)
                || Core.Input.GamePads[0].WasButtonJustPressed(Buttons.DPadUp)
                || Core.Input.GamePads[0].WasButtonJustPressed(Buttons.DPadDown)
                || (currStickY < 0.5f && prevStickY >= 0.5f))
            {
                _buttonHelper.DecrementSelection();
            }

            // TODO: Add your update logic here
            missileTestButton.Update(gameTime);
            levelButton.Update(gameTime);

            prevStickY = currStickY;

            base.Update(gameTime);
        }

        /// <summary>
        /// Loads content
        /// </summary>
        public override void LoadContent()
        {
            _spriteFont = Content.Load<SpriteFont>("ArkPixel");

            menuButtonTexture = Content.Load<Texture2D>("MenuButton-Smaller");

            missileTestButton = new Button(_spriteFont, menuButtonTexture);
            missileTestButton.buttonPosition = new Vector2(650, 190);
            missileTestButton._buttonText = Localization.GetText("GoToTestString");
            missileTestButton.Click += MissileTestButton_Click;
            _theButtons[0] = missileTestButton;

            levelButton = new Button(_spriteFont, menuButtonTexture);
            levelButton.buttonPosition = new Vector2(650, 320);
            levelButton._buttonText = Localization.GetText("GoToBalloonString");
            levelButton.Click += LevelButton_Click;
            _theButtons[1] = levelButton;

            _theButtons[0].Selected = true;
            _buttonHelper.Buttons = _theButtons;

            base.LoadContent();
        }

        /// <summary>
        /// Draws content
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Draw(GameTime gameTime)
        {
            Core.SpriteBatch.Begin();

            Core.GraphicsDevice.Clear(Color.Black);

            missileTestButton.Draw(gameTime, Core.SpriteBatch);
            levelButton.Draw(gameTime, Core.SpriteBatch);

            //Instruction strings follow here

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("LysitheaPlotString"),
                new Vector2(100, 100),
                Color.White,
                0f,
                new Vector2(0, 0),
                2f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("MoveControlsString"),
                new Vector2(100, 150),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("ShootControlsString"),
                new Vector2(100, 200),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("BombControlsString"),
                new Vector2(100, 250),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("FocusControlsString"),
                new Vector2(100, 300),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("ResetControlsString"),
                new Vector2(100, 350),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("QuitControlsString"),
                new Vector2(100, 400),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("StartGameString"),
                new Vector2(100, 450),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Sets the game state to the testing scene
        /// </summary>
        /// <param name="sender">The object signaling the event</param>
        /// <param name="e">Information about the event</param>
        private void MissileTestButton_Click(object sender, System.EventArgs e)
        {
            Core.ChangeScene(new TestingScene());
        }

        /// <summary>
        /// Sets the game state to the level scene
        /// </summary>
        /// <param name="sender">The object signaling the event</param>
        /// <param name="e">Information about the event</param>
        private void LevelButton_Click(object sender, System.EventArgs e)
        {
            Core.ChangeScene(new LevelScene());
        }
    }
}
