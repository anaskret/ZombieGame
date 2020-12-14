using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IWeapon
{
    public float Speed { get; private set; } = 5;
    public float Cooldown { get; private set; } = 2;
    public float Damage { get; private set; } = 1;
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

        var bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet2.GetComponent<Rigidbody2D>().gravityScale = 0;
        bullet2.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x == -1 && y == -1) ? (x + 0.1f) * Speed : (x == 1 && y == 1) ? x * Speed: (x - 0.1f) * Speed,
            (x == 1 && y == 1) ? (y + 0.1f) * Speed : (x == -1 && y == -1) ? y * Speed : (y - 0.1f) * Speed,
            0);

        var bullet3 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
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
        return Cooldown;
    }

    public float GetWeaponDamage()
    {
        return Damage;
    }
}
