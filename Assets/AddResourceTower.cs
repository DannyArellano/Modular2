using System.Collections.Generic;
using UnityEngine;

public class AddResourceTower : MonoBehaviour
{
    public TurnManager turnManager;
    public UIModify uiModify;
    public PlayerData playerData;

    void Start()
    {
        turnManager = GameObject.FindObjectOfType<TurnManager>();
        uiModify = GameObject.FindObjectOfType<UIModify>();
        playerData = uiModify.players[turnManager.GetCurrentPlayerTurn()];

        // Subscribe to the OnTurnStart event
        if (turnManager != null)
        {
            Debug.Log("Subscribed to OnTurnStart event");
            turnManager.OnTurnStart += OnTurnStart;
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the OnTurnStart event to avoid memory leaks
        if (turnManager != null)
        {
            turnManager.OnTurnStart -= OnTurnStart;
        }
    }

    void OnTurnStart()
    {
        if (playerData.playerID == turnManager.GetCurrentPlayerTurn())
        {
            playerData.militaryPower += 15;
        }
    }
}
