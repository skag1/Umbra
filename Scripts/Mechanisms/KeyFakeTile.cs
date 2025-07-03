using UnityEngine;

public class KeyFakeTile : MechanismActivator
{
    private BoxCollider2D keyCollider;

    private void Awake()
    {
        keyCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mechanism.Activate();
        Destroy(gameObject);
    }
}
