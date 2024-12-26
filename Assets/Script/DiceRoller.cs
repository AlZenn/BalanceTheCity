// Dice Roller Script
// Açýklama: Zar atar, sonucu UI'de gösterir ve ilgili kartlarý aktif eder.

using UnityEngine;
using UnityEngine.UI;


public class DiceRoller : MonoBehaviour
{
    #region Fields

    [Tooltip("Zar sonucunu gösterecek Text bileþeni.")]
    public Text resultText; // Zar sonucu yazdýrýlacak Text bileþeni

    [Tooltip("Düþük kart için GameObject.")]
    public GameObject lowCard; // Düþük kart GameObject

    [Tooltip("Orta kart için GameObject.")]
    public GameObject mediumCard; // Orta kart GameObject

    [Tooltip("Bonus kart için GameObject.")]
    public GameObject bonusCard; // Bonus kart GameObject

    #endregion

    #region Dice_Roll

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
        resultText.text = $"Zar Sonucu: {diceResult} - {resultMessage}"; // resultText.text = "Zar Sonucu: " + diceResult + " - " + resultMessage;
        
    }

    #endregion
}