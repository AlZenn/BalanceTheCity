using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public GameObject shopPanel;

    private void Start()
    {
        shopPanel.SetActive(false); // Ma�aza paneli ba�lang��ta kapal�
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true); // Ma�aza panelini a�
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false); // Ma�aza panelini kapat
    }
}