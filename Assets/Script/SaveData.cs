using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int happiness;
    public int cleanliness;
    public int power;
    public int money;
    public List<string> purchasedSocietyButtons;
}

public class SaveManager : MonoBehaviour
{
    private SC_ResourceManager resourceManager;
    private ShopManager shopManager;

    private void Start()
    {
        resourceManager = GetComponent<SC_ResourceManager>();
        shopManager = GetComponent<ShopManager>();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData();
        saveData.happiness = resourceManager.happiness;
        saveData.cleanliness = resourceManager.cleanliness;
        saveData.power = resourceManager.power;
        saveData.money = resourceManager.money;
        saveData.purchasedSocietyButtons = new List<string>();

        foreach (var button in shopManager.societyButtonList)
        {
            if (!shopManager.societyButtons[shopManager.societyButtonList.IndexOf(button)].interactable)
            {
                saveData.purchasedSocietyButtons.Add(button.name);
            }
        }

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string json = PlayerPrefs.GetString("SaveData");
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            resourceManager.happiness = saveData.happiness;
            resourceManager.cleanliness = saveData.cleanliness;
            resourceManager.power = saveData.power;
            resourceManager.money = saveData.money;

            foreach (var buttonName in saveData.purchasedSocietyButtons)
            {
                var button = shopManager.societyButtonList.Find(b => b.name == buttonName);
                if (button != null)
                {
                    shopManager.societyButtons[shopManager.societyButtonList.IndexOf(button)].interactable = false;
                }
            }
        }
    }
}