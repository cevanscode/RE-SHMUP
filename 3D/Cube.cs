using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;

namespace RE_SHMUP._3D
{
    public class Cube
    {
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private BasicEffect _effect;
        private Texture2D _texture;
        private GraphicsDevice _graphicsDevice;
        private RE_SHMUPGame _game;

        private Matrix world = Matrix.Identity;
        private Matrix view;
        private Matrix projection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The game being passed in</param>
        public Cube(RE_SHMUPGame game)
        {
            _game = game;
            _graphicsDevice = Core.GraphicsDevice;

            _texture = Core.Content.Load<Texture2D>("lil_bro");

            CreateVertices();
            CreateIndices();
            CreateEffect(_game);
        }

        void CreateVertices()
        {
            VertexPositionTexture[] v =
            {
                // Front
                new(new Vector3(-2, 2, -2), new Vector2(0,0)),
                new(new Vector3(2, 2, -2), new Vector2(1,0)),
                new(new Vector3(-2, -2, -2), new Vector2(0,1)),
                new(new Vector3(2, -2, -2), new Vector2(1,1)),

                // Back
                new(new Vector3(2, 2, 2), new Vector2(0,0)),
                new(new Vector3(-2, 2, 2), new Vector2(1,0)),
                new(new Vector3(2, -2, 2), new Vector2(0,1)),
                new(new Vector3(-2, -2, 2), new Vector2(1,1)),

                // Top
                new(new Vector3(-2, 2, 2), new Vector2(0,0)),
                new(new Vector3(2, 2, 2), new Vector2(1,0)),
                new(new Vector3(-2, 2, -2), new Vector2(0,1)),
                new(new Vector3(2, 2, -2), new Vector2(1,1)),

                // Bottom
                new(new Vector3(-2, -2, -2), new Vector2(0,0)),
                new(new Vector3(2, -2, -2), new Vector2(1,0)),
                new(new Vector3(-2, -2, 2), new Vector2(0,1)),
                new(new Vector3(2, -2, 2), new Vector2(1,1)),

                // Left
                new(new Vector3(-2, 2, 2), new Vector2(0,0)),
                new(new Vector3(-2, 2, -2), new Vector2(1,0)),
                new(new Vector3(-2, -2, 2), new Vector2(0,1)),
                new(new Vector3(-2, -2, -2), new Vector2(1,1)),

                // Right
                new(new Vector3(2, 2, -2), new Vector2(0,0)),
                new(new Vector3(2, 2, 2), new Vector2(1,0)),
                new(new Vector3(2, -2, -2), new Vector2(0,1)),
                new(new Vector3(2, -2, 2), new Vector2(1,1)),
            };

            _vertexBuffer = new VertexBuffer(
                _graphicsDevice,
                typeof(VertexPositionTexture),
                v.Length,
                BufferUsage.None
            );

            _vertexBuffer.SetData<VertexPositionTexture>(v);
        }

        void CreateIndices()
        {
            short[] i =
            {
                0,1,2, 2,1,3,       // Front
                4,5,6, 6,5,7,       // Back
                8,9,10, 10,9,11,    // Top
                12,13,14, 14,13,15, // Bottom
                16,17,18, 18,17,19, // Left
                20,21,22, 22,21,23  // Right
            };

            _indexBuffer = new IndexBuffer(
                _graphicsDevice,
                IndexElementSize.SixteenBits,
                i.Length,
                BufferUsage.None
            );

            _indexBuffer.SetData(i);
        }

        void CreateEffect(RE_SHMUPGame game)
        {
            _effect = new BasicEffect(_graphicsDevice)
            {
                TextureEnabled = true,
                Texture = _texture,
                LightingEnabled = false,
                VertexColorEnabled = false
            };

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                _graphicsDevice.Viewport.AspectRatio,
                0.1f,
                100f
            );
        }

        public void Update(GameTime gameTime)
        {
            float a = (float)gameTime.TotalGameTime.TotalSeconds;

            view = Matrix.CreateRotationY(a * 0.7f) * Matrix.CreateLookAt(new Vector3(0, 5, -15), Vector3.Zero, Vector3.Up);
        }

        public void Draw()
        {
            _graphicsDevice.SetVertexBuffer(_vertexBuffer);
            _graphicsDevice.Indices = _indexBuffer;

            _effect.World = world;
            _effect.View = view;
            _effect.Projection = projection;

            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphicsDevice.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    0,
                    _indexBuffer.IndexCount / 3
                );
            }
        }
    }
}
