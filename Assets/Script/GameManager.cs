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
    public Text gameOverPanelTextdesc;
    public Image gameOverImage;

    public Sprite happinessMinSprite;
    public Sprite happinessMaxSprite;
    public Sprite powerMinSprite;
    public Sprite powerMaxSprite;
    public Sprite cleanlinessMinSprite;
    public Sprite cleanlinessMaxSprite;
    public Sprite moneyMinSprite;
    public Sprite moneyMaxSprite;
    public Sprite trustMinSprite;

    public GameObject extraButtonsPanel;
    public Button showExtraButtonsButton;
    private SC_Backrooms ScriptBackrooms;
    private void Awake()
    {
        gameOverPanel.SetActive(false);
        extraButtonsPanel.SetActive(false);
        ScriptBackrooms = GameObject.FindWithTag("GameUI").GetComponent<SC_Backrooms>();
    }

    private void Start()
    {
        showExtraButtonsButton.onClick.AddListener(ShowExtraButtonsPanel);
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

            // Check trust value and end game if it's below 0
            if (ScriptBackrooms.trustSlider.value <= 0)
            {
                gameOverPanel.SetActive(true);
                gameOverPanelText.text = "Halkın Güveni Tükendi";
                gameOverPanelTextdesc.text = "Halk size olan güvenini tamamen kaybetti.";
                gameOverImage.sprite = trustMinSprite; 
                Time.timeScale = 0;
                return;
            }

            if (resourceManager.happiness <= 0)
            {
                gameOverPanel.SetActive(true);
                gameOverPanelText.text = "Halkın Terk Edişi";
                gameOverPanelTextdesc.text = "Halk şehri terk etmeye başladı.";
                gameOverImage.sprite = happinessMinSprite;
                Time.timeScale = 0;
            }
            else if (resourceManager.happiness >= presidentManager.GetMaxHappiness())
            {
                gameOverPanel.SetActive(true);
                gameOverPanelText.text = "Halkın Terk Edişi";
                gameOverPanelTextdesc.text = "Halk işlerini bırakıp sürekli kutlama yapmaya başlar. Şehir ekonomik çöküşe girer.";
                gameOverImage.sprite = happinessMaxSprite;
                Time.timeScale = 0;
            }

            if (resourceManager.power <= 0)
            {
                gameOverPanel.SetActive(true);
                gameOverPanelText.text = "Elektrik Kıtlığı";
                gameOverPanelTextdesc.text = "Enerji tükenir, şehir karanlığa gömülür ve kaosa sürüklenir.";
                gameOverImage.sprite = powerMinSprite;
                Time.timeScale = 0;
            }
            else if (resourceManager.power >= presidentManager.GetMaxPower())
            {
                gameOverPanel.SetActive(true);
                gameOverPanelText.text = "Teknolojik Ütopya";
                gameOverPanelTextdesc.text = "Teknolojiye bağımlılık sosyal bağları yok eder, şehir izole olur.";
                gameOverImage.sprite = powerMaxSprite;
                Time.timeScale = 0;
            }

            if (resourceManager.cleanliness <= 0)
            {
                gameOverPanel.SetActive(true);
                gameOverPanelText.text = "Çevre Felaketi";
                gameOverPanelTextdesc.text = "Şehirdeki çevre kirliliği halk sağlığını tehdit ediyor!";
                gameOverImage.sprite = cleanlinessMinSprite;
                Time.timeScale = 0;
            }
            else if (resourceManager.cleanliness >= presidentManager.GetMaxCleanliness())
            {
                gameOverPanel.SetActive(true);
                gameOverPanelText.text = "Hijyen Paradoksu";
                gameOverPanelTextdesc.text = "Aşırı kimyasal kullanımı çocuklarda bağışıklık sisteminin gelişememesine sebep oldu!";
                gameOverImage.sprite = cleanlinessMaxSprite;
                Time.timeScale = 0;
            }

            if (resourceManager.money <= 0)
            {
                gameOverPanel.SetActive(true);
                gameOverPanelText.text = "Halk İsyanı";
                gameOverPanelTextdesc.text = "Bütçe yetersizliği halkın size olan güvenini kaybetmenize yol açtı.";
                gameOverImage.sprite = moneyMinSprite;
                Time.timeScale = 0;
            }
            else if (resourceManager.money >= presidentManager.GetMaxMoney())
            {
                gameOverPanel.SetActive(true);
                gameOverPanelText.text = "Finansal Çöküş";
                gameOverPanelTextdesc.text = "Fazla birikim, sosyal hizmetlerde çöküşe yol açtı.";
                gameOverImage.sprite = moneyMaxSprite;
                Time.timeScale = 0;
            }
        }
    }

    private void ShowExtraButtonsPanel()
    {
        extraButtonsPanel.SetActive(true);
    }

    public void restartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        PlayerPrefs.DeleteAll();
    }
}