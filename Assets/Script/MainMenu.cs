using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string nextLevel = "game2";
    
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
    }

    private void StartGame()
    {
        
        SceneManager.LoadScene(nextLevel);
    }

    private void TogglePanel(GameObject panel, bool isActive)
    {
        panel.SetActive(isActive);
    }

    private void QuitGame()
    {
        
        Application.Quit();
    }
}
