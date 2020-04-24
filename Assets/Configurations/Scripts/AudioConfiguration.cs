using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName="Configuration/Audio")]
public class AudioConfiguration : ScriptableObject
{

    [Header("Default values")]
    [Range(0f, 100)]
    public int MasterVolumeDefault = 100;
    [Range(0f, 100)]
    public int MusicVolumeDefault = 100;
    [Range(0f, 100)]
    public int EffectsVolumeDefault = 100;

    [Header("Mixer")]
	public AudioMixer MusicMixer;
	public AudioMixer EffectsMixer;

    private int m_MasterVolume = 100;
    public int MasterVolume { get => m_MasterVolume; set => m_MasterVolume = value; }

    private int m_MusicVolume = 100;
    public int MusicVolume { get => m_MusicVolume; set => m_MusicVolume = value; }
    
    private int m_EffectsVolume = 100;
    public int EffectsVolume { get => m_EffectsVolume; set => m_EffectsVolume = value; }

    public void Default()
    {
        m_MasterVolume = MasterVolumeDefault;
        m_MusicVolume = MusicVolumeDefault;
        m_EffectsVolume = EffectsVolumeDefault;
    }

    public void Apply()
    {
        float normalizedMusicVolume = NormalizeVolume(m_MusicVolume);
        float normalizedSfxVolume = NormalizeVolume(m_EffectsVolume);
        
        MusicMixer.SetFloat("Volume", Mathf.Log10(normalizedMusicVolume) * 20f);
        EffectsMixer.SetFloat("Volume", Mathf.Log10(normalizedSfxVolume) * 20f);
    }

    float NormalizeVolume(int volume)
    {
        float normalized = (m_MasterVolume / 100f) * (volume / 100f);
        return Mathf.Clamp(normalized, 0.0001f, 1f);
    }
    
}