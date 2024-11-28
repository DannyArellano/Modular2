using UnityEngine;

public class RecruitTile : Tile
{
    public int increaseAmount = 10;
    public int price = 10;

    public override void ActivateTileEffect(Player player)
    {
        player.Recruit(increaseAmount);
        player.ObtenerMovimientos(1);
    }

}