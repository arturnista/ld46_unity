using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    
    [SerializeField] private Image _valueImage;

    private PlayerHealth _playerHealth;
    private float _lastHealth;
    
    private GameController _gameController;

    void OnEnable()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
        _gameController.OnGameStart += HandleGameStart;
        _valueImage.fillAmount = 1f;
    }

    void OnDisable()
    {
        _gameController.OnGameStart -= HandleGameStart;
    }

    void HandleGameStart()
    {   
        _playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        if (_playerHealth) 
        {
            _playerHealth.OnDeath += HandlePlayerDeath;
            _playerHealth.OnTakeDamage += HandlePlayerTakeDamage;
            _playerHealth.OnHeal += HandlePlayerHeal;
            UpdatePlayerHealthBarValue();
        }
    }

    void HandlePlayerTakeDamage(float damage)
    {
        UpdatePlayerHealthBarValue();
    }

    void HandlePlayerHeal(float amount)
    {
        UpdatePlayerHealthBarValue();
    }

    void HandlePlayerDeath()
    {
        UpdatePlayerHealthBarValue();
    }

    void UpdatePlayerHealthBarValue()
    {
        float perc = Mathf.Clamp01(_playerHealth.Health / _playerHealth.MaxHealth);
        _valueImage.fillAmount = perc;
    }

}
