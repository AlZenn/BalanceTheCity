using System;
using UnityEngine;
using UnityEngine.UI;

public class SC_Achievement : MonoBehaviour
{
    private float timer;

    [Header("Panel Objeleri")]
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private Image achievementImage; // Başarım görseli
    [SerializeField] private Text achievementTitleText; // Başlık için
    [SerializeField] private Text achievementDescriptionText; // Açıklama için
    [SerializeField] private float timerCooldown = 2f;

    [Header("Animasyon Ayarları")]
    [SerializeField] private Transform startPoint; // Panelin başlangıç noktası (üstte)
    [SerializeField] private Transform endPoint;   // Panelin aşağıda duracağı nokta
    [SerializeField] private float animationSpeed = 2f; // Animasyonun hızı

    [Header("Başarım Sprite'ları ve Metinleri")]
    [SerializeField] private Sprite happinessSprite;
    [SerializeField] private Sprite cleanlinessSprite;
    [SerializeField] private Sprite powerSprite;
    [SerializeField] private Sprite moneySprite;
    [SerializeField] private Sprite dayWinSprite;
    [SerializeField] private Sprite trustSprite;
    

    private bool isAnimating = false;

    void Awake()
    {
        achievementPanel.SetActive(false);
    }

    private void Start()
    {
        achievementTitleText.text = "";
        achievementDescriptionText.text = "";

        // Prefabstan yükle
        LoadAchievementStates();
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Happiness") > 30 && !IsAchievementTaken("Happiness"))
        {
            SetAchievementTaken("Happiness");
            ShowAchievementPanel(happinessSprite, "Mutlu Şehir", "Halkın mutluluğunu 30 ve üzeri bir değerde tuttun!");
        }
        if (PlayerPrefs.GetInt("Cleanliness") > 30 && !IsAchievementTaken("Cleanliness"))
        {
            SetAchievementTaken("Cleanliness");
            ShowAchievementPanel(cleanlinessSprite, "Temizlik Şampiyonu", "Temizlik oranını 30 ve üzerinde tutarak uzun süre şehri yönetmeye devam ettin!");
        }
        if (PlayerPrefs.GetInt("Power") > 30 && !IsAchievementTaken("Power"))
        {
            SetAchievementTaken("Power");
            ShowAchievementPanel(powerSprite, "Demir Yumruk", "Güç kaynağını 30 ve üzeri bir değerde tutarak başkanlık görevini sürdürdün!");
        }
        if (PlayerPrefs.GetInt("Money") > 30 && !IsAchievementTaken("Money"))
        {
            SetAchievementTaken("Money");
            ShowAchievementPanel(moneySprite, "Altın Çağ", "Para kaynağını 30 ve üzeri bir değerde tutarak başkanlık görevini sürdürdün!");
        }
        if (PlayerPrefs.GetInt("Day") >= 30 && PlayerPrefs.GetFloat("Votes") > 50 && !IsAchievementTaken("DayWin"))
        {
            SetAchievementTaken("DayWin");
            ShowAchievementPanel(dayWinSprite, "Başkanık Dönemi", "30 gün sonunda başkanlığı kazandınız!");
        }
        if (PlayerPrefs.GetFloat("Trust") == 0 && PlayerPrefs.GetInt("Day") >= 30 && !IsAchievementTaken("Trust"))
        {
            SetAchievementTaken("Trust");
            ShowAchievementPanel(trustSprite, "Dışarıda Kal", "Backrooms alanını hiç kullanmadan, sadece şehri yönlendirerek 30 gün başkanlığa devam ettin!");
        }
        
    }

    private void ShowAchievementPanel(Sprite sprite, string title, string description)
    {
        if (isAnimating) return; // Animasyon devam ediyorsa yeni bir gösterim başlatma

        achievementImage.sprite = sprite; 
        achievementTitleText.text = title; 
        achievementDescriptionText.text = description;

        achievementPanel.SetActive(true);
        StartCoroutine(AnimatePanel());
    }

    private System.Collections.IEnumerator AnimatePanel()
    {
        isAnimating = true;

        // Panel yukarıdan aşağıya
        float t = 0f;
        while (t < 2f)
        {
            t += Time.deltaTime * animationSpeed;
            achievementPanel.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
            yield return null;
        }
        
        yield return new WaitForSeconds(timerCooldown);

        // Panel aşağıdan yukarı
        t = 0f;
        while (t < 2f)
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
        PlayerPrefs.Save(); // Değerleri kalıcı hale getir
    }
    
    private void LoadAchievementStates()
    {
        Debug.Log("Başarımlar yüklendi.");
    }
}
