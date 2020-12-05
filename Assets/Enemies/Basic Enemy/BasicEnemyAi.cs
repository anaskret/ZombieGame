using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAi : EnemyModel
{
    [SerializeField] private float speed;
    [SerializeField] private float currentDistance;
    [SerializeField] private int health;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myAnimator = GetComponent<Animator>();

        Health = health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsDead();

        currentDistance = Vector3.Distance(player.transform.position, transform.position);

        myAnimator.SetFloat("distanceFromPlayer", currentDistance);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInContact = false;
        }
    }

    public void AttackPlayer()
    {
        if (isInContact == false)
        {
            var dif = player.transform.position - transform.position;
            transform.position += dif.normalized * Time.deltaTime * speed;
        }
    }
}
