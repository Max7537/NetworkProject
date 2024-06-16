using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _playerObj;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletPoint;

    public event UnityAction<Transform> Shooting;

    private HpObject _hpObject;


    //[SerializeField] private Slider _bar;

    void Start()
    {
        _hpObject = GetComponent<HpObject>();
    }

    private  void Update()
    {
        if (isLocalPlayer)
        {
            FollowMouse();
            Move();
            Shoot();
        }
        //_bar.value = _hpObject.CurrentHp / _hpObject.MaxHp;
    }

    [ClientCallback]
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shooting?.Invoke(_bulletPoint);
        }
    }

    private void SpawnBullets()
    {
        var bullet = Instantiate(_bullet, _bulletPoint.position, Quaternion.identity);
        bullet.transform.Rotate(_bulletPoint.rotation.eulerAngles, Space.World);
        NetworkServer.Spawn(bullet.gameObject);
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.World);
        }
    }

    private void FollowMouse()
    {
        var mousePos = Input.mousePosition;
        var wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));

        _playerObj.transform.rotation = Quaternion.LookRotation(Vector3.forward, wantedPos - _playerObj.transform.position);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out healBox box))
        {
            NetworkServer.Destroy(box.gameObject);
        }
        if (collision.TryGetComponent(out Bullet bullet))
        {
            NetworkServer.Destroy(bullet.gameObject);
        }
    }
}

//–еализовать стрельбу
//—делать так, чтобы они пули спаунились и  двигались на обоих клиентах.
//ѕули уничтожаютс€ либо через несколько секунд после спауна, либо когда удар€ютс€ о что либо.