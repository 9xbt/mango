using IL2CPU.API.Attribs;
using PrismAPI.Graphics;
using PrismAPI.Graphics.Fonts;

namespace mango
{
    public static class Resources
    {
        [ManifestResourceStream(ResourceName = "mango.Resources.Font.btf")] private static byte[] rawFont;
        public static Font Font = new Font(rawFont, 16);

        [ManifestResourceStream(ResourceName = "mango.Resources.mango.bmp")] private static byte[] rawLogo;
        public static Canvas Logo;

        [ManifestResourceStream(ResourceName = "mango.Resources.Mouse.bmp")] private static byte[] rawMouse;
        public static Canvas Mouse;

        [ManifestResourceStream(ResourceName = "mango.Resources.Background.bmp")] private static byte[] rawBackground;
        public static Canvas Background;

        public static void Initialize()
        {
            Logger.InfoLog("Initializing embedded resource 1/3...");
            Logo = Image.FromBitmap(rawLogo, false);
            Logger.InfoLog("Initializing embedded resource 2/3...");
            Mouse = Image.FromBitmap(rawMouse, false);
            Logger.InfoLog("Initializing embedded resource 3/3...");
            Background = Image.FromBitmap(rawBackground, false);

            Logger.SuccessLog("Embedded resources initialized.");
        }
    }
}
