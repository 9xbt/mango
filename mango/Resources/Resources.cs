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
        public static Canvas Logo = Image.FromBitmap(rawLogo, false);
    }
}
