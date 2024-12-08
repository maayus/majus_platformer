using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    private Camera cam;


    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        var mouse_position = Input.mousePosition;
        var world_position = cam.ScreenToWorldPoint(mouse_position);
        transform.LookAt(world_position, Vector3.forward);
    }
}
