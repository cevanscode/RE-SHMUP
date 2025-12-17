using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using RE_SHMUP._3D;


namespace RE_SHMUP.Scenes
{
    public class SplashScene : Scene
    {
        Cube _cube;
        RE_SHMUPGame _game;
        private SpriteFont _spriteFont;

        public SplashScene(RE_SHMUPGame game)
        {
            _game = game;
        }

        public override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            _spriteFont = Content.Load<SpriteFont>("ArkPixel");
            _cube = new Cube(_game);
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            _cube.Update(gameTime);

            if (gameTime.TotalGameTime.TotalSeconds >= 5
                || Core.Input.Keyboard.WasKeyJustPressed(Keys.Space)
                || Core.Input.Keyboard.WasKeyJustPressed(Keys.Z)
                || Core.Input.GamePads[0].WasButtonJustPressed(Buttons.A))
            {
                Core.ChangeScene(new TitleScene());
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);

            Core.SpriteBatch.Begin();

            // TODO: Add your drawing code here
            _cube.Draw();

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("splashScreenText1"),
                new Vector2(300, 80),
                Color.White,
                0f,
                new Vector2(0, 0),
                3f,
                SpriteEffects.None,
                0f);


            base.Draw(gameTime);

            Core.SpriteBatch.End();
        }
    }
}