using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour, IWeapon
{
    public static float Speed { get; private set; } = 4f;
    public static float Cooldown { get; private set; } = 0.1f;
    public static float ReloadCooldown { get; private set; } = 4f;
    public static float Damage { get; private set; } = 0.1f;
    public static bool IsAvailable { get; private set; } = false;
    public static int Ammo { get; private set; } = 100;

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

    public void Shoot(GameObject flame, Transform transform, float x, float y)
    {
        if (Ammo <= 0)
            return;

        Ammo--;

        float randomY = Random.Range(-0.1f, 0.1f);
        float randomX = Random.Range(-0.1f, 0.1f);
        //var flame = Instantiate(flamePrefab, transform.position, transform.rotation);
        flame.GetComponent<Rigidbody2D>().gravityScale = 0;
        flame.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x + randomX) * Speed,
            (y + randomY) * Speed,
            0);
    }

    public bool IsWeaponAvailable()
    {
        return IsAvailable;
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

    public float GetWeaponSpeed()
    {
        return Speed;
    }


    public void Reload()
    {
        Ammo = startAmmo;
    }

    public void Shoot(GameObject bulletPrefab, GameObject bulletPrefab2, GameObject bulletPrefab3, Transform transform, float x, float y)
    {
        throw new System.NotImplementedException();
    }

    public float GetWeaponAmmo()
    {
        return Ammo;
    }
}
