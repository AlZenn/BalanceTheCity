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
    private bool isRolling = false; // zar dönüyor mu?

    [SerializeField] private float waitTimer = 1f; // zar sonucu çıktıktan sonra kaç saniye beklesin?
    [SerializeField] private float rollTimer = 1f; // dönme zamanlayıcısı

    [Header("Diğer Scriptler")] public SC_ResourceManager ScriptResourceManager;
    public SC_CardSwipe ScriptCardSwipe;

    [Header("Matematiksel Değerler")] public int[] Positifdegerler = new int[4];
    public int[] Negatifdegerler = new int[4];
    public bool denemeBool = true;
    public int denemeInt = 0;

    public Vector3[] rotations; // Her zar sonucu için dönüş değerleri
    public float rotateDuration = 1f; // Hedef rotasyona dönüş süresi
    public bool ChaosCard = false;
    
    
    private AudioSource audioSource;

    // currentCard değişkeni eklendi
    public Card currentCard;

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
        isRolling = false;
        Button_Dice.interactable = false;
    }

    private void diceResultFunction(int diceResult)
    {
        Debug.Log("Zar sonucu: " + diceResult);
        StartCoroutine(ShowDiceResult(diceResult));
    }

    private IEnumerator ShowDiceResult(int diceResult)
    {
        Button_Dice.interactable = false;
        yield return new WaitForSeconds(waitTimer); // zar atıldıktan sonra beklenir

        currentCard = null;

        // %5 ihtimalle incidentCards listesinden kart seç
        if (Random.value <= 0.05f)
        {
            if (ScriptResourceManager.incidentCards != null && ScriptResourceManager.incidentCards.Count > 0)
            {
                int randomIndex = Random.Range(0, ScriptResourceManager.incidentCards.Count);
                currentCard = ScriptResourceManager.incidentCards[randomIndex];
                ChaosCard = true;
            }
        }

        // %10 ihtimalle buildingCards listesinden kart seç
        if (currentCard == null && Random.value <= 0.10f)
        {
            if (ScriptResourceManager.buildingCards != null && ScriptResourceManager.buildingCards.Count > 0)
            {
                int randomIndex = Random.Range(0, ScriptResourceManager.buildingCards.Count);
                currentCard = ScriptResourceManager.buildingCards[randomIndex];
            }
        }

        // %85 ihtimalle diğer kart listelerinden kart seç
        if (currentCard == null) // Eğer incidentCards veya buildingCards listesinden seçilmediyse
        {
            if (diceResult >= 1 && diceResult <= 3)
            {
                currentCard = GetRandomCard(ScriptResourceManager.lowCards);
            }
            else if (diceResult >= 4 && diceResult <= 5)
            {
                currentCard = GetRandomCard(ScriptResourceManager.midCards);
            }
            else if (diceResult == 6)
            {
                currentCard = GetRandomCard(ScriptResourceManager.bonusCards);
            }
        }

        if (currentCard != null)
        {
            // Kart bilgisini UI'da göster
            DisplayCard(currentCard);
        }
    }

    private Card GetRandomCard(List<Card> cardList)
    {
        if (cardList == null || cardList.Count == 0) return null;
        int randomIndex = Random.Range(0, cardList.Count);
        return cardList[randomIndex];
    }

    public Transform InitialPosition; // Hedef pozisyon
    public Transform StartPosition; // Başlangıç pozisyonu
    public float AnimationDuration = 1f; // Animasyon süresi
    private Coroutine currentAnimation;
    
    private void DisplayCard(Card card)
    {
        if (!isSwitched)
        {
            Card_Object.SetActive(true);
            Card_Object.transform.position = StartPosition.position;
           // Card_Object.transform.position = InitialPosition.position;
        }
        else if (isSwitched)
        {
            Card_Object.SetActive(true);
             // Hedef pozisyonda kalır
            return;
        }
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(AnimateCardToPosition(InitialPosition.position));
        
        
        
        
        Debug.Log($"\nSeçilen Kart: {card.name}");

        Card_Title.text = card.name;
        Image cardImage = Card_Photo.GetComponent<Image>();
        if (cardImage != null)
        {
            cardImage.sprite = card.cardPhoto; // Fotoğraf atanır
        }

        UpdateEffectTexts(card);
    }
    
    public void ResetCardPosition()
    {
        // Daha önce çalışan animasyon varsa durdur
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }

        // Animasyonu başlat
        currentAnimation = StartCoroutine(AnimateCardToPosition(StartPosition.position));
    }

    private IEnumerator AnimateCardToPosition(Vector3 targetPosition)
    {
        Vector3 start = Card_Object.transform.position;
        Vector3 end = targetPosition;
        float elapsedTime = 0f;

        while (elapsedTime < AnimationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / AnimationDuration;

            // Lerp ile pozisyonu geçiş yap
            Card_Object.transform.position = Vector3.Lerp(start, end, t);

            yield return null; // Bir frame bekle
        }

        // Pozisyonu kesin olarak hedefe sabitle
        Card_Object.transform.position = end;

        // Animasyon tamamlandığında currentAnimation'ı sıfırla
        currentAnimation = null;
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

        Debug.Log($"Mutluluk: {approveEffect.happinessChange} Temizlik: {approveEffect.cleanlinessChange}");

        // stringden integer'a değer döndürür.
        // mathf.round = float

        Positifdegerler[0] = Mathf.RoundToInt(approveEffect.happinessChange); // 0 == mutluluk
        Positifdegerler[1] = Mathf.RoundToInt(approveEffect.cleanlinessChange); // 1 = temizlik
        Positifdegerler[2] = Mathf.RoundToInt(approveEffect.powerChange); // 2 güç
        Positifdegerler[3] = Mathf.RoundToInt(approveEffect.moneyChange); // 3 para

        Negatifdegerler[0] = Mathf.RoundToInt(rejectEffect.happinessChange);
        Negatifdegerler[1] = Mathf.RoundToInt(rejectEffect.cleanlinessChange);
        Negatifdegerler[2] = Mathf.RoundToInt(rejectEffect.powerChange);
        Negatifdegerler[3] = Mathf.RoundToInt(rejectEffect.moneyChange);
    }

    public bool isSwitched = false;

    private void Update()
    {
        if (Card_Object.activeSelf == true || isRolling == true)
        {
            Button_Dice.interactable = false;
        }

        if (denemeBool == true) // Card swipe'dan değer alır.
        {

                if (denemeInt == 1) // Sağ kaydırıldı
                {
                    Debug.Log("Sağa kaydırıldı, değerler pozitif etkilendi.");
                    Card_Object.transform.position = InitialPosition.position;
                    denemeBool = false;
                }
                else if (denemeInt == 2) // Sola kaydırıldı
                {
                    Debug.Log("Sola kaydırıldı, değerler negatif etkilendi.");
                    Card_Object.transform.position = InitialPosition.position;
                    denemeBool = false;
                }
            
        }
    }

    public void OnSwipeUp()
    {
        if (isSwitched == true)
        {
            Debug.Log("Yukarı kaydırıldı.");
            if (denemeInt == 1) // Sağ kaydırıldıktan sonra yukarı
            {
                ApplyMultiplierToPositive(2f); // 2 katı
                
                denemeInt = 0;
                
                
            }
            else if (denemeInt == 2) // Sola kaydırıldıktan sonra yukarı
            {
                ApplyMultiplierToNegative(2f); // 2 katı
                
                denemeInt = 0;
                
            }

            isSwitched = false; // İşlem tamamlandı
        }
    }

    public void OnSwipeDown()
    {
        if (isSwitched)
        {
            Debug.Log("Aşağı kaydırıldı.");
            if (denemeInt == 1) // Sağ kaydırıldıktan sonra aşağı
            {
                ApplyMultiplierToPositive(0.5f); // 0.5 katı
                denemeInt = 0;
            }
            else if (denemeInt == 2) // Sola kaydırıldıktan sonra aşağı
            {
                ApplyMultiplierToNegative(0.5f); // 0.5 katı
                denemeInt = 0;
            }
            
            isSwitched = false; // İşlem tamamlandı
        }
    }

    private void ApplyMultiplierToPositive(float multiplier)
    {
        // Pozitif değerleri güncelle
        int sonuc1 = (int)((Positifdegerler[0] * multiplier) + ScriptResourceManager.happiness);
        int sonuc2 = (int)((Positifdegerler[1] * multiplier) + ScriptResourceManager.cleanliness);
        int sonuc3 = (int)((Positifdegerler[2] * multiplier) + ScriptResourceManager.power);
        int sonuc4 = (int)((Positifdegerler[3] * multiplier) + ScriptResourceManager.money);


        //Debug.Log(sonuc1 + sonuc2 + sonuc3 + sonuc4);

        ScriptResourceManager.gameEffectsTexts[0].text = sonuc1.ToString();
        ScriptResourceManager.gameEffectsTexts[1].text = sonuc2.ToString();
        ScriptResourceManager.gameEffectsTexts[2].text = sonuc3.ToString();
        ScriptResourceManager.gameEffectsTexts[3].text = sonuc4.ToString();

        ScriptResourceManager.happiness = sonuc1;
        ScriptResourceManager.cleanliness = sonuc2;
        ScriptResourceManager.power = sonuc3;
        ScriptResourceManager.money = sonuc4;

        Card_Object.SetActive(false);
        Debug.Log("kart onaylandı");
        isSwitched = false;
        ScriptResourceManager.UpdateSliders();
        ScriptResourceManager.SaveGame();
    }

    private void ApplyMultiplierToNegative(float multiplier)
    {
        int sonuc1 = (int)((Negatifdegerler[0] * multiplier) + ScriptResourceManager.happiness);
        int sonuc2 = (int)((Negatifdegerler[1] * multiplier) + ScriptResourceManager.cleanliness);
        int sonuc3 = (int)((Negatifdegerler[2] * multiplier) + ScriptResourceManager.power);
        int sonuc4 = (int)((Negatifdegerler[3] * multiplier) + ScriptResourceManager.money);

        ScriptResourceManager.gameEffectsTexts[0].text = sonuc1.ToString();
        ScriptResourceManager.gameEffectsTexts[1].text = sonuc2.ToString();
        ScriptResourceManager.gameEffectsTexts[2].text = sonuc3.ToString();
        ScriptResourceManager.gameEffectsTexts[3].text = sonuc4.ToString();

        ScriptResourceManager.happiness = sonuc1;
        ScriptResourceManager.cleanliness = sonuc2;
        ScriptResourceManager.power = sonuc3;
        ScriptResourceManager.money = sonuc4;

        Card_Object.SetActive(false);
        Debug.Log("kart reddedildi");
        isSwitched = false;
        ScriptResourceManager.UpdateSliders();
        ScriptResourceManager.SaveGame();

    }
}
