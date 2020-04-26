using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioOptions : MonoBehaviour
{
    
    [SerializeField] private AudioConfiguration _audioConfiguration = default;
    [Space]
    [SerializeField] private UISlider _masterSlider = default;
    [SerializeField] private UISlider _musicSlider = default;
    [SerializeField] private UISlider _sfxSlider = default;

    void OnEnable()
    {
        _masterSlider.Value = _audioConfiguration.MasterVolume;
        _musicSlider.Value = _audioConfiguration.MusicVolume;
        _sfxSlider.Value = _audioConfiguration.EffectsVolume;

        _masterSlider.OnValueChanged.AddListener(HandleMasterChange);
        _musicSlider.OnValueChanged.AddListener(HandleMusicChange);
        _sfxSlider.OnValueChanged.AddListener(HandleSfxChange);
    }

    void OnDisable()
    {
        _masterSlider.OnValueChanged.RemoveListener(HandleMasterChange);
        _musicSlider.OnValueChanged.RemoveListener(HandleMusicChange);
        _sfxSlider.OnValueChanged.RemoveListener(HandleSfxChange);
    }

    void HandleMasterChange(float volume)
    {
        _audioConfiguration.MasterVolume = Mathf.RoundToInt(volume);
    }

    void HandleMusicChange(float volume)
    {
        _audioConfiguration.MusicVolume = Mathf.RoundToInt(volume);
    }

    void HandleSfxChange(float volume)
    {
        _audioConfiguration.EffectsVolume = Mathf.RoundToInt(volume);
    }

    public void Apply()
    {
        _audioConfiguration.Apply();
        ConfigurationManager.SaveAudioToFile(_audioConfiguration);
    }

}
