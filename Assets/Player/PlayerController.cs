using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData
{
    public double PositionX { get; private set; }
    public double PositionY { get; private set; }
    public double Health { get; private set; }

    public PlayerData(double positionX, double positionY, double health)
    {
        PositionX = positionX;
        PositionY = positionY;
        Health = health;
    }
    public PlayerData()
    { }
}

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
    [SerializeField] private GameObject questScreenPrefab;
    private GameObject currentWeapon;

    //shooting
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject flamePrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float flameSpeed;

    [SerializeField] private GameObject pistolPoint;
    [SerializeField] private GameObject shotgunPoint;
    [SerializeField] private GameObject riflePoint;
    [SerializeField] private GameObject arPoint;
    [SerializeField] private GameObject flamethrowerPoint;

    [SerializeField] private GameObject pistolModel;
    [SerializeField] private GameObject shotgunModel;
    [SerializeField] private GameObject rifleModel;
    [SerializeField] private GameObject arModel;
    [SerializeField] private GameObject flamethrowerModel;

    private GameObject currentWeaponModel;

    private float lastFire;
    private float currentCooldown = 1;

    [SerializeField] private GameObject grenadePrefab;

    [SerializeField] private Text ammo;

    private bool dpadInput = false;

    public string SaveID { get; } = "player";

    private IWeapon weapon;

    public JsonData SavedData { 
        get
        {
            var data = new PlayerData(transform.position.x, transform.position.y, PlayerModel.Health);
            var serizalizeData = JsonMapper.ToJson(data);
            return serizalizeData;
        }
    }

    void Start()
    {
        newRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = gameObject.GetComponent<Animator>();

        slider.value = PlayerModel.CalculateHealth();

        currentWeapon = pistol;
        currentWeaponModel = pistolModel;
        weapon = PlayerModel.SwitchWeapon((int)PlayerModel.SelectedWeapon);
    }

    private void FixedUpdate()
    {
        slider.value = PlayerModel.CalculateHealth();
        numberOfCoins.text = PlayerModel.Coins.ToString();
        numberOfFirstAidKits.text = PlayerModel.AvailableFirstAidKits.ToString();
        numberOfGrenades.text = PlayerModel.AvailableGrenades.ToString();
        ammo.text = $"Ammo: {weapon.GetWeaponAmmo()}";

        OpenQuests();

        if (startBlinking)
        {
            StartBlinking();
        }

        if (PlayerModel.Health <= 0)
        {
            /*SavingService.LoadGame("newCheckpoint.json");*/
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

        weapon = PlayerModel.SwitchWeapon((int)PlayerModel.SelectedWeapon);
        currentCooldown = weapon.GetWeaponCooldown();


        if (CanShoot(shootVertical, shootHorizontal) && Time.time > lastFire + currentCooldown)
        {
            Vector3 lookVec = new Vector3(shootHorizontal, shootVertical, 4096);
            transform.rotation = Quaternion.LookRotation(lookVec, Vector3.back);

            var gunPoint = GetGunPoint();

            if ((int)PlayerModel.SelectedWeapon == 4)
            {
                GameObject flame = ObjectPool.SharedInstance.GetPooledFlameObject();

                if (flame != null)
                {
                    SetUpBullet(flame, gunPoint);
                    weapon.Shoot(flame, gunPoint.transform, shootHorizontal, shootVertical);
                }
            }
            else if((int)PlayerModel.SelectedWeapon == 1)
            {
                GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
                if (bullet == null)
                    return;

                SetUpBullet(bullet, gunPoint);
                GameObject bullet2 = ObjectPool.SharedInstance.GetPooledObject();
                if (bullet2 == null)
                    return;
                
                SetUpBullet(bullet2, gunPoint);
                GameObject bullet3 = ObjectPool.SharedInstance.GetPooledObject();
                if (bullet3 == null)
                    return;

                SetUpBullet(bullet3, gunPoint);
                
                weapon.Shoot(bullet, bullet2, bullet3, gunPoint.transform, shootHorizontal, shootVertical);
                
            }
            else
            {
                GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
                if (bullet != null)
                {
                    SetUpBullet(bullet, gunPoint);
                    weapon.Shoot(bullet, gunPoint.transform, shootHorizontal, shootVertical);
                }
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

    public void LoadFromData(JsonData data)
    {
        var playerObject = JsonMapper.ToObject<PlayerData>(data.ToJson());
        transform.position = new Vector3((float)playerObject.PositionX, (float)playerObject.PositionY);
        PlayerModel.LoadModel((float)playerObject.Health);
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
            default:
                ShowCurrentWeapon(pistol);
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

    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 3.0f;
    [HideInInspector]
    public bool startBlinking = false;

    public void StartBlinking()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    private void ShowCurrentWeapon(GameObject weapon)
    {
        currentWeaponModel.SetActive(false);
        currentWeapon.SetActive(false);

        weapon.SetActive(true);
        var weaponModel = GetWeaponModel();
        weaponModel.SetActive(true);

        currentWeaponModel = weaponModel;
        currentWeapon = weapon;
    }

    public void IsTalking(bool isTalking)
    {
        myAnimator.SetBool("isTalking", isTalking);
    }

    private GameObject GetGunPoint()
    {
        int index = (int)PlayerModel.SelectedWeapon;
        switch (index)
        {
            case 0: return pistolPoint;
            case 1: return shotgunPoint;
            case 2: return riflePoint;
            case 3: return arPoint;
            case 4: return flamethrowerPoint;
            default: return pistolPoint;
        }
    }

    private GameObject GetWeaponModel()
    {
        int index = (int)PlayerModel.SelectedWeapon;
        switch (index)
        {
            case 0: return pistolModel;
            case 1: return shotgunModel;
            case 2: return rifleModel;
            case 3: return arModel;
            case 4: return flamethrowerModel;
            default: return pistolModel;
        }
    }

    private void SetUpBullet(GameObject bullet, GameObject gunPoint)
    {
        bullet.transform.position = gunPoint.transform.position;
        bullet.transform.rotation = gunPoint.transform.rotation;
        bullet.SetActive(true);
    }

    private void OpenQuests()
    {
        if (Input.GetKeyDown("joystick button 6")) 
        {
            Instantiate(questScreenPrefab);
        }
    }
}
