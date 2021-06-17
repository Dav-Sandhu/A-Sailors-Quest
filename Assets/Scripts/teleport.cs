using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    private bool dir;
    private float timer;

    void Start()
    {
        timer = 1f;
        dir = true;
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<Player>().teleport <= 5)
            {
                col.gameObject.GetComponent<Player>().teleport += 1;
                col.gameObject.GetComponent<Player>().extraJump = true;
            }
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
