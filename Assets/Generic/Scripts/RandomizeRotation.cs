using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeRotation : MonoBehaviour
{
    
    void Start()
    {
        float angle = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

}
