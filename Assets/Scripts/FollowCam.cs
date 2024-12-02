using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;

    [Range(0.01f, 0.9f)]
    public float smoothness = 0.125f;

    private Vector3 offset;


    void Start()
    {
        offset = transform.position + Vector3.up - target.position;
    }


    void LateUpdate()
    {
        var targetPos = target.position + offset;

        var smoothTarget = Vector3.Lerp(transform.position, targetPos, 1 - smoothness);

        transform.position = smoothTarget;
    }
}
