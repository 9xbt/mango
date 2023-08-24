#pragma warning disable CS0649

using IL2CPU.API.Attribs;
using PrismAPI.Graphics;
using PrismAPI.Graphics.Fonts;

namespace mango
{
    public static class Resources
    {
        [ManifestResourceStream(ResourceName = "mango.Resources.DefaultFont.btf")] public static byte[] rawFont;
        public static Font Font = new Font(rawFont, 16);

        [ManifestResourceStream(ResourceName = "mango.Resources.mango.bmp")] private static byte[] rawLogo;
        public static Canvas Logo;

        [ManifestResourceStream(ResourceName = "mango.Resources.Mouse.bmp")] private static byte[] rawMouse;
        public static Canvas Mouse;

        [ManifestResourceStream(ResourceName = "mango.Resources.resize-se-nw.bmp")] private static byte[] rawResizeSE_NW;
        public static Canvas ResizeSE_NW;

        [ManifestResourceStream(ResourceName = "mango.Resources.resize-sw-ne.bmp")] private static byte[] rawResizeSW_NE;
        public static Canvas ResizeSW_NE;

        [ManifestResourceStream(ResourceName = "mango.Resources.Background.bmp")] private static byte[] rawBackground;
        public static Canvas Background;

        public static void GenerateFont() => Font = new Font(rawFont, 16);

        public static void Initialize()
        {
            Logo = Image.FromBitmap(rawLogo, false);
            Mouse = Image.FromBitmap(rawMouse, false);
            ResizeSE_NW = Image.FromBitmap(rawResizeSE_NW, false);
            ResizeSW_NE = Image.FromBitmap(rawResizeSW_NE, false);
            Background = Image.FromBitmap(rawBackground, false);

            Logger.SuccessLog("Embedded resources initialized.");
        }
    }
}
