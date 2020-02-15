using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class LevelManager : MonoBehaviour
{
    public GameObject button;
    public Transform lvlPanel;
    public List<Level> levelList;
    

    void Start()
    {
        FillList();
    }


    void FillList()
    {
        var lvlNumber = 0;
        foreach (var level in levelList)
        {
            //create new buttons
            var newButton = Instantiate(button) as GameObject;
            var levelButtonScript = newButton.GetComponent<LevelButton>();
            ++lvlNumber;
            levelButtonScript.levelText.text = level.levelText = lvlNumber.ToString();

            if (SaveGame.Load<int>("Level" + levelButtonScript.levelText.text) == 1)
            {
                level.itIsunlocked = 1;
                level.isInteractable = true;
            }

            levelButtonScript.isUnlocked = level.itIsunlocked;
            levelButtonScript.GetComponent<Button>().interactable = level.isInteractable;
            levelButtonScript.GetComponent<Button>().onClick.AddListener(() => LoadLevels("Level" + levelButtonScript.levelText.text));

            //Stars number
            if (SaveGame.Load<int>("Level" + levelButtonScript.levelText.text + "_score") > 0)
            {
                levelButtonScript.star1.SetActive(true);
            }

            if (SaveGame.Load<int>("Level" + levelButtonScript.levelText.text + "_score") > 50)
            {
                levelButtonScript.star2.SetActive(true);
            }

            if (SaveGame.Load<int>("Level" + levelButtonScript.levelText.text + "_score") > 75)
            {
                levelButtonScript.star3.SetActive(true);
            }

            newButton.transform.SetParent(lvlPanel);
        }
        SaveAll();
    }
    public void DeleteAll()
    {
        SaveGame.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void SaveAll()
    {
            GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
            foreach (var buttons in allButtons)
            {
                LevelButton levelButtonScript = buttons.GetComponent<LevelButton>();
                SaveGame.Save<int>("Level" + levelButtonScript.levelText.text, levelButtonScript.isUnlocked);
            }
        
    }

    void LoadLevels(string value)
    {
        SceneManager.LoadScene(value);
    }

    public void LoadLastOpendLvl()
    {
        foreach (var lvl in levelList.Where(x => x.isInteractable))
        {
            SceneManager.LoadScene($"Level{lvl.levelText}");
        }
    }
}

[System.Serializable]
public class Level
{
    public string levelText;
    public int itIsunlocked;
    public bool isInteractable;

}

