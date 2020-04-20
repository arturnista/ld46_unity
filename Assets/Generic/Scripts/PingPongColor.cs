using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongColor : MonoBehaviour
{
    
    [SerializeField] private Color _fColor;
    [SerializeField] private Color _sColor;
    [SerializeField] private float _speed = 0f;
    
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine(PingPongCoroutine());
    }

    IEnumerator PingPongCoroutine()
    {
        float perc = 0f;
        while (true)
        {
            while (perc < 1f)
            {
                perc += _speed * Time.deltaTime;
                UpdateColor(perc);
                yield return null;
            }

            while (perc > 0f)
            {
                perc -= _speed * Time.deltaTime;
                UpdateColor(perc);
                yield return null;
            }
        }
    }

    void UpdateColor(float perc)
    {
        Color color = Color.Lerp(_fColor, _sColor, perc);
        _spriteRenderer.color = color;
    }
}
