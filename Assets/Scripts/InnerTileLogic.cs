using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerTileLogic : MonoBehaviour
{
    public GameObject[] allTiles;
    public PlayerMovement playerMovement;
    void Awake()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        allTiles = GameObject.FindGameObjectsWithTag("InnerTile");
    }
}
