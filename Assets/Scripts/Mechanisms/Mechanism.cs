using UnityEngine;

public abstract class Mechanism : MonoBehaviour
{
    public abstract void Activate();
    public abstract void Deactivate();

    public bool isActivated;
}