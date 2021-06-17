using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    private Vector3 pos; 
    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);
    }
}
