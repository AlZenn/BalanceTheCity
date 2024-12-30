// Kart Kaydýrma Scripti
// Açýklama: Aktif kartlar mouse pozisyonunda hareket eder ve çýktý verir.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSwipe : MonoBehaviour
{
    #region Variables


    [Tooltip("Ýlk pozisyonu saklamak için kullanýlýr.")] // Ýlk pozisyonu saklamak için
    private Vector3 initialPosition;

    [Tooltip("Sürükleme iþleminin aktif olup olmadýðýný kontrol eder.")] // Sürükleme durumunu kontrol etmek için
    private bool isDragging = false;

    [Tooltip("Kartýn hedef mesafesi. Bu mesafeyi geçerse iþlem gerçekleþir.")]
    [Header("Ekran Alaný")]
    [SerializeField] private int targetDistance = 450;

    [Tooltip("Deneme için kullanýlan Text bileþeni.")]
    [SerializeField] private Text deneme;

    [Tooltip("Kartlarýn bulunduðu GameObject dizisi.")]
    public GameObject VisibleCard;

    [SerializeField] private Button diceButton;
    public DiceRoller diceRoller;
    #endregion

    #region Start Items
    void Start()
    {
        // Objenin baþlangýç pozisyonunu kaydet
        initialPosition = transform.position;
    }
    #endregion

    #region Pointer Events
    // event trigger componentine baðlý
    public void OnPointerDown(BaseEventData data) // Event trigger componentine baðlý
    {
        // Sürükleme iþlemini baþlat
        isDragging = true;
    }

    public void OnDrag(BaseEventData data)
    {
        if (isDragging)
        {
            // PointerEventData ile fare pozisyonunu al
            PointerEventData pointerData = data as PointerEventData;
            if (pointerData != null)
            {
                // Fare pozisyonunu dünya pozisyonuna doðrudan uygula
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 0; // Z eksenini sabit tut
                transform.position = mousePosition;
            }
        }
    }
    public void OnPointerUp(BaseEventData data)
    {
        // Sürükleme iþlemini bitir
        isDragging = false;

        // Objenin býrakýldýðý pozisyon ile baþlangýç pozisyonunu karþýlaþtýr
        float distanceX = transform.position.x - initialPosition.x;

        if (distanceX >= targetDistance) // Saðda býrakýldýysa
        {
            deneme.text = "Kabul Edildi";
            //VisibleCard.SetActive(false);
            diceButton.interactable = true;

            diceRoller.denemeBool = true;
            diceRoller.denemeInt = 1;
        }
        else if (distanceX <= -targetDistance) // Solda býrakýldýysa
        {
            deneme.text = "Reddedildi";
            //VisibleCard.SetActive(false);
            diceButton.interactable = true;

            diceRoller.denemeBool = true;
            diceRoller.denemeInt = 2;
        }

        // Obje baþlangýç pozisyonuna geri döner
        transform.position = initialPosition;
    }
    #endregion
}