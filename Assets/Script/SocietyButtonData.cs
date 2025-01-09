using System.Collections.Generic;

public class SocietyButtonData
{
    public static List<SocietyButton> GetSocietyButtons()
    {
        return new List<SocietyButton>
        {
            new SocietyButton
            {
                name = "Aktivist Kart�",
                happinessRequirement = 0,
                cleanlinessRequirement = 10,
                powerRequirement = 0,
                moneyRequirement = 0,
                votePercentage = 5,
                boostCondition = "Left Wing Kart�",
                contraCondition = "Fabrikac�lar Kart�",
                cleanlinessIncreasesRequirement = 3
            },
            new SocietyButton
            {
                name = "Business Kart�",
                happinessRequirement = 0,
                cleanlinessRequirement = 0,
                powerRequirement = 10,
                moneyRequirement = 0,
                votePercentage = 7,
                contraCondition = "Aktivistler",
                powerIncreasesRequirement = 3
            },
            new SocietyButton
            {
                name = "Left Wing Kart�",
                happinessRequirement = 10,
                cleanlinessRequirement = 0,
                powerRequirement = 0,
                moneyRequirement = 0,
                votePercentage = 8,
                boostCondition = "Aktivistler",
                contraCondition = "Right Wing Kart�",
                happinessIncreasesRequirement = 3
            },
            new SocietyButton
            {
                name = "Right Wing Kart�",
                happinessRequirement = 0,
                cleanlinessRequirement = 0,
                powerRequirement = 0,
                moneyRequirement = 10,
                votePercentage = 8,
                contraCondition = "Left Wing",
                moneyIncreasesRequirement = 3
            },
            new SocietyButton
            {
                name = "Sendikalar Kart�",
                happinessRequirement = 0,
                cleanlinessRequirement = 8,
                powerRequirement = 0,
                moneyRequirement = 0,
                votePercentage = 6,
                boostCondition = "Left Wing Kart�",
                contraCondition = "End�striyel Kart�",
                cleanlinessAndHappinessIncreasesRequirement = 2
            },
            new SocietyButton
            {
                name = "Sanat��lar Kart�",
                happinessRequirement = 8,
                cleanlinessRequirement = 0,
                powerRequirement = 0,
                moneyRequirement = 0,
                votePercentage = 5,
                boostCondition = "Aktivistler",
                happinessIncreasesRequirement = 2
            },
            new SocietyButton
            {
                name = "End�striyel Kart�",
                happinessRequirement = 0,
                cleanlinessRequirement = 0,
                powerRequirement = 12,
                moneyRequirement = 0,
                votePercentage = 10,
                contraCondition = "Aktivistler",
                powerIncreasesRequirement = 3
            },
            new SocietyButton
            {
                name = "�evreciler Kart�",
                happinessRequirement = 0,
                cleanlinessRequirement = 10,
                powerRequirement = 0,
                moneyRequirement = 0,
                votePercentage = 7,
                boostCondition = "Left Wing Kart�",
                contraCondition = "Fabrikac�lar Kart�",
                cleanlinessIncreasesRequirement = 3
            }
        };
    }
}