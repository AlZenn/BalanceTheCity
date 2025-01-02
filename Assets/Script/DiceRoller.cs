using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class DiceRoller : MonoBehaviour
{
    [Header("Basic Items")]
    public GameObject CardAfterDice; // Zar atýldýktan sonra ekrana çýkan kart 
    public Text CardTitleGameObject; // kartýn baþlýðý
    public GameObject CardPhotoGameObject;

    [SerializeField] private GameObject dice; // Zar 3D objesi
    [SerializeField] private Text resultText; // Zar sonucu yazdýrýlacak Text bileþeni
    [SerializeField] private float rollDuration = 1.5f; // Zar döndürme süresi
    private bool isRolling = false; // Zar döndürülüyor mu?
    [SerializeField] private float waitTimer = 1f;
    [SerializeField] private Button diceButton; // zar butonu týklanabilir.
    [SerializeField] private Material[] cardColors; // kart material controller

    public ResourceManager resourceManager; 

    private void Awake()
    {
        CardAfterDice.SetActive(false); // kartý deaktif yapýyoruz.
    }

    public void RollDice()
    {
        if (!isRolling) // butona týklanýnca animasyon giriyor.
        {
            StartCoroutine(RollDiceAnimation());
        }
    }

    private IEnumerator RollDiceAnimation() 
    {
        isRolling = true;
        diceButton.interactable = false; // bug çözümü için 


        // Zar animasyonu baþlat
        float elapsedTime = 0f;
        Quaternion initialRotation = dice.transform.rotation;

        while (elapsedTime < rollDuration)
        {
            elapsedTime += Time.deltaTime;
            dice.transform.Rotate(new Vector3(360 * Time.deltaTime, 360 * Time.deltaTime, 180 * Time.deltaTime)); // Zar döndürme 3d model geldikten sonra deðiþecek
            yield return null;
        }
        dice.transform.rotation = initialRotation;

        // Zar sonucunu üret
        int diceResult = Random.Range(1, 7);
        StartCoroutine(ShowDiceResult(diceResult));

        isRolling = false;
    }

    private IEnumerator ShowDiceResult(int diceResult)
    {
        resultText.text = $"Zar Sonucu: {diceResult}";

        // 3 saniye bekle
        yield return new WaitForSeconds(waitTimer);

        // Tüm kartlarý deaktif et
        CardAfterDice.SetActive(false);

        // Zar sonucuna göre kart seç
        Card selectedCard = null;
        if (diceResult >= 1 && diceResult <= 3) // çýkan zar deðerine göre material deðiþiyor. ve resource managerden random kart seçiliyor.
        {
            selectedCard = GetRandomCard(resourceManager.lowCards);
            CardAfterDice.GetComponent<Image>().material = cardColors[0];
        }
        else if (diceResult >= 4 && diceResult <= 5)
        {
            selectedCard = GetRandomCard(resourceManager.midCards);
            CardAfterDice.GetComponent<Image>().material = cardColors[1];
        }
        else if (diceResult == 6)
        {
            selectedCard = GetRandomCard(resourceManager.bonusCards);
            CardAfterDice.GetComponent<Image>().material = cardColors[2];
        }
        
        if (selectedCard != null)
        {
            // Kart bilgisini UI'da göster
            DisplayCard(selectedCard);
        }

        diceButton.interactable = true;
    }

    private Card GetRandomCard(List<Card> cardList) // kart listesinde kart varsa kart sayýsý ve 0 aralýðýnda kart seçer ve döndürür.
    {
        if (cardList == null || cardList.Count == 0) return null;
        int randomIndex = Random.Range(0, cardList.Count);
        return cardList[randomIndex];
    }

    private void DisplayCard(Card card) // seçilen random kartý gösterir. adýný günceller. resmini günceller. efektleri günceller.
    {
        CardAfterDice.SetActive(true);
        // Kart adýný ve etkilerini UI'da göster
        resultText.text += $"\nSeçilen Kart: {card.name}";

        CardTitleGameObject.text = card.name;
        Image cardImage = CardPhotoGameObject.GetComponent<Image>();
        if (cardImage != null)
        {
            cardImage.sprite = card.cardPhoto; // Fotoðraf atanýr
        }


        // Kart efektlerini ResourceManager'dan al ve göster
        UpdateEffectTexts(card);
    }
    public int[] Positifdegerler = new int[4];
    public int[] Negatifdegerler = new int[4];

    public bool denemeBool = true;
    public int denemeInt = 0;

    private void UpdateEffectTexts(Card card)
    {
        var approveEffect = card.approveEffect;
        var rejectEffect = card.rejectEffect;

        // Pozitif ve negatif etkileri güncelle
        resourceManager.cardEffectsPositiveTexts[0].text = $"Mutluluk: {approveEffect.happinessChange}";
        resourceManager.cardEffectsPositiveTexts[1].text = $"Temizlik: {approveEffect.cleanlinessChange}";
        resourceManager.cardEffectsPositiveTexts[2].text = $"Güç: {approveEffect.powerChange}";
        resourceManager.cardEffectsPositiveTexts[3].text = $"Para: {approveEffect.moneyChange}";

        resourceManager.cardEffectsNegativeTexts[0].text = $"Mutluluk: {rejectEffect.happinessChange}";
        resourceManager.cardEffectsNegativeTexts[1].text = $"Temizlik: {rejectEffect.cleanlinessChange}";
        resourceManager.cardEffectsNegativeTexts[2].text = $"Güç: {rejectEffect.powerChange}";
        resourceManager.cardEffectsNegativeTexts[3].text = $"Para: {rejectEffect.moneyChange}";

        Debug.Log($"Mutluluk: {approveEffect.happinessChange}Temizlik: {approveEffect.cleanlinessChange}");

        // stringden integer'a deðer döndürür. 
        Positifdegerler[0] = Mathf.RoundToInt(approveEffect.happinessChange); 
        Positifdegerler[1] = Mathf.RoundToInt(approveEffect.cleanlinessChange);
        Positifdegerler[2] = Mathf.RoundToInt(approveEffect.powerChange);
        Positifdegerler[3] = Mathf.RoundToInt(approveEffect.moneyChange);


        Negatifdegerler[0] = Mathf.RoundToInt(rejectEffect.happinessChange);
        Negatifdegerler[1] = Mathf.RoundToInt(rejectEffect.cleanlinessChange);
        Negatifdegerler[2] = Mathf.RoundToInt(rejectEffect.powerChange);
        Negatifdegerler[3] = Mathf.RoundToInt(rejectEffect.moneyChange);
        }

    private void Update() 
    {
        if (denemeBool == true) // card swipe'dan deðer alýr.
        {
            if (denemeInt == 1) 
            {
                changePositive();
                denemeInt = 0;
                denemeBool = false;
            }
            else if (denemeInt == 2)
            {
                changeNegative();
                denemeInt = 0;
                denemeBool = false;
            }
        }
    }
            

    public CardSwipe SCCardSwipe;
    // card swipe'daki deðerler olduktan sonra positif ya da negatife gider
    // positifse - negatifse güncellenir.
    // ekrandaki deðerler güncellenir 
    // kart yok olur.

    public void changePositive() 
    {
        int sonuc1 = Positifdegerler[0] + resourceManager.happiness;
        int sonuc2 = Positifdegerler[1] + resourceManager.cleanliness; 
        int sonuc3 = Positifdegerler[2] + resourceManager.power; 
        int sonuc4 = Positifdegerler[3] + resourceManager.money;

        resourceManager.gameEffectsTexts[0].text = sonuc1.ToString();
        resourceManager.gameEffectsTexts[1].text = sonuc2.ToString();
        resourceManager.gameEffectsTexts[2].text = sonuc3.ToString();
        resourceManager.gameEffectsTexts[3].text = sonuc4.ToString();

        resourceManager.happiness = Convert.ToInt32(resourceManager.gameEffectsTexts[0].text);
        resourceManager.cleanliness = Convert.ToInt32(resourceManager.gameEffectsTexts[1].text);
        resourceManager.power = Convert.ToInt32(resourceManager.gameEffectsTexts[2].text);
        resourceManager.money = Convert.ToInt32(resourceManager.gameEffectsTexts[3].text);

        SCCardSwipe.VisibleCard.SetActive(false);

        Debug.Log("kabul edildi");
    }
    public void changeNegative()
    {
        int sonuc1 = Negatifdegerler[0] + resourceManager.happiness;
        int sonuc2 = Negatifdegerler[1] + resourceManager.cleanliness;
        int sonuc3 = Negatifdegerler[2] + resourceManager.power;
        int sonuc4 = Negatifdegerler[3] + resourceManager.money;

        resourceManager.gameEffectsTexts[0].text = sonuc1.ToString();
        resourceManager.gameEffectsTexts[1].text = sonuc2.ToString();
        resourceManager.gameEffectsTexts[2].text = sonuc3.ToString();
        resourceManager.gameEffectsTexts[3].text = sonuc4.ToString();

        resourceManager.happiness = Convert.ToInt32(resourceManager.gameEffectsTexts[0].text);
        resourceManager.cleanliness = Convert.ToInt32(resourceManager.gameEffectsTexts[1].text);
        resourceManager.power = Convert.ToInt32(resourceManager.gameEffectsTexts[2].text);
        resourceManager.money = Convert.ToInt32(resourceManager.gameEffectsTexts[3].text);

        SCCardSwipe.VisibleCard.SetActive(false);

        Debug.Log("reddedildi");
    }
}