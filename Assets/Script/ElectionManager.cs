using UnityEngine;
using UnityEngine.UI;

public class ElectionManager : MonoBehaviour
{
    [Header("Text Elements")]
    public Text voteText;

    [Header("Resource Manager")]
    public SC_ResourceManager resourceManager;

    [Header("Shop Manager")]
    public ShopManager shopManager;

    [Header("President Manager")]
    public PresidentManager presidentManager;

    [Header("Win/Lose Panels")]
    public GameObject winPanel; // Kazand�n�z paneli
    public GameObject losePanel; // Kaybettiniz paneli

    [Header("Day System Elements")]
    //public int day;
    [SerializeField] private Text dayText;

    private float totalVotes = 0;

    private void Start()
    {
        // Ba�lang��ta panelleri gizle
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        // G�n sistemini ba�lat
        if (PlayerPrefs.HasKey("Day"))
        {
            dayText.text = "Day: " + PlayerPrefs.GetInt("Day");
        }
        else
        {
            dayText.text = "Day: " + resourceManager.day;
        }
        

        // Butonlar�n sat�n al�nma durumlar�n� kontrol et ve oy de�erlerini ekle
        CheckPurchasedButtons();
        ApplyPresidentEffects();
        UpdateVoteText();
    }

    private void CheckPurchasedButtons()
    {
        if (PlayerPrefs.HasKey("Aktivist"))
        {
            totalVotes += 12;
            if (PlayerPrefs.HasKey("LeftWing"))
                totalVotes += 4; // Boost: E�er "Left Wing" varsa ekstra +%4 oy
            if (PlayerPrefs.HasKey("End�striyel"))
                totalVotes -= 3; // Kontra: "End�striyel" varsa etkisi %25 azal�r
        }
        if (PlayerPrefs.HasKey("Buisness"))
        {
            totalVotes += 14;
            if (PlayerPrefs.HasKey("Aktivist"))
                totalVotes -= 14; // Kontra: "Aktivist" varsa birbirlerini etkisizle�tirir
            else if (presidentManager.GetMaxMoney() == 40) // Assuming maxMoney 40 means Economic President
                totalVotes += 3; // Boost: Ekonomist ba�kan varsa ekstra +%3 oy
        }
        if (PlayerPrefs.HasKey("LeftWing"))
        {
            totalVotes += 13;
            if (PlayerPrefs.HasKey("Aktivist"))
                totalVotes += 5; // Boost: E�er "Aktivist" varsa ekstra +%5 oy
            if (PlayerPrefs.HasKey("RightWing"))
                totalVotes -= 13; // Kontra: "Right Wing" varsa birbirlerini etkisizle�tirir
        }
        if (PlayerPrefs.HasKey("RightWing"))
        {
            totalVotes += 13;
            if (PlayerPrefs.HasKey("LeftWing"))
                totalVotes -= 13; // Kontra: "Left Wing" varsa birbirlerini etkisizle�tirir
        }
        if (PlayerPrefs.HasKey("Sendikalar"))
        {
            totalVotes += 10;
            if (PlayerPrefs.HasKey("LeftWing"))
                totalVotes += 3; // Boost: E�er "Left Wing" varsa ekstra +%3 oy
            if (PlayerPrefs.HasKey("End�striyel"))
                totalVotes -= 5; // Kontra: "End�striyel" varsa etkisi %50 azal�r
        }
        if (PlayerPrefs.HasKey("Sanat��lar"))
        {
            totalVotes += 8;
            if (PlayerPrefs.HasKey("Aktivist"))
                totalVotes += 3; // Boost: E�er "Aktivist" varsa ekstra +%3 oy
        }
        if (PlayerPrefs.HasKey("End�striyel"))
        {
            totalVotes += 15;
            if (PlayerPrefs.HasKey("Aktivist"))
                totalVotes -= 4; // Kontra: "Aktivist" varsa etkisi %25 azal�r
            else if (presidentManager.GetMaxPower() == 40) // Assuming maxPower 40 means Power Focused President
                totalVotes += 5; // Boost: G�� odakl� ba�kan varsa ekstra +%5 oy
        }
        if (PlayerPrefs.HasKey("�evreciler"))
        {
            totalVotes += 10;
            if (PlayerPrefs.HasKey("LeftWing"))
                totalVotes += 3; // Boost: E�er "Left Wing" varsa ekstra +%3 oy
        }
        if (PlayerPrefs.HasKey("Medya"))
        {
            totalVotes += 15;
            if (PlayerPrefs.HasKey("RightWing") || PlayerPrefs.HasKey("End�striyel"))
                totalVotes += 3; // Boost: E�er "Right Wing" veya "End�striyel" varsa ekstra +%3 oy
        }
    }

