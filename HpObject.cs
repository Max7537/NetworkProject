using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpObject : MonoBehaviour
{
    [SerializeField] private float _maxHp;

    private float _currentHp;

    public float CurrentHp => _currentHp;
    public float MaxHp => _maxHp;

    private void Start()
    {
        _currentHp = _maxHp;
    }

    public void ChangeHp(float value)
    {
        _currentHp -= value;

        if (_currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
