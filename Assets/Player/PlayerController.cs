using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movement
    [SerializeField] private float speed;
    Rigidbody2D newRigidbody;


    //shooting
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    private float lastFire;
    private float currentCooldown = 1;

    private bool dpadInput = false;

    void Start()
    {
        newRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(PlayerModel.Health == 0)
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

        switch((int)PlayerModel.SelectedWeapon)
        {
            case 0: currentCooldown = Pistol.Cooldown;
                break;
            case 1: currentCooldown = Shotgun.Cooldown;
                break;
            case 2: currentCooldown = Rifle.Cooldown;
                break;
        }

        if (CanShoot(shootVertical, shootHorizontal) && Time.time > lastFire + currentCooldown)
        {
            switch ((int)PlayerModel.SelectedWeapon)
            {
                case 0:
                    Pistol.Shoot(bulletPrefab, transform, shootHorizontal, shootVertical);
                    break;
                case 1:
                    Shotgun.Shoot(bulletPrefab, transform, shootHorizontal, shootVertical);
                    break;
                case 2:
                    Rifle.Shoot(bulletPrefab, transform, shootHorizontal, shootVertical);
                    break;
            }
            
            lastFire = Time.time;
        }
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
        }
        else if(dpadX == 1f && dpadInput)
        {
            StartCoroutine(DpadControl(true));
            PlayerModel.ChangeWeapon(1);
        }
    }

    private bool CanShoot(float y, float x)
    {
        if (((y < 1 && y > 0) || (y > -1 && y < 0)) && ((x < 1 && x > 0) || (x > -1 && x < 0)))
            return false;

        return true;
    }

    IEnumerator DpadControl(bool input)
    {
        dpadInput = false;
        yield return new WaitForSeconds(0.5f); 
        if (input == false) PlayerModel.ChangeWeapon(-1);  
        if (input == true) PlayerModel.ChangeWeapon(1);  

        StopCoroutine(nameof(DpadControl));
    }
}
