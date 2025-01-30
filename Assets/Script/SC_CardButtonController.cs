using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SC_CardButtonController : MonoBehaviour
{
    public SC_Dice ScriptDice;
    
    public GameObject StatsPanel;
    public GameObject DownStatsPanel;
    
    
    
    public Text Text_money;
    public Text Text_happiness;
    public Text Text_electric;
    public Text Text_clean;
    
    public Text DownTextMoney;
    public Text DownTextHappiness;
    public Text DownText_electric;
    public Text DownText_clean;
    
    
    
    public float timer = 0f;

    private void Awake()
    {
        StatsPanel.SetActive(false);
        DownStatsPanel.SetActive(false);
        
        Text_money.text = "";
        Text_happiness.text = "";
        Text_electric.text = "";
        Text_clean.text = "";

        DownTextMoney.text = "";
        DownTextHappiness.text = "";
        DownText_electric.text = "";
        DownText_clean.text = "";
    }

    private void defaultText()
    {
        Text_money.text = "";
        Text_happiness.text = "";
        Text_electric.text = "";
        Text_clean.text = "";

        DownTextMoney.text = "";
        DownTextHappiness.text = "";
        DownText_electric.text = "";
        DownText_clean.text = "";
    }

    public void buttonPara()
    {
        if (ScriptDice.ChaosCard)
        {
            Text_money.text = "K";
            Text_electric.text = "A";
            Text_clean.text = "O";
            Text_happiness.text = "S";
            ShowStatsPanel();  
        }
        if (ScriptDice.BuildingCard)
        {
            Text_money.text = "Y";
            Text_electric.text = "A";
            Text_clean.text = "P";
            Text_happiness.text = "I";
            ShowStatsPanel();  
        }
        if (!ScriptDice.isSwitched && !ScriptDice.ChaosCard && !ScriptDice.BuildingCard)
        {
            Text_happiness.text = ScriptDice.Positifdegerler[3].ToString();
            Text_money.text = ScriptDice.Negatifdegerler[3].ToString();
            ShowStatsPanel();
        }
        else if (ScriptDice.isSwitched && ScriptDice.denemeInt == 1 && !ScriptDice.ChaosCard && !ScriptDice.BuildingCard)
        {
            Text_money.text = ( 2 * ScriptDice.Positifdegerler[3]).ToString();
            Text_electric.text = ( 2 * ScriptDice.Positifdegerler[2]).ToString();
            Text_clean.text = ( 2 * ScriptDice.Positifdegerler[1]).ToString() ;
            Text_happiness.text = ( 2 * ScriptDice.Positifdegerler[0]).ToString();
            
            
            DownTextMoney.text = ( 0.5 * ScriptDice.Positifdegerler[3]).ToString();
            DownText_electric.text = ( 0.5 * ScriptDice.Positifdegerler[2]).ToString();
            DownText_clean.text = ( 0.5 * ScriptDice.Positifdegerler[1]).ToString() ;
            DownTextHappiness.text = ( 0.5 * ScriptDice.Positifdegerler[0]).ToString();
            ShowStatsPanel();
        }
        else if (ScriptDice.isSwitched && ScriptDice.denemeInt == 2&& !ScriptDice.ChaosCard && !ScriptDice.BuildingCard)
        {
            Text_money.text = ( 2 * ScriptDice.Negatifdegerler[3]).ToString();
            Text_electric.text = ( 2 * ScriptDice.Negatifdegerler[2]).ToString();
            Text_clean.text = ( 2 * ScriptDice.Negatifdegerler[1]).ToString() ;
            Text_happiness.text = ( 2 * ScriptDice.Negatifdegerler[0]).ToString();
            
            DownTextMoney.text = ( 0.5 * ScriptDice.Negatifdegerler[3]).ToString();
            DownText_electric.text = ( 0.5 * ScriptDice.Negatifdegerler[2]).ToString();
            DownText_clean.text = ( 0.5 * ScriptDice.Negatifdegerler[1]).ToString() ;
            DownTextHappiness.text = ( 0.5 * ScriptDice.Negatifdegerler[0]).ToString();
            ShowStatsPanel();
        }
    }
    public void buttonMutluluk()
    {
        if (ScriptDice.ChaosCard)
        {
            Text_money.text = "K";
            Text_electric.text = "A";
            Text_clean.text = "O";
            Text_happiness.text = "S";
            ShowStatsPanel();  
        }
        if (ScriptDice.BuildingCard)
        {
            Text_money.text = "Y";
            Text_electric.text = "A";
            Text_clean.text = "P";
            Text_happiness.text = "I";
            ShowStatsPanel();  
        }
        if (!ScriptDice.isSwitched&& !ScriptDice.ChaosCard&& !ScriptDice.BuildingCard)
        {
            Text_happiness.text = ScriptDice.Positifdegerler[0].ToString();
            Text_money.text = ScriptDice.Negatifdegerler[0].ToString();
            ShowStatsPanel();
        }
        else if (ScriptDice.isSwitched && ScriptDice.denemeInt == 1&& !ScriptDice.ChaosCard&& !ScriptDice.BuildingCard)
        {
            Text_money.text = ( 2 * ScriptDice.Positifdegerler[3]).ToString();
            Text_electric.text = ( 2 * ScriptDice.Positifdegerler[2]).ToString();
            Text_clean.text = ( 2 * ScriptDice.Positifdegerler[1]).ToString() ;
            Text_happiness.text = ( 2 * ScriptDice.Positifdegerler[0]).ToString();
            
            
            DownTextMoney.text = ( 0.5 * ScriptDice.Positifdegerler[3]).ToString();
            DownText_electric.text = ( 0.5 * ScriptDice.Positifdegerler[2]).ToString();
            DownText_clean.text = ( 0.5 * ScriptDice.Positifdegerler[1]).ToString() ;
            DownTextHappiness.text = ( 0.5 * ScriptDice.Positifdegerler[0]).ToString();
            ShowStatsPanel();
        }
        else if (ScriptDice.isSwitched && ScriptDice.denemeInt == 2&& !ScriptDice.ChaosCard&& !ScriptDice.BuildingCard)
        {
            Text_money.text = ( 2 * ScriptDice.Negatifdegerler[3]).ToString();
            Text_electric.text = ( 2 * ScriptDice.Negatifdegerler[2]).ToString();
            Text_clean.text = ( 2 * ScriptDice.Negatifdegerler[1]).ToString() ;
            Text_happiness.text = ( 2 * ScriptDice.Negatifdegerler[0]).ToString();
            
            DownTextMoney.text = ( 0.5 * ScriptDice.Negatifdegerler[3]).ToString();
            DownText_electric.text = ( 0.5 * ScriptDice.Negatifdegerler[2]).ToString();
            DownText_clean.text = ( 0.5 * ScriptDice.Negatifdegerler[1]).ToString() ;
            DownTextHappiness.text = ( 0.5 * ScriptDice.Negatifdegerler[0]).ToString();
            ShowStatsPanel();
        }
        
    }
    public void buttonGuc()
    {
        if (ScriptDice.ChaosCard)
        {
            Text_money.text = "K";
            Text_electric.text = "A";
            Text_clean.text = "O";
            Text_happiness.text = "S";
            ShowStatsPanel();  
        }
        if (ScriptDice.BuildingCard)
        {
            Text_money.text = "Y";
            Text_electric.text = "A";
            Text_clean.text = "P";
            Text_happiness.text = "I";
            ShowStatsPanel();  
        }
        if (!ScriptDice.isSwitched&& !ScriptDice.ChaosCard&& !ScriptDice.BuildingCard)
        {
            Text_happiness.text = ScriptDice.Positifdegerler[2].ToString();
            Text_money.text = ScriptDice.Negatifdegerler[2].ToString();
            ShowStatsPanel();
        }
        else if (ScriptDice.isSwitched && ScriptDice.denemeInt == 1&& !ScriptDice.ChaosCard&& !ScriptDice.BuildingCard)
        {
            Text_money.text = ( 2 * ScriptDice.Positifdegerler[3]).ToString();
            Text_electric.text = ( 2 * ScriptDice.Positifdegerler[2]).ToString();
            Text_clean.text = ( 2 * ScriptDice.Positifdegerler[1]).ToString() ;
            Text_happiness.text = ( 2 * ScriptDice.Positifdegerler[0]).ToString();
            
            
            DownTextMoney.text = ( 0.5 * ScriptDice.Positifdegerler[3]).ToString();
            DownText_electric.text = ( 0.5 * ScriptDice.Positifdegerler[2]).ToString();
            DownText_clean.text = ( 0.5 * ScriptDice.Positifdegerler[1]).ToString() ;
            DownTextHappiness.text = ( 0.5 * ScriptDice.Positifdegerler[0]).ToString();
            ShowStatsPanel();
        }
        else if (ScriptDice.isSwitched && ScriptDice.denemeInt == 2&& !ScriptDice.ChaosCard&& !ScriptDice.BuildingCard)
        {
            Text_money.text = ( 2 * ScriptDice.Negatifdegerler[3]).ToString();
            Text_electric.text = ( 2 * ScriptDice.Negatifdegerler[2]).ToString();
            Text_clean.text = ( 2 * ScriptDice.Negatifdegerler[1]).ToString() ;
            Text_happiness.text = ( 2 * ScriptDice.Negatifdegerler[0]).ToString();
            
            DownTextMoney.text = ( 0.5 * ScriptDice.Negatifdegerler[3]).ToString();
            DownText_electric.text = ( 0.5 * ScriptDice.Negatifdegerler[2]).ToString();
            DownText_clean.text = ( 0.5 * ScriptDice.Negatifdegerler[1]).ToString() ;
            DownTextHappiness.text = ( 0.5 * ScriptDice.Negatifdegerler[0]).ToString();
            ShowStatsPanel();
        }
    }
    public void buttonTemizlik()
    {
        if (ScriptDice.ChaosCard)
        {
            Text_money.text = "K";
            Text_electric.text = "A";
            Text_clean.text = "O";
            Text_happiness.text = "S";
            ShowStatsPanel();  
        }
        if (ScriptDice.BuildingCard)
        {
            Text_money.text = "Y";
            Text_electric.text = "A";
            Text_clean.text = "P";
            Text_happiness.text = "I";



            ShowStatsPanel();  
        }
        if (!ScriptDice.isSwitched&& !ScriptDice.ChaosCard&& !ScriptDice.BuildingCard)
        {
            Text_happiness.text = ScriptDice.Positifdegerler[1].ToString();
            Text_money.text = ScriptDice.Negatifdegerler[1].ToString();
            ShowStatsPanel();  
        }
        else if (ScriptDice.isSwitched && ScriptDice.denemeInt == 1&& !ScriptDice.ChaosCard&& !ScriptDice.BuildingCard)
        { 
            Text_money.text = ( 2 * ScriptDice.Positifdegerler[3]).ToString();
            Text_electric.text = ( 2 * ScriptDice.Positifdegerler[2]).ToString();
            Text_clean.text = ( 2 * ScriptDice.Positifdegerler[1]).ToString() ;
            Text_happiness.text = ( 2 * ScriptDice.Positifdegerler[0]).ToString();
            
            DownTextMoney.text = ( 0.5 * ScriptDice.Positifdegerler[3]).ToString();
            DownText_electric.text = ( 0.5 * ScriptDice.Positifdegerler[2]).ToString();
            DownText_clean.text = ( 0.5 * ScriptDice.Positifdegerler[1]).ToString() ;
            DownTextHappiness.text = ( 0.5 * ScriptDice.Positifdegerler[0]).ToString();
            ShowStatsPanel();
        }
        else if (ScriptDice.isSwitched && ScriptDice.denemeInt == 2&& !ScriptDice.ChaosCard&& !ScriptDice.BuildingCard)
        {
            Text_money.text = ( 2 * ScriptDice.Negatifdegerler[3]).ToString();
            Text_electric.text = ( 2 * ScriptDice.Negatifdegerler[2]).ToString();
            Text_clean.text = ( 2 * ScriptDice.Negatifdegerler[1]).ToString() ;
            Text_happiness.text = ( 2 * ScriptDice.Negatifdegerler[0]).ToString();
            
            DownTextMoney.text = ( 0.5 * ScriptDice.Negatifdegerler[3]).ToString();
            DownText_electric.text = ( 0.5 * ScriptDice.Negatifdegerler[2]).ToString();
            DownText_clean.text = ( 0.5 * ScriptDice.Negatifdegerler[1]).ToString() ;
            DownTextHappiness.text = ( 0.5 * ScriptDice.Negatifdegerler[0]).ToString();
            ShowStatsPanel();
        }
    }
    
    public void ShowStatsPanel()
    {
        StatsPanel.SetActive(true);
        if (ScriptDice.isSwitched) DownStatsPanel.SetActive(true);
        timer = 4f;
    }

    private void Update()
    {
        
        //Debug.Log(timer);
        if (timer >= 0f) timer -= Time.deltaTime;
        if (timer <= 0) StatsPanel.SetActive(false);
        if (timer <= 0) DownStatsPanel.SetActive(false);
        if (timer <= 0) defaultText();
        
    }
}
