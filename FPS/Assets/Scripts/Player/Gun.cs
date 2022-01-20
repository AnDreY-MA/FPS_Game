using System.Collections.Generic;
using UnityEngine;

public class Gun : Player
{
    [SerializeField] private ParticleSystem _bullet;

    protected override void Awake()
    {
        base.Awake();

        _playerInput.Player.Fire.performed += ctx => Shoot();
    }

    private void Shoot()
    {
        _bullet.Play();
        print("Play");
    }
}
