using UnityEngine;
using System.Collections;

public class ColorUtils : MonoBehaviour {

    /// <summary>
    /// Creates color with corrected brightness.
    /// </summary>
    /// <param name="color">Color to correct.</param>
    /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
    /// Negative values produce darker colors.</param>
    /// <returns>
    /// Corrected <see cref="Color"/> structure.
    /// </returns>
    public static Color ChangeColorBrightness(Color color, float correctionFactor)
    {
        float red = color.r * 255.0f;
        float green = color.g * 255.0f;
        float blue = color.b * 255.0f;

        if (correctionFactor < 0)
        {
            correctionFactor = 1 + correctionFactor;
            red *= correctionFactor;
            green *= correctionFactor;
            blue *= correctionFactor;
        }
        else
        {
            red = (255 - red) * correctionFactor + red;
            green = (255 - green) * correctionFactor + green;
            blue = (255 - blue) * correctionFactor + blue;
        }
        //return new Color(red, green, blue, color.a);
        return FromArgb(color.a, (int)red, (int)green, (int)blue);
    }

    public static Color FromArgb(float alpha, int red, int green, int blue)
    {
        float fr = ((float)red) / 255.0f;
        float fg = ((float)green) / 255.0f;
        float fb = ((float)blue) / 255.0f;
        return new Color(fr, fg, fb, alpha);
    }
}
