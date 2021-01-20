﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IWeapon
{
    public static float Speed { get; private set; } = 7.5f;
    public static float Cooldown { get; private set; } = 0.25f;
    public static float ReloadCooldown { get; private set; } = 2f;
    public static float Damage { get; private set; } = 0.3f;
    public static bool IsAvailable { get; private set; } = false;
    public static int Ammo { get; private set; } = 30;

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

    public void Shoot(GameObject bullet, Transform transform, float x, float y)
    {
        if (Ammo <= 0)
            return;

        Ammo--;
        float randomY = Random.Range(-0.2f, 0.2f);
        float randomX = Random.Range(-0.2f, 0.2f);

        //var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x + randomX) * Speed,
            (y + randomY) * Speed,
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

    public void Shoot(GameObject bulletPrefab, GameObject bulletPrefab2, GameObject bulletPrefab3, Transform transform, float x, float y)
    {
        throw new System.NotImplementedException();
    }

    public float GetWeaponAmmo()
    {
        return Ammo;
    }
}
