using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    private bool dir;
    private float timer;

    void Start() 
    {
        timer = 1f;
        dir = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Player>().invisible = true;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0) 
        {
            timer = 1f;
            dir = !dir;
        }

        if (dir)
        {
            transform.position += new Vector3(0f, Time.deltaTime, 0f);
        }
        else 
        {
            transform.position -= new Vector3(0f, Time.deltaTime, 0f);
        }
    }
}
