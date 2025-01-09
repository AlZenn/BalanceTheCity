using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public SC_ResourceManager resourceManager;
    public Button[] societyButtons; // Toplumbutonları için butonlar
    public List<SocietyButton> societyButtonList;

    private void Start()
    {
        societyButtonList = SocietyButtonData.GetSocietyButtons();

        foreach (var button in societyButtons)
        {
            button.interactable = false; // Başlangıçta tüm butonlar kilitli
        }
    }

    private void Update()
    {
        UpdateButtonInteractability();
    }

    private void UpdateButtonInteractability()
    {
        foreach (var societyButton in societyButtonList)
        {
            if (societyButton.IsUnlockConditionMet(resourceManager))
            {
                societyButtons[societyButtonList.IndexOf(societyButton)].interactable = true;
            }
            else
            {
                societyButtons[societyButtonList.IndexOf(societyButton)].interactable = false;
            }
        }
    }

    public void PurchaseSocietyButton(int index)
    {
        var societyButton = societyButtonList[index];
        if (societyButton.CanPurchase(resourceManager))
        {
            societyButton.Purchase(resourceManager);
            societyButtons[index].interactable = false; // Satın alındığında buton devre dışı
        }
    }
}
[System.Serializable]
public class SocietyButton
{
    public string name;
    public int happinessRequirement;
    public int cleanlinessRequirement;
    public int powerRequirement;
    public int moneyRequirement;
    public int votePercentage;
    public string boostCondition;
    public string contraCondition;
    public int cleanlinessIncreasesRequirement;
    public int powerIncreasesRequirement;
    public int happinessIncreasesRequirement;
    public int moneyIncreasesRequirement;
    public int cleanlinessAndHappinessIncreasesRequirement;

    public bool IsUnlockConditionMet(SC_ResourceManager resourceManager)
    {
        return resourceManager.happiness >= happinessRequirement &&
               resourceManager.cleanliness >= cleanlinessRequirement &&
               resourceManager.power >= powerRequirement &&
               resourceManager.money >= moneyRequirement &&
               resourceManager.consecutiveCleanlinessIncreases >= cleanlinessIncreasesRequirement &&
               resourceManager.consecutivePowerIncreases >= powerIncreasesRequirement &&
               resourceManager.consecutiveHappinessIncreases >= happinessIncreasesRequirement &&
               resourceManager.consecutiveMoneyIncreases >= moneyIncreasesRequirement &&
               resourceManager.consecutiveCleanlinessAndHappinessIncreases >= cleanlinessAndHappinessIncreasesRequirement;
    }

    public bool CanPurchase(SC_ResourceManager resourceManager)
    {
        // Satın alma koşullarının karşılanıp karşılanmadığını kontrol eder
        return resourceManager.cleanliness >= cleanlinessRequirement &&
               resourceManager.power >= powerRequirement &&
               resourceManager.happiness >= happinessRequirement &&
               resourceManager.money >= moneyRequirement;
    }

    public void Purchase(SC_ResourceManager resourceManager)
    {
        // Kaynakları azaltır ve toplumbutonunu satın alır
        resourceManager.cleanliness -= cleanlinessRequirement;
        resourceManager.power -= powerRequirement;
        resourceManager.happiness -= happinessRequirement;
        resourceManager.money -= moneyRequirement;
    }
}