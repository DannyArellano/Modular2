using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeterminarTipoUnidad : MonoBehaviour
{
    // Assuming unidad is a scriptable object or a component attached to the game object
    public Unidad unidad;
    public int playerID;
    public Player player;

    public void Start()
    {
        playerID = player.playerID;
        unidad.playerID = playerID;
    }

    // Method to update the playerID
    public void UpdatePlayerID(int newPlayerID)
    {
        if (unidad != null)
        {
            unidad.playerID = newPlayerID;
            Debug.Log("Player ID updated to: " + newPlayerID);
        }
    }
}

// Assuming Unidad is a scriptable object or a class with playerID property
[System.Serializable]
public class Unidad
{
    public int playerID;
}
