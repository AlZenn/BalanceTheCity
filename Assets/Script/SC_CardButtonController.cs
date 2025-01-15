using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SC_CardButtonController : MonoBehaviour
{
    public SC_Dice ScriptDice;
    public GameObject StatsPanel;
    public Text rightText;
    public Text leftText;
    public float timer = 0f;

    private void Awake()
    {
        StatsPanel.SetActive(false);
        rightText.text = "0";
        leftText.text = "0";
    }

    public void buttonPara()
    {
        rightText.text = ScriptDice.Positifdegerler[3].ToString();
        leftText.text = ScriptDice.Negatifdegerler[3].ToString();
        ShowStatsPanel();
    }
    public void buttonMutluluk()
    {
        rightText.text = ScriptDice.Positifdegerler[0].ToString();
        leftText.text = ScriptDice.Negatifdegerler[0].ToString();
        ShowStatsPanel();
    }
    public void buttonGuc()
    {
        rightText.text = ScriptDice.Positifdegerler[2].ToString();
        leftText.text = ScriptDice.Negatifdegerler[2].ToString();
        ShowStatsPanel();
    }
    public void buttonTemizlik()
    {
        rightText.text = ScriptDice.Positifdegerler[1].ToString();
        leftText.text = ScriptDice.Negatifdegerler[1].ToString();
        ShowStatsPanel();
    }
    
    public void ShowStatsPanel()
    {
        StatsPanel.SetActive(true);
        timer = 1f;
    }

    private void Update()
    {
        //Debug.Log(timer);
        if (timer >= 0f) timer -= Time.deltaTime;
        if (timer <= 0) StatsPanel.SetActive(false);
    }
}
