// Tile.cs
using System.Collections.Generic;
using UnityEngine;

public class InnerTile : MonoBehaviour
{
    public List<InnerTile> connectedTiles = new List<InnerTile>();
    public GameObject[] allTiles;

    void Start()
    {
        allTiles = GameObject.FindGameObjectsWithTag("InnerTile");
        FindAndConnectClosestTiles();
    }

    public void ConnectTile(InnerTile tile)
    {
        if (!connectedTiles.Contains(tile))
        {
            connectedTiles.Add(tile);
            tile.ConnectTile(this); // Ensure bidirectional connection
        }
    }

    private void FindAndConnectClosestTiles()
    {
        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("InnerTile");
        List<InnerTile> sortedTiles = new List<InnerTile>();

        foreach (GameObject obj in allTiles)
        {
            InnerTile tile = obj.GetComponent<InnerTile>();
            if (tile != null && tile != this)
            {
                sortedTiles.Add(tile);
            }
        }

        sortedTiles.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));

        for (int i = 0; i < Mathf.Min(3, sortedTiles.Count); i++)
        {
            ConnectTile(sortedTiles[i]);
        }
    }
}