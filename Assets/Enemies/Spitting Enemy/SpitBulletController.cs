using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitBulletController : MonoBehaviour
{
    public float lifeTime;
    private GameObject player;
    private Vector3 startPosition;
    [SerializeField] GameObject vatPrefab;
    [SerializeField] private float bulletSpeed;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        BulletMovement();

        if(Vector3.Distance(transform.position, startPosition) < 0.02f)
        {
            CreateVatOfAcid();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerModel.ChangeHealth(-0.5f);
            CreateVatOfAcid();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void BulletMovement()
    {
        var dif = startPosition - transform.position;
        transform.position += dif.normalized * Time.deltaTime * bulletSpeed;
    }

    private void CreateVatOfAcid()
    {
        _ = Instantiate(vatPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
