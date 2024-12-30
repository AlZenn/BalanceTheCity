using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    // Kaynak deðerleri
    public DiceRoller diceRoller2;

    public int happiness = 10;
    public int cleanliness = 10;
    public int power = 10;
    public int money = 10;


    public Text[] cardEffectsPositiveTexts;
    public Text[] cardEffectsNegativeTexts;
    public Text[] gameEffectsTexts;

    private void Start()
    {
        gameEffectsTexts[0].text = happiness.ToString();
        gameEffectsTexts[1].text = cleanliness.ToString();
        gameEffectsTexts[2].text = power.ToString();
        gameEffectsTexts[3].text = money.ToString();
    }
    private void Update()
    {
    }


    // Kart etkilerini güncellemek için bir metod
    public void DisplayCardEffects(Card card)
    {
        UpdateEffectTexts(cardEffectsPositiveTexts, card.approveEffect);
        UpdateEffectTexts(cardEffectsNegativeTexts, card.rejectEffect);
    }

    private void UpdateEffectTexts(Text[] texts, Effect effect)
    {
        string[] effectDescriptions = new string[4];

        effectDescriptions[0] = effect.happinessChange != 0 ? $"Mutluluk: {effect.happinessChange}" : "";
        effectDescriptions[1] = effect.cleanlinessChange != 0 ? $"Temizlik: {effect.cleanlinessChange}" : "";
        effectDescriptions[2] = effect.powerChange != 0 ? $"Güç: {effect.powerChange}" : "";
        effectDescriptions[3] = effect.moneyChange != 0 ? $"Para: {effect.moneyChange}" : "";

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = i < effectDescriptions.Length && !string.IsNullOrEmpty(effectDescriptions[i])
                ? effectDescriptions[i]
                : "";
        }
    }

    // Low kartlar
    public List<Card> lowCards = new List<Card>
    {
        new Card("Park Ýnþasý", new Effect(2, 1, 0, -2), new Effect(-1, -1, 0, 1)),
        new Card("Çöp Toplama Kampanyasý", new Effect(0, 2, 0, -1), new Effect(-1, -2, -1, 1)),
        new Card("Bisiklet Yolu Yapýmý", new Effect(2, 1, 0, -2), new Effect(-1, -1, 0, 1)),
        new Card("Vergi Artýþý", new Effect(0, 0, 0, 2), new Effect(0, 0, 0, -1)),
        new Card("Þehir Iþýklandýrmasý", new Effect(1, 0, -2, -2), new Effect(-2, 0, 1, 1)),
        new Card("Sokak Sanatlarý Festivali", new Effect(3, -1, 0, -2), new Effect(-2, 0, 0, 1)),
        new Card("Þehir Temizlik Günü", new Effect(1, 3, 0, -1), new Effect(0, -2, -1, 0)),
        new Card("Su Faturasýnda Ýndirim", new Effect(2, 0, 0, -2), new Effect(0, 0, 1, 1)),
        new Card("Yerel Spor Takýmýna Destek", new Effect(3, 0, 0, -2), new Effect(-2, 0, 0, 1)),
        new Card("Sokak Hayvanlarý Barýnaðý Yapýmý", new Effect(2, 1, 0, -2), new Effect(-1, -1, 0, 1)),
        new Card("Halk Toplantýsý Düzenleme", new Effect(2, 0, 1, 0), new Effect(-2, 0, -1, 0)),
        new Card("Mahalle Kütüphanesi Açýlmasý", new Effect(2, 0, -1, -2), new Effect(-1, 0, 1, 0)),
        new Card("Çocuk Parký Yenileme", new Effect(3, 0, 0, -2), new Effect(-1, 0, 0, 1)),
        new Card("Elektrik Patlamasý", new Effect(-3, 0, -3, -3), new Effect(-3, 0, -3, -3))
    };

    public List<Card> midCards = new List<Card>
    {
        new Card("Kanalizasyon Sisteminin Yenilenmesi", new Effect(0, 4, -2, -3), new Effect(0, -3, 0, 2)),
        new Card("Belediye Sarayý Restorasyonu", new Effect(2, 0, 0, -4), new Effect(-2, 0, 1, 0)),
        new Card("Belediye Otobüsü Alýmý", new Effect(3, 0, -2, -3), new Effect(-2, 0, 0, 1)),
        new Card("Yeni Ýmar Planý", new Effect(2, 2, 0, -3), new Effect(0, -2, 0, 1)),
        new Card("Fabrika Ýzni Verme", new Effect(0, -3, -2, 4), new Effect(0, 1, 1, -2)),
        new Card("Belediye Spor Kompleksi Yapýmý", new Effect(3, 0, -2, -4), new Effect(-2, 0, 0, 2)),
        new Card("Toplu Taþýma Geliþtirilmesi", new Effect(3, 0, -2, -4), new Effect(-2, 0, 0, 1)),
        new Card("Atýk Geri Dönüþüm Tesisi Yapýmý", new Effect(0, 4, -2, -3), new Effect(0, -2, 0, 1)),
        new Card("Yeni Park Alaný Ýlaný", new Effect(4, 2, 0, -4), new Effect(-3, 0, 0, 1)),
        new Card("Sel Felaketi", new Effect(-5, -5, 0, -5), new Effect(-5, -5, 0, -5))
    };

    // Bonus kartlar
    public List<Card> bonusCards = new List<Card>
    {
        new Card("Yerel Festivale Katýlým", new Effect(3, 0, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("Baðýþ Kampanyasý", new Effect(0, 0, 0, 4), new Effect(0, 0, 0, 0)),
        new Card("Gönüllü Temizlik Ekibi", new Effect(0, 4, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("Çocuklara Ücretsiz Eðitim", new Effect(3, 0, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("Þehirde Yenilenebilir Enerji Projesi", new Effect(0, 3, 2, 3), new Effect(0, 0, 0, 0)),
        new Card("Doðal Kaynak Baðýþý", new Effect(3, 2, 0, 0), new Effect(0, 0, 0, 0))
    };
    public void UpdateResources(Effect effect)
    {
        happiness = Mathf.Clamp(happiness + effect.happinessChange, 0, 20);
        cleanliness = Mathf.Clamp(cleanliness + effect.cleanlinessChange, 0, 20);
        power = Mathf.Clamp(power + effect.powerChange, 0, 20);
        money = Mathf.Clamp(money + effect.moneyChange, 0, 20);
    }

}

// Kart bilgisi
[System.Serializable]
public class Card
{
    public string name; // Kart adý
    public Sprite cardPhoto;
    public Effect approveEffect; // Onay etkisi
    public Effect rejectEffect; // Reddetme etkisi

    public Card(string name, Effect approveEffect, Effect rejectEffect)
    {
        this.name = name;
        this.approveEffect = approveEffect;
        this.rejectEffect = rejectEffect;
    }
}

// Kaynaklara olan etkiler
[System.Serializable]
public class Effect
{
    public int happinessChange;
    public int cleanlinessChange;
    public int powerChange;
    public int moneyChange;

    public Effect(int happinessChange, int cleanlinessChange, int powerChange, int moneyChange)
    {
        this.happinessChange = happinessChange;
        this.cleanlinessChange = cleanlinessChange;
        this.powerChange = powerChange;
        this.moneyChange = moneyChange;
    }
}
