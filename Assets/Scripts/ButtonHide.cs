using UnityEngine;
using UnityEngine.UI;

public class ButtonHide : MonoBehaviour
{
    public Button targetButton; // Reference to the button to be disabled
    public TurnManager turnManager; // Reference to the TurnManager script

    void Start()
    {
        if (targetButton == null)
        {
            Debug.LogError("Target button is not assigned!");
            return;
        }

        if (turnManager == null)
        {
            Debug.LogError("TurnManager is not assigned!");
            return;
        }
    }

    void Update()
    {
        // Check if the dice has been rolled this turn and disable the button accordingly
        if (turnManager.GetComponent<TurnManager>().diceReadThisTurn)
        {
            targetButton.interactable = false;
        }
        else
        {
            targetButton.interactable = true;
        }
    }
}