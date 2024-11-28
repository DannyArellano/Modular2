using UnityEngine;

public abstract class InnerTilesClass : MonoBehaviour, ITile
{
    public abstract string ReturnTileType();
    public abstract void ActivateTileEffect(PlayerMovement player);
    public bool isEmpty { get; set; }
    public int playerID { get; set; }
}