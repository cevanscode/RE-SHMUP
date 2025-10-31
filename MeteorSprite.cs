﻿using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using RE_SHMUP.Scenes;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace RE_SHMUP
{
    /// <summary>
    /// Sprite for a meteor object (evil)
    /// </summary>
    public class MeteorSprite
    {
        public Vector2 position;

        private Vector2 spriteOrigin;

        private Texture2D texture;

        private BoundingCircle bounds;

        private float rotationAngle;

        private Texture2D circleTexture;

        private Scene _sceneSpawnedOn;

        public Vector2 velocity;

        /// <summary>
        /// If the meteor has been destroyed
        /// </summary>
        public bool Destroyed { get; set; } = false;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">Position of a meteor</param>
        /// <param name="velocity">Velocity of a meteor</param>
        public MeteorSprite(Vector2 position, Vector2 velocity, Scene sceneSpawnedOn)
        {
            this.position = position;
            float radius = 16;
            this.bounds = new BoundingCircle(position, radius);
            this.velocity = velocity;
            _sceneSpawnedOn = sceneSpawnedOn;
        }

        /// <summary>
        /// Loads the content using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Meteor");
            circleTexture = content.Load<Texture2D>("CircleHitbox");

            spriteOrigin = new Vector2(texture.Width / 2f, texture.Height / 2f);

            float radius = texture.Width / 2f;
            this.bounds = new BoundingCircle(position, radius);
        }


        /// <summary>
        /// The updater
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += velocity;
            bounds.Center = position;

            rotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            rotationAngle %= circle;

            if (_sceneSpawnedOn is TestingScene)
            {
                if (position.X <= 0 || position.X + texture.Width >= Core.Graphics.PreferredBackBufferWidth)
                {
                    velocity.X *= -1;
                    position.X = Math.Clamp(position.X, 0, Core.Graphics.PreferredBackBufferWidth - texture.Width);
                    rotationAngle *= -1;
                }

                if (position.Y <= 0 || position.Y + texture.Height >= Core.Graphics.PreferredBackBufferHeight)
                {
                    velocity.Y *= -1;
                    position.Y = Math.Clamp(position.Y, 0, Core.Graphics.PreferredBackBufferHeight - texture.Height);
                    rotationAngle *= -1;
                }
            }
            else if (_sceneSpawnedOn is LevelScene)
            {
                if (position.X <= 48 || position.X + texture.Width >= 600)
                {
                    velocity.X *= -1;
                    position.X = Math.Clamp(position.X, 48, 600);
                    rotationAngle *= -1;
                }

                if (position.Y <= 48 || position.Y + texture.Height >= 432)
                {
                    velocity.Y *= -1;
                    position.Y = Math.Clamp(position.Y, 48, 432);
                    rotationAngle *= -1;
                }
            }
        }

        /// <summary>
        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Destroyed) return;

            Core.SpriteBatch.Draw(texture, position, null, Color.White, rotationAngle, spriteOrigin, 1.0f, SpriteEffects.None, 0f);

            //Show hitbox for testing

            //float scale = (bounds.Radius * 1f) / circleTexture.Width;
            //Core.SpriteBatch.Draw(circleTexture,
            //     bounds.Center,
            //     null,
            //     Color.Gold * 0.4f,
            //     0f,
            //     new Vector2(circleTexture.Width / 2f, circleTexture.Height / 2f),
            //     scale,
            //     SpriteEffects.None,
            //     0f);
        }

        /// <summary>
        /// Helps in calculating change for meteor collisions
        /// </summary>
        /// <param name="delta">The vector change</param>
        public void ChangeHelper(Vector2 delta)
        {
            position += delta;
            bounds.Center += delta;
        }
    }
}
