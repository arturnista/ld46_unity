using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StationRecharge : MonoBehaviour, IHealth
{

    public delegate void FinishRechargeHandler();
    public event FinishRechargeHandler OnFinishRecharge;

    public delegate void DeathHandler();
    public event DeathHandler OnDeath;

    [Tooltip("How much recharge will increase per second")]
    [SerializeField] private float _rechargeRate = 1f;
    [SerializeField] private float _rechargeRequired = 100f;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private GameObject _finalDamageCircle;
    [SerializeField] private GameObject _smallExplosionPrefab;
    [SerializeField] private GameObject _explosionPrefab;

    private bool _isRecharging;
    private bool _isDead;

    private float _rechargeAmount;
    public float RechargeAmount { get => _rechargeAmount; }

    public float MaxHealth { get => _rechargeRequired; }
    public float Health { get => _rechargeAmount; }

    void Awake()
    {
        _rechargeAmount = 0f;
        _isRecharging = false;
        _isDead = false;
    }

    void Update()
    {
        if (!_isRecharging) return;
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

    public void StartRecharging()
    {
        _isRecharging = true;
    }

    public void DealDamage(float damage)
    {
        if (_isDead) return;

        float actualDamage = damage / 6f;
        _rechargeAmount -= actualDamage;
        if (_rechargeAmount < 0)
        {
            _isDead = true;
            _rechargeAmount = 0f;
            _isRecharging = false;
            if (OnDeath != null)
            {
                StartCoroutine(DeathCoroutine());
                OnDeath();
            }
        }
    }

    IEnumerator DeathCoroutine()
    {
        GetComponent<Collider2D>().enabled = false;
        for (int i = 0; i < 12; i++)
        {
            Instantiate(_smallExplosionPrefab, transform.position + ((Vector3)Random.insideUnitCircle * 2f), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(.1f, .3f));
        }
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

}
