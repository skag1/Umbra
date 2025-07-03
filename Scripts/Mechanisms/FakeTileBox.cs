using UnityEngine;

public class FakeTileBox : Mechanism
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    public override void Activate()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isActivated = true;
    }


    public override void Deactivate()
    {
        rb.bodyType = RigidbodyType2D.Static;
        isActivated = false;
    }
}
