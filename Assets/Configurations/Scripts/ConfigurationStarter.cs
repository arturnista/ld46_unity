using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationStarter : MonoBehaviour
{
    
    [SerializeField] private AudioConfiguration _audio;

    void Awake()
    {
        ConfigurationManager.LoadAudioFromFile(_audio);

        ConfigurationManager.SaveAudioToFile(_audio);
    }

    void Start()
    {
        _audio.Apply();
    }

}
