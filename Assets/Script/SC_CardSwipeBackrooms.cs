using UnityEngine;
using UnityEngine.EventSystems;

public class SC_CardSwipeBackrooms : MonoBehaviour
{
    [Header("Diğer Scriptler")] 
    public SC_Backrooms ScriptBackrooms;
    public SC_ResourceManager ScriptResourceManager;

    [Header("Kart Kaydırma Elementleri")] 
    private Vector3 initialPosition;
    [SerializeField] private int targetDistance = 450;
    [SerializeField] private bool isDragging = false;
    void Start()
    {
        ScriptResourceManager = GameObject.FindWithTag("GameUI").GetComponent<SC_ResourceManager>();
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
            
            
            ScriptResourceManager.happiness += ScriptBackrooms.PositiveEffectsInt[3];
            ScriptResourceManager.cleanliness += ScriptBackrooms.PositiveEffectsInt[2];
            ScriptResourceManager.power += ScriptBackrooms.PositiveEffectsInt[1];
            ScriptResourceManager.money += ScriptBackrooms.PositiveEffectsInt[0];

            
            ScriptBackrooms.PositiveEffectsInt[0] = 0;
            ScriptBackrooms.PositiveEffectsInt[1] = 0;
            ScriptBackrooms.PositiveEffectsInt[2] = 0;
            ScriptBackrooms.PositiveEffectsInt[3] = 0;
            
            ScriptBackrooms.NegativeEffectsInt[0] = 0;
            ScriptBackrooms.NegativeEffectsInt[1] = 0;
            ScriptBackrooms.NegativeEffectsInt[2] = 0;
            ScriptBackrooms.NegativeEffectsInt[3] = 0;
            
            
            ScriptResourceManager.UpdateSliders();
            ScriptResourceManager.SaveGame();
            
            //ScriptResourceManager.SaveGame();
            //ScriptBackrooms.SaveTrust();
            Debug.Log("kabul edildi");
        }
        else if (distanceX <= -targetDistance) // Solda bırakıldıysa
        {
            ScriptBackrooms.denemeGuncellemeBool = true;
            ScriptBackrooms.denemeGuncellemeInt = 2;

            ScriptResourceManager.happiness += ScriptBackrooms.NegativeEffectsInt[3];
            ScriptResourceManager.cleanliness += ScriptBackrooms.NegativeEffectsInt[2];
            ScriptResourceManager.power += ScriptBackrooms.NegativeEffectsInt[1];
            ScriptResourceManager.money += ScriptBackrooms.NegativeEffectsInt[0];

            ScriptBackrooms.PositiveEffectsInt[0] = 0;
            ScriptBackrooms.PositiveEffectsInt[1] = 0;
            ScriptBackrooms.PositiveEffectsInt[2] = 0;
            ScriptBackrooms.PositiveEffectsInt[3] = 0;
            
            ScriptBackrooms.NegativeEffectsInt[0] = 0;
            ScriptBackrooms.NegativeEffectsInt[1] = 0;
            ScriptBackrooms.NegativeEffectsInt[2] = 0;
            ScriptBackrooms.NegativeEffectsInt[3] = 0;

            
            ScriptResourceManager.UpdateSliders();
            ScriptResourceManager.SaveGame();
            
            
            //ScriptResourceManager.SaveGame();
            //ScriptBackrooms.SaveTrust();
            Debug.Log("reddedildi");
        }

        // Obje başlangıç pozisyonuna geri döner
        transform.position = initialPosition;
    }
}
