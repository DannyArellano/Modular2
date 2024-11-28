using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTile : Tile
{
    public override void ActivateTileEffect(Player player)
    {
        player.ObtenerMovimientos(5);
    }
}
