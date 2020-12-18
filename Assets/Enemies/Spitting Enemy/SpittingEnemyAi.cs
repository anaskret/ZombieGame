using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingEnemyAi : EnemyModel
{ 
    [SerializeField] private GameObject bulletPrefab;
    private float lastFire;
    [SerializeField] private float fireDelay;

    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;

    //waypoints
    private Transform[] waypoints = null;
    private Transform pointA;
    private Transform pointB;
    private int currentTarget;

    protected override void Awake()
    {
        base.Awake();
        pointA = p1.transform;
        pointB = p2.transform;
        waypoints = new Transform[2]
        {
            pointA,
            pointB
        };
        currentTarget = 1;
        debugg = true;
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
        transform.position += dif.normalized * Time.deltaTime * speed;
        if (dif.magnitude < 0.3)
        {
            SetNextPoint();
        }
    }

    public override void AttackPlayer()
    {
        if (Time.time > lastFire + fireDelay)
        {
            _ = Instantiate(bulletPrefab, transform.position, transform.rotation);
            lastFire = Time.time;
        }
    }
}
