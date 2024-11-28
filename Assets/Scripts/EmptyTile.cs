using UnityEngine;

public class EmptyTile : Tile
{
    public override void ActivateTileEffect(Player player)
    {
        // Empty implementation, no effect on the player
        Debug.Log("Player landed on an empty tile. No effect.");
    }

}