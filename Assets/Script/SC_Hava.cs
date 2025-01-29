using UnityEngine;
using UnityEngine.UI;

public class SC_Hava : MonoBehaviour
{
    public GameObject havaDurumuPaneli;
    public Image havaDurumuFotograf; // Resmi göstermek için bir Image bileşeni
    public Text havadurumuBaslik;
    public Text havaDurumuAciklama; // Hava durumu açıklamasını gösterecek yeni Text bileşeni
    public GameObject iconGameObject; // Hava durumu aktifken gösterilecek ikon

    public Sprite[] HavaDurumuFotograflari = new Sprite[5]; // Hava durumu görselleri
    public string[] HavaDurumuBasliklari = { "Güneşli", "Bulutlu", "Sisli", "Yağmurlu", "Karlı" };
    public string[] HavaDurumuAciklamalari = {
        "Hava güneşli, insanlar neşeli! Halkın mutluluğu artıyor.",
        "Bulutlar gökyüzünü sardı, para azalıyor!",
        "Sisli hava ile birlikte şehirde güç düşüşü yaşandı!",
        "Yağmurla birlikte kirlilik arttı, şehrin temizliği düşüyor!",
        "Karın gelişi, insanların mutluluğunu arttırdı!"
    }; // Hava durumu açıklamaları

    private int dayCooldown = 0; // Hava durumu aktiflik süresi (5 gün)
    private bool isWeatherActive = false; // Hava durumu aktif mi?

    [SerializeField] private SC_ResourceManager ScriptResourceManager;

    private void Awake()
    {
        havaDurumuPaneli.SetActive(false);
        iconGameObject.SetActive(false);
        ScriptResourceManager = GameObject.FindWithTag("GameUI").GetComponent<SC_ResourceManager>();
    }

    private int havaDurumuSansi;
    public void WeatherControl()
    {
        havaDurumuSansi = Random.Range(1, 6); // 1-5 arasında değer oluşturur
        Debug.Log("Hava Durumu Şansı: " + havaDurumuSansi);

        if (!isWeatherActive && havaDurumuSansi == 3)
        {
            int havaDurumuSecimi = Random.Range(0, 5); // 0-4 arasında değer oluşturur
            Debug.Log("Seçilen Hava Durumu: " + HavaDurumuBasliklari[havaDurumuSecimi]);

            // Başlık ve görsel güncellemesi
            havadurumuBaslik.text = HavaDurumuBasliklari[havaDurumuSecimi];
            havaDurumuFotograf.sprite = HavaDurumuFotograflari[havaDurumuSecimi];

            // Açıklama güncellemesi
            havaDurumuAciklama.text = HavaDurumuAciklamalari[havaDurumuSecimi]; // Yeni açıklama ekleniyor

            // Paneli aktif hale getir
            havaDurumuPaneli.SetActive(true);

            // İkonu göster ve sayaç başlat
            iconGameObject.SetActive(true);
            dayCooldown = 5 + PlayerPrefs.GetInt("Day"); // 5 günlük aktiflik süresi
            isWeatherActive = true;
        }
        if (isWeatherActive == true && dayCooldown != PlayerPrefs.GetInt("Day"))
        {
            switch (havadurumuBaslik.text)
            {
                case "Güneşli":
                    GunesliEtkileri();
                    break;
                case "Bulutlu":
                    BulutluEtkileri();
                    break;
                case "Sisli":
                    SisliEtkileri();
                    break;
                case "Yağmurlu":
                    YagmurluEtkileri();
                    break;
                case "Karlı":
                    KarliEtkileri();
                    break;
            }
        }
        else if (dayCooldown == PlayerPrefs.GetInt("Day"))
        {
            isWeatherActive = false;
            WeatherControl();
        }

    }

    private void GunesliEtkileri()
    {
        if (dayCooldown != PlayerPrefs.GetInt("Day"))
        {
            ScriptResourceManager.happiness++;
            Debug.Log("0: Güneşli hava etkileri aktif!");
            ScriptResourceManager.UpdateSliders();
        }
    }

    private void BulutluEtkileri()
    {
        if (dayCooldown != PlayerPrefs.GetInt("Day"))
        {
            ScriptResourceManager.money--;
            Debug.Log("1: Bulutlu hava etkileri aktif!");
            ScriptResourceManager.UpdateSliders();
        }
    }

    private void SisliEtkileri()
    {
        if (dayCooldown != PlayerPrefs.GetInt("Day"))
        {
            ScriptResourceManager.power--;
            Debug.Log("2: Sisli hava etkileri aktif!");
            ScriptResourceManager.UpdateSliders();
        }
    }

    private void YagmurluEtkileri()
    {
        if (dayCooldown != PlayerPrefs.GetInt("Day"))
        {
            ScriptResourceManager.cleanliness--;
            Debug.Log("3: Yağmurlu hava etkileri aktif!");
            ScriptResourceManager.UpdateSliders();
        }
    }

    private void KarliEtkileri()
    {
        if (dayCooldown != PlayerPrefs.GetInt("Day"))
        {
            ScriptResourceManager.happiness++;
            Debug.Log("4: Karlı hava etkileri aktif!");
            ScriptResourceManager.UpdateSliders();
        }
    }
}
