using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public PresidentManager presidentManager;
    public SC_ResourceManager resourceManager;

    public GameObject gameOverPanel;
    public Text gameOverPanelText;    

    private void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (presidentManager.presidentSelectionPanel.activeSelf == false)
        {
            if (resourceManager == null || presidentManager == null)
            {
                return;
            }

            if (resourceManager.happiness <= 0 || resourceManager.happiness >= presidentManager.GetMaxHappiness())
            {
                gameOverPanel.SetActive(true); // deneme
                gameOverPanelText.text = "mutluluk gg";
                Time.timeScale = 0;
            }        
            if (resourceManager.power <= 0 || resourceManager.power >= presidentManager.GetMaxPower())
            {
                gameOverPanel.SetActive(true); // deneme
                gameOverPanelText.text = "güç gg";
                Time.timeScale = 0;
            }
            if (resourceManager.cleanliness <= 0 || resourceManager.cleanliness >= presidentManager.GetMaxCleanliness()) // resourceManager.cleanliness >= presidentManager.GetMaxCleanliness()
            {
                gameOverPanel.SetActive(true); // deneme
                gameOverPanelText.text = "temizlik gg";
               Time.timeScale = 0;
            }
            if (resourceManager.money <= 0 ||  resourceManager.money >= presidentManager.GetMaxMoney())
            {
                gameOverPanel.SetActive(true); // deneme
                gameOverPanelText.text = "para gg";
                Time.timeScale = 0;
            }
        }
    }
    public void restartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        PlayerPrefs.DeleteAll();
    }

}