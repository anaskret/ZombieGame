﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Shoot(GameObject bulletPrefab, Transform transform, float x, float y);
    void Shoot(GameObject bulletPrefab, GameObject bulletPrefab2, GameObject bulletPrefab3, Transform transform, float x, float y);
    void Unlock();
    bool IsWeaponAvailable();
    float GetWeaponSpeed();
    float GetWeaponCooldown();
    float GetWeaponDamage();
    float GetWeaponAmmo();
    void Reload();
}
