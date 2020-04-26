using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject _uiGame;
    [SerializeField] private GameObject _optionsGame;
    [SerializeField] private GameObject _creditsGame;

    public void OpenHome()
    {
        _uiGame.SetActive(true);
        _optionsGame.SetActive(false);
        _creditsGame.SetActive(false);
    }

    public void OpenOptions()
    {
        _uiGame.SetActive(false);
        _optionsGame.SetActive(true);
        _creditsGame.SetActive(false);
    }

    public void OpenCredits()
    {
        _uiGame.SetActive(false);
        _optionsGame.SetActive(false);
        _creditsGame.SetActive(true);
    }

}
