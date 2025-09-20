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
            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.Back) ||
                Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
                Core.Instance.Exit();

            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.Y) ||
                Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
                Core.ChangeScene(new TitleScene());

            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.A) || 
                Core.Input.Keyboard.WasKeyJustPressed(Keys.Space))
                Core.ChangeScene(new TestingScene());

            base.Update(gameTime);
        }

        /// <summary>
        /// Loads content
        /// </summary>
        public override void LoadContent()
        {
            _spriteFont = Content.Load<SpriteFont>("ArkPixel");

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
                Localization.GetText("FocusControlsString"),
                new Vector2(100, 250),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("ResetControlsString"),
                new Vector2(100, 300),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("QuitControlsString"),
                new Vector2(100, 350),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("StartGameString"),
                new Vector2(100, 400),
                Color.White,
                0f,
                new Vector2(0, 0),
                1f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
