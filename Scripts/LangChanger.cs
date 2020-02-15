using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LangChanger : MonoBehaviour
{
    Button langButton;
    Dictionary<int, string> languages;
    public bool leftButton;
    public Text currentLang;
    [SerializeField] int currentLangIndex;


    void Start()
    {
        langButton = GetComponent<Button>();
        langButton.onClick.AddListener(OnClick);
        Init();

    }

    void Update()
    {
        foreach (var item in languages)
        {
            if (item.Value == PlayerPrefs.GetString("LeanLocalization.CurrentLanguage"))
            {
                currentLangIndex = item.Key;
            }
        }
    }

    void OnClick()
    {
        LeanLocalization.CurrentLanguage = leftButton
            ? currentLangIndex == 1 ? languages[6] : languages[currentLangIndex - 1]
            : currentLangIndex == 6 ? languages[1] : languages[currentLangIndex + 1];
    }

    void Init()
    {
        languages = new Dictionary<int, string>
        {
            {1,"English" },
            {2, "German" },
            {3, "Chinese" },
            {4, "Spanish" },
            {5, "Russian" },
            {6, "Italian" }
        };
    }
}
