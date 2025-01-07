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

    // Kartların UI elemanları
    public Text BackroomCardsTitle_1;
    public GameObject BackroomCardsPhoto_1;
    public Text[] BackroomCardsPositive_1;
    public Text[] BackroomCardsNegative_1;

    public Text BackroomCardsTitle_2;
    public GameObject BackroomCardsPhoto_2;
    public Text[] BackroomCardsPositive_2;
    public Text[] BackroomCardsNegative_2;


    private void Awake()
    {
        BackroomsObject.SetActive(false);
    }

    public void ButtonBackrooms()
    {
        
        if (BackroomsObject == null)
        {
            Debug.LogError("BackroomsObject atanmadı!");
            return;
        }

        BackroomsObject.SetActive(true); // Backrooms ekranını açıyoruz.

        // İlk kartı seç
        SelectCardForBackroom(1);

        // İkinci kartı seç
        SelectCardForBackroom(2);
    }
    
    

    private void SelectCardForBackroom(int cardIndex)
    {
        // 1 ile 3 arasında bir sayı seç
        int randomChoice = UnityEngine.Random.Range(1, 4);

        Card selectedCard = null;

        // Sayıya göre kart seçimi yap
        if (randomChoice == 1)
        {
            selectedCard = GetRandomCard(ScriptResourceManager.lowCards);
        }
        else if (randomChoice == 2)
        {
            selectedCard = GetRandomCard(ScriptResourceManager.midCards);
        }
        else if (randomChoice == 3)
        {
            selectedCard = GetRandomCard(ScriptResourceManager.bonusCards);
        }

        // Seçilen kartı Backrooms kartına yansıt
        if (selectedCard != null)
        {
            DisplayCard(selectedCard, cardIndex);
        }
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
        if (card == null)
        {
            Debug.LogWarning("Gösterilecek kart bulunamadı!");
            return;
        }

        if (cardIndex == 1)
        {
            // 1. kartın başlığını ayarla
            if (BackroomCardsTitle_1 != null)
            {
                BackroomCardsTitle_1.text = card.name;
            }

            // 1. kartın fotoğrafını ayarla
            if (BackroomCardsPhoto_1 != null)
            {
                Image cardImage = BackroomCardsPhoto_1.GetComponent<Image>();
                if (cardImage != null)
                {
                    cardImage.sprite = card.cardPhoto;
                }
            }

            // 1. kartın pozitif ve negatif etkilerini güncelle
            UpdateEffectTexts(card, cardIndex);
        }
        else if (cardIndex == 2)
        {
            // 2. kartın başlığını ayarla
            if (BackroomCardsTitle_2 != null)
            {
                BackroomCardsTitle_2.text = card.name;
            }

            // 2. kartın fotoğrafını ayarla
            if (BackroomCardsPhoto_2 != null)
            {
                Image cardImage = BackroomCardsPhoto_2.GetComponent<Image>();
                if (cardImage != null)
                {
                    cardImage.sprite = card.cardPhoto;
                }
            }

            // 2. kartın pozitif ve negatif etkilerini güncelle
            UpdateEffectTexts(card, cardIndex);
        }
    }

    private void UpdateEffectTexts(Card card, int cardIndex)
    {
        if (card == null) return;

        var approveEffect = card.approveEffect;
        var rejectEffect = card.rejectEffect;

        // Pozitif etkiler
        if (cardIndex == 1 && BackroomCardsPositive_1.Length >= 4)
        {
            BackroomCardsPositive_1[0].text = approveEffect.happinessChange.ToString();
            BackroomCardsPositive_1[1].text = approveEffect.cleanlinessChange.ToString();
            BackroomCardsPositive_1[2].text = approveEffect.powerChange.ToString();
            BackroomCardsPositive_1[3].text = approveEffect.moneyChange.ToString();
        }

        if (cardIndex == 2 && BackroomCardsPositive_2.Length >= 4)
        {
            BackroomCardsPositive_2[0].text = approveEffect.happinessChange.ToString();
            BackroomCardsPositive_2[1].text = approveEffect.cleanlinessChange.ToString();
            BackroomCardsPositive_2[2].text = approveEffect.powerChange.ToString();
            BackroomCardsPositive_2[3].text = approveEffect.moneyChange.ToString();
        }

        // Negatif etkiler
        if (cardIndex == 1 && BackroomCardsNegative_1.Length >= 4)
        {
            BackroomCardsNegative_1[0].text = rejectEffect.happinessChange.ToString();
            BackroomCardsNegative_1[1].text = rejectEffect.cleanlinessChange.ToString();
            BackroomCardsNegative_1[2].text = rejectEffect.powerChange.ToString();
            BackroomCardsNegative_1[3].text = rejectEffect.moneyChange.ToString();
        }

        if (cardIndex == 2 && BackroomCardsNegative_2.Length >= 4)
        {
            BackroomCardsNegative_2[0].text = rejectEffect.happinessChange.ToString();
            BackroomCardsNegative_2[1].text = rejectEffect.cleanlinessChange.ToString();
            BackroomCardsNegative_2[2].text = rejectEffect.powerChange.ToString();
            BackroomCardsNegative_2[3].text = rejectEffect.moneyChange.ToString();
        }
    }
}
