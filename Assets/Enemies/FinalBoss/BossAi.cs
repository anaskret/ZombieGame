using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : EnemyModel
{
    [SerializeField] private GameObject acidBulletPrefab;
    private float lastFire;
    [SerializeField] private float fireDelay;

    [SerializeField] private GameObject[] targets; 

    [SerializeField] private GameObject bulletPrefab;
    private float lastFireInAllDirections;
    [SerializeField] private float fireInAllDirectionsDelay;

    public override void AttackPlayer()
    {
        if (Time.time > lastFire + fireDelay)
        {
            Instantiate(acidBulletPrefab, transform.position, transform.rotation);
            lastFire = Time.time;
        }
        if(Time.time > lastFireInAllDirections + fireInAllDirectionsDelay)
        {
            foreach(var target in targets)
            {
                var bullet =Instantiate(bulletPrefab, transform.position, transform.rotation);
                var bulletController = bullet.GetComponent<BulletController>();

                bulletController.target = target;
            }
            lastFireInAllDirections = Time.time;
        }
    }
}
