using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSwipe : MonoBehaviour
{
    private Vector3 initialPosition; // �lk pozisyonu saklamak i�in
    private bool isDragging = false; // S�r�kleme durumunu kontrol etmek i�in


    public Text deneme; // Deneme Text bile�eni
    public GameObject[] cards;


    void Start()
    {
        // Objenin ba�lang�� pozisyonunu kaydet
        initialPosition = transform.position;
    }

    // Pointer Down Event'i ile ba�lanacak metot
    public void OnPointerDown(BaseEventData data)
    {
        // S�r�kleme i�lemini ba�lat
        isDragging = true;
    }

    // Drag Event'i ile ba�lanacak metot
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

    // Pointer Up Event'i ile ba�lanacak metot
    public void OnPointerUp(BaseEventData data)
    {
        // S�r�kleme i�lemini bitir
        isDragging = false;

        // Objenin b�rak�ld��� pozisyon ile ba�lang�� pozisyonunu kar��la�t�r
        float distanceX = transform.position.x - initialPosition.x;

        if (distanceX >= 400) // Sa�da b�rak�ld�ysa
        {
            deneme.text = "true";
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].SetActive(false);
            }
        }
        else if (distanceX <= -400) // Solda b�rak�ld�ysa
        {
            deneme.text = "false";
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].SetActive(false);
            }
        }

        // Obje ba�lang�� pozisyonuna geri d�ner
        transform.position = initialPosition;
    }
}