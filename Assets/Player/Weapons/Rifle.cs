using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public static float Speed { get; private set; } = 10;
    public static float Cooldown { get; private set; } = 3;
    public static float Damage { get; private set; } = 2;
    public static bool IsAvailable { get; private set; } = true;

    public static void Shoot(GameObject bulletPrefab, Transform transform, float x, float y)
    {
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            x * Speed,
            y * Speed,
            0);
    }
}
