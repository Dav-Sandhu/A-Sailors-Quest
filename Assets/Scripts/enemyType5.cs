using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyType5 : MonoBehaviour
{
    public Transform player;
    public Camera cam;

    private Vector3 viewPos;
    private Vector3 size;
    private Vector3 startPos;
    private float groundSize;

    private bool invisible;
    private bool dir;
    private float tempx, tempy;

    void Start()
    {
        cam = UnityEngine.Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        size = GetComponent<Collider>().bounds.size;
        startPos = transform.position;
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("damage");
            FindObjectOfType<AudioManager>().Play("pop");
            Destroy(gameObject);     
        }
    }

    void Update()
    {
        viewPos = cam.WorldToViewportPoint(transform.position);
        invisible = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().invisible;

        if (transform.position.y <= -10f) { Destroy(gameObject); }

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0 && !invisible) 
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                groundSize = (hit.collider.GetComponent<Collider>().bounds.size.y / 2);

                if ((transform.position.y <= ((size.y/2) + hit.transform.position.y + groundSize)) && hit.collider.tag == "floor")
                {
                    dir = false;
                }
            }

            tempx = Input.GetAxis("Horizontal") * Time.deltaTime * -5;
            tempy = 5 * Time.deltaTime;

            if (transform.position.y >= startPos.y)
            {
                dir = true;
            }

            if (dir) 
            {
                tempy *= -1;
            }

            transform.position += new Vector3(tempx, tempy, 0);
        }
    }
}
