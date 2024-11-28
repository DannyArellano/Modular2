using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public event Action OnTurnStart;
    public event Action OnTurnEnd; // Event to notify when the turn ends

    private DiceRead diceReadScript;
    private BoardManager boardManager;
    private int currentPlayerTurn;
    private Phase currentPhase;

    public TurnPhaseDisplay turnPhaseDisplay;

    public enum Phase
    {
        DiceThrow,
        InnerBoardMovement
    }

    public int numberOfPlayers = 2; // Example total players, adjust as needed
    public bool diceReadThisTurn;

    void Start()
    {
        currentPlayerTurn = 0;
        StartNewTurn();

        diceReadScript = FindObjectOfType<DiceRead>();
        if (diceReadScript == null)
        {
            Debug.LogError("DiceReadScript is not assigned and could not be found in the scene.");
        }

        boardManager = FindObjectOfType<BoardManager>();
        if (boardManager == null)
        {
            Debug.LogError("BoardManager is not assigned and could not be found in the scene.");
        }

        currentPhase = Phase.DiceThrow; // Initialize with the first phase
    }

    public int GetCurrentPlayerTurn()
    {
        return currentPlayerTurn;
    }

    public Phase GetCurrentPhase()
    {
        return currentPhase;
    }

    public void SwitchToNextPhase()
    {
        if (currentPhase == Phase.DiceThrow)
        {
            currentPhase = Phase.InnerBoardMovement;
        }
        else
        {
            currentPhase = Phase.DiceThrow;
            currentPlayerTurn = (currentPlayerTurn + 1) % numberOfPlayers;
            StartNewTurn();
        }
    }

    public void EndTurn()
    {
        OnTurnEnd?.Invoke(); // Notify that the turn has ended
        SwitchToNextPhase();
    }

    private void StartNewTurn()
    {
        diceReadThisTurn = false;
        OnTurnStart?.Invoke();
        turnPhaseDisplay.ShowText("Turno de Jugador " + (currentPlayerTurn + 1));

        // Reset the isOnTile flag for the current player
        Player currentPlayer = GetCurrentPlayer();
        if (currentPlayer != null)
        {
            currentPlayer.isOnTile = false;
        }
    }

    private Player GetCurrentPlayer()
    {
        // Implement this method to return the current player
        // This is just a placeholder implementation
        return FindObjectOfType<Player>();
    }

    public void ReadDice()
    {
        if (diceReadThisTurn)
        {
            Debug.Log("Dice has already been read this turn.");
            return;
        }

        if (diceReadScript == null)
        {
            Debug.LogError("DiceReadScript is not assigned.");
            return;
        }

        if (boardManager == null)
        {
            Debug.LogError("BoardManager is not assigned.");
            return;
        }

        int diceValue = diceReadScript.LeerDado();
        Debug.Log("Dice read executed, value: " + diceValue);
        boardManager.MovePlayer(currentPlayerTurn, diceValue); // Move the current player based on the dice value
        diceReadThisTurn = true; // Set the flag to true after reading the dice

        // Switch to the next phase after reading the dice and moving the player
        SwitchToNextPhase();
    }
}