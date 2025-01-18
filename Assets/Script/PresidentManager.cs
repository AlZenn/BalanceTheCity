using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresidentManager : MonoBehaviour
{
    public int maxHappiness;
    public int maxCleanliness;
    public int maxPower;
    public int maxMoney;

    // Başkan seçimi için buton referansları
    public Button economicPresidentButton;
    public Button environmentalPresidentButton;
    public Button powerFocusedPresidentButton;

    // Başkan seçimi için panel referansı
    public GameObject presidentSelectionPanel;

    [Header("Save President")]
    private const string KeyIsActive = "ButtonsActive";

    // Başkan seçim anahtarları
    private const string KeyMaxHappiness = "MaxHappiness";
    private const string KeyMaxCleanliness = "MaxCleanliness";
    private const string KeyMaxPower = "MaxPower";
    private const string KeyMaxMoney = "MaxMoney";

    public SC_ResourceManager resourceManager;

    private void Start() // okulda
    {
        //resourceManager = GameObject.FindObjectOfType<SC_ResourceManager>();
        // PlayerPrefs değeri kontrol edilir
        if (PlayerPrefs.GetInt(KeyIsActive, 0) == 1)
        {
            LoadMaxValues(); // Maksimum değerleri yükle
            presidentSelectionPanel.SetActive(false); // Panel devre dışı
            Time.timeScale = 1; // Oyunu devam ettir
        }
        else
        {
            economicPresidentButton.onClick.AddListener(() => OnButtonClicked());
            environmentalPresidentButton.onClick.AddListener(() => OnButtonClicked());
            powerFocusedPresidentButton.onClick.AddListener(() => OnButtonClicked());

            Time.timeScale = 0; // Oyunu durdur
        }

        // Başkan seçimi butonlarına tıklama olayları ekle
        economicPresidentButton.onClick.AddListener(SelectEconomicPresident);
        environmentalPresidentButton.onClick.AddListener(SelectEnvironmentalPresident);
        powerFocusedPresidentButton.onClick.AddListener(SelectPowerFocusedPresident);
    }

    void OnButtonClicked()
    {
        PlayerPrefs.SetInt(KeyIsActive, 1); // Değer 1 olarak ayarlanır
        PlayerPrefs.Save(); // Değişiklikler kaydedilir
        presidentSelectionPanel.SetActive(false); // Panel devre dışı bırakılır
        Time.timeScale = 1; // Oyunu devam ettir
    }

    private void SelectEconomicPresident()
    {
        maxMoney = 40;
        maxCleanliness = 30;
        maxPower = 35;
        maxHappiness = 35;

        PlayerPrefs.SetInt("Baskan",1);
        SaveMaxValues(); // Maksimum değerleri kaydet
        resourceManager.UpdateSliderMaxValues(maxHappiness, maxCleanliness, maxPower, maxMoney);
        UpdateResourceTexts();
        ClosePanelAndResumeGame();
    }

    private void SelectEnvironmentalPresident()
    {
        maxCleanliness = 40;
        maxPower = 30;
        maxHappiness = 35;
        maxMoney = 35;

        PlayerPrefs.SetInt("Baskan", 1);
        SaveMaxValues(); // Maksimum değerleri kaydet
        resourceManager.UpdateSliderMaxValues(maxHappiness, maxCleanliness, maxPower, maxMoney);
        UpdateResourceTexts();
        ClosePanelAndResumeGame();
    }

    private void SelectPowerFocusedPresident()
    {
        maxPower = 40;
        maxHappiness = 30;
        maxCleanliness = 35;
        maxMoney = 35;

        PlayerPrefs.SetInt("Baskan", 1);
        SaveMaxValues(); // Maksimum değerleri kaydet
        resourceManager.UpdateSliderMaxValues(maxHappiness, maxCleanliness, maxPower, maxMoney);
        UpdateResourceTexts();
        ClosePanelAndResumeGame();
    }

    private void SaveMaxValues()
    {
        PlayerPrefs.SetInt(KeyMaxHappiness, maxHappiness);
        PlayerPrefs.SetInt(KeyMaxCleanliness, maxCleanliness);
        PlayerPrefs.SetInt(KeyMaxPower, maxPower);
        PlayerPrefs.SetInt(KeyMaxMoney, maxMoney);
        PlayerPrefs.Save(); // Kaydet
    }

    private void LoadMaxValues()
    {
        maxHappiness = PlayerPrefs.GetInt(KeyMaxHappiness, 0);
        maxCleanliness = PlayerPrefs.GetInt(KeyMaxCleanliness, 0);
        maxPower = PlayerPrefs.GetInt(KeyMaxPower, 0);
        maxMoney = PlayerPrefs.GetInt(KeyMaxMoney, 0);
    }

    private void UpdateResourceTexts()
    {
        // Kaynak metinlerini güncelle
        // Örnek:
        // Debug.Log("Happiness: " + maxHappiness);
        // Debug.Log("Cleanliness: " + maxCleanliness);
        // Debug.Log("Power: " + maxPower);
        // Debug.Log("Money: " + maxMoney);
    }

    private void ClosePanelAndResumeGame()
    {
        presidentSelectionPanel.SetActive(false); // Paneli gizle
        Time.timeScale = 1; // Oyunu devam ettir
    }

    public int GetMaxHappiness() => maxHappiness;
    public int GetMaxCleanliness() => maxCleanliness;
    public int GetMaxPower() => maxPower;
    public int GetMaxMoney() => maxMoney;
}