using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using RE_SHMUP.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE_SHMUP
{
    public class PirateMech
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
        /// If the mech has been destroyed
        /// </summary>
        public bool Destroyed { get; set; } = false;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">Position of the mech</param>
        /// <param name="velocity">Velocity of the mech</param>
        public PirateMech(Vector2 position, Vector2 velocity, Scene sceneSpawnedOn)
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
            texture = content.Load<Texture2D>("pirate-mech");
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
    }
}
