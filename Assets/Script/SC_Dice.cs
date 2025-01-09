using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class SC_Dice : MonoBehaviour
{
    [Header("Zar ve Kart")] public Button Button_Dice; // zarın oynaması için tıklanması gereken buton
    public GameObject Dice_Object; // zar objesi 3D
    public GameObject Card_Object; // kart objesi 2D

    [Header("Kart Elementleri")] public Text Card_Title; // Aktif Kartın başlığı
    public GameObject Card_Photo; // Aktif kartın fotografı

    [Header("Kod Elementleri")] [SerializeField]
    private bool isRolling = false; // zar dönüyor mu ?

    [SerializeField] private float waitTimer = 1f; // zar sonucu çıktıktan sonra kaç saniye beklesin?
    [SerializeField] private float rollTimer = 1f; // dönme zamanlayıcısı


    [Header("Diğer Scriptler")] public SC_ResourceManager ScriptResourceManager;
    public SC_CardSwipe ScriptCardSwipe;

    [Header("Matematiksel Değerler")]
    public int[] Positifdegerler = new int[4];
    public int[] Negatifdegerler = new int[4];
    public bool denemeBool = true;
    public int denemeInt = 0;

    
    
    public Vector3[] rotations;  // Her zar sonucu için dönüş değerleri
    public float rotateDuration = 1f; // Hedef rotasyona dönüş süresi

    private AudioSource audioSource;
    private void Awake()
    {
        Card_Object.SetActive(false);
        audioSource = Dice_Object.GetComponent<AudioSource>(); // AudioSource bileşenini al

        // playOnAwake özelliğini devre dışı bırak ve mute yap
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.mute = true; // Başlangıçta sesi mute yap
        }
    }


    public void RollDice()
    {
        if (!isRolling)
        {
            // Sesi mute'den çıkar
            if (audioSource != null)
            {
                audioSource.mute = false;
            }
            StartCoroutine(RollDiceAnimation());
            Debug.Log("zar döndürülüyor");
        }

    }

    private IEnumerator RollDiceAnimation()
    {

        isRolling = true;
        Button_Dice.interactable = false;

        // Zar sesini çal
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        float elapsedTime = 0f;

        // Zar animasyonu (rastgele dönüşler)
        while (elapsedTime < rollTimer)
        {
            elapsedTime += Time.deltaTime;

            // Rastgele dönüş
            float x = Random.Range(0, 360);
            float y = Random.Range(0, 360);
            float z = Random.Range(0, 360);
            Dice_Object.transform.Rotate(new Vector3(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime));

            yield return null;
        }

        // Zar sonucunu hesapla
        int diceResult = Random.Range(1, 7);

        // Zar sonucuna göre hedef rotasyonu belirle
        if (rotations != null && rotations.Length >= diceResult)
        {
            Vector3 targetRotation = rotations[diceResult - 1];
            yield return StartCoroutine(SmoothRotateTo(Quaternion.Euler(targetRotation)));
        }
        else
        {
            Debug.LogError("Rotations dizisi eksik veya doğru ayarlanmamış!");
        }

        isRolling = false;
        Button_Dice.interactable = true;

        // Zar sonucu işlem fonksiyonunu çalıştır
        diceResultFunction(diceResult);
    }

    private IEnumerator SmoothRotateTo(Quaternion targetRotation)
    {
        Quaternion initialRotation = Dice_Object.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotateDuration;

            // Başlangıç ve hedef rotasyon arasında yumuşak geçiş
            Dice_Object.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);

            yield return null;
        }

        // Hedef rotasyona tam olarak ayarla
        Dice_Object.transform.rotation = targetRotation;
    }

    private void diceResultFunction(int diceResult)
    {
        Debug.Log("Zar sonucu: " + diceResult);
        StartCoroutine(ShowDiceResult(diceResult));
    }



