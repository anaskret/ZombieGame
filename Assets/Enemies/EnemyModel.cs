using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyModel : MonoBehaviour
{
    public virtual float Health { get; protected set; }
    public virtual bool IsAlive { get; set; } = true;

    [SerializeField] protected float speed;
    [SerializeField] protected int health;
   
    [SerializeField] protected GameObject coinPrefab;
    [SerializeField] protected GameObject healthBarUi;
    [SerializeField] protected Slider slider;

    protected Animator myAnimator;
    protected GameObject player;

    protected bool isInContact = false;
    private float maxHealth;
    private float currentDistance;

    private float startingX;
    private float startingY;

    protected virtual void Start()
    {
        maxHealth = Health;
        slider.value = CalculateHealth();

        startingX = transform.position.x;
        startingY = transform.position.y;
    }

    protected virtual void Awake()
    {
        Physics2D.queriesStartInColliders = false;

        player = GameObject.FindGameObjectWithTag("Player");
        myAnimator = GetComponent<Animator>();

        Health = health;
    }

    protected virtual void FixedUpdate()
    {
        slider.value = CalculateHealth();

        if(Health < maxHealth)
        {
            healthBarUi.SetActive(true);
        }

        IsDead();

        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        myAnimator.SetBool("isPlayerVisible", IsPlayerVisible());
        myAnimator.SetFloat("distanceFromPlayer", currentDistance);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInContact = true;
            PlayerModel.ChangeHealth(-0.5f);
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInContact = false;
        }
    }

    public virtual void AttackPlayer()
    {
        if (isInContact == false)
        {
            var dif = player.transform.position - transform.position;
            transform.position += dif.normalized * Time.deltaTime * speed;
        }
    }

    protected virtual void IsDead()
    {
        if (Health <= 0)
        {
            _ = Instantiate(coinPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
            IsAlive = false;
        }
    }

    public virtual void ChangeEnemyHealth(float change)
    {
        Health += change;
    }

    private float CalculateHealth()
    {
        return Health / maxHealth;
    }

    protected virtual void Respawn()
    {
        transform.position = new Vector3(startingX, startingY);
    }

    protected bool debugg = false;

    protected bool IsPlayerVisible()
    {
        var dif = player.transform.position - transform.position;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dif.normalized, 20);
        Debug.DrawLine(transform.position, dif.normalized);
        if (hits != null)
        {
            foreach (var hit in hits)
            {

                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
                else if (hit.collider.CompareTag("Wall"))
                {
                    return false;
                }
            }
            return true;
        }

        return false;
    }
}
