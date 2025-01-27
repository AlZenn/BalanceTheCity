using UnityEngine;
using UnityEngine.UI;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject[] pages;  // Her bir sayfa GameObject olarak tutulacak
    public Button nextButton;   // "�leri" butonu
    public Button backButton;   // "Geri" butonu

    private int currentPage = 0;  // Ge�erli sayfa index'i

    void Start()
    {
        // �lk sayfay� g�ster
        ShowPage(currentPage);

        // Butonlara t�klama olaylar�n� ba�la
        nextButton.onClick.AddListener(NextPage);
        backButton.onClick.AddListener(BackPage);

        // E�er sadece ilk sayfada geri butonu aktif olmal�, di�erlerinde pasif
        backButton.interactable = false;
    }

    void ShowPage(int pageIndex)
    {
        // T�m sayfalar� gizle
        foreach (var page in pages)
        {
            page.SetActive(false);
        }

        // Ge�erli sayfay� g�ster
        pages[pageIndex].SetActive(true);

        // Geri butonunun aktifli�ini kontrol et
        backButton.interactable = pageIndex > 0;

        // �leri butonunun i�levini kontrol et
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