using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace RE_SHMUP
{
    public class PlayerSprite
    {
        private Texture2D texture;

        private Vector2 position = new Vector2(200, 200);

        private int frameLength = 32;

        private BoundingCircle bounds;

        private int frameCount = 3;

        private const float ANIMATION_SPEED = 0.1f;

        private double animationTimer;

        private int animationFrame;

        private int moveSpeed;

        private Vector2 origin;

        private float drawScale = 2f;

        private Texture2D circleTexture;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// The color to blend with the sprite
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("PlayerSprite");

            origin = new Vector2(frameLength / 2f, frameLength / 2f);
            float radius = (frameLength * drawScale) / 8f;
            bounds = new BoundingCircle(position, radius);

            circleTexture = content.Load<Texture2D>("CircleHitbox");
        }

        /// <summary>
        /// The updater
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        public void Update(GameTime gameTime)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > ANIMATION_SPEED)
            {
                animationFrame++;
                if (animationFrame >= frameCount)
                    animationFrame = 0;

                animationTimer -= ANIMATION_SPEED;
            }

            // Focus mode (slower speed, show hitbox) enabling
            if (Core.Input.Keyboard.IsKeyDown(Keys.LeftShift) || Core.Input.Keyboard.IsKeyDown(Keys.RightShift) || Core.Input.GamePads[0].IsButtonDown(Buttons.B))
            {
                moveSpeed = 1;
            }
            else
            {
                moveSpeed = 5;
            }

            // Controller movement
            position += Core.Input.GamePads[0].LeftThumbStick * new Vector2(moveSpeed, -moveSpeed);

            // Keyboard movement
            if (Core.Input.Keyboard.IsKeyDown(Keys.Up) || Core.Input.Keyboard.IsKeyDown(Keys.W)) position += new Vector2(0, -moveSpeed);
            if (Core.Input.Keyboard.IsKeyDown(Keys.Down) || Core.Input.Keyboard.IsKeyDown(Keys.S)) position += new Vector2(0, moveSpeed);
            if (Core.Input.Keyboard.IsKeyDown(Keys.Left) || Core.Input.Keyboard.IsKeyDown(Keys.A)) position += new Vector2(-moveSpeed, 0);
            if (Core.Input.Keyboard.IsKeyDown(Keys.Right) || Core.Input.Keyboard.IsKeyDown(Keys.D)) position += new Vector2(moveSpeed, 0);

            bounds.Center = position;
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle source = new Rectangle(animationFrame * frameLength, 0, frameLength, frameLength);

            spriteBatch.Draw(texture,
                    position,
                    source,
                    Color,
                    0f,
                    origin,
                    drawScale,
                    SpriteEffects.None,
                    1f);

            if (Core.Input.Keyboard.IsKeyDown(Keys.LeftShift) || Core.Input.Keyboard.IsKeyDown(Keys.RightShift) || Core.Input.GamePads[0].IsButtonDown(Buttons.B))
            {
                float scale = (bounds.Radius * 2f) / circleTexture.Width;

                spriteBatch.Draw(circleTexture,
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
}
