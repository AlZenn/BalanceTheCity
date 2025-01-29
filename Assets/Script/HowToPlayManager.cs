using UnityEngine;
using UnityEngine.UI;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject[] pages;  // Her bir sayfa GameObject olarak tutulacak
    public Button nextButton;   // "Ýleri" butonu
    public Button backButton;   // "Geri" butonu

    private int currentPage = 0;  // Geçerli sayfa index'i

    void Start()
    {
        // Ýlk sayfayý göster
        ShowPage(currentPage);

        // Butonlara týklama olaylarýný baðla
        nextButton.onClick.AddListener(NextPage);
        backButton.onClick.AddListener(BackPage);

        // Eðer sadece ilk sayfada geri butonu aktif olmalý, diðerlerinde pasif
        backButton.interactable = false;
    }

    void ShowPage(int pageIndex)
    {
        // Tüm sayfalarý gizle
        foreach (var page in pages)
        {
            page.SetActive(false);
        }

        // Geçerli sayfayý göster
        pages[pageIndex].SetActive(true);

        // Geri butonunun aktifliðini kontrol et
        backButton.interactable = pageIndex > 0;

        // Ýleri butonunun iþlevini kontrol et
        nextButton.interactable = pageIndex < pages.Length - 1;
    }

    void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }

    void BackPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }
}