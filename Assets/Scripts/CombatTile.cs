using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInnerTiles : InnerTilesClass
{
    public int tilePower;

    void Start(){
        isEmpty = true;
        tilePower = Random.Range(10, 30);
    }
    public override string ReturnTileType()
    {
        return "CombatInnerTiles";
    }
    public override void ActivateTileEffect(PlayerMovement player)
    {
        if(isEmpty){
            player.Fight(tilePower);
            player.GainGold(tilePower/2);
        }
        
    }
}
