using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


namespace RE_SHMUP.Scenes
{
    public class TestingScene : Scene
    {
        private MeteorSprite[] meteors;
        private List<Vector2> meteorPlacements;
        private PlayerSprite player;
        private BulletSprite[] bullets;
        private SpriteFont SpriteFont;
        private Texture2D basicStar;
        private List<Vector2> starPlacements;

        public override void Initialize()
        {
            player = new PlayerSprite();

            System.Random rand = new System.Random();
            meteors = new MeteorSprite[]
            {
                new MeteorSprite(new Vector2((float)rand.NextDouble() * Core.Graphics.PreferredBackBufferWidth, (float)rand.NextDouble() * Core.Graphics.PreferredBackBufferHeight)),
                new MeteorSprite(new Vector2((float)rand.NextDouble() * Core.Graphics.PreferredBackBufferWidth, (float)rand.NextDouble() * Core.Graphics.PreferredBackBufferHeight)),
                new MeteorSprite(new Vector2((float)rand.NextDouble() * Core.Graphics.PreferredBackBufferWidth, (float)rand.NextDouble() * Core.Graphics.PreferredBackBufferHeight)),
                new MeteorSprite(new Vector2((float)rand.NextDouble() * Core.Graphics.PreferredBackBufferWidth, (float)rand.NextDouble() * Core.Graphics.PreferredBackBufferHeight)),
                new MeteorSprite(new Vector2((float)rand.NextDouble() * Core.Graphics.PreferredBackBufferWidth, (float)rand.NextDouble() * Core.Graphics.PreferredBackBufferHeight)),
            };

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

                //foreach (var bullet in bullets)
                //{
                //    if (!meteor.Destroyed && meteor.Bounds.CollidesWith(bullet.Bounds))
                //    {
                //        meteor.Destroyed = true;
                //    }
                //}
            }

            base.Update(gameTime);
        }

        public override void LoadContent()
        {
            player.LoadContent(Content);
            basicStar = Content.Load<Texture2D>("BasicStar");
            starPlacements = new List<Vector2>();

            Random random = new Random();

            //Randomly create star coordinates
            for (int i = 0; i < 200; i++)
            {
                float x = random.Next(0, Core.Graphics.PreferredBackBufferWidth);
                float y = random.Next(0, Core.Graphics.PreferredBackBufferHeight);
                starPlacements.Add(new Vector2(x, y));
            }

            foreach (MeteorSprite m in meteors)
            {
                m.LoadContent(Content);
            }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);

            Core.SpriteBatch.Begin();

            foreach (Vector2 pos in starPlacements)
            {
                Core.SpriteBatch.Draw(basicStar, pos, Color.White);
                Core.SpriteBatch.Draw(basicStar, pos, null, Color.White * 0.6f, 0, new Vector2(0, 0), 1, SpriteEffects.None, 2f);
            }

            foreach (MeteorSprite m in meteors)
            {
                m.Draw(gameTime, Core.SpriteBatch);
            }

            player.Draw(gameTime, Core.SpriteBatch);

            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
