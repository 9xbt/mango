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
        public static Canvas Logo = Image.FromBitmap(rawLogo, false);

        [ManifestResourceStream(ResourceName = "mango.Resources.Mouse.bmp")] private static byte[] rawMouse;
        public static Canvas Mouse;

        [ManifestResourceStream(ResourceName = "mango.Resources.MouseText.bmp")] private static byte[] rawMouseText;
        public static Canvas MouseText;

        [ManifestResourceStream(ResourceName = "mango.Resources.MouseDrag.bmp")] private static byte[] rawMouseDrag;
        public static Canvas MouseDrag;

        [ManifestResourceStream(ResourceName = "mango.Resources.Busy.bmp")] private static byte[] rawBusy;
        public static Canvas Busy;

        [ManifestResourceStream(ResourceName = "mango.Resources.Link.bmp")] private static byte[] rawLink;
        public static Canvas Link;

        [ManifestResourceStream(ResourceName = "mango.Resources.Error.bmp")] private static byte[] rawError;
        public static Canvas Error;

        [ManifestResourceStream(ResourceName = "mango.Resources.Background.bmp")] private static byte[] rawBackground;
        public static Canvas Background;

        public static void GenerateFont() => Font = new Font(rawFont, 16);

        public static void Initialize()
        {
            Mouse = Image.FromBitmap(rawMouse, false);
            MouseText = Image.FromBitmap(rawMouseText, false);
            MouseDrag = Image.FromBitmap(rawMouseDrag, false);
            Busy = Image.FromBitmap(rawBusy, false);
            Link = Image.FromBitmap(rawLink, false);
            Error = Image.FromBitmap(rawError, false);
            Background = Image.FromBitmap(rawBackground, false);

            Logger.SuccessLog("Embedded resources initialized.");
        }
    }
}
