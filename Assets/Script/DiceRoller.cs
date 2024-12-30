using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class DiceRoller : MonoBehaviour
{
    [Header("Basic Items")]
    public GameObject CardAfterDice;

    public GameObject CardPhotoGameObject;
    [SerializeField] private GameObject dice; // Zar 3D objesi
    [SerializeField] private Text resultText; // Zar sonucu yazd�r�lacak Text bile�eni
    [SerializeField] private float rollDuration = 1.5f; // Zar d�nd�rme s�resi
    private bool isRolling = false; // Zar d�nd�r�l�yor mu?
    [SerializeField] private float waitTimer = 1f;
    [SerializeField] private Button diceButton;
    [SerializeField] private Material[] cardColors;

    public ResourceManager resourceManager;

    private void Awake()
    {
        CardAfterDice.SetActive(false);
    }

    public void RollDice()
    {
        if (!isRolling)
        {
            StartCoroutine(RollDiceAnimation());
        }
    }

    private IEnumerator RollDiceAnimation()
    {
        isRolling = true;
        diceButton.interactable = false;

        // Zar animasyonu ba�lat
        float elapsedTime = 0f;
        Quaternion initialRotation = dice.transform.rotation;

        while (elapsedTime < rollDuration)
        {
            elapsedTime += Time.deltaTime;
            dice.transform.Rotate(new Vector3(360 * Time.deltaTime, 360 * Time.deltaTime, 180 * Time.deltaTime)); // Zar d�nd�rme
            yield return null;
        }
        dice.transform.rotation = initialRotation;

        // Zar sonucunu �ret
        int diceResult = Random.Range(1, 7);
        StartCoroutine(ShowDiceResult(diceResult));

        isRolling = false;
    }

    private IEnumerator ShowDiceResult(int diceResult)
    {
        resultText.text = $"Zar Sonucu: {diceResult}";


        // 3 saniye bekle
        yield return new WaitForSeconds(waitTimer);

        // T�m kartlar� deaktif et
        CardAfterDice.SetActive(false);

        // Zar sonucuna g�re kart se�
        Card selectedCard = null;
        if (diceResult >= 1 && diceResult <= 3)
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
            // Kart bilgisini UI'da g�ster
            DisplayCard(selectedCard);
        }

        diceButton.interactable = true;
    }

    private Card GetRandomCard(List<Card> cardList)
    {
        if (cardList == null || cardList.Count == 0) return null;
        int randomIndex = Random.Range(0, cardList.Count);
        return cardList[randomIndex];
    }

    private void DisplayCard(Card card)
    {
        CardAfterDice.SetActive(true);
        // Kart ad�n� ve etkilerini UI'da g�ster
        resultText.text += $"\nSe�ilen Kart: {card.name}";

        Image cardImage = CardPhotoGameObject.GetComponent<Image>();
        if (cardImage != null)
        {
            cardImage.sprite = card.cardPhoto; // Foto�raf atan�r
        }


        // Kart efektlerini ResourceManager'dan al ve g�ster
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

        // Pozitif ve negatif etkileri g�ncelle
        resourceManager.cardEffectsPositiveTexts[0].text = $"Mutluluk: {approveEffect.happinessChange}";
        resourceManager.cardEffectsPositiveTexts[1].text = $"Temizlik: {approveEffect.cleanlinessChange}";
        resourceManager.cardEffectsPositiveTexts[2].text = $"G��: {approveEffect.powerChange}";
        resourceManager.cardEffectsPositiveTexts[3].text = $"Para: {approveEffect.moneyChange}";

        resourceManager.cardEffectsNegativeTexts[0].text = $"Mutluluk: {rejectEffect.happinessChange}";
        resourceManager.cardEffectsNegativeTexts[1].text = $"Temizlik: {rejectEffect.cleanlinessChange}";
        resourceManager.cardEffectsNegativeTexts[2].text = $"G��: {rejectEffect.powerChange}";
        resourceManager.cardEffectsNegativeTexts[3].text = $"Para: {rejectEffect.moneyChange}";

        Debug.Log($"Mutluluk: {approveEffect.happinessChange}Temizlik: {approveEffect.cleanlinessChange}");

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
        if (denemeBool == true)
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