using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BasicEnemy"))
        {
            collision.gameObject.GetComponent<BasicEnemyAi>().ChangeEnemyHealth(-PlayerModel.Damage);
        }
        if (collision.gameObject.CompareTag("SpittingEnemy"))
        {
            collision.gameObject.GetComponent<SpittingEnemyAi>().ChangeEnemyHealth(-PlayerModel.Damage);
        }
        if (collision.gameObject.CompareTag("Boomer"))
        {
            collision.gameObject.GetComponent<BoomerAi>().ChangeEnemyHealth(-PlayerModel.Damage);
        }
        if (collision.gameObject.CompareTag("TrailEnemy"))
        {
            collision.gameObject.GetComponent<TrailEnemyAi>().ChangeEnemyHealth(-PlayerModel.Damage);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerModel.ChangeHealth(-0.5f);
        }
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
