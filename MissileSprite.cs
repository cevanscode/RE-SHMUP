using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using SharpDX.Direct2D1.Effects;

namespace RE_SHMUP
{
    /// <summary>
    /// Sprite representing enemy missiles
    /// </summary>
    public class MissileSprite
    {
        private Texture2D texture;

        private BoundingCircle bounds;

        private int frameLength = 32;

        private int frameCount = 4;

        private const float ANIMATION_SPEED = 0.1f;

        private double animationTimer;

        private int animationFrame;

        private Vector2 origin;

        private float drawScale = 1f;

        public Vector2 position;

        public ProjectileType projectileType = ProjectileType.Kinetic;

        /// <summary>
        /// If the missile has been destroyed
        /// </summary>
        public bool Destroyed { get; set; } = false;

        //private Texture2D circleTexture;

        /// <summary>
        /// If the missile hits something (meteor/cieling)
        /// </summary>
        public bool Hit { get; set; } = false;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        public MissileSprite(Vector2 position)
        {
            this.position = position;
            this.bounds = new BoundingCircle(position - new Vector2(-16, -16), 8);
        }

        /// <summary>
        /// The updater
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        public void Update(GameTime gameTime)
        {
                position += new Vector2(0, 5); //these missiles travel downwards
                bounds.Center = position;

            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > ANIMATION_SPEED)
            {
                animationFrame++;
                if (animationFrame >= frameCount)
                    animationFrame = 0;

                animationTimer -= ANIMATION_SPEED;
            }
        }

        /// <summary>
        /// Loads the content using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("missile-Sheet");

            origin = new Vector2(frameLength / 2f, frameLength / 2f);
            float radius = (frameLength * drawScale) / 8f;
            //circleTexture = content.Load<Texture2D>("CircleHitbox");
        }

        /// <summary>
        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Hit) return;
            if (Destroyed) return;
            //Core.SpriteBatch.Draw(texture, position, null, Color.Pink);

            Rectangle source = new Rectangle(animationFrame * frameLength, 0, frameLength, frameLength);


            //Core.SpriteBatch.Draw(texture,
            //     bounds.Center,
            //     null,
            //     Color.Red * 0.5f,
            //     0f,
            //     new Vector2(texture.Width / 2f, texture.Height / 2f),
            //     0.1f,
            //     SpriteEffects.None,
            //     0f);

            Core.SpriteBatch.Draw(texture,
                position,
                source,
                Color.Red,
                0f,
                origin,
                drawScale,
                SpriteEffects.None,
                1f);

            //test code to see hitbox

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
