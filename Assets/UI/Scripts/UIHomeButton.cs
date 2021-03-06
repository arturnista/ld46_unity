using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHomeButton : MonoBehaviour
{
    void Awake()
    {
        UIController uiController = GameObject.FindObjectOfType<UIController>();
        GetComponent<Button>().onClick.AddListener(() => uiController.OpenHome());
    }
}
