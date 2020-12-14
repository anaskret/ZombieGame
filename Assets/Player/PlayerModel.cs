using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerModel
{
    public static float Health { get; private set; } = 10;
    public static float MaxHealth { get; private set; } = 10;
    public static float DamageCooldown { get; private set; } = 0;

    public static Weapons SelectedWeapon { get; private set; } = Weapons.Pistol;
    public static float Damage { get; private set; } = 1;

    public static GameObject CurrentCheckpoint { get; private set; }
    
    public static int Coins { get; private set; } = 0;

    public static int AvailableGrenades { get; private set; } = 1;

    public static int AvailableFirstAidKits { get; private set; } = 1;
    public static float FirstAidKitRegeneration{ get; private set; } = 2;

    public static List<string> ActiveQuests { get; private set; }
    public static List<string> CompletedQuests { get; private set; }

    private static int weaponIndex = 0;
    private static bool skip = true;
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
        if (DamageCooldown < Time.time && change < 0)
        {
            Health += change;
            SetDamageCooldown();
        }
        else if(change > 0 && AvailableFirstAidKits > 0)
        {
            Health += change;
            ChangeNumberOfFirstAidKits(-1);
            Debug.Log($"Player Health: {Health}");
        }
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public static void ChangeWeapon(int i)
    {
        skip = !skip;
        if (skip == true)
        {
            return;
        }

        Debug.Log("Weapon: " + weaponIndex);
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

        var weapon = SwitchWeapon(weaponIndex);

        if(weapon.IsWeaponAvailable())
        {
            Damage = weapon.GetWeaponDamage();
        }
        else
        {
            ChangeWeapon(1);
        }
    }
    
    public static IWeapon SwitchWeapon(int index)
    {
        GameObject weaponInstance = new GameObject();
        switch (index)
        {
            case 0:
                Pistol pistol = weaponInstance.AddComponent<Pistol>();
                SelectedWeapon = Weapons.Pistol;
                return pistol;
            case 1:
                Shotgun shotgun= weaponInstance.AddComponent<Shotgun>();
                SelectedWeapon = Weapons.Shotgun;
                return shotgun;
            case 2:
                Rifle rifle = weaponInstance.AddComponent<Rifle>();
                SelectedWeapon = Weapons.Rifle;
                return rifle;
            default:
                Pistol defaultPistol = weaponInstance.AddComponent<Pistol>();
                SelectedWeapon = Weapons.Pistol;
                return defaultPistol;
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

    public static void ChangeNumberOfCoins(int value)
    {
        Coins += value;
    }

    public static void ChangeNumberOfGrenades(int value)
    {
        AvailableGrenades += value;
    }
    
    public static void ChangeNumberOfFirstAidKits(int value)
    {
        AvailableFirstAidKits += value;
    }

    public static float CalculateHealth()
    {
        return Health / MaxHealth;
    }

    public static void AddActiveQuest(string questName)
    {
        if (ActiveQuests.Contains(questName))
        {
            Debug.Log("Quest name has to be unique");
            return;
        }

        ActiveQuests.Add(questName);
    }

    public static void QuestCompleted(string questName)
    {
        ActiveQuests.Remove(questName);
        CompletedQuests.Add(questName);
    }
}
