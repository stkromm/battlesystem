using UnityEngine;

public class ActivateEvent : MyMonoBehaviour
{
    public bool Show = false;
    void OnGUI()
    {
        if (Show)
        {
            ActivatedGui();
        }
    }
    public virtual void ActivatedGui() { }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (Show)
        {
            return;
        }
        if (Input.GetButtonDown("Interact"))
        {
            Show = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        Show = false;
    }
}
