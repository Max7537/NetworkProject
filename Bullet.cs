using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeAlive;
    [SerializeField] private float _transformUpdate;

    public void Start()
    {
        StartCoroutine(MoveSelf());
    }

//    [ServerCallback]
    private IEnumerator MoveSelf()
    {
        while (_timeAlive > 0)
        {
            yield return new WaitForSeconds(_transformUpdate);
            _timeAlive -= Time.deltaTime;
            transform.Translate(_speed, 0, 0);
        }
    }
}
