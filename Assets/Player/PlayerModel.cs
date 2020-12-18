using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerModel
{
    //Health
    public static float Health { get; private set; } = 10;
    public static float MaxHealth { get; private set; } = 10;

    //cooldown after which player can receive damage
    public static float DamageCooldown { get; private set; } = 0;

    //Weapons and their damage
    public static Weapons SelectedWeapon { get; private set; } = Weapons.Pistol;
    public static float Damage { get; private set; } = 1;

    //save checkpoint
    public static GameObject CurrentCheckpoint { get; private set; }
    
    //players coins
    public static int Coins { get; private set; } = 30;

    //grenades
    public static int AvailableGrenades { get; private set; } = 1;

    //first aid kits
    public static int AvailableFirstAidKits { get; private set; } = 1;
    public static float FirstAidKitRegeneration{ get; private set; } = 2;

    //quests
    public static List<string> ActiveQuests { get; private set; } = new List<string>();
    public static List<string> CompletedQuests { get; private set; } = new List<string>();

    //weapon changing
    private static int weaponIndex = 0;
    private static bool skip = true;

    //weapon cooldown
    private static readonly float damageRate = 3;

    //saves state of the players health and inventory when he reached the checkpoint
    private static float healthOnCheckpoint = 5;
    private static int grenadesOnCheckpoint = 1;
    private static int firstAidKitsOnCheckpoint = 1;
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
        else if((change > 0 && AvailableFirstAidKits > 0) && Health < MaxHealth)
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
            weaponIndex = 3;
        }
        else if (weaponIndex > 3) //domyslnie 3
        {
            weaponIndex = 0;
        }
        Debug.Log("Weapon: " + weaponIndex);

        var weapon = SwitchWeapon(weaponIndex);

        if (weapon.IsWeaponAvailable())
        {
            Damage = weapon.GetWeaponDamage();
        }
        else
        {
            skip = true;
            ChangeWeapon(i);
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
            case 3:
                AssaultRifle ar = weaponInstance.AddComponent<AssaultRifle>();
                SelectedWeapon = Weapons.AssaultRifle;
                return ar;
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
        grenadesOnCheckpoint = AvailableGrenades;
        firstAidKitsOnCheckpoint = AvailableFirstAidKits;
    }

    public static void ReloadCheckpoint(GameObject player)
    {
        player.transform.position = CurrentCheckpoint.transform.position;
        Health = healthOnCheckpoint;
        AvailableGrenades = grenadesOnCheckpoint;
        AvailableFirstAidKits = firstAidKitsOnCheckpoint;
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
