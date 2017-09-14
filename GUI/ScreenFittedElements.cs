using UnityEngine;

public class ScreenFittedElements
{
    public Rect ScreenFittedRect(float x, float y, float width, float height)
    {
        var xNew = x;
        var yNew = y;
        if (x + width > Screen.width)
        {
            xNew = xNew - width;
        }
        if (y + height > Screen.height)
        {
            yNew = yNew - height;
        }
        return new Rect(xNew, yNew, width, height);
    }
}
