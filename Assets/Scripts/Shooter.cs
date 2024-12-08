using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bullet_prefab;
    public Transform fire_point;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouse_position = Input.mousePosition;
            var world_position = Camera.main.ScreenToWorldPoint(mouse_position);
            world_position.z = 0;
            var direction = (world_position - fire_point.position).normalized;

            var bullet = Instantiate(bullet_prefab, fire_point.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().direction = direction;
        }
    }
}
