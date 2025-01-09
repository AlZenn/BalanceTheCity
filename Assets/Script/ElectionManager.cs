using System.Collections.Generic;
using UnityEngine;

public class ElectionManager : MonoBehaviour
{
    public SC_ResourceManager resourceManager;
    public List<SocietyButton> purchasedSocietyButtons;
    private int totalVotes;

    private void Start()
    {
        CalculateTotalVotes();
    }

    private void CalculateTotalVotes()
    {
        totalVotes = 0;
        foreach (var button in purchasedSocietyButtons)
        {
            totalVotes += button.votePercentage;


            if (button.boostCondition != null && purchasedSocietyButtons.Exists(b => b.name == button.boostCondition))
            {
                totalVotes += button.votePercentage / 2;
            }

            if (button.contraCondition != null && purchasedSocietyButtons.Exists(b => b.name == button.contraCondition))
            {
                totalVotes -= button.votePercentage / 2;
            }
        }

        Debug.Log("Toplam Oy: " + totalVotes);
    }
}