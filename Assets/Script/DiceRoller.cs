using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Text resultText; // Zar sonucu yazd�r�lacak Text bile�eni
    public GameObject lowCard; // D���k kart GameObject
    public GameObject mediumCard; // Orta kart GameObject
    public GameObject bonusCard; // Bonus kart GameObject

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
        resultText.text = $"Zar Sonucu: {diceResult} - {resultMessage}";
    }
}
