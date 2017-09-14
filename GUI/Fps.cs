using UnityEngine;

public class Fps : MyMonoBehaviour
{
    float _timeA;
    public int ActualFps;
    public int LastFps;
    // Use this for initialization
    void Start()
    {
        _timeA = Time.timeSinceLevelLoad;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.timeSinceLevelLoad+" "+_timeA);
        if (Time.timeSinceLevelLoad - _timeA <= 1)
        {
            ActualFps++;
        }
        else
        {
            LastFps = ActualFps + 1;
            _timeA = Time.timeSinceLevelLoad;
            ActualFps = 0;
        }
    }
    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 30, 30), "" + LastFps);
    }
}
