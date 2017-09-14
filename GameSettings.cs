using System;
using UnityEngine;

public class GameSettings : MyMonoBehaviour
{
    public Language Language = new Language();
    public void SetResolution(Resolution res, bool fullScreen)
    {
        Screen.SetResolution(res.width, res.height, fullScreen, res.refreshRate);
        PlayerPrefs.SetInt("Screen_Resolution", Array.IndexOf(Screen.resolutions, res));
        PlayerPrefs.SetString("Screen_FullScreen", fullScreen.ToString());
        PlayerPrefs.Save();
    }

    public void SetLanguage(Language lang)
    {
        Language = lang;
        PlayerPrefs.SetString("System_Language", lang.ToString());
        PlayerPrefs.Save();
    }
}
