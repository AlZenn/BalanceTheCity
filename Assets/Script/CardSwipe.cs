using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSwipe : MonoBehaviour
{
    private Vector3 initialPosition; // Ýlk pozisyonu saklamak için
    private bool isDragging = false; // Sürükleme durumunu kontrol etmek için


    public Text deneme; // Deneme Text bileþeni
    public GameObject[] cards;


    void Start()
    {
        // Objenin baþlangýç pozisyonunu kaydet
        initialPosition = transform.position;
    }

    // Pointer Down Event'i ile baðlanacak metot
    public void OnPointerDown(BaseEventData data)
    {
        // Sürükleme iþlemini baþlat
        isDragging = true;
    }

    // Drag Event'i ile baðlanacak metot
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

    // Pointer Up Event'i ile baðlanacak metot
    public void OnPointerUp(BaseEventData data)
    {
        // Sürükleme iþlemini bitir
        isDragging = false;

        // Objenin býrakýldýðý pozisyon ile baþlangýç pozisyonunu karþýlaþtýr
        float distanceX = transform.position.x - initialPosition.x;

        if (distanceX >= 400) // Saðda býrakýldýysa
        {
            deneme.text = "true";
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].SetActive(false);
            }
        }
        else if (distanceX <= -400) // Solda býrakýldýysa
        {
            deneme.text = "false";
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].SetActive(false);
            }
        }

        // Obje baþlangýç pozisyonuna geri döner
        transform.position = initialPosition;
    }
}