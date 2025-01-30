using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundMusic; // Arka plan müziği için AudioSource
    public Button soundToggleButton; // Ses kontrol butonu
    public Sprite soundOnSprite; // Ses açıkkken kullanılacak sprite
    public Sprite soundOffSprite; // Ses kapalıyken kullanılacak sprite

    private bool isSoundOn; // Sesin açık olup olmadığını kontrol eden değişken

    private void Start()
    {
        // Önceden kaydedilmiş ses durumunu yükle (Varsayılan olarak açık)
        isSoundOn = PlayerPrefs.GetInt("SoundState", 1) == 1;

        // AudioSource'un mute değerini ayarla
        backgroundMusic.mute = !isSoundOn;

        // Ses kontrol butonuna dinleyici ekle
        soundToggleButton.onClick.AddListener(ToggleSound);

        // Başlangıçta doğru sprite'ı ayarla
        UpdateSoundButtonSprite();
    }

    // Sesi aç/kapa fonksiyonu
    private void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        backgroundMusic.mute = !isSoundOn;

        // Seçimi PlayerPrefs'e kaydet
        PlayerPrefs.SetInt("SoundState", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();

        // Ses butonunun sprite'ını güncelle
        UpdateSoundButtonSprite();
    }

    // Ses butonunun sprite'ını güncelleme fonksiyonu
    private void UpdateSoundButtonSprite()
    {
        soundToggleButton.GetComponent<Image>().sprite = isSoundOn ? soundOnSprite : soundOffSprite;
    }
}