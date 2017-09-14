using UnityEngine;

public class CameraFollowMode : MyMonoBehaviour
{
    public int Width;
    public int Height;
    // Update is called once per frame
    void Update()
    {
        FollowCharacter();
    }
    void FollowCharacter()
    {
        var trans = GameObject.Find("Hero").transform.position;
        trans.y = trans.y < 145 ? 145 : trans.y;
        trans.x = trans.x < 260 ? 260 : trans.x;
        trans.x = trans.x > Width - 260 ? Width - 260 : 0;
        trans.y = trans.y > Height - 145 ? Height - 145 : 0;
        var vec = trans - Camera.main.transform.position;
        vec.z = 0f;
        Camera.main.transform.Translate(vec);
    }
}
