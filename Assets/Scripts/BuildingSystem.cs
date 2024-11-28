using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BuildingType
{
    Town,
    MilitaryTower,
    MineralMine
}

public class Building
{
    public BuildingType Type { get; private set; }
    public int GoldCost { get; private set; }
    public int ResourcePerTurn { get; private set; }
    public int playerID { get; private set; }

    public Building(BuildingType type, int goldCost, int resourcePerTurn, int pID)
    {
        Type = type;
        GoldCost = goldCost;
        ResourcePerTurn = resourcePerTurn;
        playerID = pID;
    }
}

public class BuildingSystem : MonoBehaviour
{
    public UIModify uiModify;
    public TurnManager turnManager;
    public List<Building> Buildings { get;  set; } = new List<Building>();
    public GameObject townPrefab;
    public GameObject militaryTowerPrefab;
    public GameObject mineralMinePrefab;

    public List<PlayerMovement> playerMovements = new List<PlayerMovement>();

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
    }

    // Method to add PlayerMovement objects
    public void BuildBuilding(PlayerData playerData, ITile tile)
    {
        BuildingType type = GetBuildingTypeFromTile(tile);
        int goldCost = 0;
        int resourcePerTurn = 0;
        GameObject prefab = null;

        switch (type)
        {
            case BuildingType.Town:
                goldCost = 100;
                resourcePerTurn = 10;
                prefab = townPrefab;
                break;
            case BuildingType.MilitaryTower:
                goldCost = 150;
                resourcePerTurn = 5;
                prefab = militaryTowerPrefab;
                break;
            case BuildingType.MineralMine:
                goldCost = 120;
                resourcePerTurn = 8;
                prefab = mineralMinePrefab;
                break;
        }

        if (playerData.gold >= goldCost && tile.isEmpty)
        {
            playerData.gold -= goldCost;
            Buildings.Add(new Building(type, goldCost, resourcePerTurn, playerData.playerID));
            tile.isEmpty = false;
            tile.playerID = playerData.playerID;

            // Instantiate the building
            GameObject building = Instantiate(prefab, tile.transform.position, Quaternion.identity);

            // Change the color based on playerID
            Debug.Log(turnManager.GetCurrentPlayerTurn());
            Renderer renderer = building.GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                switch (turnManager.GetCurrentPlayerTurn())
                {
                    case 0:
                        renderer.material.color = Color.red;
                        break;
                    case 1:
                        renderer.material.color = Color.blue;
                        break;
                    case 2:
                        renderer.material.color = Color.green;
                        break;
                    case 3:
                        renderer.material.color = Color.yellow;
                        break;
                    default:
                        renderer.material.color = Color.white;
                        break;
                }
            }
        }
    }

    private BuildingType GetBuildingTypeFromTile(ITile tile)
    {
        Debug.Log(tile.isEmpty);
        if (tile is InnerTileTown)
        {
            Debug.Log("TownTile");
            return BuildingType.Town;
        }
        else if (tile is InnerFortressTiles)
        {
            Debug.Log("Military Tower");
            return BuildingType.MilitaryTower;
        }
        else if (tile is InnerResourceTile)
        {
            Debug.Log("ResourceTile");
            return BuildingType.MineralMine;
        }
        else if (tile is CombatInnerTiles){
            Debug.Log("CombatTile");
            return BuildingType.MilitaryTower;
        }
        else
        {
            throw new System.Exception("Unknown tile type");
        }
    }

    // Wrapper method for button click
    public void OnBuildButtonClick()
    {
        PlayerData playerData = GetPlayerData();
        ITile tile = GetCurrentTile(); // Get the current tile
        BuildBuilding(playerData, tile);
    }

    private PlayerData GetPlayerData()
    {
        return uiModify.players[turnManager.GetCurrentPlayerTurn()];
    }

    private ITile GetCurrentTile()
    {
        if (playerMovements.Count > 0)
        {
            PlayerMovement currentPlayerMovement = playerMovements[turnManager.GetCurrentPlayerTurn()];
            InnerTile currentTile = currentPlayerMovement.GetCurrentTile();
            Debug.Log(currentTile.name);
            if (currentTile != null)
            {
                return currentTile.GetComponent<ITile>();
            }
        }
        throw new System.Exception("Player is not on a valid tile");
    }

    public void ProvideResourcesPerTurn(PlayerData playerData)
    {
        foreach (var building in Buildings)
        {
            if (building.playerID == playerData.playerID)
            {
                switch (building.Type)
                {
                    case BuildingType.Town:
                        playerData.gold += building.ResourcePerTurn;
                        break;
                    case BuildingType.MilitaryTower:
                        playerData.militaryPower += building.ResourcePerTurn;
                        break;
                    case BuildingType.MineralMine:
                        playerData.mineralNodo += building.ResourcePerTurn;
                        break;
                }
            }
        }
    }
}