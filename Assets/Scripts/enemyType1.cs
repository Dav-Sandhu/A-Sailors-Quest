using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyType1 : MonoBehaviour
{
    public GameObject Bullet;
    public Camera cam;
    private float force = 10.0f;
    private Vector3 viewPos;

    public Transform player;
    private Vector3 target;

    private float xshot;
    private float yshot;

    private bool active;
    private bool invisible;

    void Start()
    {
        cam = UnityEngine.Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        active = false;

        StartCoroutine(shoot());
    }

    IEnumerator shoot() 
    {
        while (true) 
        {
            viewPos = cam.WorldToViewportPoint(transform.position);
            invisible = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().invisible;

            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.z > 0)
            {
                active = true;
            }

            if (!invisible && active)
            {

                if (-0.5f <= (transform.position.x - player.position.x) && (transform.position.x - player.position.x) <= 0.5f)
                {
                    xshot = 0;
                }
                else if (transform.position.x > player.position.x)
                {
                    xshot = -1;
                }
                else
                {
                    xshot = 1;
                }

                if (player.position.y > transform.position.y && player.position.y - transform.position.y >= 1.0f)
                {
                    yshot = 1;
                }
                else
                {
                    yshot = 0;
                }

                if (transform.position.y <= -10f) { Destroy(gameObject); }

                target = new Vector3(xshot, yshot, 0);

                GameObject projectile = Instantiate(Bullet, transform.position + target, Quaternion.identity) as GameObject;
                projectile.GetComponent<Rigidbody>().AddForce(target * force, ForceMode.Impulse);
                yield return new WaitForSeconds(2.0f);
            }

            yield return null;
        }
    }
}
