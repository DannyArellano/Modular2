using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerFortressTiles : InnerTilesClass
{
    public int recruiting;
    public int gainGold;

    void Start(){
        isEmpty = true;
        recruiting = Random.Range(30, 40);
        gainGold = Random.Range(5, 15);
    }
    public override string ReturnTileType()
    {
        return "InnerFortressTiles";
    }
    public override void ActivateTileEffect(PlayerMovement player)
    {
        if(isEmpty){
            player.Recruit(recruiting);
            player.GainGold(gainGold);
        }
    }
}