using UnityEngine;
using UnityEngine.EventSystems;

public class SC_CardSwipeBackrooms : MonoBehaviour
{
    [Header("Diğer Scriptler")] public SC_Backrooms ScriptBackrooms;
    
    [Header("Kart Kaydırma Elementleri")] 
    private Vector3 initialPosition;
    [SerializeField] private int targetDistance = 450;
    [SerializeField] private bool isDragging = false;
    void Start()
    {
        initialPosition = transform.position; // Objenin başlangıç pozisyonunu kaydet
    }
    public void OnPointerDown(BaseEventData data) // Event trigger componentine bağlı
    {
        isDragging = true; // Sürükleme işlemini başlat
    }

    public void OnDrag(BaseEventData data)
    {
        if (isDragging)
        {
            // PointerEventData ile fare pozisyonunu al
            PointerEventData pointerData = data as PointerEventData;
            if (pointerData != null)
            {
                // Fare pozisyonunu dünya pozisyonuna doğrudan uygula
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 0; // Z eksenini sabit tut
                transform.position = mousePosition;
            }
        }
    }
    public void OnPointerUp(BaseEventData data)
    {
        // Sürükleme işlemini bitir
        isDragging = false;

        // Objenin bırakıldığı pozisyon ile başlangıç pozisyonunu karşılaştır
        float distanceX = transform.position.x - initialPosition.x;

        if (distanceX >= targetDistance) // Sağda bırakıldıysa
        {
            ScriptBackrooms.denemeGuncellemeBool = true;
            ScriptBackrooms.denemeGuncellemeInt = 1;
            
            Debug.Log("kabul edildi");
        }
        else if (distanceX <= -targetDistance) // Solda bırakıldıysa
        {
            ScriptBackrooms.denemeGuncellemeBool = true;
            ScriptBackrooms.denemeGuncellemeInt = 2;
            
            Debug.Log("reddedildi");
        }

        // Obje başlangıç pozisyonuna geri döner
        transform.position = initialPosition;
    }
}
