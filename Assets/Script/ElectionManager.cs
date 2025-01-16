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
    public GameObject winPanel; // Kazandýnýz paneli
    public GameObject losePanel; // Kaybettiniz paneli

    [Header("Day System Elements")]
    public int day;
    [SerializeField] private Text dayText;

    private float totalVotes = 0;

    private void Start()
    {
        // Baþlangýçta panelleri gizle
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        // Gün sistemini baþlat
        dayText.text = "Day: " + day;

        // Butonlarýn satýn alýnma durumlarýný kontrol et ve oy deðerlerini ekle
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
                totalVotes += 4; // Boost: Eðer "Left Wing" varsa ekstra +%4 oy
            if (PlayerPrefs.HasKey("Endüstriyel"))
                totalVotes -= 3; // Kontra: "Endüstriyel" varsa etkisi %25 azalýr
        }
        if (PlayerPrefs.HasKey("Buisness"))
        {
            totalVotes += 14;
            if (PlayerPrefs.HasKey("Aktivist"))
                totalVotes -= 14; // Kontra: "Aktivist" varsa birbirlerini etkisizleþtirir
            else if (presidentManager.GetMaxMoney() == 40) // Assuming maxMoney 40 means Economic President
                totalVotes += 3; // Boost: Ekonomist baþkan varsa ekstra +%3 oy
        }
        if (PlayerPrefs.HasKey("LeftWing"))
        {
            totalVotes += 13;
            if (PlayerPrefs.HasKey("Aktivist"))
                totalVotes += 5; // Boost: Eðer "Aktivist" varsa ekstra +%5 oy
            if (PlayerPrefs.HasKey("RightWing"))
                totalVotes -= 13; // Kontra: "Right Wing" varsa birbirlerini etkisizleþtirir
        }
        if (PlayerPrefs.HasKey("RightWing"))
        {
            totalVotes += 13;
            if (PlayerPrefs.HasKey("LeftWing"))
                totalVotes -= 13; // Kontra: "Left Wing" varsa birbirlerini etkisizleþtirir
        }
        if (PlayerPrefs.HasKey("Sendikalar"))
        {
            totalVotes += 10;
            if (PlayerPrefs.HasKey("LeftWing"))
                totalVotes += 3; // Boost: Eðer "Left Wing" varsa ekstra +%3 oy
            if (PlayerPrefs.HasKey("Endüstriyel"))
                totalVotes -= 5; // Kontra: "Endüstriyel" varsa etkisi %50 azalýr
        }
        if (PlayerPrefs.HasKey("Sanatçýlar"))
        {
            totalVotes += 8;
            if (PlayerPrefs.HasKey("Aktivist"))
                totalVotes += 3; // Boost: Eðer "Aktivist" varsa ekstra +%3 oy
        }
        if (PlayerPrefs.HasKey("Endüstriyel"))
        {
            totalVotes += 15;
            if (PlayerPrefs.HasKey("Aktivist"))
                totalVotes -= 4; // Kontra: "Aktivist" varsa etkisi %25 azalýr
            else if (presidentManager.GetMaxPower() == 40) // Assuming maxPower 40 means Power Focused President
                totalVotes += 5; // Boost: Güç odaklý baþkan varsa ekstra +%5 oy
        }
        if (PlayerPrefs.HasKey("Çevreciler"))
        {
            totalVotes += 10;
            if (PlayerPrefs.HasKey("LeftWing"))
                totalVotes += 3; // Boost: Eðer "Left Wing" varsa ekstra +%3 oy
        }
        if (PlayerPrefs.HasKey("Medya"))
        {
            totalVotes += 15;
            if (PlayerPrefs.HasKey("RightWing") || PlayerPrefs.HasKey("Endüstriyel"))
                totalVotes += 3; // Boost: Eðer "Right Wing" veya "Endüstriyel" varsa ekstra +%3 oy
        }
    }

    private void ApplyPresidentEffects()
    {
        // Ekstra baþkan etkilerini buraya ekleyin
        if (presidentManager.GetMaxMoney() == 40)
        {
            // Economic President bonuslarý
            if (PlayerPrefs.HasKey("Buisness"))
            {
                totalVotes += 3; // Ekonomist baþkan varsa ekstra +%3 oy
            }
        }
        if (presidentManager.GetMaxPower() == 40)
        {
            // Power Focused President bonuslarý
            if (PlayerPrefs.HasKey("Endüstriyel"))
            {
                totalVotes += 5; // Güç odaklý baþkan varsa ekstra +%5 oy
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
                    totalVotes += 4; // Boost: Eðer "Left Wing" varsa ekstra +%4 oy
                if (PlayerPrefs.HasKey("Endüstriyel"))
                    totalVotes -= 3; // Kontra: "Endüstriyel" varsa etkisi %25 azalýr
                break;
            case "Buisness":
                totalVotes += 14;
                if (PlayerPrefs.HasKey("Aktivist"))
                    totalVotes -= 14; // Kontra: "Aktivist" varsa birbirlerini etkisizleþtirir
                else if (presidentManager.GetMaxMoney() == 40)
                    totalVotes += 3; // Boost: Ekonomist baþkan varsa ekstra +%3 oy
                break;
            case "LeftWing":
                totalVotes += 13;
                if (PlayerPrefs.HasKey("Aktivist"))
                    totalVotes += 5; // Boost: Eðer "Aktivist" varsa ekstra +%5 oy
                if (PlayerPrefs.HasKey("RightWing"))
                    totalVotes -= 13; // Kontra: "Right Wing" varsa birbirlerini etkisizleþtirir
                break;
            case "RightWing":
                totalVotes += 13;
                if (PlayerPrefs.HasKey("LeftWing"))
                    totalVotes -= 13; // Kontra: "Left Wing" varsa birbirlerini etkisizleþtirir
                break;
            case "Sendikalar":
                totalVotes += 10;
                if (PlayerPrefs.HasKey("LeftWing"))
                    totalVotes += 3; // Boost: Eðer "Left Wing" varsa ekstra +%3 oy
                if (PlayerPrefs.HasKey("Endüstriyel"))
                    totalVotes -= 5; // Kontra: "Endüstriyel" varsa etkisi %50 azalýr
                break;
            case "Sanatçýlar":
                totalVotes += 8;
                if (PlayerPrefs.HasKey("Aktivist"))
                    totalVotes += 3; // Boost: Eðer "Aktivist" varsa ekstra +%3 oy
                break;
            case "Endüstriyel":
                totalVotes += 15;
                if (PlayerPrefs.HasKey("Aktivist"))
                    totalVotes -= 4; // Kontra: "Aktivist" varsa etkisi %25 azalýr
                else if (presidentManager.GetMaxPower() == 40)
                    totalVotes += 5; // Boost: Güç odaklý baþkan varsa ekstra +%5 oy
                break;
            case "Çevreciler":
                totalVotes += 10;
                if (PlayerPrefs.HasKey("LeftWing"))
                    totalVotes += 3; // Boost: Eðer "Left Wing" varsa ekstra +%3 oy
                break;
            case "Medya":
                totalVotes += 15;
                if (PlayerPrefs.HasKey("RightWing") || PlayerPrefs.HasKey("Endüstriyel"))
                    totalVotes += 3; // Boost: Eðer "Right Wing" veya "Endüstriyel" varsa ekstra +%3 oy
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
        if (day >= 30)
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
        PlayerPrefs.SetInt("Day", day);
    }

    public void dayTextUpdate()
    {
        day++;
        dayText.text = "Day: " + day;

        daySave();
        CheckWinOrLose();
    }
}