using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public GameObject shopPanel;

    private void Start()
    {
        shopPanel.SetActive(false); // Maðaza paneli baþlangýçta kapalý
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true); // Maðaza panelini aç
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false); // Maðaza panelini kapat
    }
}