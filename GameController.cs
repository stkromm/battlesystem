using UnityEngine;

public class GameController : MyMonoBehaviour
{
    float _savedTimeScale;
    public bool ToggleWeather()
    {
        return true;
    }
    public bool ToggleTime()
    {
        return true;
    }

    public bool StopGame()
    {
        return true;
    }
    public void SetTime(int i) { }
    public void StopEvents()
    {
        if ((int)Time.timeScale == 0)
        {
            return;
        }
        _savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void ResumeEvents()
    {
        if ((int)_savedTimeScale != 0)
            Time.timeScale = _savedTimeScale;
        Time.timeScale = 1;

    }
    public void ResumeGame()
    {

    }

}
