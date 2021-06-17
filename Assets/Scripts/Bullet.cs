using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float curTime;
    private float timer;

    void Start()
    {
        curTime = 0.0f;
        timer = 5.0f;
    }

    void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "Player") 
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime >= timer) 
        {
            Destroy(gameObject);
        }
    }
}
