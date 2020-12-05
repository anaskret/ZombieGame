using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingEnemyAi : EnemyModel
{
    [SerializeField] private float currentDistance;
    [SerializeField] private GameObject bulletPrefab;
    private float lastFire;
    [SerializeField] private float fireDelay;
    [SerializeField] private float enemySpeed;

    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;

    //waypoints
    private Transform[] waypoints = null;
    private Transform pointA;
    private Transform pointB;
    private int currentTarget;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myAnimator = GetComponent<Animator>();
        pointA = p1.transform;
        pointB = p2.transform;
        waypoints = new Transform[2]
        {
            pointA,
            pointB
        };
        currentTarget = 1;

        Health = 6;
    }

    void FixedUpdate()
    {
        IsDead();

        currentDistance = player.transform.position.x - transform.position.x;
        myAnimator.SetFloat("distanceFromPlayer", currentDistance);
    }

    public void SetNextPoint()
    {
        switch (currentTarget)
        {
            case 0:
                currentTarget = 1;
                break;
            case 1:
                currentTarget = 0;
                break;
        }
    }

    public void MoveToCurrentTarget()
    {
        var dif = waypoints[currentTarget].position - transform.position;
        transform.position += dif.normalized * Time.deltaTime * enemySpeed;
        if (dif.magnitude < 0.3)
        {
            SetNextPoint();
        }
    }

    public void Shoot()
    {
        if (Time.time > lastFire + fireDelay)
        {
            _ = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            lastFire = Time.time;
        }
    }
}
