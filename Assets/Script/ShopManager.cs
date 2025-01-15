using UnityEngine;
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

    void Start()
    {
        // Başlangıçta butonları kilitli yap ve tıklama olaylarını bağla
        LockAllButtons();
        SetButtonListeners();
        LoadButtonStates();
    }

    void Update()
    {
        // Butonların kilidini açma şartlarını kontrol et
        CheckButtonUnlockConditions();
    }

    void LockAllButtons()
    {
        AktivistButton.interactable = false;
        BuisnessButton.interactable = false;
        LeftWingButton.interactable = false;
        RightWingButton.interactable = false;
        SendikalarButton.interactable = false;
        SanatçılarButton.interactable = false;
        EndüstriyelButton.interactable = false;
        ÇevrecilerButton.interactable = false;
        MedyaButton.interactable = false;
    }

    void SetButtonListeners()
    {
        AktivistButton.onClick.AddListener(() => TryBuyButton(AktivistButton, 10, "Cleanliness", "Aktivist"));
        BuisnessButton.onClick.AddListener(() => TryBuyButton(BuisnessButton, 10, "Power", "Buisness"));
        LeftWingButton.onClick.AddListener(() => TryBuyButton(LeftWingButton, 10, "Happiness", "LeftWing"));
        RightWingButton.onClick.AddListener(() => TryBuyButton(RightWingButton, 10, "Money", "RightWing"));
        SendikalarButton.onClick.AddListener(() => TryBuySendikalarButton(SendikalarButton, 8, "Cleanliness", 8, "Happiness", "Sendikalar"));
        SanatçılarButton.onClick.AddListener(() => TryBuyButton(SanatçılarButton, 8, "Happiness", "Sanatçılar"));
        EndüstriyelButton.onClick.AddListener(() => TryBuyButton(EndüstriyelButton, 12, "Power", "Endüstriyel"));
        ÇevrecilerButton.onClick.AddListener(() => TryBuyButton(ÇevrecilerButton, 10, "Cleanliness", "Çevreciler"));
        MedyaButton.onClick.AddListener(() => TryBuyButton(MedyaButton, 12, "Money", "Medya"));
    }

    void CheckButtonUnlockConditions()
    {
        if (resourceManager.consecutiveCleanlinessIncreases >= 3 && !PlayerPrefs.HasKey("Aktivist"))
        {
            AktivistButton.interactable = true;
        }

        if (resourceManager.consecutivePowerIncreases >= 3)
        {
            if (!PlayerPrefs.HasKey("Buisness"))
                BuisnessButton.interactable = true;
            if (!PlayerPrefs.HasKey("Endüstriyel"))
                EndüstriyelButton.interactable = true;
            if (!PlayerPrefs.HasKey("Medya"))
                MedyaButton.interactable = true;
        }

        if (resourceManager.consecutiveHappinessIncreases >= 3 && !PlayerPrefs.HasKey("LeftWing"))
        {
            LeftWingButton.interactable = true;
        }

        if (resourceManager.consecutiveMoneyIncreases >= 3 && !PlayerPrefs.HasKey("RightWing"))
        {
            RightWingButton.interactable = true;
        }

        if (resourceManager.consecutiveHappinessIncreases >= 2 && resourceManager.consecutiveCleanlinessIncreases >= 2 && !PlayerPrefs.HasKey("Sendikalar"))
        {
            SendikalarButton.interactable = true;
        }

        if (resourceManager.consecutiveHappinessIncreases >= 2 && !PlayerPrefs.HasKey("Sanatçılar"))
        {
            SanatçılarButton.interactable = true;
        }

        if (resourceManager.consecutiveCleanlinessIncreases >= 3 && !PlayerPrefs.HasKey("Çevreciler"))
        {
            ÇevrecilerButton.interactable = true;
        }
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
            Debug.Log($"{button.name} satın almak için yeterli {resourceType} yok.");
        }

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
    }
}