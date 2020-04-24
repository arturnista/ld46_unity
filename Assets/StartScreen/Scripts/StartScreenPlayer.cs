using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenPlayer : MonoBehaviour
{

    [SerializeField] private float m_Speed = 7f;
    public float Speed { get => m_Speed; set => m_Speed = value; }
    
    void Update()
    {
        transform.Translate(Vector2.up * m_Speed * Time.deltaTime, Space.Self);
    }

}
