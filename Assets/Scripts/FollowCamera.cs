using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    float xOffset;

    void Start()
    {
        xOffset = transform.position.x - target.position.x;
    }

    void FixedUpdate()
    {
        if(!GameManager.Instance.isPlayerDead)
        {
            Vector3 desiredPosition = new Vector3(target.position.x + xOffset, transform.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
