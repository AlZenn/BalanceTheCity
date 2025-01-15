using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SC_ResourceManager : MonoBehaviour
{
    // Temel Unsurlar
    public int happiness = 10;
    public int cleanliness = 10;
    public int power = 10;
    public int money = 10;

    // Kaynak Text Objeleri
    public Text[] cardEffectsPositiveTexts;
    public Text[] cardEffectsNegativeTexts;
    public Text[] gameEffectsTexts;

    // Gün Sistemi Elemanları
    [SerializeField] private int day;
    [SerializeField] private Text dayText;

    // Scripts
    private SC_Backrooms ScriptBackrooms;

    private int previousHappiness;
    private int previousCleanliness;
    private int previousPower;
    private int previousMoney;

    // Ardışık artış sayaçları
    [SerializeField] public int consecutiveCleanlinessIncreases;
    [SerializeField] public int consecutivePowerIncreases;
    [SerializeField] public int consecutiveHappinessIncreases;
    [SerializeField] public int consecutiveMoneyIncreases;

    private List<Effect> activeEffects = new List<Effect>();

    private void Start()
    {
        LoadGame();
        ScriptBackrooms = GameObject.FindWithTag("GameUI").GetComponent<SC_Backrooms>();
        dayText.text = "Day: " + day;
        ScriptBackrooms.ButtonBackrooms();

        previousHappiness = happiness;
        previousCleanliness = cleanliness;
        previousPower = power;
        previousMoney = money;

        consecutiveCleanlinessIncreases = 0;
        consecutivePowerIncreases = 0;
        consecutiveHappinessIncreases = 0;
        consecutiveMoneyIncreases = 0;
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Happiness", happiness);
        PlayerPrefs.SetInt("Cleanliness", cleanliness);
        PlayerPrefs.SetInt("Power", power);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("day", day);
        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("Happiness") || PlayerPrefs.HasKey("Cleanliness"))
        {
            happiness = PlayerPrefs.GetInt("Happiness");
            cleanliness = PlayerPrefs.GetInt("Cleanliness");
            power = PlayerPrefs.GetInt("Power");
            money = PlayerPrefs.GetInt("Money");

            gameEffectsTexts[0].text = happiness.ToString();
            gameEffectsTexts[1].text = cleanliness.ToString();
            gameEffectsTexts[2].text = power.ToString();
            gameEffectsTexts[3].text = money.ToString();

            Debug.Log("Game Loaded!");
        }
        else
        {
            happiness = 10;
            cleanliness = 10;
            power = 10;
            money = 10;

            gameEffectsTexts[0].text = happiness.ToString();
            gameEffectsTexts[1].text = cleanliness.ToString();
            gameEffectsTexts[2].text = power.ToString();
            gameEffectsTexts[3].text = money.ToString();
        }

        if (PlayerPrefs.HasKey("Day"))
        {
            day = PlayerPrefs.GetInt("Day");
            dayText.text = "Day: " + day;
            Debug.Log("Day Loaded!");
        }
        else
        {
            day = 1;
            dayText.text = "Day: " + day;
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
        ScriptBackrooms.ButtonBackrooms();
        daySave();
        UpdateActiveEffects();
    }

    private void UpdateActiveEffects()
    {
        List<Effect> effectsToRemove = new List<Effect>();

        foreach (Effect effect in activeEffects)
        {
            ApplyEffect(effect);

            effect.duration--;
            if (effect.duration <= 0)
            {
                effectsToRemove.Add(effect);
            }
        }

        foreach (Effect effect in effectsToRemove)
        {
            activeEffects.Remove(effect);
        }

        // Güncel değerleri UI'a yansıt
        gameEffectsTexts[0].text = happiness.ToString();
        gameEffectsTexts[1].text = cleanliness.ToString();
        gameEffectsTexts[2].text = power.ToString();
        gameEffectsTexts[3].text = money.ToString();
    }

    private void ApplyEffect(Effect effect)
    {
        happiness += effect.happinessChange;
        cleanliness += effect.cleanlinessChange;
        power += effect.powerChange;
        money += effect.moneyChange;

        // Ardışık artışları güncelle
        UpdateConsecutiveIncreases(effect.happinessChange, effect.cleanlinessChange, effect.powerChange, effect.moneyChange);
    }

    public void ApplyBuildingEffect(Effect effect)
    {
        Effect newEffect = new Effect(
            effect.happinessChange,
            effect.cleanlinessChange,
            effect.powerChange,
            effect.moneyChange,
            5 // Etki süresi 5 gün
        );

        activeEffects.Add(newEffect);
    }

    public void UpdateStats(int happinessChange, int cleanlinessChange, int powerChange, int moneyChange)
    {
        UpdateConsecutiveIncreases(happinessChange, cleanlinessChange, powerChange, moneyChange);

        happiness += happinessChange;
        cleanliness += cleanlinessChange;
        power += powerChange;
        money += moneyChange;

        previousHappiness = happiness;
        previousCleanliness = cleanliness;
        previousPower = power;
        previousMoney = money;

        // Güncel değerleri UI'a yansıt
        gameEffectsTexts[0].text = happiness.ToString();
        gameEffectsTexts[1].text = cleanliness.ToString();
        gameEffectsTexts[2].text = power.ToString();
        gameEffectsTexts[3].text = money.ToString();
    }

    private void UpdateConsecutiveIncreases(int happinessChange, int cleanlinessChange, int powerChange, int moneyChange)
    {
        if (happinessChange > 0 && happiness > previousHappiness)
        {
            consecutiveHappinessIncreases++;
        }
        else
        {
            consecutiveHappinessIncreases = 0;
        }

        if (cleanlinessChange > 0 && cleanliness > previousCleanliness)
        {
            consecutiveCleanlinessIncreases++;
        }
        else
        {
            consecutiveCleanlinessIncreases = 0;
        }

        if (powerChange > 0 && power > previousPower)
        {
            consecutivePowerIncreases++;
        }
        else
        {
            consecutivePowerIncreases = 0;
        }

        if (moneyChange > 0 && money > previousMoney)
        {
            consecutiveMoneyIncreases++;
        }
        else
        {
            consecutiveMoneyIncreases = 0;
        }

        previousHappiness = happiness;
        previousCleanliness = cleanliness;
        previousPower = power;
        previousMoney = money;
    }

    public void IncreaseCleanliness(int amount)
    {
        cleanliness += amount;
        consecutiveCleanlinessIncreases++;
        consecutivePowerIncreases = 0;
        consecutiveHappinessIncreases = 0;
        consecutiveMoneyIncreases = 0;
    }

    public void IncreasePower(int amount)
    {
        power += amount;
        consecutivePowerIncreases++;
        consecutiveCleanlinessIncreases = 0;
        consecutiveHappinessIncreases = 0;
        consecutiveMoneyIncreases = 0;
    }

    public void IncreaseHappiness(int amount)
    {
        happiness += amount;
        consecutiveHappinessIncreases++;
        consecutiveCleanlinessIncreases = 0;
        consecutivePowerIncreases = 0;
        consecutiveMoneyIncreases = 0;
    }

    public void IncreaseMoney(int amount)
    {
        money += amount;
        consecutiveMoneyIncreases++;
        consecutiveCleanlinessIncreases = 0;
        consecutivePowerIncreases = 0;
        consecutiveHappinessIncreases = 0;
    }

    public void IncreaseCleanlinessAndHappiness(int cleanlinessAmount, int happinessAmount)
    {
        cleanliness += cleanlinessAmount;
        happiness += happinessAmount;
        consecutiveCleanlinessIncreases++;
        consecutiveHappinessIncreases++;
        consecutivePowerIncreases = 0;
        consecutiveMoneyIncreases = 0;
    }

    public void ResetCleanlinessIncreases() => consecutiveCleanlinessIncreases = 0;
    public void ResetPowerIncreases() => consecutivePowerIncreases = 0;
    public void ResetHappinessIncreases() => consecutiveHappinessIncreases = 0;
    public void ResetMoneyIncreases() => consecutiveMoneyIncreases = 0;

    public List<Card> lowCards = new List<Card>
    {
        new Card("Park İnşası", new Effect(2, 1, 0, -2), new Effect(-1, -1, 0, 1)),
        new Card("Çöp Toplama Kampanyası", new Effect(0, 2, 0, -1), new Effect(-1, -2, -1, 1)),
        new Card("Bisiklet Yolu Yapımı", new Effect(2, 1, 0, -2), new Effect(-1, -1, 0, 1)),
        new Card("Vergi Artışı", new Effect(0, 0, 0, 2), new Effect(0, 0, 0, -1)),
        new Card("Şehir Işıklandırması", new Effect(1, 0, -2, -2), new Effect(-2, 0, 1, 1)),
        new Card("Sokak Sanatları Festivali", new Effect(3, -1, 0, -2), new Effect(-2, 0, 0, 1)),
        new Card("Şehir Temizlik Günü", new Effect(1, 3, 0, -1), new Effect(0, -2, -1, 0)),
        new Card("Su Faturasında İndirim", new Effect(2, 0, 0, -2), new Effect(0, 0, 1, 1)),
        new Card("Yerel Spor Takımına Destek", new Effect(3, 0, 0, -2), new Effect(-2, 0, 0, 1)),
        new Card("Sokak Hayvanları Barınağı Yapımı", new Effect(2, 1, 0, -2), new Effect(-1, -1, 0, 1)),
        new Card("Halk Toplantısı Düzenleme", new Effect(2, 0, 1, 0), new Effect(-2, 0, -1, 0)),
        new Card("Mahalle Kütüphanesi Açılması", new Effect(2, 0, -1, -2), new Effect(-1, 0, 1, 0)),
        new Card("Çocuk Parkı Yenileme", new Effect(3, 0, 0, -2), new Effect(-1, 0, 0, 1)),
        new Card("Elektrik Patlaması", new Effect(-3, 0, -3, -3), new Effect(-3, 0, -3, -3))
    };

    public List<Card> midCards = new List<Card>
    {
        new Card("Kanalizasyon Sisteminin Yenilenmesi", new Effect(0, 4, -2, -3), new Effect(0, -3, 0, 2)),
        new Card("Belediye Sarayı Restorasyonu", new Effect(2, 0, 0, -4), new Effect(-2, 0, 1, 0)),
        new Card("Belediye Otobüsü Alımı", new Effect(3, 0, -2, -3), new Effect(-2, 0, 0, 1)),
        new Card("Yeni İmar Planı", new Effect(2, 2, 0, -3), new Effect(0, -2, 0, 1)),
        new Card("Fabrika İzni Verme", new Effect(0, -3, -2, 4), new Effect(0, 1, 1, -2)),
        new Card("Belediye Spor Kompleksi Yapımı", new Effect(3, 0, -2, -4), new Effect(-2, 0, 0, 2)),
        new Card("Toplu Taşıma Geliştirilmesi", new Effect(3, 0, -2, -4), new Effect(-2, 0, 0, 1)),
        new Card("Atık Geri Dönüşüm Tesisi Yapımı", new Effect(0, 4, -2, -3), new Effect(0, -2, 0, 1)),
        new Card("Yeni Park Alanı İlanı", new Effect(4, 2, 0, -4), new Effect(-3, 0, 0, 1)),
        new Card("Sel Felaketi", new Effect(-5, -5, 0, -5), new Effect(-5, -5, 0, -5))
    };

    public List<Card> bonusCards = new List<Card>
    {
        new Card("Yerel Festivale Katılım", new Effect(3, 0, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("Bağış Kampanyası", new Effect(0, 0, 0, 4), new Effect(0, 0, 0, 0)),
        new Card("Gönüllü Temizlik Ekibi", new Effect(0, 4, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("Çocuklara Ücretsiz Eğitim", new Effect(3, 0, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("Şehirde Yenilenebilir Enerji Projesi", new Effect(0, 3, 2, 3), new Effect(0, 0, 0, 0)),
        new Card("Doğal Kaynak Bağışı", new Effect(3, 2, 0, 0), new Effect(0, 0, 0, 0))
    };

    public List<Card> buildingCards = new List<Card>()
    {
        new Card("Tiyatro", new Effect(1, 0, 0, 0, 5), new Effect(0, 0, 0, 0)),
        new Card("Pazar", new Effect(0, 0, 0, 1, 5), new Effect(0, 0, 0, 0)),
        new Card("Temizlik", new Effect(0, 1, 0, 0, 5), new Effect(0, 0, 0, 0)),
        new Card("Temizlik2", new Effect(0, 1, 0, 0, 5), new Effect(0, 0, 0, 0)),
        new Card("Temizlik3", new Effect(0, 1, 0, 0, 5), new Effect(0, 0, 0, 0)),
        new Card("Güç", new Effect(0, 0, 1, 0, 5), new Effect(0, 0, 0, 0)),
        new Card("Güç2", new Effect(0, 0, 1, 0, 5), new Effect(0, 0, 0, 0)),
        new Card("Güç3", new Effect(0, 0, 1, 0, 5), new Effect(0, 0, 0, 0)),
        new Card("mutluluk2", new Effect(1, 0, 0, 0, 5), new Effect(0, 0, 0, 0)),
        new Card("para2", new Effect(0, 0, 0, 1, 5), new Effect(0, 0, 0, 0)),
    };


    public List<Card> PeopleCards = new List<Card>()
    {

    };
}

[System.Serializable]
public class Card
{
    public string name;
    public Sprite cardPhoto;
    public Effect approveEffect;
    public Effect rejectEffect;

    public Card(string name, Effect approveEffect, Effect rejectEffect)
    {
        this.name = name;
        this.approveEffect = approveEffect;
        this.rejectEffect = rejectEffect;
    }
}

[System.Serializable]
public class Effect
{
    public int happinessChange;
    public int cleanlinessChange;
    public int powerChange;
    public int moneyChange;
    public int duration; 

    public Effect(int happinessChange, int cleanlinessChange, int powerChange, int moneyChange, int duration = 0)
    {
        this.happinessChange = happinessChange;
        this.cleanlinessChange = cleanlinessChange;
        this.powerChange = powerChange;
        this.moneyChange = moneyChange;
        this.duration = duration; 
    }
}