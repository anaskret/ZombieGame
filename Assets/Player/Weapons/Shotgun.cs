using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public static float Speed { get; private set; } = 5;
    public static float Cooldown { get; private set; } = 2;
    public static float Damage { get; private set; } = 1;
    public static bool IsAvailable { get; private set; } = true;


    public static void Unlock()
    {
        IsAvailable = true;
    }
    public static void Shoot(GameObject bulletPrefab, Transform transform, float x, float y)
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
}
