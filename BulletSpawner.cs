using UnityEngine;
using Mirror;
using System.Collections.Generic;
using System;

public class BulletSpawner : NetworkBehaviour
{
    [SerializeField] private List<Player> _players = new List<Player>();
    [SerializeField] private Bullet _bullet;

    [ServerCallback]
    public void AddPlayer(List<Player> players)
    {
        foreach (Player player in players)
        {
            _players.Add(player);
            player.Shooting += Shoot;
        }
    }

    [ServerCallback]
    private void Shoot(Transform position)
    {
        var bullet = Instantiate(_bullet, position.position, Quaternion.identity);
        bullet.transform.Rotate(position.rotation.eulerAngles, Space.World);
        NetworkServer.Spawn(bullet.gameObject);
    }
}