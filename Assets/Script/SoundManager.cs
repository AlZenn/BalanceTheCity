using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundMusic; // Arka plan müziði için AudioSource
    public Button soundToggleButton; // Ses kontrol butonu
    public Sprite soundOnSprite; // Ses açýkken kullanýlacak sprite
    public Sprite soundOffSprite; // Ses kapalýyken kullanýlacak sprite

    private bool isSoundOn = true; // Sesin açýk olup olmadýðýný kontrol eden deðiþken

    private void Start()
    {
        // Ses kontrol butonuna dinleyici ekle
        soundToggleButton.onClick.AddListener(ToggleSound);
        // Baþlangýçta doðru sprite'ý ayarla
        UpdateSoundButtonSprite();
    }

    // Sesi aç/kapa fonksiyonu
    private void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        backgroundMusic.mute = !isSoundOn;
        UpdateSoundButtonSprite();
    }

    // Ses butonunun sprite'ýný güncelleme fonksiyonu
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