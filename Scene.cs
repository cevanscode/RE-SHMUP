using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary;


namespace RE_SHMUP
{
    public abstract class Scene : IDisposable
    {
        /// <summary>
        /// Gets content manager to load a scene's assets
        /// </summary>
        protected ContentManager Content { get; }

        /// <summary>
        /// Tells if a scene is disposed
        /// </summary>
        public bool IsDisposed { get; private set; }
        
        public Scene()
        {
            Content = new ContentManager(Core.Content.ServiceProvider);

            Content.RootDirectory = Core.Content.RootDirectory;
        }

        ~Scene() => Dispose(false);

        public virtual void Initialize()
        {
            LoadContent();
        }

        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) { return; }

            if (disposing)
            {
                UnloadContent();
                Content.Dispose();
            }
        }
    }
}
