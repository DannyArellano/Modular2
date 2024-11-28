using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Rigidbody rbdos;
    Vector3 fuerzaLanzado;
    Vector3 fuerzaRot;

    public void LanzarDosDados(){
        LanzarDadoUno();
        LanzarDadoDos();
    }
    public void LanzarDadoUno(){
        fuerzaLanzado = Vector3.up*Random.Range(5,10);
        fuerzaRot = new Vector3 (Random.Range(-50,50),Random.Range(-50,50),Random.Range(-50,50));
        rb.AddForce(fuerzaLanzado, ForceMode.Impulse);
        rb.AddTorque(fuerzaRot, ForceMode.Impulse);
    }
    public void LanzarDadoDos(){
        fuerzaLanzado = Vector3.up*Random.Range(5,10);
        fuerzaRot = new Vector3 (Random.Range(-50,50),Random.Range(-50,50),Random.Range(-50,50));
        rbdos.AddForce(fuerzaLanzado, ForceMode.Impulse);
        rbdos.AddTorque(fuerzaRot, ForceMode.Impulse);
    }
}
