using System;
using UnityEngine;
using UnityEngine.UI;

public class SC_Achievement : MonoBehaviour
{
    private float timer;

    [Header("Panel Objeleri")]
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private Text achievementPanelText;
    [SerializeField] private float timerCooldown = 2f;

    [Header("Animasyon Ayarları")]
    [SerializeField] private Transform startPoint; // Panelin başlangıç noktası (üstte)
    [SerializeField] private Transform endPoint;   // Panelin aşağıda duracağı nokta
    [SerializeField] private float animationSpeed = 2f; // Animasyonun hızı

    private bool isAnimating = false;

    void Awake()
    {
        achievementPanel.SetActive(false);
    }

    private void Start()
    {
        achievementPanelText.text = "";
        
        // Prefabstan yükle
        LoadAchievementStates();
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Happiness") > 30 && !IsAchievementTaken("Happiness"))
        {
            SetAchievementTaken("Happiness");
            ShowAchievementPanel("Mutluluk 30+");
        }
        if (PlayerPrefs.GetInt("Cleanliness") > 30 && !IsAchievementTaken("Cleanliness"))
        {
            SetAchievementTaken("Cleanliness");
            ShowAchievementPanel("Cleanliness 30+");
        }
        if (PlayerPrefs.GetInt("Power") > 30 && !IsAchievementTaken("Power"))
        {
            SetAchievementTaken("Power");
            ShowAchievementPanel("Power 30+");
        }
        if (PlayerPrefs.GetInt("Money") > 30 && !IsAchievementTaken("Money"))
        {
            SetAchievementTaken("Money");
            ShowAchievementPanel("Money 30+");
        }
        if (PlayerPrefs.GetInt("Day") >= 30 && PlayerPrefs.GetFloat("Votes") > 50 && !IsAchievementTaken("DayWin"))
        {
            SetAchievementTaken("DayWin");
            ShowAchievementPanel("Gün 30 oldu ve başkanlık kazanıldı");
        }
        if (PlayerPrefs.GetFloat("Trust") == 0 && PlayerPrefs.GetInt("Day") >= 30 && !IsAchievementTaken("Trust"))
        {
            SetAchievementTaken("Trust");
            ShowAchievementPanel("Hiç backrooms kullanılmadı.");
        }
        if (PlayerPrefs.GetInt("Day") >= 30 && !IsAchievementTaken("Day30"))
        {
            SetAchievementTaken("Day30");
            ShowAchievementPanel("Gün 30 başkanlık yapıldı");
        }
    }

    private void ShowAchievementPanel(string achievementText)
    {
        if (isAnimating) return; // Animasyon devam ediyorsa yeni bir gösterim başlatma

        achievementPanelText.text = achievementText;
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
