using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace RE_SHMUP.Scenes
{
    public class SettingsScene : Scene
    {
        #region Textures
        private SpriteFont _spriteFont;
        private Texture2D menuButtonTexture;
        private Texture2D mousePointer;
        #endregion

        private Button[] _theButtons = new Button[7];
        private ButtonHelper _buttonHelper = new ButtonHelper();
        private float prevStickY = 0;
        private float currStickY = 0;

        #region Buttons
        public Button languageButton;
        public Button volumeMusicUpButton;
        public Button volumeMusicDownButton;
        public Button volumeSFXUpButton;
        public Button volumeSFXDownButton;
        public Button backToMenuButton;
        public Button resolutionChangeButton;
        #endregion

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            currStickY = Core.Input.GamePads[0].LeftThumbStick.Y;

            if (Core.Input.GamePads[0].WasButtonJustPressed(Buttons.Back) || Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
                Core.Instance.Exit();

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Down)
                || Core.Input.GamePads[0].WasButtonJustPressed(Buttons.DPadDown)
                || (currStickY < -0.5f && prevStickY >= -0.5f))
            {
                _buttonHelper.IncrementSelection();
            }

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Up)
                || Core.Input.GamePads[0].WasButtonJustPressed(Buttons.DPadUp)
                || (currStickY < 0.5f && prevStickY >= 0.5f))
            {
                _buttonHelper.DecrementSelection();
            }

            languageButton.Update(gameTime);
            volumeMusicUpButton.Update(gameTime);
            volumeMusicDownButton.Update(gameTime);
            volumeSFXUpButton.Update(gameTime);
            volumeSFXDownButton.Update(gameTime);
            backToMenuButton.Update(gameTime);
            resolutionChangeButton.Update(gameTime);
        }

        public override void LoadContent()
        {
            _spriteFont = Content.Load<SpriteFont>("ArkPixel");
            menuButtonTexture = Content.Load<Texture2D>("MenuButton-Smaller");
            mousePointer = Content.Load<Texture2D>("Pointer");

            _spriteFont = Content.Load<SpriteFont>("ArkPixel");

            backToMenuButton = new Button(_spriteFont, menuButtonTexture);
            backToMenuButton.buttonPosition = new Vector2(400, 10);
            backToMenuButton._buttonText = Localization.GetText("BackToMenu");
            backToMenuButton.Click += BackToMenuButton_Click;
            _theButtons[0] = backToMenuButton;

            languageButton = new Button(_spriteFont, menuButtonTexture);
            languageButton.buttonPosition = new Vector2(400, 80);
            languageButton._buttonText = Localization.GetText("LanguageLabel");
            languageButton.Click += LanguageButton_Click;
            _theButtons[1] = languageButton;

            volumeMusicDownButton = new Button(_spriteFont, menuButtonTexture);
            volumeMusicDownButton.buttonPosition = new Vector2(300, 150);
            volumeMusicDownButton._buttonText = Localization.GetText("VolumeMusicDown");
            volumeMusicDownButton.Click += VolumeDownButton_Click;
            _theButtons[2] = volumeMusicDownButton;

            volumeMusicUpButton = new Button(_spriteFont, menuButtonTexture);
            volumeMusicUpButton.buttonPosition = new Vector2(500, 150);
            volumeMusicUpButton._buttonText = Localization.GetText("VolumeMusicUp");
            volumeMusicUpButton.Click += VolumeUpButton_Click;
            _theButtons[3] = volumeMusicUpButton;

            volumeSFXDownButton = new Button(_spriteFont, menuButtonTexture);
            volumeSFXDownButton.buttonPosition = new Vector2(300, 220);
            volumeSFXDownButton._buttonText = Localization.GetText("VolumeSFXDown");
            volumeSFXDownButton.Click += VolumeSFXDownButton_Click;
            _theButtons[4] = volumeSFXDownButton;

            volumeSFXUpButton = new Button(_spriteFont, menuButtonTexture);
            volumeSFXUpButton.buttonPosition = new Vector2(500, 220);
            volumeSFXUpButton._buttonText = Localization.GetText("VolumeSFXUp");
            volumeSFXUpButton.Click += VolumeSFXUpButton_Click;
            _theButtons[5] = volumeSFXUpButton;

            resolutionChangeButton = new Button(_spriteFont, menuButtonTexture);
            resolutionChangeButton.buttonPosition = new Vector2(400, 290);
            resolutionChangeButton._buttonText = Localization.GetText("ResolutionChange");
            resolutionChangeButton.Click += ResolutionChangeButton_Click;
            _theButtons[6] = resolutionChangeButton;

            _theButtons[0].Selected = true;
            _buttonHelper.Buttons = _theButtons;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Core.SpriteBatch.Begin();

            backToMenuButton.Draw(gameTime, Core.SpriteBatch);
            languageButton.Draw(gameTime, Core.SpriteBatch);
            volumeMusicUpButton.Draw(gameTime, Core.SpriteBatch);
            volumeMusicDownButton.Draw(gameTime, Core.SpriteBatch);
            volumeSFXDownButton.Draw(gameTime, Core.SpriteBatch);
            volumeSFXUpButton.Draw(gameTime, Core.SpriteBatch);
            resolutionChangeButton.Draw(gameTime, Core.SpriteBatch);

            Core.SpriteBatch.DrawString(_spriteFont,
                ((int)Math.Round(Core.Audio.SongVolume * 10f)).ToString(),
                new Vector2(440, 160),
                Color.Red,
                0f,
                new Vector2(0, 0),
                3f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.DrawString(_spriteFont,
                ((int)Math.Round(Core.Audio.SoundEffectVolume * 10f)).ToString(),
                new Vector2(440, 230),
                Color.Red,
                0f,
                new Vector2(0, 0),
                3f,
                SpriteEffects.None,
                0f);

            Core.SpriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Changes the language when connected button is clicked
        /// </summary>
        /// <param name="sender">The object signaling the event</param>
        /// <param name="e">Information about the event</param>
        private void LanguageButton_Click(object sender, System.EventArgs e)
        {
            if (Localization.CurrentCulture.TwoLetterISOLanguageName == "ja")
            {
                Localization.SetLanguage("en");
                languageButton._buttonText = Localization.GetText("LanguageLabel");
            }
            else
            {
                Localization.SetLanguage("ja");
                languageButton._buttonText = Localization.GetText("LanguageLabel");
            }
            volumeMusicUpButton._buttonText = Localization.GetText("VolumeUp");
            volumeMusicDownButton._buttonText = Localization.GetText("VolumeDown");
            backToMenuButton._buttonText = Localization.GetText("BackToMenu");
            resolutionChangeButton._buttonText = Localization.GetText("ResolutionChange");
        }

        private void BackToMenuButton_Click(object sender, EventArgs e)
        {
            Core.ChangeScene(new TitleScene());
        }

        private void VolumeUpButton_Click(object sender, EventArgs e)
        {
            Core.Audio.SongVolume += 0.1f;
        }

        private void VolumeDownButton_Click(object sender, EventArgs e)
        {
            Core.Audio.SongVolume -= 0.1f;
        }

        private void VolumeSFXUpButton_Click(object sender, EventArgs e)
        {
            Core.Audio.SoundEffectVolume += 0.1f;
        }

        private void VolumeSFXDownButton_Click(object sender, EventArgs e)
        {
            Core.Audio.SoundEffectVolume -= 0.1f;
        }

        private void ResolutionChangeButton_Click(object sender, EventArgs e)
        {

        }
    }
}
