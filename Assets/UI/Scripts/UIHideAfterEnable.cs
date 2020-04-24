using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UICanvas))]
[RequireComponent(typeof(CanvasGroup))]
public class UIHideAfterEnable : MonoBehaviour
{
    
    [SerializeField] private float _hideAfter = 2f;
    [SerializeField] private float _hideTime = 2f;
    
    private UICanvas _uiCanvas;

    void Awake()
    {
        _uiCanvas = GetComponent<UICanvas>();
    }

    void OnEnable()
    {
        _uiCanvas.Hide(_hideTime, _hideAfter, true, true);
    }

}
