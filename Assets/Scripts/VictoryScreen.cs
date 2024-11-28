using UnityEngine;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    public TextMeshProUGUI victoryText;

    void Start()
    {
        int winningPlayerID = PlayerPrefs.GetInt("WinningPlayerID", -1);
        if (winningPlayerID != -1)
        {
            victoryText.text = "Player " + (winningPlayerID + 1) + " wins!";
        }
        else
        {
            victoryText.text = "No winner!";
        }
    }
}