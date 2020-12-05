using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerModel
{
    public static float Health { get; private set; } = 5;
    public static Weapons SelectedWeapon { get; private set; } = Weapons.Pistol;
    public static float DamageCooldown { get; private set; } = 0;
    public static float Damage { get; private set; } = Pistol.Damage;
    public static GameObject CurrentCheckpoint { get; private set; }

    private static int weaponIndex = 0;
    private static bool skip = false;
    private static readonly float damageRate = 3;

    private static float healthOnCheckpoint = 5;
    public enum Weapons
    {
        Pistol = 0,
        Shotgun = 1,
        Rifle = 2,
        AssaultRifle = 3,
    };

    public static void SetDamageCooldown()
    {
        DamageCooldown = Time.time + damageRate;
    }

    public static void ChangeHealth(float change)
    {
        if (DamageCooldown < Time.time)
        {
            Health += change;
            SetDamageCooldown();
            Debug.Log($"Player Health: {Health}");
        }
    }

    public static void ChangeWeapon(int i)
    {
        if(skip == true)
        {
            skip = false;
            return;
        }
        skip = true;

        weaponIndex += i;

        if (weaponIndex < 0)
        { 
            weaponIndex = 2; 
        }
        else if (weaponIndex > 2) //domyslnie 3
        {
            weaponIndex = 0;
        }
        Debug.Log("Weapon: " + weaponIndex);

        switch (weaponIndex)
        {
            case 0: 
                SelectedWeapon = Weapons.Pistol;
                Damage = Pistol.Damage;
                break;
            case 1:
                if (Shotgun.IsAvailable == false)
                {
                    ChangeWeapon(1);
                }
                SelectedWeapon = Weapons.Shotgun;
                Damage = Shotgun.Damage;
                break;
            case 2:
                if(Rifle.IsAvailable == false)
                {
                    ChangeWeapon(1);
                }
                SelectedWeapon = Weapons.Rifle;
                Damage = Rifle.Damage;
                break;
        }
    }
    
    public static void SetCheckpoint(GameObject checkpoint)
    {
        CurrentCheckpoint = checkpoint;
        healthOnCheckpoint = Health;
    }

    public static void ReloadCheckpoint(GameObject player)
    {
        player.transform.position = CurrentCheckpoint.transform.position;
        Health = healthOnCheckpoint;
    }
}
