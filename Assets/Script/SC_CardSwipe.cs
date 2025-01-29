using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SC_CardSwipe : MonoBehaviour
{
    [Header("Diğer Scriptler")]
    [SerializeField]
    private SC_Dice ScriptDice;
    public SC_ResourceManager ScriptResourceManager;

    [Header("Kart Kaydırma Elementleri")]
    private Vector3 initialPosition;
    [SerializeField] private int targetDistance = 450;
    [SerializeField] private bool isDragging = false;

    [SerializeField] private AudioClip dragSound; // Sürükleme sesi
    private AudioSource audioSource;
    //private bool hasPlayedSound = false;

    void Start()
    {
        ScriptResourceManager = GameObject.FindWithTag("GameUI").GetComponent<SC_ResourceManager>();
        ScriptDice = GameObject.FindWithTag("GameUI").GetComponent<SC_Dice>();

        initialPosition = cardTransform.position; // Objenin başlangıç pozisyonunu kaydet
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = dragSound;
        audioSource.loop = false; // Sesin sürekli çalmamasını sağla
        audioSource.playOnAwake = false; // Kart ekrana geldiğinde ses çalınmamasını sağla
    }

    public void OnPointerDown(BaseEventData data) // Event trigger componentine bağlı
    {
        isDragging = true; // Sürükleme işlemini başlat
        //hasPlayedSound = false; // Ses bayrağını sıfırla

        // Sesi çal
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            //hasPlayedSound = true; // Ses çalındı bayrağını ayarla
        }
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

    public Transform cardTransform;
    public void OnPointerUp(BaseEventData data)
    {
        // Sürükleme işlemini bitir
        isDragging = false;

        // Objenin bırakıldığı pozisyon ile başlangıç pozisyonunu karşılaştır
        float distanceX = transform.position.x - cardTransform.position.x;
        float distanceY = transform.position.y - cardTransform.position.y;

        if (!ScriptDice.ChaosCard && ScriptDice.isSwitched == false)
        {
            if (distanceX >= targetDistance) // Sağda bırakıldıysa
            {
                ScriptDice.isSwitched = true;
                ScriptDice.denemeBool = true;
                ScriptDice.denemeInt = 1;
                Debug.Log("sağa kaydırıldı");
            }
            else if (distanceX <= -targetDistance) // Solda bırakıldıysa
            {
                ScriptDice.isSwitched = true;
                ScriptDice.denemeBool = true;
                ScriptDice.denemeInt = 2;
                Debug.Log("sola kaydırıldı");
            }
            
        }else if (ScriptDice.ChaosCard && distanceX >= targetDistance || distanceX <= -targetDistance)
        {
            ScriptResourceManager.happiness = Random.Range(5, 30);
            ScriptResourceManager.cleanliness = Random.Range(5, 30);
            ScriptResourceManager.money = Random.Range(5, 30);
            ScriptResourceManager.power = Random.Range(5, 30);
                
            ScriptResourceManager.UpdateSliders();
            ScriptDice.Card_Object.SetActive(false);
            ScriptResourceManager.dayTextUpdate();
            ScriptDice.Button_Dice.interactable = true;
            ScriptDice.ChaosCard = false;
            ScriptDice.isSwitched = false;
        }

        
        
        
        if (!ScriptDice.ChaosCard && ScriptDice.isSwitched == true)
        {
            if (distanceY >= (targetDistance - 100))
            {
                ScriptDice.Card_Object.SetActive(false);
                ScriptDice.Button_Dice.interactable = true;
                ScriptResourceManager.dayTextUpdate();
                ScriptDice.OnSwipeUp();
                //ScriptDice.ResetCardPosition();
                
                Debug.Log("Yukarı Kaydırıldı");
            }
            else if (distanceY <= -(targetDistance - 100))
            {
                ScriptDice.Card_Object.SetActive(false);
               // ScriptDice.changeNegative(0.5f); // Değerlerin yarısı
                ScriptDice.Button_Dice.interactable = true;
                ScriptResourceManager.dayTextUpdate();
                //ScriptDice.ResetCardPosition();
                
                
                
                ScriptDice.OnSwipeDown();
                Debug.Log("aşağı kaydırıldı");
            }
        }

        // Obje başlangıç pozisyonuna geri döner
        transform.position = initialPosition;
    }
}