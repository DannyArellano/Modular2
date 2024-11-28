using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData playerData = null;
    public InnerTile currentPlayerTile;
    public int playerID;
    public TurnManager turnManager;
    private Transform previousPosition;
    private Renderer renderer;

    void Start()
    {
        previousPosition = this.transform;
        turnManager = FindObjectOfType<TurnManager>();
        playerData = GetComponentInParent<PlayerData>();
        playerID = playerData.playerID;

        // Get the Renderer component
        renderer = GetComponent<Renderer>();

        // Set the color based on playerID
        SetColorBasedOnPlayerID();
        if (turnManager != null)
        {
            turnManager.OnTurnEnd += OnTurnEnd; // Subscribe to the end turn event
        }
    }

    void OnDestroy()
    {
        if (turnManager != null)
        {
            turnManager.OnTurnEnd -= OnTurnEnd; // Unsubscribe from the end turn event
        }
    }

    void OnTurnEnd()
    {
        // Check if the player has landed on an inner tile and activate the tile effect
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "InnerTile")
            {
                Debug.Log("Tile was Activated");
                hitCollider.gameObject.GetComponent<InnerTilesClass>().ActivateTileEffect(this);
            }
        }
    }

    void Update()
    {
        if (turnManager.GetCurrentPhase() == TurnManager.Phase.InnerBoardMovement && playerID == turnManager.GetCurrentPlayerTurn())
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleMouseClick();
            }
        }
    }

    private void HandleMouseClick()
    {
        if (playerID == turnManager.GetCurrentPlayerTurn())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                InnerTile clickedTile = hit.collider.GetComponent<InnerTile>();
                if (clickedTile != null && currentPlayerTile != null)
                {
                    Debug.Log("Tile found.");
                    if (MoveToTile(clickedTile))
                    {
                        if (playerData.cantidadMovimientos == 0)
                        {
                            InnerTilesClass tileClass = clickedTile.GetComponent<InnerTilesClass>();
                            if (tileClass != null)
                            {
                                tileClass.ActivateTileEffect(this);
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("No tile found.");
                }
            }
        }
    }

    public bool MoveToTile(InnerTile targetTile)
    {
        if (targetTile == null || playerData.cantidadMovimientos <= 0)
        {
            return false;
        }

        // Check if the target tile is within the connected tiles
        bool isConnected = false;
        foreach (InnerTile tile in currentPlayerTile.connectedTiles)
        {
            if (tile == targetTile)
            {
                isConnected = true;
                break;
            }
        }

        if (!isConnected)
        {
            return false;
        }

        // Assuming InnerTile has a position property
        Vector3 targetPosition = targetTile.transform.position;

        // Move the player to the target position
        previousPosition = this.transform;
        transform.position = targetPosition;

        // Update the current player tile
        currentPlayerTile = targetTile;
        playerData.cantidadMovimientos -= 1;
        return true;
    }

/*     void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player collided with " + other.gameObject.name);
        if (other.gameObject.tag == "InnerTile")
        {
            Debug.Log("Tile was Activated");    
            other.gameObject.GetComponent<InnerTilesClass>().ActivateTileEffect(this);
        }
    } */

    public void InitializeCurrentPlayerTile(InnerTile initialTile)
    {
        currentPlayerTile = initialTile;
    }

    public InnerTile GetCurrentTile()
    {
        return currentPlayerTile;
    }

    public void GainGold(int goldAmount)
    {
        playerData.gold += goldAmount;
    }

    public void Fight(int lostMilitaryPower)
    {
        playerData.militaryPower -= lostMilitaryPower;
    }

    public void Recruit(int increaseAmount)
    {
        try
        {
            playerData.militaryPower += increaseAmount;
        }
        catch (Exception ex)
        {
            //Debug.LogError("An error occurred while increasing military power: " + ex.Message);
        }
    }

    private void SetColorBasedOnPlayerID()
    {
        if (renderer != null)
        {
            switch (playerID)
            {
                case 0:
                    renderer.material.color = Color.red;
                    break;
                case 1:
                    renderer.material.color = Color.blue;
                    break;
                case 2:
                    renderer.material.color = Color.green;
                    break;
                case 3:
                    renderer.material.color = Color.yellow;
                    break;
                default:
                    renderer.material.color = Color.white;
                    break;
            }
        }
    }
}