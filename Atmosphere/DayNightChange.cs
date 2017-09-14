using UnityEngine;
using System.Collections;

public class DayNightChange : MonoBehaviour
{
    public Light[] Daylight;
    public Light[] Nightlight;
    private float _timeInterval = 0.0005f;
    private bool _delay = true;

    void Start()
    {
        enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!(_delay && enabled))
        {
            return;
        }
        _delay = false;
        StartCoroutine(PastTime());


    }

    public IEnumerator PastTime()
    {
        if (Daylight[0].intensity <= 0 || Daylight[0].intensity >= 0.6)
        {
            if (Daylight[0].intensity < 0)
            {
                Daylight[0].intensity = 0;
            }
            _timeInterval = _timeInterval * (-1);
        }
        foreach (var l in Daylight)
        {
            l.intensity = l.intensity + _timeInterval;
        }
        foreach (var l in Nightlight)
        {
            l.intensity = l.intensity - _timeInterval;
        }
        yield return new WaitForSeconds(1.1f);
        _delay = true;
    }
}
