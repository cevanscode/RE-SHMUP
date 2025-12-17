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

        private float _endTimer = 0f;
        private const float EndDelay = 5f;

        private int _score;

        private Rank rank;

        public CreditsScene(StoryModeScene storyModeScene)
        {
            //_score = storyModeScene.score;
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

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Core.ChangeScene(new TitleScene());
            }

            _scrollY -= _scrollSpeed * delta;

            float creditsHeight = GetCreditsHeight();

            if (_scrollY + creditsHeight < 0)
            {
                _endTimer += delta;

                if (_endTimer >= EndDelay)
                {
                    Core.ChangeScene(new TitleScene());
                }
            }


            base.Update(gameTime);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            try
            {
                string savePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "RE_SHMUP",
                    "bestScore.txt");

                if (File.Exists(savePath))
                {
                    string content = File.ReadAllText(savePath);
                    int.TryParse(content, out _score);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading best time: " + ex.Message);
            }

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
                    _creditLines.Add(new CreditLine($"Best RANK: {rank}... Perfect, ready for Mass Production!", true));
                    break;
                case Rank.A:
                    _creditLines.Add(new CreditLine($"Best RANK: {rank}... Excellent, a solid machine!", true));
                    break;
                case Rank.B:
                    _creditLines.Add(new CreditLine($"BestRANK: {rank}... Good, but requires more research.", true));
                    break;
                case Rank.C:
                    _creditLines.Add(new CreditLine($"Best RANK: {rank}... Average, the brass was unimpressed.", true));
                    break;
                case Rank.D:
                    _creditLines.Add(new CreditLine($"Best RANK: {rank}... Lysithea was dismantled after the incident.", true));
                    break;
                case Rank.Unranked:
                    _creditLines.Add(new CreditLine($"", true));
                    break;
                case Rank.F:
                    _creditLines.Add(new CreditLine($"Best RANK: {rank}... How did you even get this?", true));
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
            if (_score >= 5000)
            {
                return Rank.S;
            }
            else if (_score >= 4000)
            {
                return Rank.A;
            }
            else if (_score >= 3000)
            {
                return Rank.B;
            }
            else if (_score > 1000)
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

        private float GetCreditsHeight()
        {
            float height = 0f;

            foreach (var line in _creditLines)
            {
                SpriteFont font = _spriteFont;
                height += font.MeasureString(line.Text).Y + 10;
            }

            return height;
        }
    }
}
