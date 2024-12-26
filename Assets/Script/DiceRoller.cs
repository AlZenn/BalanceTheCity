// Dice Roller Script
// A��klama: Zar atar, sonucu UI'de g�sterir ve ilgili kartlar� aktif eder.

using UnityEngine;
using UnityEngine.UI;


public class DiceRoller : MonoBehaviour
{
    #region Fields

    [Tooltip("Zar sonucunu g�sterecek Text bile�eni.")]
    public Text resultText; // Zar sonucu yazd�r�lacak Text bile�eni

    [Tooltip("D���k kart i�in GameObject.")]
    public GameObject lowCard; // D���k kart GameObject

    [Tooltip("Orta kart i�in GameObject.")]
    public GameObject mediumCard; // Orta kart GameObject

    [Tooltip("Bonus kart i�in GameObject.")]
    public GameObject bonusCard; // Bonus kart GameObject

    #endregion

    #region Dice_Roll

    public void RollDice()
    {
        int diceResult = Random.Range(1, 7); // 1 ile 6 aras�nda bir de�er �retir
        string resultMessage = ""; // Mesaj i�in de�i�ken

        // T�m kartlar� deaktif et
        lowCard.SetActive(false);
        mediumCard.SetActive(false);
        bonusCard.SetActive(false);

        // Zar sonucuna g�re i�lem yap
        if (diceResult >= 1 && diceResult <= 3)
        {
            resultMessage = "D���k";
            lowCard.SetActive(true); // D���k kart� aktif et
        }
        else if (diceResult == 4 || diceResult == 5)
        {
            resultMessage = "Orta";
            mediumCard.SetActive(true); // Orta kart� aktif et
        }
        else if (diceResult == 6)
        {
            resultMessage = "Bonus!";
            bonusCard.SetActive(true); // Bonus kart� aktif et
        }

        // Sonucu Text bile�enine yazd�r
        resultText.text = $"Zar Sonucu: {diceResult} - {resultMessage}"; // resultText.text = "Zar Sonucu: " + diceResult + " - " + resultMessage;
        
    }

    #endregion
}