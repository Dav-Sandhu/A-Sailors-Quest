using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public Camera cam;

    private float speed;
    private bool stop;
    private Vector3 viewPos;
    private Vector3 startPos;
    private Vector3 size;
    private float timer;
    private float timer2;
    private float groundSize;
    public bool immunity;
    public bool tempstop;
    private bool active;
    public int health;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = UnityEngine.Camera.main;

        immunity = false;
        stop = false;
        tempstop = false;
        active = false;
        health = 5;
        speed = 5f;
        size = GetComponent<Collider>().bounds.size;
        startPos = transform.position;
        timer = 0.1f;
        timer2 = 2f;
    }

    void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "Player") 
        {
            stop = false;
        }
    }

    void Update()
    {
        if (immunity)      
        {
            timer -= Time.deltaTime;
            if(timer <= 0) 
            { 
                immunity = false;
                if (health > 0)
                {
                    tempstop = true;
                    transform.position = new Vector3(transform.position.x + 15f, startPos.y, transform.position.z);
                }
            }
        }
        else { timer = 0.1f; }

        viewPos = cam.WorldToViewportPoint(transform.position);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            groundSize = (hit.collider.GetComponent<Collider>().bounds.size.y / 2);

            if ((transform.position.y <= ((size.y / 2) + hit.transform.position.y + groundSize)) && hit.collider.tag == "floor")
            {
                stop = false;
            }
        }
        
        if (transform.position.y < -5) 
        {
            transform.position = new Vector3(transform.position.x, -5f, transform.position.z);
            stop = false;
        }

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.z > 0) 
        {
            active = true;
        }

        if ( active && !stop && !tempstop)
        {
            if (startPos.y <= transform.position.y)
            {
                if ((player.position.x + (size.x / 2)) > (transform.position.x) && (player.position.x < transform.position.x + (size.x / 2)))
                {
                    StartCoroutine(attack());
                }
                else if (player.position.x > transform.position.x)
                {
                    transform.position += new Vector3(1f, 0, 0) * Time.deltaTime * speed;
                }
                else if (player.position.x < transform.position.x)
                {
                    transform.position += new Vector3(-1f, 0, 0) * Time.deltaTime * speed;
                }
            }
            else 
            {
                transform.position += new Vector3(0, 1f, 0) * Time.deltaTime * speed;
            }
        }
        else if (stop) 
        {
            transform.position += new Vector3(0, -1f, 0) * Time.deltaTime * speed * speed;
        }

        if (tempstop) 
        {
            timer2 -= Time.deltaTime;

            if (timer2 <= 0) 
            {
                tempstop = false;
                timer2 = 2f;
            }
        }
    }

    IEnumerator attack() 
    {
        stop = true;
        yield return new WaitForSeconds(0.5f);
    }
}
