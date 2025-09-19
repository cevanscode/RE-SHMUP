using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;


namespace RE_SHMUP.Scenes
{
    public class TestingScene : Scene
    {
        private MeteorSprite[] meteors;
        private PlayerSprite player;
        private SpriteFont SpriteFont;

        public override void Initialize()
        {
            player = new PlayerSprite();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.Back) || Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
                Core.Instance.Exit();

            player.Update(gameTime);

            foreach (var meteor in meteors)
            {
                if (!meteor.Destroyed && meteor.Bounds.CollidesWith(player.Bounds))
                {
                    Core.ChangeScene(new TestingScene());
                }
            }

            base.Update(gameTime);
        }

        public override void LoadContent()
        {
            player.LoadContent(Content);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);

            //Core.SpriteBatch.Begin();

            player.Draw(gameTime, Core.SpriteBatch);

            //Core.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
