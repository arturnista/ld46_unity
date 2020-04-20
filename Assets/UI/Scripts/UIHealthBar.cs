using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    
    [SerializeField] private Image _valueImage;

    private PlayerHealth _playerHealth;
    private float _lastHealth;

    void OnEnable()
    {
        _playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        if (_playerHealth) 
        {
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

    void UpdatePlayerHealthBarValue()
    {
        float perc = Mathf.Clamp01(_playerHealth.Health / _playerHealth.MaxHealth);
        _valueImage.fillAmount = perc;
    }

}
