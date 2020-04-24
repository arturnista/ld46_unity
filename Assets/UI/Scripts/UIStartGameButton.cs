using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartGameButton : MonoBehaviour
{
    
    private Button _button;
    private GameController _gameController;

    void Awake()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleOnClick);
    }

    void HandleOnClick()
    {
        _gameController.StartGame();
    }

}
