using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIChangeSceneButton : MonoBehaviour
{
    
    [SerializeField] private string _sceneName;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(_sceneName));
    }

}
