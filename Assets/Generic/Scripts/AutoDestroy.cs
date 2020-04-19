using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    
    [SerializeField] private float m_Time = 5f;
    public float Time { get => m_Time; }

    void Start()
    {
        Destroy(gameObject, m_Time);
    }

}
