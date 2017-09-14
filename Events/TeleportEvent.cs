using UnityEngine;

public class TeleportEvent : MyMonoBehaviour
{
    // The Name of the Scene, that should be loaded.
    // "" means, that the Scene doesn't change during the TeleportEvent.
    public int Scene = 0;
    // The x Coordinate of the TeleportEvent position
    public float XHero = 0f;
    // The y Coordinate of the TeleportEvent position
    public float YHero = 0f;
    // The Direction the _hero should look, after the TeleportEvent. -1 means, do not change the faced Direction.
    public int FacedDirection = 0;
    // The _hero of the recent map
    private readonly GameObject _hero = GameObject.Find("Hero");


    void OnTriggerEnter2D(Collider2D other)
    {
        var nextHero = _hero;
        if (nextHero == null)
        {
            return;
        }
        // Load next map
        Application.LoadLevel(Scene);
        nextHero = _hero;
        // MoveTransform the _hero to the desired position
        XHero = XHero - _hero.transform.position.x;
        YHero = YHero - _hero.transform.position.y;
        nextHero.transform.Translate(XHero, YHero, 0.0f);
        if (FacedDirection != 0)
        {
            // some code to change the Direction, the _hero is facing		
        }
    }
}
