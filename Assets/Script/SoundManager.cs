using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundMusic; // Arka plan m�zi�i i�in AudioSource
    public Button soundToggleButton; // Ses kontrol butonu
    public Sprite soundOnSprite; // Ses a��kken kullan�lacak sprite
    public Sprite soundOffSprite; // Ses kapal�yken kullan�lacak sprite

    private bool isSoundOn = true; // Sesin a��k olup olmad���n� kontrol eden de�i�ken

    private void Start()
    {
        // Ses kontrol butonuna dinleyici ekle
        soundToggleButton.onClick.AddListener(ToggleSound);
        // Ba�lang��ta do�ru sprite'� ayarla
        UpdateSoundButtonSprite();
    }

    // Sesi a�/kapa fonksiyonu
    private void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        backgroundMusic.mute = !isSoundOn;
        UpdateSoundButtonSprite();
    }

    // Ses butonunun sprite'�n� g�ncelleme fonksiyonu
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