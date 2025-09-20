using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using RE_SHMUP.Scenes;

namespace RE_SHMUP
{
    public class InstructionsScene : Scene
    {
        private SpriteFont _spriteFont;

        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.Back) ||
                Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
                Core.Instance.Exit();

            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.A) || 
                Core.Input.Keyboard.WasKeyJustPressed(Keys.Space))
                Core.ChangeScene(new TestingScene());

            base.Update(gameTime);
        }

        public override void LoadContent()
        {
            _spriteFont = Content.Load<SpriteFont>("ArkPixel");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Core.SpriteBatch.Begin();

            Core.GraphicsDevice.Clear(Color.Black);

            //Instruction strings follow here

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText(""),
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
