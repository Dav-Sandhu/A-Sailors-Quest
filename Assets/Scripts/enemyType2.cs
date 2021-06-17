using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyType2 : MonoBehaviour
{
    public Camera cam;
    public Transform player;
    public Material nextMaterial;
    public Material curMaterial;
    public GameObject SmallExplosion;
    public bool invinsible;
    private bool invisible;

    private float timer;
    private float range;
    private float curTime;
    private float countDown;
    private int dmgPlayer;
    private bool pauseTime;
    private Vector3 viewPos;

    void Start()
    {
        cam = UnityEngine.Camera.main;

        curMaterial = gameObject.GetComponent<Renderer>().material;
        nextMaterial = Resources.Load("Transparent Black", typeof(Material)) as Material;
        curTime = 5.0f;
        countDown = 1.0f;
        dmgPlayer = 0;
        pauseTime = false;
        invinsible = false;
        timer = 0.5f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        invisible = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().invisible;
        viewPos = cam.WorldToViewportPoint(transform.position);
        range = player.position.x - transform.position.x;

        if (!invisible)
        {
            if (dmgPlayer > 0) 
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    timer = 0.5f;
                    player.GetComponent<Player>().health -= dmgPlayer;
                    dmgPlayer = 0;
                }
            }

            if (!pauseTime)
            {
                //checks to see if enemy is within camera range or near player for slight offscreen
                if ((viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0) || (range <= 5 && range >= -5))
                {
                    curTime = transportTimer(curTime);
                }
            }
            else if (pauseTime)
            {
                countDown = transportTimer(countDown);

                if (countDown <= 0.0f)
                {
                    matChange();
                    invinsible = false;

                    GameObject explosion = Instantiate(SmallExplosion, transform.position, Quaternion.identity) as GameObject;

                    Collider[] cols = Physics.OverlapSphere(transform.position, 5f);       //gets objects within explosion radius

                    foreach (Collider obj in cols)
                    {
                        Rigidbody rb = obj.GetComponent<Rigidbody>();

                        if (obj.gameObject.tag == "Player")
                        {
                            int temp = Mathf.FloorToInt(range);                 //converts current distance to int
                            if (temp < 0)
                            {
                                temp *= -1;
                            }

                            dmgPlayer = (5 - temp);    //deals damage based on distance from explosion
                        }

                        if (rb != null)
                        {
                            rb.AddExplosionForce(500f, transform.position, 5f);
                        }
                    }

                    pauseTime = false;
                    curTime = 5.0f;
                    countDown = 1.0f;
                }
            }

            if (curTime <= 0.0f && !pauseTime)
            {
                if (range <= 0)
                {
                    transportDir(1.5f);
                }
                else
                {
                    transportDir(-1.5f);
                }

                matChange();

                curTime = 5.0f;

                invinsible = true;
                pauseTime = true;
            }
        }

        if (transform.position.y <= -10f) { Destroy(gameObject); }
    }

    void matChange() 
    {
        gameObject.GetComponent<Renderer>().material = nextMaterial;

        Renderer[] children;
        children = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in children)
        {
            rend.material = nextMaterial;
        }

        nextMaterial = curMaterial;
        curMaterial = gameObject.GetComponent<Renderer>().material;
    }

    void transportDir(float dir) 
    {
        transform.position = player.position + new Vector3(dir, 0, 0);
    }

    float transportTimer(float t) 
    {
        return t -= Time.deltaTime;
    }
}
