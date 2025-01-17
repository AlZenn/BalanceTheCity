using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string nextLevel = "cutscene";

    [Header("Panels")]
    public GameObject howToPlayPanel;
    public GameObject creditsPanel;
    public GameObject settingsPanel;

    [Header("Buttons")]
    public Button startButton;
    public Button howToPlayButton;
    public Button closeHowToPlayButton;
    public Button creditsButton;
    public Button closeCreditsButton;
    public Button settingsButton;
    public Button closeSettingsButton;
    public Button quitButton;
    public Button soundToggleButton;

    [Header("Sound Settings")]
    public AudioSource backgroundMusic;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private bool isSoundOn = true;

    private void Start()
    {
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        settingsPanel.SetActive(false);

        startButton.onClick.AddListener(StartGame);
        howToPlayButton.onClick.AddListener(() => TogglePanel(howToPlayPanel, true));
        closeHowToPlayButton.onClick.AddListener(() => TogglePanel(howToPlayPanel, false));
        creditsButton.onClick.AddListener(() => TogglePanel(creditsPanel, true));
        closeCreditsButton.onClick.AddListener(() => TogglePanel(creditsPanel, false));
        settingsButton.onClick.AddListener(() => TogglePanel(settingsPanel, true));
        closeSettingsButton.onClick.AddListener(() => TogglePanel(settingsPanel, false));
        quitButton.onClick.AddListener(QuitGame);
        soundToggleButton.onClick.AddListener(ToggleSound);

        // Set the initial sound button sprite
        UpdateSoundButtonSprite();
    }

    private void StartGame()
    {
        if (PlayerPrefs.HasKey("Baskan"))
        {
            SceneManager.LoadScene("game2");
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
        
    }

    private void TogglePanel(GameObject panel, bool isActive)
    {
        panel.SetActive(isActive);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        backgroundMusic.mute = !isSoundOn;
        UpdateSoundButtonSprite();
    }

    private void UpdateSoundButtonSprite()
    {
        if (isSoundOn)
        {
            soundToggleButton.GetComponent<Image>().sprite = soundOnSprite;
        }
        else
        {
            soundToggleButton.GetComponent<Image>().sprite = soundOffSprite;
        }
    }
}