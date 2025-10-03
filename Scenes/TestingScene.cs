using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace RE_SHMUP.Scenes
{
    /// <summary>
    /// A scene for testing basic gameplay mechinics (collisions, sprite animation, etc.)
    /// </summary>
    public class TestingScene : Scene
    {
        private List<MeteorSprite> meteors;

        private PlayerSprite player;

        private List<BulletSprite> bullets;

        private SpriteFont _spriteFont;

        private Texture2D basicStar;

        private List<Vector2> starPlacements;

        private int meteorCount = 10;

        private SoundEffect _shootSoundEffect;
        private SoundEffect _explodeSoundEffect;
        private SoundEffect _beamShotSoundEffect;

        /// <summary>
        /// Initializes content
        /// </summary>
        public override void Initialize()
        {
            player = new PlayerSprite();

            bullets = new List<BulletSprite>();

            System.Random rand = new System.Random();

            meteors = new List<MeteorSprite>();

            for (int i = 0; i < meteorCount; i++)
            {
                Vector2 randPos = new Vector2((float)rand.NextDouble() * Core.Graphics.PreferredBackBufferWidth,
                    (float)rand.NextDouble() * Core.Graphics.PreferredBackBufferHeight);
                Vector2 randVelocity = new Vector2(rand.Next(1, 3), 
                    rand.Next(1, 3));
                meteors.Add(new MeteorSprite(randPos, randVelocity));
            }

            base.Initialize();
        }

        /// <summary>
        /// The updater
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            //exit program 
            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.Back) || 
                Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
                Core.Instance.Exit();

            //reset to title
            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.Y) || 
                Core.Input.Keyboard.WasKeyJustPressed(Keys.R))
                Core.ChangeScene(new TitleScene());

            //Shoot
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Z) || 
                Core.Input.Keyboard.WasKeyJustPressed(Keys.Space) || 
                Core.Input.GamePads[0].WasButtonJustPressed(Buttons.A))
            {
                BulletSprite bullet = new BulletSprite(player.Bounds.Center + new Vector2(0, 16));
                bullet.LoadContent(Content);
                _shootSoundEffect.Play();

                bullets.Add(bullet);
            }

            foreach (var bullet in bullets)
            {
                bullet.Update(gameTime);
            }

            foreach (var meteor in meteors)
            {
                meteor.Update(gameTime);
            }

            player.Update(gameTime);

            //meteor x bullet x player
            foreach (var meteor in meteors)
            {
                if (!meteor.Destroyed && meteor.Bounds.CollidesWith(player.Bounds))
                {
                    Core.ChangeScene(new TestingScene());
                }

                foreach (var bullet in bullets)
                {
                    if (!meteor.Destroyed && meteor.Bounds.CollidesWith(bullet.Bounds))
                    {
                        meteor.Destroyed = true;
                        bullet.Hit = true;
                        meteorCount--;
                        _explodeSoundEffect.Play();
                    }
                }
            }

            //meteor x meteor
            for (int i = 0; i < meteors.Count; i++)
            {
                for (int j = i + 1; j < meteors.Count; j++)
                {
                    MeteorSprite m1 = meteors[i];
                    MeteorSprite m2 = meteors[j];

                    if (m1.Destroyed || m2.Destroyed) continue;

                    if (!m1.Bounds.CollidesWith(m2.Bounds)) continue;

                    Vector2 normal = m2.Bounds.Center - m1.Bounds.Center;
                    float distance = normal.Length();

                    if (distance < 0.0001f)
                    {
                        normal = new Vector2(1f, 0f);
                        distance = 0.0001f;
                    }
                    normal /= distance;

                    float overlap = (m1.Bounds.Radius + m2.Bounds.Radius) - distance;
                    if (overlap > 0f)
                    {
                        m1.ChangeHelper(-normal * overlap * 0.5f);
                        m2.ChangeHelper(normal * overlap * 0.5f);
                    }

                    Vector2 relativeVelocity = m1.velocity - m2.velocity;
                    float velocityOfNormal = Vector2.Dot(relativeVelocity, normal);

                    if (velocityOfNormal > 0) continue;

                    float the_bouncer = 1f;

                    float impulse = -(1f + the_bouncer) * velocityOfNormal / 2f;

                    Vector2 impulseVector = impulse * normal;

                    m1.velocity += impulseVector;
                    m2.velocity -= impulseVector;
                }
            }

            //kill bullets that are dead (hit something or went off top)
            bullets.RemoveAll(b => b.Hit || b.Bounds.Center.Y < 0);

            base.Update(gameTime);
        }

        /// <summary>
        /// Loads content
        /// </summary>
        public override void LoadContent()
        {
            player.LoadContent(Content);
            basicStar = Content.Load<Texture2D>("BasicStar");
            starPlacements = new List<Vector2>();
            _spriteFont = Content.Load<SpriteFont>("ArkPixel");

            _beamShotSoundEffect = Content.Load<SoundEffect>("beam_weapon");
            _explodeSoundEffect = Content.Load<SoundEffect>("explode");
            _shootSoundEffect = Content.Load<SoundEffect>("shoot");

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

            foreach (BulletSprite b in bullets)
            {
                b.LoadContent(Content);
            }

            base.LoadContent();
        }

        /// <summary>
        /// Draws content
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);

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

            foreach (MeteorSprite m in meteors)
            {
                m.Draw(gameTime, Core.SpriteBatch);
            }

            foreach (BulletSprite b in bullets)
            {
                b.Draw(gameTime, Core.SpriteBatch);
            }

            if (meteorCount == 0)
            {
                Core.SpriteBatch.DrawString(_spriteFont,
                    Localization.GetText("GoodJobString"),
                    new Vector2(100, 100),
                    Color.White,
                    0f,
                    new Vector2(0, 0),
                    2f,
                    SpriteEffects.None,
                    0f);
            }

            player.Draw(gameTime, Core.SpriteBatch);

            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
