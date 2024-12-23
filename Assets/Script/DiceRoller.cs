using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Text resultText; // Zar sonucu yazdýrýlacak Text bileþeni
    public GameObject lowCard; // Düþük kart GameObject
    public GameObject mediumCard; // Orta kart GameObject
    public GameObject bonusCard; // Bonus kart GameObject

    public void RollDice()
    {
        int diceResult = Random.Range(1, 7); // 1 ile 6 arasýnda bir deðer üretir
        string resultMessage = ""; // Mesaj için deðiþken

        // Tüm kartlarý deaktif et
        lowCard.SetActive(false);
        mediumCard.SetActive(false);
        bonusCard.SetActive(false);

        // Zar sonucuna göre iþlem yap
        if (diceResult >= 1 && diceResult <= 3)
        {
            resultMessage = "Düþük";
            lowCard.SetActive(true); // Düþük kartý aktif et
        }
        else if (diceResult == 4 || diceResult == 5)
        {
            resultMessage = "Orta";
            mediumCard.SetActive(true); // Orta kartý aktif et
        }
        else if (diceResult == 6)
        {
            resultMessage = "Bonus!";
            bonusCard.SetActive(true); // Bonus kartý aktif et
        }

        // Sonucu Text bileþenine yazdýr
        resultText.text = $"Zar Sonucu: {diceResult} - {resultMessage}";
    }
}
