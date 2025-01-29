using UnityEngine;

public class SC_PhoneShakeDedector : MonoBehaviour
{
    public SC_Dice ScriptDice;
    public float shakeThreshold = 2.0f; // Sallama hareketi algılama eşiği
    private Vector3 lastAcceleration;
    private Vector3 currentAcceleration;
    private Vector3 accelerationDelta;
    void Start()
    {
        // İlk ivme değerini al
        lastAcceleration = Input.acceleration;
        currentAcceleration = Input.acceleration;
    }
    void Update()
    {
        // Şu anki ivme değerini güncelle
        currentAcceleration = Input.acceleration;

        // İvme değişimini hesapla
        accelerationDelta = currentAcceleration - lastAcceleration;

        // Eğer ivme değişimi eşik değerinden büyükse sallama algılandı
        if (accelerationDelta.sqrMagnitude >= shakeThreshold * shakeThreshold)
        {
            ScriptDice.RollDice();
        }

        // Son ivme değerini güncelle
        lastAcceleration = currentAcceleration;
    }

    
    
}
