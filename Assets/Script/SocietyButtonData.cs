using System.Collections.Generic;

public class SocietyButtonData
{
    public static List<SocietyButton> GetSocietyButtons()
    {
        return new List<SocietyButton>
        {
            new SocietyButton
            {
                name = "Aktivist Kartý",
                happinessRequirement = 0,
                cleanlinessRequirement = 10,
                powerRequirement = 0,
                moneyRequirement = 0,
                votePercentage = 5,
                boostCondition = "Left Wing Kartý",
                contraCondition = "Fabrikacýlar Kartý",
                cleanlinessIncreasesRequirement = 3
            },
            new SocietyButton
            {
                name = "Business Kartý",
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
                name = "Left Wing Kartý",
                happinessRequirement = 10,
                cleanlinessRequirement = 0,
                powerRequirement = 0,
                moneyRequirement = 0,
                votePercentage = 8,
                boostCondition = "Aktivistler",
                contraCondition = "Right Wing Kartý",
                happinessIncreasesRequirement = 3
            },
            new SocietyButton
            {
                name = "Right Wing Kartý",
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
                name = "Sendikalar Kartý",
                happinessRequirement = 0,
                cleanlinessRequirement = 8,
                powerRequirement = 0,
                moneyRequirement = 0,
                votePercentage = 6,
                boostCondition = "Left Wing Kartý",
                contraCondition = "Endüstriyel Kartý",
                cleanlinessAndHappinessIncreasesRequirement = 2
            },
            new SocietyButton
            {
                name = "Sanatçýlar Kartý",
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
                name = "Endüstriyel Kartý",
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
                name = "Çevreciler Kartý",
                happinessRequirement = 0,
                cleanlinessRequirement = 10,
                powerRequirement = 0,
                moneyRequirement = 0,
                votePercentage = 7,
                boostCondition = "Left Wing Kartý",
                contraCondition = "Fabrikacýlar Kartý",
                cleanlinessIncreasesRequirement = 3
            }
        };
    }
}