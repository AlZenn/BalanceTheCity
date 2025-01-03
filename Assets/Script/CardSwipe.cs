// Kart Kayd�rma Scripti
// A��klama: Aktif kartlar mouse pozisyonunda hareket eder ve ��kt� verir.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSwipe : MonoBehaviour
{
    #region Variables

    private Vector3 initialPosition;// �lk pozisyonu saklamak i�in
    private bool isDragging = false;// S�r�kleme durumunu kontrol etmek i�in

    [Header("Ekran Alan�")]
    [SerializeField] private int targetDistance = 450;
    [SerializeField] private Text deneme;
    public GameObject VisibleCard;
    [SerializeField] private Button diceButton;

    public DiceRoller diceRoller;
    #endregion

    #region Start Items
    void Start()
    {
        initialPosition = transform.position; // Objenin ba�lang�� pozisyonunu kaydet
    }
    #endregion

    #region Pointer Events
    // event trigger componentine ba�l�
    public void OnPointerDown(BaseEventData data) // Event trigger componentine ba�l�
    {
        isDragging = true; // S�r�kleme i�lemini ba�lat
    }

    public void OnDrag(BaseEventData data)
    {
        if (isDragging)
        {
            // PointerEventData ile fare pozisyonunu al
            PointerEventData pointerData = data as PointerEventData;
            if (pointerData != null)
            {
                // Fare pozisyonunu d�nya pozisyonuna do�rudan uygula
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 0; // Z eksenini sabit tut
                transform.position = mousePosition;
            }
        }
    }
    public void OnPointerUp(BaseEventData data)
    {
        // S�r�kleme i�lemini bitir
        isDragging = false;

        // Objenin b�rak�ld��� pozisyon ile ba�lang�� pozisyonunu kar��la�t�r
        float distanceX = transform.position.x - initialPosition.x;

        if (distanceX >= targetDistance) // Sa�da b�rak�ld�ysa
        {
            deneme.text = "Kabul Edildi";
            //VisibleCard.SetActive(false);
            diceButton.interactable = true; // bug olmamas� ad�na eklendi

            diceRoller.denemeBool = true; // dice roller scripti ile ba�lant�
            diceRoller.denemeInt = 1; 
        }
        else if (distanceX <= -targetDistance) // Solda b�rak�ld�ysa
        {
            deneme.text = "Reddedildi";
            //VisibleCard.SetActive(false);
            diceButton.interactable = true; // bug olmamas� ad�na eklendi

            diceRoller.denemeBool = true; // dice roller scripti ile ba�lant�
            diceRoller.denemeInt = 2;
        }

        // Obje ba�lang�� pozisyonuna geri d�ner
        transform.position = initialPosition;
    }
    #endregion
}