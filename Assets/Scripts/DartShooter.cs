using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartShooter : MonoBehaviour
{
    public GameObject bullet_prefab;
    public GameObject player;
    public Transform fire_point;

    void Start()
    {
        StartCoroutine(StartCountdown(2f));
    }

    void shoot()
    {
        var direction = (player.transform.position - transform.position).normalized;

        var bullet = Instantiate(bullet_prefab, fire_point.position, Quaternion.identity);
        bullet.GetComponent<Dart>().direction = direction;
        StartCoroutine(StartCountdown(2f));
    }

    public IEnumerator StartCountdown(float countdownValue = 10)
    {
        float currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(2f);
            currCountdownValue--;
        }
        shoot();
    }
}
