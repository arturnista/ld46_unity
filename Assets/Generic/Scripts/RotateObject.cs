using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    
    [SerializeField] private float _speed;

    void Update()
    {
        transform.Rotate(0f, 0f, _speed * Time.deltaTime);
    }

}
