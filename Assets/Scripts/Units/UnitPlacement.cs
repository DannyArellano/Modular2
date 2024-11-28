using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    public int playerID;
    public Transform[] positions;
    [SerializeField] Collider thisCol;

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "PlayerUnit"){
            playerID = col.gameObject.GetComponent<DeterminarTipoUnidad>().playerID;
            col.transform.position = positions[playerID].position;
            col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
