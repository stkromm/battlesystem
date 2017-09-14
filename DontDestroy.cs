using UnityEngine;

public class DontDestroy : MyMonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
