using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using RE_SHMUP.Scenes;

namespace RE_SHMUP
{
    /// <summary>
    /// The base game. Has mostly been separated out due to the MonoGameLibrary
    /// </summary>
    public class RE_SHMUPGame : Core
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RE_SHMUPGame() : base("RE_SHMUP", 800, 480, false)
        {
            IsMouseVisible = false;
        }

        /// <summary>
        /// Startup stuff
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            ChangeScene(new TitleScene());
        }

        /// <summary>
        /// Loads content
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

            base.LoadContent();
        }

        /// <summary>
        /// Updates the game state repeatedly
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws onto the game canvas
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
