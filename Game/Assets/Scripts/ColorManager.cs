using UnityEngine;

public static class ColorManager
{
    public static Color ColorRGB(int r, int g, int b)
    {
        return new Color(r/255f, g/255f, b/255f);
    }
    public static Color Red = ColorRGB(255, 70, 70);
    public static Color Blue = ColorRGB(70, 70, 255);
    public static Color Green = ColorRGB(23, 255, 116);
    public static Color White = ColorRGB(255, 255, 255);
    public static Color Yellow = ColorRGB(254, 245, 63);
} 