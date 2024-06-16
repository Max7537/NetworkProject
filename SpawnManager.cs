using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using Org.BouncyCastle.Asn1.Mozilla;

public class SpawnManager : NetworkManager
{
    [SerializeField] private List<Transform> _points = new List<Transform>();
    [SerializeField] private List<Player> _players = new List<Player>();
    [SerializeField] private HealSpawner _healSpawner;
    [SerializeField] private BulletSpawner _spawner;
    [SerializeField] private BulletSpawner _currentSpawn;

    public override void OnStartHost()
    {
        var spawner = Instantiate(_healSpawner);

        NetworkServer.Spawn(spawner.gameObject);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        var player = Instantiate(playerPrefab, _points[0].transform.position, playerPrefab.transform.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
        _players.Add(player.GetComponent<Player>());

        if (numPlayers >= 2)
        {
            _currentSpawn = Instantiate(_spawner);
            NetworkServer.Spawn(_currentSpawn.gameObject);
            _currentSpawn.AddPlayer(_players);
        }


    }
}
