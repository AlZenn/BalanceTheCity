using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button AktivistButton;
    public Button BuisnessButton;
    public Button LeftWingButton;
    public Button RightWingButton;
    public Button SendikalarButton;
    public Button SanatçılarButton;
    public Button EndüstriyelButton;
    public Button ÇevrecilerButton;
    public Button MedyaButton;

    [Header("Resource Manager")]
    public SC_ResourceManager resourceManager;

    [Header("Election Manager")]
    public ElectionManager electionManager;

    [Header("Confirmation Panel")]
    public GameObject confirmationPanel;
    public Button confirmButton;
    public Button cancelButton;
    public Image confirmationImage; // Yeni Image bileşeni

    [Header("Sprites")]
    public Sprite aktivistSprite;
    public Sprite buisnessSprite;
    public Sprite leftWingSprite;
    public Sprite rightWingSprite;
    public Sprite sendikalarSprite;
    public Sprite sanatçılarSprite;
    public Sprite endüstriyelSprite;
    public Sprite çevrecilerSprite;
    public Sprite medyaSprite;

    private Button currentButton;
    private int requiredAmount;
    private string resourceType;
    private string buttonKey;

    void Start()
    {
        SetButtonListeners();
        LoadButtonStates();
        confirmationPanel.SetActive(false);
    }

    void Update()
    {
        // Onay butonunun kilidini açma şartlarını kontrol et
        CheckButtonUnlockConditions();
    }

    void SetButtonListeners()
    {
        AktivistButton.onClick.AddListener(() => ShowConfirmationPanel(AktivistButton, 10, "Cleanliness", "Aktivist", aktivistSprite));
        BuisnessButton.onClick.AddListener(() => ShowConfirmationPanel(BuisnessButton, 10, "Power", "Buisness", buisnessSprite));
        LeftWingButton.onClick.AddListener(() => ShowConfirmationPanel(LeftWingButton, 10, "Happiness", "LeftWing", leftWingSprite));
        RightWingButton.onClick.AddListener(() => ShowConfirmationPanel(RightWingButton, 10, "Money", "RightWing", rightWingSprite));
        SendikalarButton.onClick.AddListener(() => ShowConfirmationPanel(SendikalarButton, 8, "Cleanliness", 8, "Happiness", "Sendikalar", sendikalarSprite));
        SanatçılarButton.onClick.AddListener(() => ShowConfirmationPanel(SanatçılarButton, 8, "Happiness", "Sanatçılar", sanatçılarSprite));
        EndüstriyelButton.onClick.AddListener(() => ShowConfirmationPanel(EndüstriyelButton, 12, "Power", "Endüstriyel", endüstriyelSprite));
        ÇevrecilerButton.onClick.AddListener(() => ShowConfirmationPanel(ÇevrecilerButton, 10, "Cleanliness", "Çevreciler", çevrecilerSprite));
        MedyaButton.onClick.AddListener(() => ShowConfirmationPanel(MedyaButton, 12, "Money", "Medya", medyaSprite));

        confirmButton.onClick.AddListener(() => ConfirmPurchase());
        cancelButton.onClick.AddListener(() => CancelPurchase());
    }

    void ShowConfirmationPanel(Button button, int requiredAmount, string resourceType, string buttonKey, Sprite sprite)
    {
        currentButton = button;
        this.requiredAmount = requiredAmount;
        this.resourceType = resourceType;
        this.buttonKey = buttonKey;
        confirmationImage.sprite = sprite; // Onay panelindeki resmi ayarla
        confirmationPanel.SetActive(true);
        CheckButtonUnlockConditions();
    }

    //sendikalar
    void ShowConfirmationPanel(Button button, int requiredAmount1, string resourceType1, int requiredAmount2, string resourceType2, string buttonKey, Sprite sprite)
    {
        currentButton = button;
        this.requiredAmount = requiredAmount1; // İlk kaynağı kullanacağız
        this.resourceType = resourceType1; // İlk kaynağı kullanacağız
        this.buttonKey = buttonKey;
        confirmationImage.sprite = sprite; // Onay panelindeki resmi ayarla
        confirmationPanel.SetActive(true);
        CheckButtonUnlockConditions();
    }

    void ConfirmPurchase()
    {
        if (currentButton != null)
        {
            if (buttonKey == "Sendikalar")
            {
                TryBuySendikalarButton(currentButton, requiredAmount, resourceType, 8, "Happiness", buttonKey);
            }
            else
            {
                TryBuyButton(currentButton, requiredAmount, resourceType, buttonKey);
            }
            currentButton = null;
            confirmationPanel.SetActive(false);
        }
    }

    void CancelPurchase()
    {
        currentButton = null;
        confirmationPanel.SetActive(false);
    }

    void CheckButtonUnlockConditions()
    {
        bool canConfirm = false;

        switch (buttonKey)
        {
            case "Aktivist":
                canConfirm = resourceManager.consecutiveCleanlinessIncreases >= 2 && !PlayerPrefs.HasKey("Aktivist"); // 3'ten 2'ye düşürülmüş
                break;
            case "Buisness":
                canConfirm = resourceManager.consecutivePowerIncreases >= 2 && !PlayerPrefs.HasKey("Buisness"); // 3'ten 2'ye düşürülmüş
                break;
            case "LeftWing":
                canConfirm = resourceManager.consecutiveHappinessIncreases >= 2 && !PlayerPrefs.HasKey("LeftWing"); // 3'ten 2'ye düşürülmüş
                break;
            case "RightWing":
                canConfirm = resourceManager.consecutiveMoneyIncreases >= 2 && !PlayerPrefs.HasKey("RightWing"); // 3'ten 2'ye düşürülmüş
                break;
            case "Sendikalar":
                canConfirm = resourceManager.consecutiveHappinessIncreases >= 2 && resourceManager.consecutiveCleanlinessIncreases >= 2 && !PlayerPrefs.HasKey("Sendikalar");
                break;
            case "Sanatçılar":
                canConfirm = resourceManager.consecutiveHappinessIncreases >= 2 && !PlayerPrefs.HasKey("Sanatçılar");
                break;
            case "Endüstriyel":
                canConfirm = resourceManager.consecutivePowerIncreases >= 2 && !PlayerPrefs.HasKey("Endüstriyel"); // 3'ten 2'ye düşürülmüş
                break;
            case "Çevreciler":
                canConfirm = resourceManager.consecutiveCleanlinessIncreases >= 2 && !PlayerPrefs.HasKey("Çevreciler"); // 3'ten 2'ye düşürülmüş
                break;
            case "Medya":
                canConfirm = resourceManager.consecutivePowerIncreases >= 2 && !PlayerPrefs.HasKey("Medya"); // 3'ten 2'ye düşürülmüş
                break;
        }

        confirmButton.interactable = canConfirm;
    }

    void TryBuyButton(Button button, int requiredAmount, string resourceType, string buttonKey)
    {
        if (!button.interactable)
            return;

        bool canBuy = false;

        // Kullanıcının gerekli kaynağa sahip olup olmadığını kontrol et
        switch (resourceType)
        {
            case "Cleanliness":
                canBuy = resourceManager.cleanliness >= requiredAmount;
                if (canBuy) resourceManager.cleanliness -= requiredAmount;
                break;
            case "Power":
                canBuy = resourceManager.power >= requiredAmount;
                if (canBuy) resourceManager.power -= requiredAmount;
                break;
            case "Happiness":
                canBuy = resourceManager.happiness >= requiredAmount;
                if (canBuy) resourceManager.happiness -= requiredAmount;
                break;
            case "Money":
                canBuy = resourceManager.money >= requiredAmount;
                if (canBuy) resourceManager.money -= requiredAmount;
                break;
        }

        // Koşulları kontrol et
        bool conditionsMet = false;

        switch (buttonKey)
        {
            case "Aktivist":
                conditionsMet = resourceManager.consecutiveCleanlinessIncreases >= 2 && !PlayerPrefs.HasKey("Aktivist");
                break;
            case "Buisness":
                conditionsMet = resourceManager.consecutivePowerIncreases >= 2 && !PlayerPrefs.HasKey("Buisness");
                break;
            case "LeftWing":
                conditionsMet = resourceManager.consecutiveHappinessIncreases >= 2 && !PlayerPrefs.HasKey("LeftWing");
                break;
            case "RightWing":
                conditionsMet = resourceManager.consecutiveMoneyIncreases >= 2 && !PlayerPrefs.HasKey("RightWing");
                break;
            case "Sendikalar":
                conditionsMet = resourceManager.consecutiveHappinessIncreases >= 2 && resourceManager.consecutiveCleanlinessIncreases >= 2 && !PlayerPrefs.HasKey("Sendikalar");
                break;
            case "Sanatçılar":
                conditionsMet = resourceManager.consecutiveHappinessIncreases >= 1 && !PlayerPrefs.HasKey("Sanatçılar");
                break;
            case "Endüstriyel":
                conditionsMet = resourceManager.consecutivePowerIncreases >= 2 && !PlayerPrefs.HasKey("Endüstriyel");
                break;
            case "Çevreciler":
                conditionsMet = resourceManager.consecutiveCleanlinessIncreases >= 2 && !PlayerPrefs.HasKey("Çevreciler");
                break;
            case "Medya":
                conditionsMet = resourceManager.consecutivePowerIncreases >= 2 && !PlayerPrefs.HasKey("Medya");
                break;
        }

        // Eğer kullanıcı yeterli kaynağa sahipse ve koşullar sağlanmışsa, buton kilitlenir ve durumu kaydedilir
        if (canBuy && conditionsMet)
        {
            button.interactable = false;
            PlayerPrefs.SetInt(buttonKey, 1);
            PlayerPrefs.Save();

            // Oy oranlarını güncelle
            electionManager.AddVotes(buttonKey);

            Debug.Log($"{button.name} satın alındı ve kilitlendi.");
        }
        else
        {
            if (!canBuy)
            {
                Debug.Log($"{button.name} satın almak için yeterli {resourceType} yok.");
            }
            if (!conditionsMet)
            {
                Debug.Log($"{button.name} satın almak için gerekli koşullar sağlanmadı.");
            }
        }
        Debug.Log($"Kalan {resourceType}: {resourceManager.cleanliness}, {resourceManager.happiness}, {resourceManager.power}, {resourceManager.money}");

        UpdateResourceTexts();
    }

    void TryBuySendikalarButton(Button button, int requiredAmount1, string resourceType1, int requiredAmount2, string resourceType2, string buttonKey)
    {
        if (!button.interactable)
            return;

        bool canBuy = false;

        // Kullanıcının gerekli kaynaklara sahip olup olmadığını kontrol et
        if (resourceType1 == "Cleanliness" && resourceType2 == "Happiness")
        {
            canBuy = resourceManager.cleanliness >= requiredAmount1 && resourceManager.happiness >= requiredAmount2;
            if (canBuy)
            {
                resourceManager.cleanliness -= requiredAmount1;
                resourceManager.happiness -= requiredAmount2;
            }
        }
        Debug.Log($"Kalan {resourceType}: {resourceManager.cleanliness}, {resourceManager.happiness}, {resourceManager.power}, {resourceManager.money}");


        // Eğer kullanıcı yeterli kaynağa sahipse, buton kilitlenir ve durumu kaydedilir
        if (canBuy)
        {
            button.interactable = false;
            PlayerPrefs.SetInt(buttonKey, 1);
            PlayerPrefs.Save();

            // Oy oranlarını güncelle
            electionManager.AddVotes(buttonKey);

            Debug.Log($"{button.name} satın alındı ve kilitlendi.");
        }
        else
        {
            Debug.Log($"{button.name} satın almak için yeterli kaynak yok.");
        }

        UpdateResourceTexts();
    }

    void LoadButtonStates()
    {
        // Kaydedilen buton durumlarını yükle
        if (PlayerPrefs.HasKey("Aktivist")) AktivistButton.interactable = false;
        if (PlayerPrefs.HasKey("Buisness")) BuisnessButton.interactable = false;
        if (PlayerPrefs.HasKey("LeftWing")) LeftWingButton.interactable = false;
        if (PlayerPrefs.HasKey("RightWing")) RightWingButton.interactable = false;
        if (PlayerPrefs.HasKey("Sendikalar")) SendikalarButton.interactable = false;
        if (PlayerPrefs.HasKey("Sanatçılar")) SanatçılarButton.interactable = false;
        if (PlayerPrefs.HasKey("Endüstriyel")) EndüstriyelButton.interactable = false;
        if (PlayerPrefs.HasKey("Çevreciler")) ÇevrecilerButton.interactable = false;
        if (PlayerPrefs.HasKey("Medya")) MedyaButton.interactable = false;
    }

    void UpdateResourceTexts()
    {
        resourceManager.gameEffectsTexts[0].text = resourceManager.happiness.ToString();
        resourceManager.gameEffectsTexts[1].text = resourceManager.cleanliness.ToString();
        resourceManager.gameEffectsTexts[2].text = resourceManager.power.ToString();
        resourceManager.gameEffectsTexts[3].text = resourceManager.money.ToString();
        resourceManager.UpdateSliders();
    }
}