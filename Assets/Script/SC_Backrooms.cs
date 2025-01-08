using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Backrooms : MonoBehaviour
{
    [Header("Diğer Scriptler")]
    [SerializeField] private SC_ResourceManager ScriptResourceManager;

    [Header("Objeler")]
    public GameObject BackroomsObject;
    public GameObject CardDetailPanel; // Kart detay paneli

    // Kart detay panelindeki öğeler
    [Header("Panel Değerleri")]
    public Text PanelTitle;
    public Image PanelPhoto;
    public Text[] PanelPositiveEffects;
    public Text[] PanelNegativeEffects;

    // Kartların UI elemanları
    [Header("UI içerikleri,")]
    public Text BackroomCardsTitle_1;
    public GameObject BackroomCardsPhoto_1;
    public Text[] BackroomCardsPositive_1;
    public Text[] BackroomCardsNegative_1;

    public Text BackroomCardsTitle_2;
    public GameObject BackroomCardsPhoto_2;
    public Text[] BackroomCardsPositive_2;
    public Text[] BackroomCardsNegative_2;

    
    private Card selectedCard1;
    private Card selectedCard2;

    public GameObject card1;
    public GameObject card2;

    [Header("Kart değer güncelleme")] 
    public bool denemeGuncellemeBool = false;
    public int denemeGuncellemeInt = 0;
    

    [Header("Güven Değeri")] 
    public Slider trustSlider;
    
    private void Awake()
    {
        BackroomsObject.SetActive(false);
        trustSlider.value = 1f;
        if (CardDetailPanel != null)
        {
            CardDetailPanel.SetActive(false); // Kart detay panelini başlangıçta kapat
        }
    }

    public void ButtonBackrooms()
    {
        if (BackroomsObject == null)
        {
            Debug.LogError("BackroomsObject atanmadı!");
            return;
        }
        
        card1.SetActive(true);
        card2.SetActive(true);
        BackroomsObject.SetActive(true); // Backrooms ekranını açıyoruz.

        // İlk kartı seç
        selectedCard1 = SelectCardForBackroom(1);

        // İkinci kartı seç
        selectedCard2 = SelectCardForBackroom(2);
    }

    private Card SelectCardForBackroom(int cardIndex)
    {
        int randomChoice = UnityEngine.Random.Range(1, 4);
        Card selectedCard = null;

        if (randomChoice == 1)
            selectedCard = GetRandomCard(ScriptResourceManager.lowCards);
        else if (randomChoice == 2)
            selectedCard = GetRandomCard(ScriptResourceManager.midCards);
        else if (randomChoice == 3)
            selectedCard = GetRandomCard(ScriptResourceManager.bonusCards);

        if (selectedCard != null)
        {
            DisplayCard(selectedCard, cardIndex);
        }

        return selectedCard;
    }

    private Card GetRandomCard(List<Card> cardList)
    {
        if (cardList == null || cardList.Count == 0)
        {
            Debug.LogWarning("Kart listesi boş!");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, cardList.Count);
        return cardList[randomIndex];
    }

    private void DisplayCard(Card card, int cardIndex)
    {
        if (card == null) return;

        if (cardIndex == 1)
        {
            BackroomCardsTitle_1.text = card.name;
            BackroomCardsPhoto_1.GetComponent<Image>().sprite = card.cardPhoto;
            UpdateEffectTexts(card, BackroomCardsPositive_1, BackroomCardsNegative_1);
        }
        else if (cardIndex == 2)
        {
            BackroomCardsTitle_2.text = card.name;
            BackroomCardsPhoto_2.GetComponent<Image>().sprite = card.cardPhoto;
            UpdateEffectTexts(card, BackroomCardsPositive_2, BackroomCardsNegative_2);
        }
    }

    private void UpdateEffectTexts(Card card, Text[] positiveEffects, Text[] negativeEffects)
    {
        if (card == null) return;

        positiveEffects[0].text = card.approveEffect.happinessChange.ToString();
        positiveEffects[1].text = card.approveEffect.cleanlinessChange.ToString();
        positiveEffects[2].text = card.approveEffect.powerChange.ToString();
        positiveEffects[3].text = card.approveEffect.moneyChange.ToString();

        negativeEffects[0].text = card.rejectEffect.happinessChange.ToString();
        negativeEffects[1].text = card.rejectEffect.cleanlinessChange.ToString();
        negativeEffects[2].text = card.rejectEffect.powerChange.ToString();
        negativeEffects[3].text = card.rejectEffect.moneyChange.ToString();
    }

    public void CardPressedOne()
    {
        if (selectedCard1 != null)
        {
            ShowCardDetails(selectedCard1);
        }
    }

    public void CardPressedTwo()
    {
        if (selectedCard2 != null)
        {
            ShowCardDetails(selectedCard2);
        }
    }

    private void ShowCardDetails(Card card)
    {
        Debug.Log("kart özellikleri güncellendi");
        if (CardDetailPanel == null || card == null) return;

        CardDetailPanel.SetActive(true);
        PanelTitle.text = card.name;
        PanelPhoto.sprite = card.cardPhoto;
        UpdateEffectTexts(card, PanelPositiveEffects, PanelNegativeEffects);
    }
    
        private void Update() 
    {
       
        if (denemeGuncellemeBool == true) // card swipe'dan değer alır.
        {
            if (denemeGuncellemeInt == 1) 
            {
                changePositive1();
                denemeGuncellemeInt = 0;
                denemeGuncellemeBool = false;
                trustSlider.value -= 0.05f;
            }
            else if (denemeGuncellemeInt == 2)
            {
                changeNegative1();
                denemeGuncellemeInt = 0;
                denemeGuncellemeBool = false;
                trustSlider.value -= 0.05f;
            }
        }
    }
    public void changePositive1() 
    {
        UpdateResourceValues(PanelPositiveEffects);
        CardDetailPanel.SetActive(false);
        Debug.Log("Kart onaylandı ve pozitif etkiler uygulandı.");
        trustSlider.value -= 0.1f;
    }

    public void changeNegative1()
    {
        UpdateResourceValues(PanelNegativeEffects);
        CardDetailPanel.SetActive(false);
        Debug.Log("Kart reddedildi ve negatif etkiler uygulandı.");
        trustSlider.value -= 0.1f;
    }
    
    private void UpdateResourceValues(Text[] effectTexts)
    {
        if (effectTexts == null || effectTexts.Length < 4)
        {
            Debug.LogWarning("Etkiler güncellenirken geçersiz metin dizisi!");
            return;
        }

        int[] resourceValues = {
            ScriptResourceManager.happiness,
            ScriptResourceManager.cleanliness,
            ScriptResourceManager.power,
            ScriptResourceManager.money
        };

        for (int i = 0; i < effectTexts.Length; i++)
        {
            if (int.TryParse(effectTexts[i].text, out int effectValue))
            {
                resourceValues[i] += effectValue;
                ScriptResourceManager.gameEffectsTexts[i].text = resourceValues[i].ToString();
            }
            else
            {
                Debug.LogWarning($"Etki metni ({effectTexts[i].text}) bir sayıya çevrilemedi.");
            }
        }

        // Kaynak değerlerini güncelle
        ScriptResourceManager.happiness = resourceValues[0];
        ScriptResourceManager.cleanliness = resourceValues[1];
        ScriptResourceManager.power = resourceValues[2];
        ScriptResourceManager.money = resourceValues[3];
    }
}
