using UnityEngine;

public class PlayerEncounter : MonoBehaviour
{
    public void HandleEncounter(PlayerData player1, PlayerData player2)
    {
        // Calculate 25% of each player's military power
        int player1PowerLoss = Mathf.FloorToInt(player1.militaryPower * 0.25f);
        int player2PowerLoss = Mathf.FloorToInt(player2.militaryPower * 0.25f);

        // Subtract the calculated power from each player
        player1.militaryPower -= player1PowerLoss;
        player2.militaryPower -= player2PowerLoss;

        // Log the encounter results
        Debug.Log($"Player {player1.playerID} and Player {player2.playerID} encountered each other.");
        Debug.Log($"Player {player1.playerID} lost {player1PowerLoss} military power, remaining: {player1.militaryPower}");
        Debug.Log($"Player {player2.playerID} lost {player2PowerLoss} military power, remaining: {player2.militaryPower}");
    }
}