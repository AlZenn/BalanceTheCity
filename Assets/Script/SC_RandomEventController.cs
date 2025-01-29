using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SC_RandomEventController : MonoBehaviour
{
    public SC_ResourceManager ScriptResourceManager;
    public GameObject RandomEventCard;
    private SpriteRenderer cardSpriteRenderer;

    private bool isActiveEvent = false;
    private int eventDelayDay = 0;

    private int taskDuration;
    private int taskEndTime;
    public string taskType;
    private bool taskActive;
    private int taskGoal;
    private int taskProgress;

    public GameObject TaskPanel;
    public Image TaskImage;
    public Text TaskDescriptionText;
    public Text TaskRemainingTimeText;
    public Button CloseTaskButton;
    public Button ReopenTaskButton; // Görev panelini tekrar açma butonu

    public Sprite TaskSprite1; // 1. görev için sprite
    public Sprite TaskSprite2; // 2. görev için sprite
    public Sprite TaskSprite3; // 3. görev için sprite

    private void Awake()
    {
        RandomEventCard = GameObject.FindWithTag("RandomEventCard");
        RandomEventCard.SetActive(false);

        TaskPanel.SetActive(false);
        CloseTaskButton.onClick.AddListener(CloseTaskPanel);
        ReopenTaskButton.onClick.AddListener(ReopenTaskPanel);
        ReopenTaskButton.gameObject.SetActive(false); // Başlangıçta gizli

        cardSpriteRenderer = RandomEventCard.GetComponent<SpriteRenderer>();
    }

    public void RandomEventTrigger()
    {
        int randomNumber = Random.Range(1, 11);  // 1 ile 10 arasında bir sayı
        Debug.Log("Random Event sayısı: " + randomNumber);
        if (randomNumber == 1) // %10 şans var
        {
            isActiveEvent = true;
            eventDelayDay = 5;
        }
    }

    public void ShowTaskPanel(string description, int duration, string type, Sprite taskSprite, int goal)
    {
        TaskPanel.SetActive(true);
        taskDuration = duration;
        taskEndTime = ScriptResourceManager.day + duration;
        taskType = type;
        taskGoal = goal;
        taskProgress = 0;
        taskActive = true;

        TaskDescriptionText.text = $"{taskProgress}/{taskGoal} {description}";
        TaskImage.sprite = taskSprite;
        UpdateTaskRemainingTime();
        ReopenTaskButton.gameObject.SetActive(false); // Görev paneli açıldığında tekrar açma butonunu gizle
    }

    public void CloseTaskPanel()
    {
        TaskPanel.SetActive(false);
        ReopenTaskButton.gameObject.SetActive(true); // Panel kapanınca tekrar açma butonunu göster
    }

    public void ReopenTaskPanel()
    {
        TaskPanel.SetActive(true);
        UpdateTaskRemainingTime();
    }

    public void UpdateTaskProgress()
    {
        if (taskActive)
        {
            int progress = 0;

            switch (taskType)
            {
                case "Happiness":
                    progress = ScriptResourceManager.happiness - ScriptResourceManager.previousHappiness;
                    ScriptResourceManager.previousHappiness = ScriptResourceManager.happiness;
                    break;
                case "Cleanliness":
                    progress = ScriptResourceManager.cleanliness - ScriptResourceManager.previousCleanliness;
                    ScriptResourceManager.previousCleanliness = ScriptResourceManager.cleanliness;
                    break;
                case "Power":
                    progress = ScriptResourceManager.power - ScriptResourceManager.previousPower;
                    ScriptResourceManager.previousPower = ScriptResourceManager.power;
                    break;
                case "Money":
                    progress = ScriptResourceManager.money - ScriptResourceManager.previousMoney;
                    ScriptResourceManager.previousMoney = ScriptResourceManager.money;
                    break;
            }

            if (progress > 0)
            {
                taskProgress += progress;
                Debug.Log($"Task Progress Updated: {taskProgress}/{taskGoal} ({taskType})");
                TaskDescriptionText.text = $"{taskProgress}/{taskGoal} {taskType}";
                if (taskProgress >= taskGoal)
                {
                    taskActive = false;
                    ReopenTaskButton.gameObject.SetActive(false);
                    TaskPanel.SetActive(false);
                    Debug.Log("Task Completed");
                }
            }
        }
    }

    public void UpdateTaskRemainingTime()
    {
        if (taskActive)
        {
            int remainingTime = taskEndTime - ScriptResourceManager.day;
            TaskRemainingTimeText.text = "" + remainingTime + " gün";

            if (remainingTime <= 0)
            {
                ApplyTaskPenalty();
                taskActive = false;
                ReopenTaskButton.gameObject.SetActive(false); // Görev süresi dolunca butonu gizle
            }
        }
    }

    private void ApplyTaskPenalty()
    {
        switch (taskType)
        {
            case "Happiness":
                ScriptResourceManager.happiness -= 10;
                break;
            case "Cleanliness":
                ScriptResourceManager.cleanliness -= 5;
                ScriptResourceManager.money -= 5;
                break;
            case "Money":
                
                ScriptResourceManager.happiness -= 5;
                ScriptResourceManager.power -= 5;
                break;
        }

        ScriptResourceManager.UpdateSliders();
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
        ScriptResourceManager.gameEffectsTexts[3].text = ScriptResourceManager.money.ToString(); // para
    }
}