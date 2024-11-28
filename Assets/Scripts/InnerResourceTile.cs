using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerResourceTile : InnerTilesClass
{
    public int tileGain;
    
    void Start(){
        isEmpty = true;
        tileGain = Random.Range(20, 60);
    }
    public override string ReturnTileType()
    {
        return "InnerResourceTile";
    }
    public override void ActivateTileEffect(PlayerMovement player)
    {
        player.GainGold(tileGain);
    }

}