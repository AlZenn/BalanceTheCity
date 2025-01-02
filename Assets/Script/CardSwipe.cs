// Kart Kaydýrma Scripti
// Açýklama: Aktif kartlar mouse pozisyonunda hareket eder ve çýktý verir.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSwipe : MonoBehaviour
{
    #region Variables

    private Vector3 initialPosition;// Ýlk pozisyonu saklamak için
    private bool isDragging = false;// Sürükleme durumunu kontrol etmek için

    [Header("Ekran Alaný")]
    [SerializeField] private int targetDistance = 450;
    [SerializeField] private Text deneme;
    public GameObject VisibleCard;
    [SerializeField] private Button diceButton;

    public DiceRoller diceRoller;
    #endregion

    #region Start Items
    void Start()
    {
        initialPosition = transform.position; // Objenin baþlangýç pozisyonunu kaydet
    }
    #endregion

    #region Pointer Events
    // event trigger componentine baðlý
    public void OnPointerDown(BaseEventData data) // Event trigger componentine baðlý
    {
        isDragging = true; // Sürükleme iþlemini baþlat
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
            diceButton.interactable = true; // bug olmamasý adýna eklendi

            diceRoller.denemeBool = true; // dice roller scripti ile baðlantý
            diceRoller.denemeInt = 1; 
        }
        else if (distanceX <= -targetDistance) // Solda býrakýldýysa
        {
            deneme.text = "Reddedildi";
            //VisibleCard.SetActive(false);
            diceButton.interactable = true; // bug olmamasý adýna eklendi

            diceRoller.denemeBool = true; // dice roller scripti ile baðlantý
            diceRoller.denemeInt = 2;
        }

        // Obje baþlangýç pozisyonuna geri döner
        transform.position = initialPosition;
    }
    #endregion
}