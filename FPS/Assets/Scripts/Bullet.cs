using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<Target>(out Target target))
        {
            target.TakeDamage(10);
        }
    }
}
