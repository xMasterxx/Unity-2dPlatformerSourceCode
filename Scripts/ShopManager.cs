using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text playersCoins;
    public List<ShopItem> shopItems;
    
    void Update()
    {
        PlayerCoins();
    }

    public void DisableSkins()
    {
        foreach (var shopItem in shopItems.Where(x => x.isActive))
        {
            shopItem.isActive = false;
        }
    }

    void PlayerCoins()
    {
        playersCoins.text = SaveGame.Load<int>("Coins").ToString();
    }
}
