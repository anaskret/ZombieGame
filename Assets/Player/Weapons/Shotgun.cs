using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IWeapon
{
    public static float Speed { get; private set; } = 7.5f;
    public static float Cooldown { get; private set; } = 1f;
    public static float ReloadCooldown { get; private set; } = 2f;
    public static float Damage { get; private set; } = 0.5f;
    public static bool IsAvailable { get; private set; } = false;
    public static int Ammo { get; private set; } = 8;

    private static readonly int startAmmo = Ammo;

    private bool isReloading = false;
    private float reloadStart;

    public void Unlock()
    {
        IsAvailable = true;
    }


    private void Update()
    {
        if (Ammo <= 0 && !isReloading)
        {
            isReloading = true;
            reloadStart = Time.time + ReloadCooldown;
        }

        if (reloadStart < Time.time && isReloading)
        {
            Reload();
            isReloading = false;
        }
    }

    public void Shoot(GameObject bullet, GameObject bullet2, GameObject bullet3, Transform transform, float x, float y)
    {
        if (Ammo <= 0)
            return;

        Ammo--;

        //var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            x * Speed,
            y * Speed,
            0);

        //var bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet2.GetComponent<Rigidbody2D>().gravityScale = 0;
        bullet2.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x == -1 && y == -1) ? (x + 0.1f) * Speed : (x == 1 && y == 1) ? x * Speed: (x - 0.1f) * Speed,
            (x == 1 && y == 1) ? (y + 0.1f) * Speed : (x == -1 && y == -1) ? y * Speed : (y - 0.1f) * Speed,
            0);

        //var bullet3 = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet3.GetComponent<Rigidbody2D>().gravityScale = 0;
        bullet3.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x == -1 && y == -1) ? (x - 0.1f) * Speed : (x == 1 && y == 1) ? x * Speed : (x + 0.1f) * Speed,
           (x == 1 && y == 1) ? (y - 0.1f) * Speed : (x == -1 && y == -1) ? y * Speed : (y + 0.1f) * Speed,
            0);
    }

    public bool IsWeaponAvailable()
    {
        return IsAvailable;
    }

    public float GetWeaponSpeed()
    {
        return Speed;
    }

    public float GetWeaponCooldown()
    {
        if (Ammo <= 0)
            return ReloadCooldown;

        return Cooldown;
    }

    public float GetWeaponDamage()
    {
        return Damage;
    }

    public void Reload()
    {
        Ammo = startAmmo;
    }

    public void Shoot(GameObject bulletPrefab, Transform transform, float x, float y)
    {
        throw new System.NotImplementedException();
    }

    public float GetWeaponAmmo()
    {
        return Ammo;
    }
}
