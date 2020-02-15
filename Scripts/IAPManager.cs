using BayatGames.SaveGameFree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{

    public void BuyGold1k()
    {
        var coinsNumber = SaveGame.Load<int>("Coins");
        SaveGame.Save<int>("Coins", coinsNumber += 1000);
    }

    public void BuyGold10k()
    {
        var coinsNumber = SaveGame.Load<int>("Coins");
        SaveGame.Save<int>("Coins", coinsNumber += 10000);
    }


    public void BuyPantsSkin()
    {
        SaveGame.Save<bool>($"Player_pantsPurchasedStatus", true);
    }

}

