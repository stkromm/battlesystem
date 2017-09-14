using UnityEngine;

public class Clipping : MyMonoBehaviour
{
    public int SortingOrder = 0;
    private SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }
        if (_sprite)
        {
            _sprite.sortingLayerName = "Clipped";
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_sprite)
        {
            _sprite.sortingLayerName = "Default";
        }
    }

}

