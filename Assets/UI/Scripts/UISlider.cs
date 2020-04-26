using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UISlider : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _valueText = default;
    [SerializeField] private Slider _slider = default;
    [Space]
    [SerializeField] private string _preString = "";
    [SerializeField] private string _postString = "";

    public float Value
    {
        get => _slider.value;
        set
        {
            _slider.value = value;
            HandleValueChange(value);
        }
    }
    public UnityEvent<float> OnValueChanged { get => _slider.onValueChanged; }

    void OnEnable()
    {
        _slider.onValueChanged.AddListener(HandleValueChange);
        HandleValueChange(_slider.value);
    }

    void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(HandleValueChange);
    }

    void HandleValueChange(float value)
    {
        _valueText.text = _preString + value.ToString() + _postString;
    }

}
