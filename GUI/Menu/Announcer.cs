using UnityEngine;
using System.Collections;

public class Announcer : MyMonoBehaviour
{
    public AudioClip Sound;
    private readonly Queue _announcements = new Queue();
    private bool _announcing;
    private bool _playedSound;
    private string _message = "";
    private const float TimeToWait = 3.0f;
    private float _timeStarted;
    private bool _announcingDelay = true;
    // Update is called once per frame
    void Update()
    {
        if (!_announcingDelay)
        {
            _announcingDelay = true;
        }
        if (!_announcing && _announcements.Count != 0)
        {
            var s = "";
            var o = _announcements.Dequeue();
            if (o is string)
            {
                s = o.ToString();
            }
            if (s == "") return;
            _message = s;
            _timeStarted = Time.realtimeSinceStartup;
            _announcing = true;
            _playedSound = false;
            return;
        }
        if (!_playedSound)
        {
            GetComponent<AudioSource>().PlayOneShot(Sound);
            _playedSound = true;
        }
        _announcing = !(Time.realtimeSinceStartup - _timeStarted >= TimeToWait);
    }

    void OnGUI()
    {
        if (_announcing && _message != "")
            GUI.Box(new Rect(0, 0, 500, 100), _message);
    }

    public void AddAnnouncement(string s)
    {
        if (_announcingDelay)
        {
            _announcements.Enqueue(s);
        }
    }
}
