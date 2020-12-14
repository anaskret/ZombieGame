using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    [SerializeField] private GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var grandadeRigidbody = gameObject.GetComponent<Rigidbody2D>();
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Coin"))
        {
            grandadeRigidbody.velocity = new Vector2(0, 0);
        }
    }

    public void ThrowGrenade(float x, float y)
    {
        Debug.Log(x);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(
            x * speed,
            y * speed,
            0);
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //Boom();
        Destroy(gameObject);
    }
}
