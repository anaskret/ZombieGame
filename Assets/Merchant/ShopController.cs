using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private int riflePrice;
    [SerializeField] private Text riflePriceText;
    [SerializeField] private GameObject rifle;
    
    [SerializeField] private int arPrice;
    [SerializeField] private Text arPriceText;
    [SerializeField] private GameObject ar;

    [SerializeField] private int grenadePrice;
    [SerializeField] private Text grenadePriceText;
    [SerializeField] private Text grenadeYouOwnText;

    [SerializeField] private int firstAidKitPrice;
    [SerializeField] private Text firstAidKitPriceText;
    [SerializeField] private Text firstAidKitYouOwnText;

    [SerializeField] private Text playerCoins;

    private static bool isRifleBought = false;
    private static bool isARBought = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        riflePriceText.text = $"Price: {riflePrice}";
        arPriceText.text = $"Price: {arPrice}";
        firstAidKitPriceText.text = $"Price: {firstAidKitPrice}";
        firstAidKitYouOwnText.text = $"Price: {PlayerModel.AvailableFirstAidKits}";
        grenadePriceText.text = $"Price: {grenadePrice}";
        grenadeYouOwnText.text = $"Price: {PlayerModel.AvailableGrenades}";
        playerCoins.text = $"Your coins: {PlayerModel.Coins}";
    }

    private void Update()
    {
        playerCoins.text = $"Your coins: {PlayerModel.Coins}";
        firstAidKitYouOwnText.text = $"Price: {PlayerModel.AvailableFirstAidKits}";
        grenadeYouOwnText.text = $"Price: {PlayerModel.AvailableGrenades}";

        if (isRifleBought)
        {
            rifle.SetActive(false);
        }
        if (isARBought)
        {
            ar.SetActive(false);
        }
    }

    public void BuyFirstAidKit()
    {
        if (PlayerModel.Coins < firstAidKitPrice)
            return;
        PlayerModel.ChangeNumberOfFirstAidKits(1);
        PlayerModel.ChangeNumberOfCoins(-firstAidKitPrice);
    }

    public void BuyGrenade()
    {
        if (PlayerModel.Coins < grenadePrice)
            return;
        PlayerModel.ChangeNumberOfGrenades(1);
        PlayerModel.ChangeNumberOfCoins(-grenadePrice);
    }

    public void BuyRifle()
    {
        if (PlayerModel.Coins < riflePrice || isRifleBought)
            return;

        var gameObject = new GameObject();
        Rifle rifle = gameObject.AddComponent<Rifle>();
        rifle.Unlock();
        isRifleBought = true;
        PlayerModel.ChangeNumberOfCoins(-riflePrice);
    }
    
    public void BuyAR()
    {
        if (PlayerModel.Coins < arPrice || isARBought)
            return;

        var gameObject = new GameObject();
        AssaultRifle ar = gameObject.AddComponent<AssaultRifle>();
        ar.Unlock();
        isARBought = true;
        PlayerModel.ChangeNumberOfCoins(-arPrice);
    }

    public void EndTalking()
    {
        player.GetComponent<PlayerController>().IsTalking(false);
        Destroy(gameObject);
    }
}
