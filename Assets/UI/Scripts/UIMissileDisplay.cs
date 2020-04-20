﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissileDisplay : MonoBehaviour
{
    
    [SerializeField] private GameObject _itemPrefab;

    private PlayerMissileAttack _playerMissileAttack;
    private int _lastAmount;

    void OnEnable()
    {
        _playerMissileAttack = GameObject.FindObjectOfType<PlayerMissileAttack>();
        if (_playerMissileAttack != null)
        {
            _lastAmount = _playerMissileAttack.MissileAmount;
        }
    }

    void Update()
    {
        if (_lastAmount != _playerMissileAttack.MissileAmount)
        {
            _lastAmount = _playerMissileAttack.MissileAmount;
            foreach (Transform item in transform)
            {
                Destroy(item.gameObject);
            }
            for (int i = 0; i < _lastAmount; i++)
            {
                Instantiate(_itemPrefab, transform);
            }
        }
    }

}
