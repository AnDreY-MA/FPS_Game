using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bullet;
    [SerializeField] private GameObject _spark;

    private List<ParticleCollisionEvent> _colEvent = new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other)
    {
        int events = _bullet.GetCollisionEvents(other, _colEvent);

        for (int i = 0; i < events; i++)
        {
            Instantiate(_spark, _colEvent[0].intersection, Quaternion.identity);
        }

        if (other.TryGetComponent<Target>(out Target target))
        {
            Destroy(target);
        }
    }
}
