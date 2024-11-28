using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int playerID;
    public float militaryPower=50;
    public float gold=1000;

    public float mineralNodo;
    public int cantidadMovimientos=1000;

    public Player playerScript;

    void Start(){
        playerScript = GetComponentInChildren<Player>();
        playerID = playerScript.playerID;
    }
}
