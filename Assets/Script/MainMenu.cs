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

    private bool isSoundOn;
    
    [Header("Sosyal Medya Linkleri")]
    [SerializeField] private string sosyal_medya_ayse = "https://alzennn.itch.io/sehri-yonet";
    [SerializeField] private string sosyal_medya_alzenn = "https://alzennn.itch.io/sehri-yonet";
    [SerializeField] private string sosyal_medya_oguzhan = "https://alzennn.itch.io/sehri-yonet";
    [SerializeField] private string sosyal_medya_simge = "https://alzennn.itch.io/sehri-yonet";
    
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

        // Önceden kaydedilmiş ses ayarını yükle
        isSoundOn = PlayerPrefs.GetInt("SoundState", 1) == 1;
        backgroundMusic.mute = !isSoundOn;
        
        // Başlangıçta doğru sprite'ı ayarla
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

        // Seçimi kaydet
        PlayerPrefs.SetInt("SoundState", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateSoundButtonSprite();
    }

    private void UpdateSoundButtonSprite()
    {
        soundToggleButton.GetComponent<Image>().sprite = isSoundOn ? soundOnSprite : soundOffSprite;
    }

    public void button_social_alzenn()
    {
        if (sosyal_medya_alzenn != null)
        {
            Application.OpenURL(sosyal_medya_alzenn);
        }
    }
    public void button_social_ayse()
    {
        if (sosyal_medya_ayse != null)
        {
            Application.OpenURL(sosyal_medya_ayse);
        }
    }
    public void button_social_simge()
    {
        if (sosyal_medya_simge != null)
        {
            Application.OpenURL(sosyal_medya_simge);
        }
    }
    public void button_social_oguzhan()
    {
        if (sosyal_medya_oguzhan != null)
        {
            Application.OpenURL(sosyal_medya_oguzhan);
        }
    }
}
