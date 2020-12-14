using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
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
            collision.GetComponent<BasicEnemyAi>().ChangeEnemyHealth(-PlayerModel.Damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("SpittingEnemy"))
        {
            collision.GetComponent<SpittingEnemyAi>().ChangeEnemyHealth(-PlayerModel.Damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Boomer"))
        {
            collision.GetComponent<BoomerAi>().ChangeEnemyHealth(-PlayerModel.Damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("TrailEnemy"))
        {
            collision.GetComponent<TrailEnemyAi>().ChangeEnemyHealth(-PlayerModel.Damage);
            Destroy(gameObject);
        }
    }
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
