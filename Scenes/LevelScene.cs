﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using RE_SHMUP.Scenes;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RE_SHMUP
{
    public class LevelScene : Scene, IParticleEmitter
    {
        #region Important fields for making stuff
        private SpriteFont _spriteFont;
        private BasicTilemap _tilemap;
        #endregion

        #region Particle Stuff
        ExplosionParticleSystem _explosions;
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        #endregion

        #region Sound Stuff
        private SoundEffect _shootSoundEffect;
        private SoundEffect _explodeSoundEffect;
        private SoundEffect _beamShotSoundEffect;
        #endregion

        #region Larger Screen Transforms
        private bool _shaking;
        private float _shakeTime;
        #endregion

        #region Sprites/Objects
        private List<MeteorSprite> meteors;
        private int meteorCount = 10;

        private PlayerSprite player;

        private List<BulletSprite> bullets;

        private List<MissileSprite> missiles;
        
        private Texture2D basicStar;
        private List<Vector2> starPlacements;
        #endregion


        #region Bomb fields

        private int bombCount = 3;

        private bool bombActive = false;

        private float bombDuration = 2f; // iframes

        private float bombTimer = 0f;

        private bool bombWaveActive = false;

        private float bombWaveRadius = 0f;

        private float bombWaveMaxRadius;

        private Vector2 bombWaveCenter;

        private float bombWaveSpeed = 800f;

        #endregion

        #region Score Saving
        private float _survivalTime = 0f;

        private float _lastSurvivalTime = 0f;

        public static float _bestSurvivalTime = 0f;

        private int _score = 0;

        /// <summary>
        /// Name of the player who achieved a score
        /// </summary>
        private string _scorerName;

        private bool _playerDead = false;

        private bool _timerStart = false;
        #endregion

        /// <summary>
        /// Initializes content
        /// </summary>
        public override void Initialize()
        {
            player = new PlayerSprite();

            bullets = new List<BulletSprite>();

            missiles = new List<MissileSprite>();

            System.Random rand = new System.Random();

            meteors = new List<MeteorSprite>();

            _explosions = new ExplosionParticleSystem(Core.Instance, 20);
            Core.Instance.Components.Add(_explosions);

            for (int i = 0; i < meteorCount; i++)
            {
                Vector2 randPos = new Vector2((float)rand.NextDouble() * Core.Graphics.PreferredBackBufferWidth,
                    (float)rand.NextDouble() * Core.Graphics.PreferredBackBufferHeight);
                Vector2 randVelocity = new Vector2(rand.Next(1, 3),
                    rand.Next(1, 3));
                meteors.Add(new MeteorSprite(randPos, randVelocity));
            }

            bombWaveMaxRadius = MathF.Max(Core.Graphics.PreferredBackBufferWidth, Core.Graphics.PreferredBackBufferHeight);

            //get best time
            try
            {
                string savePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "RE_SHMUP",
                    "bestTime.txt");

                if (File.Exists(savePath))
                {
                    string content = File.ReadAllText(savePath);
                    float.TryParse(content, out _bestSurvivalTime);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading best time: " + ex.Message);
            }

            base.Initialize();
        }

        /// <summary>
        /// The updater
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            #region Controls
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

            if ((Core.Input.Keyboard.WasKeyJustPressed(Keys.X) ||
                 Core.Input.GamePads[0].WasButtonJustPressed(Buttons.B)) &&
                 bombCount > 0 && !bombActive)
            {
                ActivateBomb();
            }

            if (bombWaveActive)
            {
                bombWaveRadius += bombWaveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                foreach (MeteorSprite meteor in meteors)
                {
                    if (!meteor.Destroyed)
                    {
                        float distance = Vector2.Distance(bombWaveCenter, meteor.position);
                        if (distance < bombWaveRadius)
                        {
                            meteor.Destroyed = true;
                            meteorCount--;
                            _explodeSoundEffect.Play();
                            _explosions.PlaceExplosion(meteor.position);
                        }
                    }
                }


                foreach (MissileSprite missile in missiles)
                {
                    if (!missile.Destroyed)
                    {
                        float distance = Vector2.Distance(bombWaveCenter, missile.position);
                        if (distance < bombWaveRadius)
                        {
                            missile.Destroyed = true;
                            _score += 100;
                            _explodeSoundEffect.Play();
                            _explosions.PlaceExplosion(missile.position);
                        }
                    }
                }

                if (bombWaveRadius >= bombWaveMaxRadius)
                {
                    bombWaveActive = false;
                }
            }


            foreach (var bullet in bullets)
            {
                bullet.Update(gameTime);
            }
            #endregion

            foreach (var meteor in meteors)
            {
                meteor.Update(gameTime);
            }

            foreach (var missile in missiles)
            {
                missile.Update(gameTime);
            }

            player.Update(gameTime);

            //meteor x bullet x player
            foreach (var meteor in meteors)
            {
                if (!bombActive && !meteor.Destroyed && meteor.Bounds.CollidesWith(player.Bounds))
                {
                    Core.ChangeScene(new LevelScene()); //this will change to destroy an Orbiter when they are added
                }

                foreach (var bullet in bullets)
                {
                    if (!meteor.Destroyed && meteor.Bounds.CollidesWith(bullet.Bounds))
                    {
                        meteor.Destroyed = true;
                        bullet.Hit = true;
                        meteorCount--;
                        _explodeSoundEffect.Play();
                        _explosions.PlaceExplosion(meteor.position);
                        _shakeTime = 0;
                        _shaking = true;
                    }
                }
            }

            foreach (var missile in missiles)
            {
                if (!missile.Destroyed && missile.Bounds.CollidesWith(player.Bounds))
                {
                    _playerDead = true;
                    _lastSurvivalTime = _survivalTime;
                    if (_lastSurvivalTime > _bestSurvivalTime)
                    {
                        _bestSurvivalTime = _lastSurvivalTime;

                        //saving the best time
                        try
                        {
                            string savePath = Path.Combine(
                                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                "RE_SHMUP");

                            Directory.CreateDirectory(savePath);

                            string filePath = Path.Combine(savePath, "bestTime.txt");

                            using (StreamWriter sw = new StreamWriter(filePath))
                            {
                                sw.WriteLine(_bestSurvivalTime);
                            }

                            System.Diagnostics.Debug.WriteLine($"Best time saved to: {filePath}");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Error writing best time: " + ex.Message);
                        }
                    }

                    Core.ChangeScene(new LevelScene()); //this will change to destroy an Orbiter when they are added
                }

                if (missile.position.Y > Core.Graphics.PreferredBackBufferHeight && !missile.Destroyed)
                {
                    missile.Destroyed = true;
                }

                foreach (var bullet in bullets)
                {
                    if (!missile.Destroyed && missile.Bounds.CollidesWith(bullet.Bounds))
                    {
                        missile.Destroyed = true;
                        bullet.Hit = true;
                        _explodeSoundEffect.Play();
                        _explosions.PlaceExplosion(missile.position);
                        _shakeTime = 0;
                        _shaking = true;
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

            if (bombActive)
            {
                bombTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (bombTimer <= 0)
                {
                    bombActive = false;
                }
            }

            //kill bullets that are dead (hit something or went off top)
            bullets.RemoveAll(b => b.Hit || b.Bounds.Center.Y < 0);

            //kill all missiles that are dead (destroyed by bomb/bullet or went off bottom
            missiles.RemoveAll(m => m.Destroyed);

            if (!_playerDead && _timerStart)
            {
                _survivalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

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

            Song noisy = Content.Load<Song>("noisy_battle");

            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }

            MediaPlayer.Play(noisy);

            MediaPlayer.IsRepeating = true;

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

            foreach (MissileSprite m in missiles)
            {
                m.LoadContent(Content);
            }

            foreach (BulletSprite b in bullets)
            {
                b.LoadContent(Content);
            }

            _tilemap = Content.Load<BasicTilemap>("Tilemap");

            base.LoadContent();
        }

        /// <summary>
        /// Draws content
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Black);

            _tilemap.Draw(gameTime, Core.SpriteBatch);

            Matrix shakeTransform = Matrix.Identity;
            if (_shaking)
            {
                _shakeTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                // Matrix shakeRotation = Matrix.CreateRotationZ(MathF.Cos(_shakeTime));
                Matrix shakeTranslation = Matrix.CreateTranslation(10 * MathF.Sin(_shakeTime), 10 * MathF.Cos(_shakeTime), 0);
                shakeTransform = shakeTranslation;
                if (_shakeTime > 500) _shaking = false;
            }

            Core.SpriteBatch.Begin(transformMatrix: shakeTransform);

            Core.SpriteBatch.DrawString(_spriteFont,
                $"Bombs: {bombCount}",
                new Vector2(10, 10),
                Color.White);

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

            foreach (MissileSprite m in missiles)
            {
                m.Draw(gameTime, Core.SpriteBatch);
            }

            Core.SpriteBatch.DrawString(_spriteFont,
                Localization.GetText("BestTimeString") + $": {_bestSurvivalTime:F2}s",
                new Vector2(10, 30),
                Color.White);

            if (meteorCount == 0)
            {
                if (_survivalTime < 5)
                {
                    Core.SpriteBatch.DrawString(_spriteFont,
                        Localization.GetText("DefeatPiratesString"),
                        new Vector2(100, 100),
                        Color.White,
                        0f,
                        new Vector2(0, 0),
                        2f,
                        SpriteEffects.None,
                        0f);
                }
                _timerStart = true;

                Core.SpriteBatch.DrawString(_spriteFont,
                    Localization.GetText("TimeString") + $": {_survivalTime:F2}s",
                    new Vector2(10, 50),
                    Color.White);
            }

            if (bombActive && bombTimer > bombDuration - 0.1f)
            {
                Core.SpriteBatch.Draw(basicStar,
                    new Rectangle(0, 0, Core.Graphics.PreferredBackBufferWidth, Core.Graphics.PreferredBackBufferHeight),
                    Color.White * 0.5f);
            }

            if (bombWaveActive)
            {
                float scale = bombWaveRadius / (basicStar.Width / 2f);

                Core.SpriteBatch.Draw(basicStar,
                    bombWaveCenter,
                    null,
                    Color.Cyan * 0.25f,
                    0f,
                    new Vector2(basicStar.Width / 2f, basicStar.Height / 2f),
                    scale,
                    SpriteEffects.None,
                    0f);
            }

            player.Draw(gameTime, Core.SpriteBatch);

            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Makes a bomb go off, sending out its wave
        /// </summary>
        private void ActivateBomb()
        {
            bombActive = true;
            bombTimer = bombDuration;
            bombCount--;

            bombWaveActive = true;
            bombWaveRadius = 0f;
            bombWaveCenter = new Vector2(
                Core.Graphics.PreferredBackBufferWidth / 2f,
                Core.Graphics.PreferredBackBufferHeight / 2f
            );

            bullets.Clear();
        }
    }
}
