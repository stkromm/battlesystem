using UnityEngine;

public class MessageDialog : MyMonoBehaviour
{

    public Message[] Dialog;
    public int I = -1;
    private bool _show;
    void Update()
    {
        if (!(_show && Input.GetButtonDown("Interact")))
        {
            return;
        }
        I += 1;
        if (I != Dialog.Length)
        {
            return;
        }
        I = -1;
        _show = false;
    }
    void OnGUI()
    {
        var elements = new ScreenFittedElements();
        if (!_show)
        {
            return;
        }
        if (!(I >= Dialog.Length))
            GUI.Label(elements.ScreenFittedRect(Dialog[I].Acteur.transform.position.x, Dialog[I].Acteur.transform.position.y, (float)Screen.width / 2, (float)Screen.height / 12), Dialog[I].MessageString);
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (_show)
        {
            return;
        }
        if (Input.GetButtonDown("Interact"))
        {
            _show = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        _show = false;
        I = -1;
    }
}
