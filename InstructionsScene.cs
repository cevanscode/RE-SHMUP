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
            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.A) || Core.Input.Keyboard.WasKeyJustPressed(Keys.Space))
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
            Core.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
