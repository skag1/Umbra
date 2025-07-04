using UnityEngine;

public class ParticleFollower : MonoBehaviour
{
    [SerializeField] private Transform target;

    public void SetTarget(Transform Target)
    {
        target = Target;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
        }
    }
}