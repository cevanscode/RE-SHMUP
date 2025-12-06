using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace RE_SHMUP.Scenes
{
    public class SettingsScene : Scene
    {
        #region Textures
        private SpriteFont _spriteFont;
        private Texture2D menuButtonTexture;
        private Texture2D mousePointer;
        #endregion

        private Button[] _theButtons = new Button[5];
        private ButtonHelper _buttonHelper = new ButtonHelper();
        private float prevStickY = 0;
        private float currStickY = 0;

        #region Buttons
        public Button languageButton;
        public Button volumeUpButton;
        public Button volumeDownButton;
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
                || Core.Input.GamePads[0].WasButtonJustPressed(Buttons.DPadDown)
                || (currStickY < -0.5f && prevStickY >= -0.5f))
            {
                _buttonHelper.IncrementSelection();
            }

            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Up)
                || Core.Input.GamePads[0].WasButtonJustPressed(Buttons.DPadUp)
                || Core.Input.GamePads[0].WasButtonJustPressed(Buttons.DPadDown)
                || (currStickY < 0.5f && prevStickY >= 0.5f))
            {
                _buttonHelper.DecrementSelection();
            }

            languageButton.Update(gameTime);
            volumeUpButton.Update(gameTime);
            volumeDownButton.Update(gameTime);
            backToMenuButton.Update(gameTime);
            resolutionChangeButton.Update(gameTime);
        }

        public override void LoadContent()
        {
            Localization.SetLanguage("en");

            _spriteFont = Content.Load<SpriteFont>("ArkPixel");
            menuButtonTexture = Content.Load<Texture2D>("MenuButton-Smaller");
            mousePointer = Content.Load<Texture2D>("Pointer");

            _spriteFont = Content.Load<SpriteFont>("ArkPixel");

            backToMenuButton = new Button(_spriteFont, menuButtonTexture);
            backToMenuButton.buttonPosition = new Vector2(400, 10);
            backToMenuButton._buttonText = Localization.GetText("Test");
            backToMenuButton.Click += BackToMenuButton_Click;
            _theButtons[0] = backToMenuButton;

            languageButton = new Button(_spriteFont, menuButtonTexture);
            languageButton.buttonPosition = new Vector2(400, 80);
            languageButton._buttonText = Localization.GetText("Test");
            languageButton.Click += LanguageButton_Click;
            _theButtons[1] = languageButton;

            volumeUpButton = new Button(_spriteFont, menuButtonTexture);
            volumeUpButton.buttonPosition = new Vector2(400, 150);
            volumeUpButton._buttonText = Localization.GetText("Test");
            volumeUpButton.Click += VolumeUpButton_Click;
            _theButtons[2] = volumeUpButton;

            volumeDownButton = new Button(_spriteFont, menuButtonTexture);
            volumeDownButton.buttonPosition = new Vector2(400, 220);
            volumeDownButton._buttonText = Localization.GetText("Test");
            volumeDownButton.Click += VolumeDownButton_Click;
            _theButtons[3] = volumeDownButton;

            resolutionChangeButton = new Button(_spriteFont, menuButtonTexture);
            resolutionChangeButton.buttonPosition = new Vector2(400, 290);
            resolutionChangeButton._buttonText = Localization.GetText("Test");
            resolutionChangeButton.Click += ResolutionChangeButton_Click;
            _theButtons[4] = volumeUpButton;

            _theButtons[0].Selected = true;
            _buttonHelper.Buttons = _theButtons;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Core.SpriteBatch.Begin();

            backToMenuButton.Draw(gameTime, Core.SpriteBatch);
            languageButton.Draw(gameTime, Core.SpriteBatch);
            volumeUpButton.Draw(gameTime, Core.SpriteBatch);
            volumeDownButton.Draw(gameTime, Core.SpriteBatch);
            resolutionChangeButton.Draw(gameTime, Core.SpriteBatch);

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
            volumeUpButton._buttonText = Localization.GetText("VolumeUp");
            volumeDownButton._buttonText = Localization.GetText("VolumeDown");
            backToMenuButton._buttonText = Localization.GetText("BackToMenu");
            resolutionChangeButton._buttonText = Localization.GetText("ResolutionChange");
        }

        private void BackToMenuButton_Click(object sender, EventArgs e)
        {
            Core.ChangeScene(new TitleScene());
        }

        private void VolumeUpButton_Click(object sender, EventArgs e)
        {
            //turns up volume
        }

        private void VolumeDownButton_Click(object sender, EventArgs e)
        {
            //turns down volume
        }

        private void ResolutionChangeButton_Click(object sender, EventArgs e)
        {
            //turns down volume
        }
    }
}
