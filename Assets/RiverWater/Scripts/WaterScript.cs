using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private Vector3 StartPos;

    void Start() 
    {
        StartPos = transform.position;
    }

    void Update()
    {
       transform.position -= new Vector3(Time.deltaTime * 2, 0, 0);

        if (StartPos.x - transform.position.x >= 100) 
        {
            transform.position = StartPos;
        }
    }
}
