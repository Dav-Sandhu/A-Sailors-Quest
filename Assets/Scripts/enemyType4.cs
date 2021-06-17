using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyType4 : MonoBehaviour
{
    public GameObject laser;
    public Camera cam;
    public Transform player;
    private Vector3 target;
    private Vector3 initPos;
    private float timer;
    private int curDir;
    private bool mov;
    private bool stop;
    private bool invisible;
    private bool active;
    private Vector3 viewPos;

    void Start()
    {
        mov = true;
        stop = false;
        active = false;

        target = new Vector3(0.0f, -0.25f, 0.0f);
        timer = 3.0f;
        curDir = 0;

        cam = UnityEngine.Camera.main;
        initPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        invisible = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().invisible;
        viewPos = cam.WorldToViewportPoint(transform.position);

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.z > 0)
        {
            active = true;
        }

        if (!invisible && active)
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
            else { stop = false; }

            if (timer <= 0)
            {
                FindObjectOfType<AudioManager>().Play("flame");
                GameObject projectile = Instantiate(laser, transform.position + target, laser.transform.rotation) as GameObject;
                timer = 3.0f;
                mov = false;
            }
            else if (timer <= 1.75f && timer > 0)
            {
                FindObjectOfType<AudioManager>().Stop("flame");
                mov = true;
            }

            if (mov)
            {
                if (transform.position.x > player.position.x)
                {
                    movEnemy(-1);
                }
                else
                {
                    movEnemy(1);
                }
            }
        }

        if (transform.position.y <= initPos.y) 
        {
            transform.position = new Vector3(transform.position.x, initPos.y, transform.position.z);
        }

        if (transform.position.y <= -10f) { Destroy(gameObject); }
    }

    void movEnemy(float n)
    {
        if (!stop) { transform.position += (new Vector3(5, 0, 0) * Time.deltaTime * n); }
    }
}
