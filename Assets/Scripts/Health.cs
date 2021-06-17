using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<Player>().health += 2;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(0, 85 * Time.deltaTime, 0);
    }
}
