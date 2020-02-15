using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    Text priceandStatus;
    Button scinObject;
    Image goldCoinsImage;
    public string id;
    public int price;
    public bool isPurchased;
    public bool isActive;
    public float playerSpeed;
    public float maxLivesNumber;
    int playerCoins;
    ShopManager shopManagerScript;
    

    void Awake()
    {
        shopManagerScript = GameObject.Find("ShopMenu").GetComponent<ShopManager>();
        scinObject = GetComponent<Button>();
        priceandStatus = GetComponentInChildren<Text>();
        goldCoinsImage = priceandStatus.GetComponentInChildren<Image>();
    }

    private void Start()
    {
        scinObject.onClick.AddListener(OnClick);
        if (id != "Player_pants")
        {
            priceandStatus.text = price.ToString();
        }
        
    }

    void Update()
    {
        playerCoins = SaveGame.Load<int>("Coins");
        isActive = (SaveGame.Load<string>("ActiveSkin") == id);
        isPurchased = SaveGame.Load<bool>($"{id}PurchasedStatus");

        if (isPurchased)
        {
            priceandStatus.text = "Activate";
            goldCoinsImage.gameObject.SetActive(false);
        }

        if (isActive)
        {
            scinObject.interactable = false;
            priceandStatus.text = "Activated";
        }
        else if (!isActive && isPurchased)
        {
            scinObject.interactable = true;
            priceandStatus.text = "Activate";
        }
    }

    public void OnClick()
    {
        if (isPurchased)
        {
            var playerStats = new Dictionary<string, float>
            {
                { "maxspeed", playerSpeed },
                { "maxlives", maxLivesNumber }
            };
            SaveGame.Save<Dictionary<string, float>>("playerStats", playerStats);
            SaveGame.Save<string>("ActiveSkin", id);
            shopManagerScript.DisableSkins();
            isActive = true;
        }
        else if (!isPurchased)
        {
            if (id != "Player_pants")
            {
                if (playerCoins >= price)
                {
                    playerCoins -= price;
                    isPurchased = true;
                    SaveGame.Save<bool>($"{id}PurchasedStatus", true);
                    SaveGame.Save<int>("Coins", playerCoins);
                }
                else
                {
                    priceandStatus.text = "Don't enough money";
                    StartCoroutine(ChangeText(priceandStatus, 2f));
                }
            }
            
        }
    }
    private IEnumerator ChangeText(Text _text, float _time)
    {
        yield return new WaitForSeconds(_time);
        _text.text = price.ToString();
    }
}
