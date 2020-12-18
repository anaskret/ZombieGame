using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol: MonoBehaviour, IWeapon
{
    public static float Speed { get; private set; } = 6;
    public static float Cooldown { get; private set; } = 0.6f;
    public static float ReloadCooldown { get; private set; } = 2f;
    public static float Damage { get; private set; } = 1;
    public static bool IsAvailable { get; private set; } = true;
    public static int Ammo{ get; private set; } = 7;

    private static readonly int startAmmo = Ammo;
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

        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            x * Speed,
            y * Speed,
            0);
    }

    public bool IsWeaponAvailable()
    {
        return IsAvailable;
    }

    public void Unlock()
    {
        throw new System.NotImplementedException();
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
