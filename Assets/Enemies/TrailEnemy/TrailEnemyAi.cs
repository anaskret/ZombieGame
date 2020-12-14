using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEnemyAi : EnemyModel
{
    [SerializeField] private GameObject smallAcidPrefab;

    private float cooldown = 0;
    private readonly float cooldownTime = 3;
    private readonly float abilityTime = 4;

    protected override void Awake()
    {
        base.Awake();
        cooldown += Time.time + abilityTime;
    }

    public override void AttackPlayer()
    {
        if (isInContact == false)
        {
            if (cooldown >= Time.time)
            { 
                _ = Instantiate(smallAcidPrefab, transform.position, transform.rotation); 
            }
            
            if(cooldown + abilityTime < Time.time)
            {
                cooldown += Time.time + cooldownTime;
            }
        }

        base.AttackPlayer();
    }
}
