using System.Collections;
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
        GameController gameController = GameObject.FindObjectOfType<GameController>();
        gameController.OnGameStart += HandleGameStart;
    }

    void HandleGameStart()
    {
        _playerMissileAttack = GameObject.FindObjectOfType<PlayerMissileAttack>();
        _lastAmount = 0;
    }

    void Update()
    {
        if (_playerMissileAttack == null) return;
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
