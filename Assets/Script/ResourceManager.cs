using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    // Kaynak de�erleri
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


    // Kart etkilerini g�ncellemek i�in bir metod
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
        effectDescriptions[2] = effect.powerChange != 0 ? $"G��: {effect.powerChange}" : "";
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
        new Card("Park �n�as�", new Effect(2, 1, 0, -2), new Effect(-1, -1, 0, 1)),
        new Card("��p Toplama Kampanyas�", new Effect(0, 2, 0, -1), new Effect(-1, -2, -1, 1)),
        new Card("Bisiklet Yolu Yap�m�", new Effect(2, 1, 0, -2), new Effect(-1, -1, 0, 1)),
        new Card("Vergi Art���", new Effect(0, 0, 0, 2), new Effect(0, 0, 0, -1)),
        new Card("�ehir I��kland�rmas�", new Effect(1, 0, -2, -2), new Effect(-2, 0, 1, 1)),
        new Card("Sokak Sanatlar� Festivali", new Effect(3, -1, 0, -2), new Effect(-2, 0, 0, 1)),
        new Card("�ehir Temizlik G�n�", new Effect(1, 3, 0, -1), new Effect(0, -2, -1, 0)),
        new Card("Su Faturas�nda �ndirim", new Effect(2, 0, 0, -2), new Effect(0, 0, 1, 1)),
        new Card("Yerel Spor Tak�m�na Destek", new Effect(3, 0, 0, -2), new Effect(-2, 0, 0, 1)),
        new Card("Sokak Hayvanlar� Bar�na�� Yap�m�", new Effect(2, 1, 0, -2), new Effect(-1, -1, 0, 1)),
        new Card("Halk Toplant�s� D�zenleme", new Effect(2, 0, 1, 0), new Effect(-2, 0, -1, 0)),
        new Card("Mahalle K�t�phanesi A��lmas�", new Effect(2, 0, -1, -2), new Effect(-1, 0, 1, 0)),
        new Card("�ocuk Park� Yenileme", new Effect(3, 0, 0, -2), new Effect(-1, 0, 0, 1)),
        new Card("Elektrik Patlamas�", new Effect(-3, 0, -3, -3), new Effect(-3, 0, -3, -3))
    };

    public List<Card> midCards = new List<Card>
    {
        new Card("Kanalizasyon Sisteminin Yenilenmesi", new Effect(0, 4, -2, -3), new Effect(0, -3, 0, 2)),
        new Card("Belediye Saray� Restorasyonu", new Effect(2, 0, 0, -4), new Effect(-2, 0, 1, 0)),
        new Card("Belediye Otob�s� Al�m�", new Effect(3, 0, -2, -3), new Effect(-2, 0, 0, 1)),
        new Card("Yeni �mar Plan�", new Effect(2, 2, 0, -3), new Effect(0, -2, 0, 1)),
        new Card("Fabrika �zni Verme", new Effect(0, -3, -2, 4), new Effect(0, 1, 1, -2)),
        new Card("Belediye Spor Kompleksi Yap�m�", new Effect(3, 0, -2, -4), new Effect(-2, 0, 0, 2)),
        new Card("Toplu Ta��ma Geli�tirilmesi", new Effect(3, 0, -2, -4), new Effect(-2, 0, 0, 1)),
        new Card("At�k Geri D�n���m Tesisi Yap�m�", new Effect(0, 4, -2, -3), new Effect(0, -2, 0, 1)),
        new Card("Yeni Park Alan� �lan�", new Effect(4, 2, 0, -4), new Effect(-3, 0, 0, 1)),
        new Card("Sel Felaketi", new Effect(-5, -5, 0, -5), new Effect(-5, -5, 0, -5))
    };

    // Bonus kartlar
    public List<Card> bonusCards = new List<Card>
    {
        new Card("Yerel Festivale Kat�l�m", new Effect(3, 0, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("Ba��� Kampanyas�", new Effect(0, 0, 0, 4), new Effect(0, 0, 0, 0)),
        new Card("G�n�ll� Temizlik Ekibi", new Effect(0, 4, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("�ocuklara �cretsiz E�itim", new Effect(3, 0, 0, 0), new Effect(0, 0, 0, 0)),
        new Card("�ehirde Yenilenebilir Enerji Projesi", new Effect(0, 3, 2, 3), new Effect(0, 0, 0, 0)),
        new Card("Do�al Kaynak Ba����", new Effect(3, 2, 0, 0), new Effect(0, 0, 0, 0))
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
    public string name; // Kart ad�
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
