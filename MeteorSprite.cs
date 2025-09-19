using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace RE_SHMUP
{
    public class MeteorSprite
    {
        private Vector2 position;

        private Texture2D texture;

        private BoundingCircle bounds;

        //private Texture2D circleTexture;

        /// <summary>
        /// If the meteor has been destroyed
        /// </summary>
        public bool Destroyed { get; set; } = false;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        public MeteorSprite(Vector2 position)
        {
            this.position = position;
            this.bounds = new BoundingCircle(position - new Vector2(-16, -16), 8);
        }

        /// <summary>
        /// Loads the content using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Meteor");
            //circleTexture = content.Load<Texture2D>("CircleHitbox");
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

            //float scale = (bounds.Radius * 2f) / circleTexture.Width;

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
    }
}
