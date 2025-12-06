using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace RE_SHMUP
{
    public class Localization : Game
    {
        /// <summary>
        /// Resource mamnager for localization
        /// </summary>
        private static ResourceManager _resourceManager = new ResourceManager("RE-SHMUP.Strings", Assembly.GetExecutingAssembly());

        /// <summary>
        /// The current language culture in use
        /// </summary>
        public static CultureInfo CurrentCulture { get; private set; } = CultureInfo.CurrentUICulture;

        /// <summary>
        /// Class to set the language culture
        /// </summary>
        /// <param name="culture">Language to swap to</param>
        public static void SetLanguage(string culture)
        {
            CurrentCulture = new CultureInfo(culture);
        }

        /// <summary>
        /// Grabs relevant text from the resource file
        /// </summary>
        /// <param name="langKey">The key corresponding to the string</param>
        /// <returns>The string which corresponded to the given key</returns>
        public static string GetText(string langKey)
        {
            return _resourceManager.GetString(langKey, CurrentCulture) ?? $"[{langKey}]";
        }

        public static void ValidateString(SpriteFont font, string text)
        {
            foreach (char c in text)
            {
                if (!font.Characters.Contains(c))
                {
                    throw new Exception($"Missing glyph: '{c}' (U+{((int)c):X4})");
                }
            }
        }

    }
}
