using UnityEngine;

public class DiceRead : MonoBehaviour
{
    [SerializeField] Rigidbody dado;
    [SerializeField] Rigidbody dadodos;

    [SerializeField] int playerID; // Assuming playerID is an integer identifying the player

    public int LeerDado(){
        return LeerDadoUno() + LeerDadoDos();
    }
    public int LeerDadoDos()
    {
        (float x, float y) value = (Mathf.Round(dadodos.transform.eulerAngles.x), Mathf.Round(dadodos.transform.eulerAngles.z));
        switch(value){
            case (270,0):
                return(1);
            case (0,0):
                return(2);
            case (0,270):
                return(3);
            case (0,90):
                return(4);
            case (0,180):
                return(5);
            case (90,0):
                return(6);
            default: return(0); 
        }
    }
    public int LeerDadoUno()
    {
        (float x, float y) value = (Mathf.Round(dado.transform.eulerAngles.x), Mathf.Round(dado.transform.eulerAngles.z));
        switch(value){
            case (270,0):
                return(1);
            case (0,0):
                return(2);
            case (0,270):
                return(3);
            case (0,90):
                return(4);
            case (0,180):
                return(5);
            case (90,0):
                return(6);
            default: return(0); 
        }
    }
}