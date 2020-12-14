using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol: MonoBehaviour, IWeapon
{
    public float Speed { get; private set; } = 6;
    public float Cooldown { get; private set; } = 1;
    public float Damage { get; private set; } = 1;
    public bool IsAvailable { get; private set; } = true;

    public void Shoot(GameObject bulletPrefab, Transform transform, float x, float y)
    {
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
        return Cooldown;
    }

    public float GetWeaponDamage()
    {
        return Damage;
    }
}
