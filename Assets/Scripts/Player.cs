using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int playerID;
    public GameObject playerManager;
    public Renderer[] playerRenderers; // Array of Renderer components
    private Rigidbody rb; // Reference to the Rigidbody component
    public bool isOnTile = false; // Variable to track if the player is on a tile
    private TurnManager turnManager;

    void Start()
    {
        playerManager = this.transform.parent.gameObject;
        SetColorBasedOnPlayerID();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is not attached to the object!");
        }

        turnManager = FindObjectOfType<TurnManager>();
        if (turnManager == null)
        {
            Debug.LogError("TurnManager is not assigned and could not be found in the scene.");
        }
    }

    void SetColorBasedOnPlayerID()
    {
        if (playerRenderers == null || playerRenderers.Length == 0)
        {
            Debug.LogError("Player Renderers are not assigned!");
            return;
        }

        Color playerColor;

        switch (playerID)
        {
            case 0:
                playerColor = Color.red;
                break;
            case 1:
                playerColor = Color.blue;
                break;
            case 2:
                playerColor = Color.green;
                break;
            case 3:
                playerColor = Color.yellow;
                break;
            default:
                playerColor = Color.white;
                break;
        }

        foreach (Renderer renderer in playerRenderers)
        {
            renderer.material.color = playerColor;
        }
    }

    void Update()
    {
        if (turnManager.diceReadThisTurn && !isOnTile && HasStoppedMoving())
        {
            CheckForTileCollision();
        }

        CheckVictoryCondition();
    }

    public bool HasStoppedMoving()
    {
        return rb.velocity == Vector3.zero;
    }

    private void CheckForTileCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            Tile tile = hitCollider.GetComponent<Tile>();
            if (tile != null)
            {
                tile.ActivateTileEffect(this);
                isOnTile = true;
                break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tile" && !isOnTile)
        {
            isOnTile = true;
            PlayerData playerData = playerManager.GetComponent<PlayerData>();
            Tile tile = other.gameObject.GetComponent<Tile>();
            if (tile != null)
            {
                tile.ActivateTileEffect(this);
            }
        }
    }


    public void Recruit(int increaseAmount)
    {
        Debug.Log("Player recruited " + increaseAmount + " units.");
        playerManager.GetComponent<PlayerData>().militaryPower += increaseAmount; 
    }
    public void GainGold(int increaseAmount)
    {
        Debug.Log("Player gained " + increaseAmount + " gold.");
        playerManager.GetComponent<PlayerData>().gold += increaseAmount;
    }

    public void MineMineral(int increaseAmount)
    {
        Debug.Log("Player mined " + increaseAmount + " mineral.");
        playerManager.GetComponent<PlayerData>().mineralNodo += increaseAmount;
    }
    
    public void ObtenerMovimientos(int increaseAmount)
    {
        Debug.Log("Player obtained " + increaseAmount + " movements.");
        playerManager.GetComponent<PlayerData>().cantidadMovimientos += increaseAmount;
    }

    private void CheckVictoryCondition()
    {
        PlayerData playerData = playerManager.GetComponent<PlayerData>();
        if (playerData.gold >= 1000 && playerData.militaryPower >= 1000 && playerData.mineralNodo >= 200)
        {
            Debug.Log("Player " + playerID + " wins!");
            PlayerPrefs.SetInt("WinningPlayerID", playerID);
            SceneManager.LoadScene("VictoryScene");
        }
    }
}