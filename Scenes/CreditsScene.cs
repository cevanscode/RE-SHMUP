using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using RE_SHMUP.Scenes;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RE_SHMUP.Scenes
{
    public class CreditsScene : Scene
    {
        #region Important fields for making stuff
        private SpriteFont _spriteFont;
        private BasicTilemap _tilemap;
        #endregion

        private List<CreditLine> _creditLines = new();
        private float _scrollY;
        private float _scrollSpeed = 40f;

        private int _score;

        private Rank rank;

        public CreditsScene(StoryModeScene storyModeScene)
        {
            _score = storyModeScene.score;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Core.Input.Keyboard.IsKeyDown(Keys.Z)
                || Core.Input.Keyboard.IsKeyDown(Keys.Space)
                || Core.Input.GamePads[0].IsButtonDown(Buttons.A)
            )
            {
                _scrollSpeed = 160f;
            }
            else
            {
                _scrollSpeed = 40f;
            }

            _scrollY -= _scrollSpeed * delta;

            base.Update(gameTime);
        }


        public override void LoadContent()
        {
            base.LoadContent();

            _spriteFont = Content.Load<SpriteFont>("ArkPixel");

            var lines = File.ReadAllLines("credits.txt");

            foreach (var line in lines)
            {
                if (line.StartsWith("%"))
                {
                    _creditLines.Add(new CreditLine(line.Substring(1), true));
                }
                else
                {
                    _creditLines.Add(new CreditLine(line, false));
                }
            }

            rank = GiveRanking();
            switch(rank)
            {
                case Rank.S:
                    _creditLines.Add(new CreditLine($"RANK: {rank}... Perfect, ready for Mass Production!", true));
                    break;
                case Rank.A:
                    _creditLines.Add(new CreditLine($"RANK: {rank}... Excellent, a solid machine!", true));
                    break;
                case Rank.B:
                    _creditLines.Add(new CreditLine($"RANK: {rank}... Good, but requires more research.", true));
                    break;
                case Rank.C:
                    _creditLines.Add(new CreditLine($"RANK: {rank}... Average, the brass was unimpressed.", true));
                    break;
                case Rank.D:
                    _creditLines.Add(new CreditLine($"RANK: {rank}... Lysithea was dismantled after the incident.", true));
                    break;
                case Rank.Unranked:
                    _creditLines.Add(new CreditLine($"", true));
                    break;
                case Rank.F:
                    _creditLines.Add(new CreditLine($"RANK: {rank}... How did you even get this?", true));
                    break;
            }

            _scrollY = Core.GraphicsDevice.Viewport.Height;
        }

        public override void Draw(GameTime gameTime)
        {
            Core.SpriteBatch.Begin();

            float y = _scrollY;

            foreach (var line in _creditLines)
            {
                Vector2 size = _spriteFont.MeasureString(line.Text);

                float x = (Core.GraphicsDevice.Viewport.Width - size.X) / 2;

                Core.SpriteBatch.DrawString(_spriteFont, line.Text, new Vector2(x, y), Color.White);
                y += size.Y + 20;
            }

            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public Rank GiveRanking()
        {
            if (_score >= 1000)
            {
                return Rank.S;
            }
            else if (_score >= 500)
            {
                return Rank.A;
            }
            else if (_score >= 250)
            {
                return Rank.B;
            }
            else if (_score > 100)
            {
                return Rank.C;
            }
            else if (_score > 0)
            {
                return Rank.D;
            }
            else if (_score == -420)
            {
                return Rank.Unranked;
            }
            else //if you somehow get a score lower than 0... if that's even possible
            {
                return Rank.F;
            }
        }
    }
}
