using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, ISaveable
{
    //movement
    [SerializeField] private float speed;
    Rigidbody2D newRigidbody;
    Animator myAnimator;

    //UI
    [SerializeField] private GameObject healthBarUi;
    [SerializeField] private Slider slider;
    [SerializeField] private Text numberOfCoins;
    [SerializeField] private Text numberOfFirstAidKits;
    [SerializeField] private Text numberOfGrenades;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject rifle;
    [SerializeField] private GameObject assaultRifle;
    [SerializeField] private GameObject flamethrower;

    private GameObject currentWeapon;


    //shooting
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject flamePrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float flameSpeed;
    private float lastFire;
    private float currentCooldown = 1;

    [SerializeField] private GameObject grenadePrefab;

    private bool dpadInput = false;

    public string SaveID { get; } = "player";

    public JsonData SavedData { get; }

    void Start()
    {
        newRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = gameObject.GetComponent<Animator>();

        slider.value = PlayerModel.CalculateHealth();

        currentWeapon = pistol;
    }

    private void FixedUpdate()
    {
        slider.value = PlayerModel.CalculateHealth();
        numberOfCoins.text = PlayerModel.Coins.ToString();
        numberOfFirstAidKits.text = PlayerModel.AvailableFirstAidKits.ToString();
        numberOfGrenades.text = PlayerModel.AvailableGrenades.ToString();

        if (PlayerModel.Health <= 0)
        {
            PlayerModel.ReloadCheckpoint(gameObject);
        }
    }

    public void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        newRigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
    }

    public void InitiateShooting()
    {
        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        var weapon = PlayerModel.SwitchWeapon((int)PlayerModel.SelectedWeapon);
        currentCooldown = weapon.GetWeaponCooldown();


        if (CanShoot(shootVertical, shootHorizontal) && Time.time > lastFire + currentCooldown)
        {
            Vector3 lookVec = new Vector3(shootHorizontal, shootVertical, 4096);
            transform.rotation = Quaternion.LookRotation(lookVec, Vector3.back);
            if ((int)PlayerModel.SelectedWeapon == 4)
            {
                weapon.Shoot(flamePrefab, transform, shootHorizontal, shootVertical);
            }
            else
            {
                weapon.Shoot(bulletPrefab, transform, shootHorizontal, shootVertical);
            }
            lastFire = Time.time;
        }
    }

    public void InitiateGrenadeThrow()
    {
        if (PlayerModel.AvailableGrenades <= 0)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        var grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);

        var grenadeController = grenade.GetComponent<GrenadeController>();
        grenadeController.ThrowGrenade(horizontal, vertical);

        PlayerModel.ChangeNumberOfGrenades(-1);
    }

    public void UseFirstAidKit()
    {
        if (PlayerModel.AvailableFirstAidKits <= 0)
            return;

        PlayerModel.ChangeHealth(PlayerModel.FirstAidKitRegeneration);
    }

    public void WeaponSelect()
    {
        if (Input.GetAxis("DPAD X") == 0.0)
        {
            dpadInput = true;
        }

        float dpadX = Input.GetAxisRaw("DPAD X");

        if (dpadX == -1f && dpadInput)
        {
            StartCoroutine(DpadControl(false));
            PlayerModel.ChangeWeapon(-1);
            CurrentWeapon();
        }
        else if(dpadX == 1f && dpadInput)
        {
            StartCoroutine(DpadControl(true));
            PlayerModel.ChangeWeapon(1);
            CurrentWeapon();
        }
    }

    private bool CanShoot(float y, float x)
    {
        if (((y < 1 && y >= 0) || (y > -1 && y <= 0)) && ((x < 1 && x >= 0) || (x > -1 && x <= 0)))
            return false;

        return true;
    }

    private void CurrentWeapon()
    {
        switch ((int)PlayerModel.SelectedWeapon)
        {
            case 0:
                ShowCurrentWeapon(pistol);
                break;
            case 1:
                ShowCurrentWeapon(shotgun);
                break;
            case 2:
                ShowCurrentWeapon(rifle);
                break;
            case 3:
                ShowCurrentWeapon(assaultRifle);
                break;
            case 4:
                ShowCurrentWeapon(flamethrower);
                break;
        }
    }

    IEnumerator DpadControl(bool input)
    {
        dpadInput = false;
        yield return new WaitForSeconds(0.5f); 
        if (input == false) PlayerModel.ChangeWeapon(-1);  
        if (input == true) PlayerModel.ChangeWeapon(1);  

        StopCoroutine(nameof(DpadControl));
    }


    private void ShowCurrentWeapon(GameObject weapon)
    {
        weapon.SetActive(true);
        currentWeapon.SetActive(false);
        currentWeapon = weapon;
    }

    public void IsTalking(bool isTalking)
    {
        myAnimator.SetBool("isTalking", isTalking);
    }

    public void LoadFromData(JsonData data)
    {
        throw new System.NotImplementedException();
    }
}
