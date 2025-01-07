using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SC_ResourceManager : MonoBehaviour
{
    [Header("Temel Unsurlar")]
    public int happiness = 10;
    public int cleanliness = 10;
    public int power = 10;
    public int money = 10;
    
    [Header("Kaynak Text Objeleri")]
    public Text[] cardEffectsPositiveTexts;
    public Text[] cardEffectsNegativeTexts;
    public Text[] gameEffectsTexts;
    public Slider trustSlider;
    
    private void Start()
    {
        gameEffectsTexts[0].text = happiness.ToString();
        gameEffectsTexts[1].text = cleanliness.ToString();
        gameEffectsTexts[2].text = power.ToString();
        gameEffectsTexts[3].text = money.ToString();
        trustSlider.value = 1f;
    }

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

    // Bonus kartlar
    public List<Card> bonusCards = new List<Card>
    {
        new Card("Yerel Festivale Katılım", new Effect(3, 0, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("Bağış Kampanyası", new Effect(0, 0, 0, 4), new Effect(0, 0, 0, 0)),
        new Card("Gönüllü Temizlik Ekibi", new Effect(0, 4, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("Çocuklara Ücretsiz Eğitim", new Effect(3, 0, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("Şehirde Yenilenebilir Enerji Projesi", new Effect(0, 3, 2, 3), new Effect(0, 0, 0, 0)),
        new Card("Doğal Kaynak Bağışı", new Effect(3, 2, 0, 0), new Effect(0, 0, 0, 0))
    };

}

// Kart bilgisi
[System.Serializable]
public class Card
{
    public string name; // Kart adı
    public Sprite cardPhoto; // kart Sprite'ı
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
