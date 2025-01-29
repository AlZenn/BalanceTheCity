using UnityEngine;
using UnityEngine.UI;

public class PresidentManager : MonoBehaviour
{
    public int maxHappiness;
    public int maxCleanliness;
    public int maxPower;
    public int maxMoney;

    // Başkan seçimi için genel panel referansı
    public GameObject presidentSelectionPanel;

    // Her başkan için ayrı panel referansları
    public GameObject economicPresidentPanel;
    public GameObject environmentalPresidentPanel;
    public GameObject powerFocusedPresidentPanel;

    // Başkan panellerini açan butonlar
    public Button openEconomicPresidentPanelButton;
    public Button openEnvironmentalPresidentPanelButton;
    public Button openPowerFocusedPresidentPanelButton;

    // Başkan seçme butonları ve kapatma butonları
    public Button selectEconomicPresidentButton;
    public Button closeEconomicPresidentPanelButton;

    public Button selectEnvironmentalPresidentButton;
    public Button closeEnvironmentalPresidentPanelButton;

    public Button selectPowerFocusedPresidentButton;
    public Button closePowerFocusedPresidentPanelButton;

    [Header("Save President")]
    private const string KeyIsActive = "ButtonsActive";

    private const string KeyMaxHappiness = "MaxHappiness";
    private const string KeyMaxCleanliness = "MaxCleanliness";
    private const string KeyMaxPower = "MaxPower";
    private const string KeyMaxMoney = "MaxMoney";

    public SC_ResourceManager resourceManager;

    private void Start()
    {
        if (PlayerPrefs.GetInt(KeyIsActive, 0) == 1)
        {
            LoadMaxValues();
            presidentSelectionPanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0; // Oyun durdurulur
        }

        // Panel açma butonlarının işlevlerini bağlama
        openEconomicPresidentPanelButton.onClick.AddListener(() => ShowPanel(economicPresidentPanel));
        openEnvironmentalPresidentPanelButton.onClick.AddListener(() => ShowPanel(environmentalPresidentPanel));
        openPowerFocusedPresidentPanelButton.onClick.AddListener(() => ShowPanel(powerFocusedPresidentPanel));

        // Panel kapatma butonlarının işlevlerini bağlama
        closeEconomicPresidentPanelButton.onClick.AddListener(() => ClosePanel(economicPresidentPanel));
        closeEnvironmentalPresidentPanelButton.onClick.AddListener(() => ClosePanel(environmentalPresidentPanel));
        closePowerFocusedPresidentPanelButton.onClick.AddListener(() => ClosePanel(powerFocusedPresidentPanel));

        // Başkan seçme butonlarının işlevlerini bağlama
        selectEconomicPresidentButton.onClick.AddListener(SelectEconomicPresident);
        selectEnvironmentalPresidentButton.onClick.AddListener(SelectEnvironmentalPresident);
        selectPowerFocusedPresidentButton.onClick.AddListener(SelectPowerFocusedPresident);
    }

    private void ShowPanel(GameObject panel)
    {
        DisableAllPanels(); // Diğer panelleri kapat
        panel.SetActive(true); // İlgili paneli aç
    }

    private void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    private void DisableAllPanels()
    {
        economicPresidentPanel.SetActive(false);
        environmentalPresidentPanel.SetActive(false);
        powerFocusedPresidentPanel.SetActive(false);
    }

    private void SelectEconomicPresident()
    {
        maxMoney = 40;
        maxCleanliness = 30;
        maxPower = 35;
        maxHappiness = 35;

        SavePresidentSelection();
    }

    private void SelectEnvironmentalPresident()
    {
        maxCleanliness = 40;
        maxPower = 30;
        maxHappiness = 35;
        maxMoney = 35;

        SavePresidentSelection();
    }

    private void SelectPowerFocusedPresident()
    {
        maxPower = 40;
        maxHappiness = 30;
        maxCleanliness = 35;
        maxMoney = 35;

        SavePresidentSelection();
    }

    private void SavePresidentSelection()
    {
        PlayerPrefs.SetInt(KeyIsActive, 1);
        PlayerPrefs.SetInt("Baskan", 1);
        SaveMaxValues();
        resourceManager.UpdateSliderMaxValues(maxHappiness, maxCleanliness, maxPower, maxMoney);
        UpdateResourceTexts();
        DisableAllPanels();
        presidentSelectionPanel.SetActive(false);
        Time.timeScale = 1; // Oyunu devam ettir
    }

    private void SaveMaxValues()
    {
        PlayerPrefs.SetInt(KeyMaxHappiness, maxHappiness);
        PlayerPrefs.SetInt(KeyMaxCleanliness, maxCleanliness);
        PlayerPrefs.SetInt(KeyMaxPower, maxPower);
        PlayerPrefs.SetInt(KeyMaxMoney, maxMoney);
        PlayerPrefs.Save();
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
        // Kaynak metinlerini güncelleme işlemi
    }

    public int GetMaxHappiness() => maxHappiness;
    public int GetMaxCleanliness() => maxCleanliness;
    public int GetMaxPower() => maxPower;
    public int GetMaxMoney() => maxMoney;
}
