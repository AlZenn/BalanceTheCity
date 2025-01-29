using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SC_Achievement : MonoBehaviour
{
    [Header("Panel Objeleri")]
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private Image achievementImage;
    [SerializeField] private Text achievementTitleText;
    [SerializeField] private Text achievementDescriptionText;
    [SerializeField] private float timerCooldown = 2f;

    [Header("Animasyon Ayarları")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float animationSpeed = 2f;

    [Header("Başarım Sprite'ları ve Metinleri")]
    [SerializeField] private Sprite happinessSprite;
    [SerializeField] private Sprite cleanlinessSprite;
    [SerializeField] private Sprite powerSprite;
    [SerializeField] private Sprite moneySprite;
    [SerializeField] private Sprite dayWinSprite;
    [SerializeField] private Sprite trustSprite;

    private SC_ResourceManager ScriptResourceManager;
    private SC_Backrooms ScriptBackrooms;

    private bool isAnimating = false;
    private Queue<Action> achievementQueue = new Queue<Action>(); // Sıraya alınan başarımlar

    void Awake()
    {
        achievementPanel.SetActive(false);
        ScriptResourceManager = GameObject.FindWithTag("GameUI").GetComponent<SC_ResourceManager>();
        ScriptBackrooms = GameObject.FindWithTag("GameUI").GetComponent<SC_Backrooms>();
    }

    private void Start()
    {
        achievementTitleText.text = "";
        achievementDescriptionText.text = "";
        LoadAchievementStates();
    }

    void Update()
    {
// Başarımları sıraya al
        if (!IsAchievementTaken("Happiness1") && ScriptResourceManager.happiness > 30)
        {
            EnqueueAchievement("Happiness1", happinessSprite, 
                "Mutlu Şehir", "Halkın mutluluğunu 30 ve üzeri bir değerde tuttun!");
        }

        if (!IsAchievementTaken("Cleanliness1") && ScriptResourceManager.cleanliness > 30)
        {
            EnqueueAchievement("Cleanliness1", cleanlinessSprite, 
                "Temizlik Şampiyonu", "Temizlik oranını 30 ve üzerinde tutarak uzun süre şehri yönetmeye devam ettin!");
        }

        if (!IsAchievementTaken("Power1") && ScriptResourceManager.power > 30)
        {
            EnqueueAchievement("Power1", powerSprite, 
                "Demir Yumruk", "Güç kaynağını 30 ve üzeri bir değerde tutarak başkanlık görevini sürdürdün!");
        }

        if (!IsAchievementTaken("Money1") && ScriptResourceManager.money > 30)
        {
            EnqueueAchievement("Money1", moneySprite, 
                "Altın Çağ", "Para kaynağını 30 ve üzeri bir değerde tutarak başkanlık görevini sürdürdün!");
        }

        if (!IsAchievementTaken("DayWin1") && ScriptResourceManager.day >= 30 && PlayerPrefs.GetFloat("Votes") > 50)
        {
            EnqueueAchievement("DayWin1", dayWinSprite, 
                "Başkanlık Dönemi", "30 gün sonunda başkanlığı kazandınız!");
        }

        if (!IsAchievementTaken("Trust1") && ScriptBackrooms.trustSlider.value == 1f && PlayerPrefs.GetInt("Day") >= 30)
        {
            EnqueueAchievement("Trust1", trustSprite, 
                "Dışarıda Kal", "Backrooms alanını hiç kullanmadan, sadece şehri yönlendirerek 30 gün başkanlığa devam ettin!");
        }

// EnqueueAchievement fonksiyonunu güncelle

        // Eğer şu an animasyon çalışmıyorsa sıradaki başarıyı göster
        if (!isAnimating && achievementQueue.Count > 0)
        {
            achievementQueue.Dequeue()?.Invoke();
        }
    }

    private void EnqueueAchievement(string key, Sprite sprite, string title, string description)
    {
        SetAchievementTaken(key); // Başarımın alındığını kaydet

        // Gösterme işlemini sıraya al
        achievementQueue.Enqueue(() => ShowAchievement(key, sprite, title, description));
    }


    private void ShowAchievement(string key, Sprite sprite, string title, string description)
    {
        if (isAnimating) return;

        // Başarım alındı olarak kaydediliyor
        SetAchievementTaken(key);

        achievementImage.sprite = sprite;
        achievementTitleText.text = title;
        achievementDescriptionText.text = description;

        achievementPanel.SetActive(true);
        StartCoroutine(AnimatePanel());
    }

    private IEnumerator AnimatePanel()
    {
        isAnimating = true;

        // Panel yukarıdan aşağıya gelsin
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * animationSpeed;
            achievementPanel.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
            yield return null;
        }

        yield return new WaitForSeconds(timerCooldown);

        // Panel aşağıdan yukarı çıksın
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * animationSpeed;
            achievementPanel.transform.position = Vector3.Lerp(endPoint.position, startPoint.position, t);
            yield return null;
        }

        achievementPanel.SetActive(false);
        isAnimating = false;
    }

    private bool IsAchievementTaken(string achievementKey)
    {
        return PlayerPrefs.GetInt(achievementKey, 0) == 1;
    }

    private void SetAchievementTaken(string achievementKey)
    {
        PlayerPrefs.SetInt(achievementKey, 1);
        PlayerPrefs.Save();
    }

    private void LoadAchievementStates()
    {
        Debug.Log("Başarımlar yüklendi.");
    }
}
