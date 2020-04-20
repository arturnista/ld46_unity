using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StationRecharge : MonoBehaviour, IHealth
{

    public delegate void FinishRechargeHandler();
    public event FinishRechargeHandler OnFinishRecharge;

    [Tooltip("How much recharge will increase per second")]
    [SerializeField] private float _rechargeRate = 1f;
    [SerializeField] private float _rechargeRequired = 100f;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private GameObject _finalDamageCircle;

    private float _rechargeAmount;

    public float MaxHealth { get => _rechargeRequired; }
    public float Health { get => _rechargeAmount; }

    void Awake()
    {
        _rechargeAmount = 0f;
    }

    void Update()
    {
        _rechargeAmount += Time.deltaTime * _rechargeRate;
        if (_rechargeAmount >= _rechargeRequired)
        {
            if (OnFinishRecharge != null)
            {
                OnFinishRecharge();
            }
            _finalDamageCircle.SetActive(true);
            enabled = false;
        }
    }

    void LateUpdate()
    {
        _valueText.text = Mathf.RoundToInt(_rechargeAmount) + "%";
    }

    public void DealDamage(float damage)
    {
        float actualDamage = damage / 5f;
        _rechargeAmount -= actualDamage >= 1f ? actualDamage : 1f;
        if (_rechargeAmount < 0) _rechargeAmount = 0f;
    }

}
