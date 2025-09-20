using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace RE_SHMUP
{
    public class MeteorSprite
    {
        private Vector2 position;

        private Texture2D texture;

        private BoundingCircle bounds;


        private Texture2D circleTexture;

        public Vector2 velocity;

        /// <summary>
        /// If the meteor has been destroyed
        /// </summary>
        public bool Destroyed { get; set; } = false;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        public MeteorSprite(Vector2 position, Vector2 velocity)
        {
            this.position = position;
            this.bounds = new BoundingCircle(position - new Vector2(-16, -16), 8);
            this.velocity = velocity;
        }

        /// <summary>
        /// Loads the content using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Meteor");
            circleTexture = content.Load<Texture2D>("CircleHitbox");
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            bounds.Center += velocity;

            if (position.X <= 0|| position.X >= Core.Graphics.PreferredBackBufferWidth)
            {
                velocity *= -1;
                position.X = Math.Clamp(position.X, 0, Core.Graphics.PreferredBackBufferWidth);
            }
            if (position.Y <= 0 || position.Y >= Core.Graphics.PreferredBackBufferHeight)
            {
                velocity *= -1;
                position.Y = Math.Clamp(position.Y, 0, Core.Graphics.PreferredBackBufferHeight);
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

            Core.SpriteBatch.Draw(texture, position, null, Color.White);

            //Show hitbox for testing

            float scale = (bounds.Radius * 2f) / circleTexture.Width;

            Core.SpriteBatch.Draw(circleTexture,
                 bounds.Center,
                 null,
                 Color.Gold * 0.4f,
                 0f,
                 new Vector2(circleTexture.Width / 2f, circleTexture.Height / 2f),
                 scale,
                 SpriteEffects.None,
                 0f);

        }
    }
}
