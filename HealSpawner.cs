using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealSpawner : NetworkBehaviour
{
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private float max_x;
    [SerializeField] private float max_y;
    [SerializeField] private healBox _heal;

    public override void OnStartServer()
    {
        StartCoroutine(Spawner());
    }

    [ServerCallback]
    private IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnCooldown);
            var item = Instantiate(_heal, new Vector2(
                Random.Range(-max_x, max_x), 
                Random.Range(-max_y, max_y)), 
                Quaternion.identity);
            NetworkServer.Spawn(item.gameObject);
        }
    }
}
