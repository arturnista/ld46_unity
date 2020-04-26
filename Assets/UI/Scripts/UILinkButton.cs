using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILinkButton : MonoBehaviour
{

    [SerializeField] private string _link = "";
    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
    }

    void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClick);
    }

    void HandleClick()
    {
        Application.OpenURL(_link);
    }

}
