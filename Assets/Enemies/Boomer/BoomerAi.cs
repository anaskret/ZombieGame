using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerAi : EnemyModel
{
    [SerializeField] private GameObject acidPrefab;
    
    protected override void IsDead()
    {
        if(Health <= 0)
            _ = Instantiate(acidPrefab, transform.position, transform.rotation);
        base.IsDead();
    }
}
