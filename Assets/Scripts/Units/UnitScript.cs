using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Unidad", menuName = "Unidad")]
public class UnitScript : ScriptableObject
{
    public int playerID;
    public String unitName;
    public int unitID;
    public int cost;
    public int HP;

}