    private void ApplyPresidentEffects()
    {
        // Ekstra ba�kan etkilerini buraya ekleyin
        if (presidentManager.GetMaxMoney() == 40)
        {
            // Economic President bonuslar�
            if (PlayerPrefs.HasKey("Buisness"))
            {
                totalVotes += 3; // Ekonomist ba�kan varsa ekstra +%3 oy
            }
        }
        if (presidentManager.GetMaxPower() == 40)
        {
            // Power Focused President bonuslar�
            if (PlayerPrefs.HasKey("End�striyel"))
            {
                totalVotes += 5; // G�� odakl� ba�kan varsa ekstra +%5 oy
            }
        }
    }

    public void AddVotes(string buttonKey)
    {
        switch (buttonKey)
        {
            case "Aktivist":
                totalVotes += 12;
                if (PlayerPrefs.HasKey("LeftWing"))
                    totalVotes += 4; // Boost: E�er "Left Wing" varsa ekstra +%4 oy
                if (PlayerPrefs.HasKey("End�striyel"))
                    totalVotes -= 3; // Kontra: "End�striyel" varsa etkisi %25 azal�r
                break;
            case "Buisness":
                totalVotes += 14;
                if (PlayerPrefs.HasKey("Aktivist"))
                    totalVotes -= 14; // Kontra: "Aktivist" varsa birbirlerini etkisizle�tirir
                else if (presidentManager.GetMaxMoney() == 40)
                    totalVotes += 3; // Boost: Ekonomist ba�kan varsa ekstra +%3 oy
                break;
            case "LeftWing":
                totalVotes += 13;
                if (PlayerPrefs.HasKey("Aktivist"))
                    totalVotes += 5; // Boost: E�er "Aktivist" varsa ekstra +%5 oy
                if (PlayerPrefs.HasKey("RightWing"))
                    totalVotes -= 13; // Kontra: "Right Wing" varsa birbirlerini etkisizle�tirir
                break;
            case "RightWing":
                totalVotes += 13;
                if (PlayerPrefs.HasKey("LeftWing"))
                    totalVotes -= 13; // Kontra: "Left Wing" varsa birbirlerini etkisizle�tirir
                break;
            case "Sendikalar":
                totalVotes += 10;
                if (PlayerPrefs.HasKey("LeftWing"))
                    totalVotes += 3; // Boost: E�er "Left Wing" varsa ekstra +%3 oy
                if (PlayerPrefs.HasKey("End�striyel"))
                    totalVotes -= 5; // Kontra: "End�striyel" varsa etkisi %50 azal�r
                break;
            case "Sanat��lar":
                totalVotes += 8;
                if (PlayerPrefs.HasKey("Aktivist"))
                    totalVotes += 3; // Boost: E�er "Aktivist" varsa ekstra +%3 oy
                break;
            case "End�striyel":
                totalVotes += 15;
                if (PlayerPrefs.HasKey("Aktivist"))
                    totalVotes -= 4; // Kontra: "Aktivist" varsa etkisi %25 azal�r
                else if (presidentManager.GetMaxPower() == 40)
                    totalVotes += 5; // Boost: G�� odakl� ba�kan varsa ekstra +%5 oy
                break;
            case "�evreciler":
                totalVotes += 10;
                if (PlayerPrefs.HasKey("LeftWing"))
                    totalVotes += 3; // Boost: E�er "Left Wing" varsa ekstra +%3 oy
                break;
            case "Medya":
                totalVotes += 15;
                if (PlayerPrefs.HasKey("RightWing") || PlayerPrefs.HasKey("End�striyel"))
                    totalVotes += 3; // Boost: E�er "Right Wing" veya "End�striyel" varsa ekstra +%3 oy
                break;
        }

        UpdateVoteText();
    }

    private void UpdateVoteText()
    {
        voteText.text = $" Votes: {totalVotes}%";
        CheckWinOrLose();
    }

    private void CheckWinOrLose()
    {
        if (resourceManager.day >= 30)
        {
            if (totalVotes >= 50)
            {
                winPanel.SetActive(true);
                losePanel.SetActive(false);
            }
            else
            {
                winPanel.SetActive(false);
                losePanel.SetActive(true);
            }
        }
        else
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }
    }

    public void daySave()
    {
        PlayerPrefs.SetInt("Day", resourceManager.day);
    }

    public void dayTextUpdate()
    {
        resourceManager.day++;
        dayText.text = "Day: " + resourceManager.day;

        daySave();
        CheckWinOrLose();
    }
}