using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayRandomSound : MonoBehaviour
{
    
    [SerializeField] private List<AudioClip> _clips;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_clips.Count > 0)
        {
            _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Count)]);
        }
    }

}
