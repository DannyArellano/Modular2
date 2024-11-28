// ITile.cs
using UnityEngine;

public interface ITile
{
    bool isEmpty { get; set; }
    int playerID { get; set; }
    Transform transform { get; }
}