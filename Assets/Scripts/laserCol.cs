using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserCol : MonoBehaviour
{
    private float timer;
    private bool hit;

    void Start() 
    {
        hit = false;
        timer = 0.25f;
    }

    void Update() 
    {
        if (hit) 
        {
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
            else 
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void OnParticleCollision(GameObject o)
    {
        if (o.tag == "Player" && !hit)
        {
            hit = true;
            o.GetComponent<Player>().health -= 1;
        }
    }
}
