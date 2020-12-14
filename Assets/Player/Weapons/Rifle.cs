using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour, IWeapon
{
    public float Speed { get; private set; } = 10;
    public float Cooldown { get; private set; } = 3;
    public float Damage { get; private set; } = 2;
    public bool IsAvailable { get; private set; } = true;

    public void Unlock()
    {
        IsAvailable = true;
    }

    public void Shoot(GameObject bulletPrefab, Transform transform, float x, float y)
    {
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
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

    public float GetWeaponSpeed()
    {
        return Speed;
    }

    public float GetWeaponCooldown()
    {
        return Cooldown;
    }

    public float GetWeaponDamage()
    {
        return Damage;
    }
}
