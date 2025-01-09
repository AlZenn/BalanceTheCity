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

    private void Start()
    {
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
        maxMoney = 25;
        maxCleanliness = 17;
        maxPower = 20;
        maxHappiness = 20;

        SaveMaxValues(); // Maksimum değerleri kaydet
        UpdateResourceTexts();
        ClosePanelAndResumeGame();
    }

    private void SelectEnvironmentalPresident()
    {
        maxCleanliness = 25;
        maxPower = 17;
        maxHappiness = 20;
        maxMoney = 20;

        SaveMaxValues(); // Maksimum değerleri kaydet
        UpdateResourceTexts();
        ClosePanelAndResumeGame();
    }

    private void SelectPowerFocusedPresident()
    {
        maxPower = 25;
        maxHappiness = 17;
        maxCleanliness = 20;
        maxMoney = 20;

        SaveMaxValues(); // Maksimum değerleri kaydet
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
