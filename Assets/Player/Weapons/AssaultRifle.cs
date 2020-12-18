using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IWeapon
{
    public static float Speed { get; private set; } = 5;
    public static float Cooldown { get; private set; } = 0.25f;
    public static float ReloadCooldown { get; private set; } = 2f;
    public static float Damage { get; private set; } = 0.3f;
    public static bool IsAvailable { get; private set; } = false;
    public static int Ammo { get; private set; } = 30;

    private static readonly int startAmmo = Ammo;
    public void Unlock()
    {
        IsAvailable = true;
    }

    public void Shoot(GameObject bulletPrefab, Transform transform, float x, float y)
    {
        if (Ammo <= 0)
        {
            Reload();
        }
        else
        {
            Ammo--;
        }
        float randomY = Random.Range(-0.2f, 0.2f);
        float randomX = Random.Range(-0.2f, 0.2f);
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
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
}