/// <summary>
/// 
/// </summary>
/// <param name="diceResult"></param>
/// <returns></returns>

    private IEnumerator ShowDiceResult(int diceResult)
    {
        yield return new WaitForSeconds(waitTimer); // zar atıldıktan sonra beklenir

        Card selectedCard = null;
        if (diceResult >= 1 &&
            diceResult <=
            3) // çıkan zar değerine göre material değişiyor. ve resource managerden random kart seçiliyor.
        {
            selectedCard = GetRandomCard(ScriptResourceManager.lowCards);
        }
        else if (diceResult >= 4 && diceResult <= 5)
        {
            selectedCard = GetRandomCard(ScriptResourceManager.midCards);
        }
        else if (diceResult == 6)
        {
            selectedCard = GetRandomCard(ScriptResourceManager.bonusCards);
        }

        if (selectedCard != null)
        {
            // Kart bilgisini UI'da göster
            DisplayCard(selectedCard);
        }

        Button_Dice.interactable = true;

    }



    private Card GetRandomCard(List<Card> cardList) // kart listesinde kart varsa kart sayısı ve 0 aralığında kart seçer ve döndürür.
    {
        if (cardList == null || cardList.Count == 0) return null;
        int randomIndex = Random.Range(0, cardList.Count);
        return cardList[randomIndex];
    }

    private void
        DisplayCard(Card card) // seçilen random kartı gösterir. adını günceller. resmini günceller. efektleri günceller.
    {
        Card_Object.SetActive(true);
        Debug.Log($"\nSeçilen Kart: {card.name}");

        Card_Title.text = card.name;
        Image cardImage = Card_Photo.GetComponent<Image>();
        if (cardImage != null)
        {
            cardImage.sprite = card.cardPhoto; // Fotoğraf atanır
        }

        UpdateEffectTexts(card);
    }


    private void UpdateEffectTexts(Card card)
    {
        var approveEffect = card.approveEffect;
        var rejectEffect = card.rejectEffect;

        // Pozitif ve negatif etkileri güncelle
        ScriptResourceManager.cardEffectsPositiveTexts[0].text = $"Mutluluk: {approveEffect.happinessChange}";
        ScriptResourceManager.cardEffectsPositiveTexts[1].text = $"Temizlik: {approveEffect.cleanlinessChange}";
        ScriptResourceManager.cardEffectsPositiveTexts[2].text = $"Güç: {approveEffect.powerChange}";
        ScriptResourceManager.cardEffectsPositiveTexts[3].text = $"Para: {approveEffect.moneyChange}";

        ScriptResourceManager.cardEffectsNegativeTexts[0].text = $"Mutluluk: {rejectEffect.happinessChange}";
        ScriptResourceManager.cardEffectsNegativeTexts[1].text = $"Temizlik: {rejectEffect.cleanlinessChange}";
        ScriptResourceManager.cardEffectsNegativeTexts[2].text = $"Güç: {rejectEffect.powerChange}";
        ScriptResourceManager.cardEffectsNegativeTexts[3].text = $"Para: {rejectEffect.moneyChange}";

        Debug.Log($"Mutluluk: {approveEffect.happinessChange}Temizlik: {approveEffect.cleanlinessChange}");

        // stringden integer'a değer döndürür. 
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
        if (Card_Object.activeSelf == true)
        {
            Button_Dice.interactable = false;
        }
        
        
        if (denemeBool == true) // card swipe'dan değer alır.
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
    public void changePositive() 
    {
        int sonuc1 = Positifdegerler[0] + ScriptResourceManager.happiness;
        int sonuc2 = Positifdegerler[1] + ScriptResourceManager.cleanliness; 
        int sonuc3 = Positifdegerler[2] + ScriptResourceManager.power; 
        int sonuc4 = Positifdegerler[3] + ScriptResourceManager.money;

        ScriptResourceManager.gameEffectsTexts[0].text = sonuc1.ToString();
        ScriptResourceManager.gameEffectsTexts[1].text = sonuc2.ToString();
        ScriptResourceManager.gameEffectsTexts[2].text = sonuc3.ToString();
        ScriptResourceManager.gameEffectsTexts[3].text = sonuc4.ToString();

        ScriptResourceManager.happiness = Convert.ToInt32(ScriptResourceManager.gameEffectsTexts[0].text);
        ScriptResourceManager.cleanliness = Convert.ToInt32(ScriptResourceManager.gameEffectsTexts[1].text);
        ScriptResourceManager.power = Convert.ToInt32(ScriptResourceManager.gameEffectsTexts[2].text);
        ScriptResourceManager.money = Convert.ToInt32(ScriptResourceManager.gameEffectsTexts[3].text);

        Card_Object.SetActive(false);
        Debug.Log("kart onaylandı");
    }
    public void changeNegative()
    {
        int sonuc1 = Negatifdegerler[0] + ScriptResourceManager.happiness;
        int sonuc2 = Negatifdegerler[1] + ScriptResourceManager.cleanliness;
        int sonuc3 = Negatifdegerler[2] + ScriptResourceManager.power;
        int sonuc4 = Negatifdegerler[3] + ScriptResourceManager.money;

        ScriptResourceManager.gameEffectsTexts[0].text = sonuc1.ToString();
        ScriptResourceManager.gameEffectsTexts[1].text = sonuc2.ToString();
        ScriptResourceManager.gameEffectsTexts[2].text = sonuc3.ToString();
        ScriptResourceManager.gameEffectsTexts[3].text = sonuc4.ToString();

        ScriptResourceManager.happiness = Convert.ToInt32(ScriptResourceManager.gameEffectsTexts[0].text);
        ScriptResourceManager.cleanliness = Convert.ToInt32(ScriptResourceManager.gameEffectsTexts[1].text);
        ScriptResourceManager.power = Convert.ToInt32(ScriptResourceManager.gameEffectsTexts[2].text);
        ScriptResourceManager.money = Convert.ToInt32(ScriptResourceManager.gameEffectsTexts[3].text);

        Card_Object.SetActive(false);
        Debug.Log("kart reddedildi");
    }
}
