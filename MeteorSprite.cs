using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE_SHMUP
{
    public class MeteorSprite
    {
        private Vector2 position;

        private Texture2D texture;

        private BoundingCircle bounds;

        public bool Destroyed { get; set; } = false;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Meteor");
        }

        /// <summary>
        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Destroyed) return;
            //animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //if (animationTimer > ANIMATION_SPEED)
            //{
            //    animationFrame++;
            //    if (animationFrame > 7) animationFrame = 0;
            //    animationTimer -= ANIMATION_SPEED;
            //}

            //var source = new Rectangle(animationFrame * 16, 0, 16, 16);
            //spriteBatch.Draw(texture, position, source, Color.White);
        }
    }
}
