using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyType3 : MonoBehaviour
{
    public Camera cam;
    public Transform player;

    private float timer;
    private float speedup;
    private bool stop;
    private bool invisible;
    private int curDir;
    private Vector3 viewPos;

    void Start()
    {
        cam = UnityEngine.Camera.main;

        speedup = 3.0f;
        curDir = 0;
        stop = false;

        timer = 5.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        viewPos = cam.WorldToViewportPoint(transform.position);
        invisible = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().invisible;

        if (!invisible)
        {
            timer -= Time.deltaTime;

            if (player.position.x + 0.5f >= transform.position.x && player.position.x - 0.5f <= transform.position.x)
            {
                stop = true;
            }
            else if (player.position.x > transform.position.x && curDir == 0)
            {
                transform.RotateAround(transform.position, Vector3.up, 180f);
                curDir = 1;
                stop = false;
            }
            else if (player.position.x < transform.position.x && curDir == 1)
            {
                transform.RotateAround(transform.position, Vector3.up, 180f);
                curDir = 0;
                stop = false;
            }
            else
            {
                stop = false;
            }


            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                if (timer <= 0)
                {
                    speedup = 8.0f;
                    timer = 5.0f;
                }
                else if (timer >= 2 && timer < 3)
                {
                    speedup = 3.0f;
                }

                if (transform.position.x > player.position.x)
                {
                    movEnemy(-1);
                }
                else
                {
                    movEnemy(1);
                }
            }

            if(player.position.y - transform.position.y < .1)
                transform.position += (new Vector3(0, -1, 0) * Time.deltaTime);
            else if(player.position.y - transform.position.y > -.1)
                transform.position += (new Vector3(0, 1, 0) * Time.deltaTime);
      
        }

        if (transform.position.y <= -10f) { Destroy(gameObject); }
    }

    void movEnemy(float n) 
    {
        if (!stop) { transform.position += (new Vector3(1, 0, 0) * Time.deltaTime * n * speedup); }
    }
}
