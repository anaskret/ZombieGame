using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour, IWeapon
{
    public static float Speed { get; private set; } = 2.5f;
    public static float Cooldown { get; private set; } = 0.1f;
    public static float ReloadCooldown { get; private set; } = 4f;
    public static float Damage { get; private set; } = 0.2f;
    public static bool IsAvailable { get; private set; } = true;
    public static int Ammo { get; private set; } = 100;

    private static readonly int startAmmo = Ammo;
    public void Unlock()
    {
        IsAvailable = true;
    } 
    public void Shoot(GameObject flamePrefab, Transform transform, float x, float y)
    {
        if (Ammo <= 0)
        {
            Reload();
        }
        else
        {
            Ammo--;
        }


        float randomY = Random.Range(-0.3f, 0.3f);
        float randomX = Random.Range(-0.3f, 0.3f);
        var flame = Instantiate(flamePrefab, transform.position, transform.rotation);
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


    
}
