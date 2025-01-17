using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI; // list için gerekli
using Random = UnityEngine.Random; // random için gerekli

public class SC_RandomEventController : MonoBehaviour
{
    public SC_ResourceManager ScriptResourceManager;
    public GameObject RandomEventCard;
    public List<string> weatherConditions = new List<string> { "kar", "yağmur", "güneş", "sis", "bulutlu" };
    public Sprite[] weatherSprites = new Sprite[5];
    private SpriteRenderer cardSpriteRenderer; // IMAGE KOYULACAK OBJE ATANACAK


    string selectedWeather;
    private bool isActiveEvent = false;
    private int eventDelayDay = 0;
    private void Awake()
    {
        RandomEventCard = GameObject.FindWithTag("RandomEventCard");
        RandomEventCard.SetActive(false);



    }

    public void RandomEventTrigger()
    {
        int randomNumber = Random.Range(1, 11);  // 1 ile 10 arasında bir sayı
        Debug.Log("Random Event sayısı: " + randomNumber);
        if (randomNumber == 1) // %10 şans var
        {
            int randomIndex = Random.Range(0, weatherConditions.Count); // Listeyi karıştır
            selectedWeather = weatherConditions[randomIndex];
            activeEvent(selectedWeather);
            Debug.Log("event delay day :" + eventDelayDay);
            Debug.Log("mevcut yeni event :" + selectedWeather);


            isActiveEvent = true;
            eventDelayDay = 5;
        }
        else
        {
            if (selectedWeather != null || eventDelayDay > 0)
            {
                activeEvent(selectedWeather);
                eventDelayDay--;
                Debug.Log("event delay day :" + eventDelayDay);
                Debug.Log("mevcut event :" + selectedWeather);

            }
        }
    }

    public void activeEvent(string selectedEvent)
    {
        if (isActiveEvent == true && selectedEvent != null)
        {
            // 5 gün sonrası için durumu uygula
            if (selectedEvent == weatherConditions[0]) // kar ise
            {
                cardSpriteRenderer.sprite = weatherSprites[0];
                applyActiveEventHapiness();
            }
            else if (selectedEvent == weatherConditions[1]) // yağmur ise
            {
                cardSpriteRenderer.sprite = weatherSprites[1];
                applyActiveEventClean();
            }
            else if (selectedEvent == weatherConditions[2]) // güneş ise
            {
                cardSpriteRenderer.sprite = weatherSprites[2];
                applyActiveEventMoney();
            }
            else if (selectedEvent == weatherConditions[3]) // sis ise
            {
                cardSpriteRenderer.sprite = weatherSprites[3];
                applyActiveEventPower();
            }
            else if (selectedEvent == weatherConditions[4]) // bulutlu ise
            {
                cardSpriteRenderer.sprite = weatherSprites[4];
                applyActiveEventMoney();
                // Bulutlu hava durumu için başka bir işlem eklenebilir
            }
        }
    }

    // Mutluluk etkileme fonksiyonu
    public void applyActiveEventHapiness()
    {
        RandomEventCard.SetActive(true);
        Debug.Log("mutluluk azaldı");
        ScriptResourceManager.happiness--;
        ScriptResourceManager.gameEffectsTexts[0].text = ScriptResourceManager.happiness.ToString(); // mutluluk
    }

    // Temizlik etkileme fonksiyonu
    public void applyActiveEventClean()
    {
        RandomEventCard.SetActive(true);
        Debug.Log("temizlik azaldı");
        ScriptResourceManager.cleanliness--;
        ScriptResourceManager.gameEffectsTexts[1].text = ScriptResourceManager.cleanliness.ToString(); // temizlik
    }
    public void applyActiveEventPower()
    {
        RandomEventCard.SetActive(true);
        Debug.Log("güç azaldı");
        ScriptResourceManager.power--;
        ScriptResourceManager.gameEffectsTexts[2].text = ScriptResourceManager.power.ToString(); // güç
    }
    public void applyActiveEventMoney()
    {
        RandomEventCard.SetActive(true);
        Debug.Log("para azaldı");
        ScriptResourceManager.money--;
        ScriptResourceManager.gameEffectsTexts[3].text = ScriptResourceManager.money.ToString(); //para
    }
}
