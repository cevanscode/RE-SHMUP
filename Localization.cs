using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Resources;
using System.Globalization;
using System.Reflection;

namespace RE_SHMUP
{
    public class Localization : Game
    {
        private static ResourceManager _resourceManager = new ResourceManager("RE-SHMUP.Strings", Assembly.GetExecutingAssembly());

        public static CultureInfo CurrentCulture { get; private set; } = CultureInfo.CurrentUICulture;

        public static void SetLanguage(string culture)
        {
            CurrentCulture = new CultureInfo(culture);
        }

        public static string GetText(string langKey)
        {
            return _resourceManager.GetString(langKey, CurrentCulture) ?? $"[{langKey}]";
        }
    }
}
