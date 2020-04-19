using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StationRecharge : MonoBehaviour
{

    public delegate void FinishRechargeHandler();
    public event FinishRechargeHandler OnFinishRecharge;

    [Tooltip("How much recharge will increase per second")]
    [SerializeField] private float _rechargeRate = 1f;
    [SerializeField] private float _rechargeRequired = 100f;
    [SerializeField] private TextMeshProUGUI _valueText;

    private float _rechargeAmount;

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
            enabled = false;
        }
    }

    void LateUpdate()
    {
        _valueText.text = Mathf.RoundToInt(_rechargeAmount) + "%";
    }

}
