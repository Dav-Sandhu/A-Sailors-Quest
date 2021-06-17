using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothspeed = 0.125f;

    void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;

        if (transform.position.x > target.position.x) 
        {
            desiredPos.x = transform.position.x;
        }

        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothspeed);
        transform.position = smoothPos;
    }
}
