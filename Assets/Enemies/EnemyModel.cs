using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    protected Animator myAnimator;
    protected GameObject player;
    protected bool isInContact = false;

    public virtual float Health { get; protected set; }

    protected virtual void IsDead()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void ChangeEnemyHealth(float change)
    {
        Health += change;
        Debug.Log($"Enemy Health: {Health}");
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInContact = true;
            PlayerModel.ChangeHealth(-0.5f);
        }
    }
}
