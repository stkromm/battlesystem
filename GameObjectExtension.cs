using UnityEngine;

public static class GameObjectExtension
{

    public static T GetSafeComponent<T>(this GameObject obj) where T : MyMonoBehaviour
    {
        var component = obj.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError("Expected to find component of type "
               + typeof(T) + " but found none", obj);
        }

        return component;
    }
}
