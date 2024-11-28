using UnityEngine;
using TMPro;
using System.Linq;

public class UIModify : MonoBehaviour
{
    public TextMeshProUGUI militaryPowerText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI mineralText;
    public TextMeshProUGUI movementAmountText;
    public TurnManager turnManager;

    public PlayerData[] players;

    void Start()
    {
        if (turnManager == null)
        {
            Debug.LogError("TurnManager is not assigned.");
        }
    }

    void Update()
    {
        UpdateResourceText(turnManager.GetCurrentPlayerTurn());
    }

    public void UpdateResourceText(int playerID)
    {
        if (playerID < 0 || playerID >= players.Length)
        {
            Debug.LogError("Invalid playerID");
            return;
        }

        PlayerData currentPlayer = players[playerID];
        moneyText.text = currentPlayer.gold.ToString();
        mineralText.text = currentPlayer.mineralNodo.ToString();
        movementAmountText.text = currentPlayer.cantidadMovimientos.ToString();
        militaryPowerText.text = currentPlayer.militaryPower.ToString();
    }

    public void RefreshPlayersArray()
    {
        players = GameObject.FindGameObjectsWithTag("Player")
                            .Select(go => go.GetComponent<PlayerData>())
                            .ToArray();

        if (players == null || players.Length == 0)
        {
            Debug.LogError("No players found with the 'Player' tag.");
        }
    }
}