// InnerTileTown.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerTileTown : InnerTilesClass
{
    public int recruiting;
    public int gainGold;

    void Start(){
        isEmpty = true;
        recruiting = Random.Range(10, 20);
        gainGold = Random.Range(15, 40);
    }
    public override string ReturnTileType()
    {
        return "InnerTileTown";
    }
    public override void ActivateTileEffect(PlayerMovement player)
    {
        if(isEmpty){
            player.Recruit(recruiting);
            player.GainGold(gainGold);
        }
    }
}