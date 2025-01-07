using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SC_CardSwipe : MonoBehaviour
{
    [Header("Diğer Scriptler")] [SerializeField]
    private SC_Dice ScriptDice;
    
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
            ScriptDice.Card_Object.SetActive(false);
            ScriptDice.Button_Dice.interactable = true;
            ScriptDice.denemeBool = true;
            ScriptDice.denemeInt = 1;
            
            Debug.Log("kabul edildi");
        }
        else if (distanceX <= -targetDistance) // Solda bırakıldıysa
        {
            ScriptDice.Card_Object.SetActive(false);
            ScriptDice.Button_Dice.interactable = true;
            ScriptDice.denemeBool = true; // dice roller scripti ile bağlantı
            ScriptDice.denemeInt = 2;
            Debug.Log("reddedildi");
        }

        // Obje başlangıç pozisyonuna geri döner
        transform.position = initialPosition;
    }
}
