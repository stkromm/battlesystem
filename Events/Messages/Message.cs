using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class Message
{
    [NonSerialized]
    public Transform Acteur;
    public string MessageString;
    public bool Enabled;

    public IEnumerator ShowMessage()
    {
        yield return null;
    }
}
