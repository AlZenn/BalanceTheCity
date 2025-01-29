using UnityEngine;
using UnityEngine.UI;

public class AchievementsMenu : MonoBehaviour
{
    [SerializeField] private Image[] achievementImages; // Baþarýmlar için Image objeleri (6 adet)
    private string[] achievementKeys = { "Happiness", "Cleanliness", "Power", "Money", "DayWin", "Trust" };

    private void Start()
    {
        UpdateAchievements();
    }

    private void UpdateAchievements()
    {
        for (int i = 0; i < achievementKeys.Length; i++)
        {
            if (PlayerPrefs.GetInt(achievementKeys[i], 0) == 1) // Baþarým yapýldýysa
            {
                achievementImages[i].color = Color.white; // Beyaz renkte göster
            }
            else
            {
                achievementImages[i].color = new Color(0.2f, 0.2f, 0.2f); // Siyah renkte göster
            }
        }
    }
}
