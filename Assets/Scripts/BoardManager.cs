using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab for player objects
    public GameObject playerContainerPrefab; // Reference to the player container prefab
    public GameObject wolfPrefab; // Prefab for wolf objects
    public Transform[] boardSpaces; // Array of positions representing the board spaces
    public List<Transform> players = new List<Transform>(); // List of player objects
    public int[] playerPositions; // Track each player's current position index
    public TurnManager turnManager; // Reference to TurnManager script
    public DiceRead diceRead; // Reference to DiceRead script
    public float moveSpeed = 5f; // Speed of player movement
    public Transform cameraTransform;
    public float cameraMoveSpeed = 5f;
    public UIModify uiModify;
    public InnerTileLogic innerTiles; 
    public BuildingSystem buildingSystem;
    public GameObject[] playerModelPrefabs; // Array of player model prefabs to choose from
    public GameObject[] petModelPrefabs; // Array of pet model prefabs to choose from
    public Color[] playerColors; // Array of colors for each player

    void Start()
    {
        buildingSystem = FindObjectOfType<BuildingSystem>();
        if (turnManager == null)
        {
            Debug.LogError("TurnManager is not assigned!");
            return;
        }

        InitializePlayers();
    }

    void InitializePlayers()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab is not assigned!");
            return;
        }

        if (playerContainerPrefab == null)
        {
            Debug.LogError("Player container prefab is not assigned!");
            return;
        }

        playerPositions = new int[turnManager.numberOfPlayers]; // Initialize player positions array

        for (int i = 0; i < turnManager.numberOfPlayers; i++)
        {
            // Instantiate the player container prefab at position (0, 0, 0)
            GameObject playerContainer = Instantiate(playerContainerPrefab, Vector3.zero, Quaternion.identity);
            playerContainer.name = "Player" + i;
            uiModify.RefreshPlayersArray();

            // Instantiate the player object as a child of the playerContainer at the position of the first tile
            GameObject playerObject = Instantiate(playerPrefab, boardSpaces[0].position + new Vector3(0, 1, 0), Quaternion.identity, playerContainer.transform);
            players.Add(playerObject.transform);
            playerPositions[i] = 0; // Start all players at the first position

            // Assign playerID to the player object
            Player playerScript = playerObject.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.playerID = i;
            }

            // Load the player model data and swap the model
            ModelData playerModelData = ModelDataHandler.LoadPlayerModelData();
            if (playerModelData != null)
            {
                Debug.Log("Loaded player model data: " + playerModelData.modelName);
                foreach (GameObject modelPrefab in playerModelPrefabs)
                {
                    if (modelPrefab.name == playerModelData.modelName)
                    {
                        // Find the child object that contains the model
                        Transform modelContainer = playerObject.transform.Find("ModelContainer");
                        if (modelContainer != null)
                        {
                            // Destroy all children of the model container
                            foreach (Transform child in modelContainer)
                            {
                                Destroy(child.gameObject);
                            }

                            // Instantiate the saved model as a child of the model container
                            GameObject newModel = Instantiate(modelPrefab, modelContainer);

                            // Optionally, reset the new model's local position, rotation, and scale
                            newModel.transform.localPosition = Vector3.zero;
                            newModel.transform.localRotation = Quaternion.identity;
                            newModel.transform.localScale = Vector3.one;

                            // Change the color of the player model based on the player ID
                            Renderer playerRenderer = newModel.GetComponentInChildren<Renderer>();
                            if (playerRenderer != null && playerColors.Length > i)
                            {
                                playerRenderer.material.color = playerColors[i];
                            }
                        }
                        else
                        {
                            Debug.LogError("ModelContainer not found in player prefab.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Debug.LogError("Player model data not found.");
            }

            // Instantiate the pet model for each player
            if (wolfPrefab != null)
            {
                // Instantiate the pet container prefab at position (0, 0, 0)
                GameObject petContainer = Instantiate(wolfPrefab, Vector3.zero, Quaternion.identity, playerContainer.transform);
                petContainer.name = "Pet" + i;

                // Load the pet model data and swap the model
                ModelData petModelData = ModelDataHandler.LoadPetModelData();
                if (petModelData != null)
                {
                    Debug.Log("Loaded pet model data: " + petModelData.modelName);
                    foreach (GameObject modelPrefab in petModelPrefabs)
                    {
                        if (modelPrefab.name == petModelData.modelName)
                        {
                            // Find the child object that contains the model
                            Transform modelContainer = petContainer.transform.Find("ModelContainer");
                            if (modelContainer != null)
                            {
                                // Destroy all children of the model container
                                foreach (Transform child in modelContainer)
                                {
                                    Destroy(child.gameObject);
                                }

                                // Instantiate the saved model as a child of the model container
                                GameObject newModel = Instantiate(modelPrefab, modelContainer);

                                // Optionally, reset the new model's local position, rotation, and scale
                                newModel.transform.localPosition = Vector3.zero;
                                newModel.transform.localRotation = Quaternion.identity;
                                newModel.transform.localScale = Vector3.one;

                                // Change the color of the pet model based on the player ID
                                Renderer petRenderer = newModel.GetComponentInChildren<Renderer>();
                                if (petRenderer != null && playerColors.Length > i)
                                {
                                    petRenderer.material.color = playerColors[i];
                                }
                            }
                            else
                            {
                                Debug.LogError("ModelContainer not found in pet prefab.");
                            }
                            break;
                        }
                    }
                }
                else
                {
                    Debug.LogError("Pet model data not found.");
                }

                // Place pet in a random inner tile from InnerTiles.allTiles
                int randomIndex = Random.Range(0, innerTiles.allTiles.Length); // Assuming InnerTiles.allTiles is an array
                InnerTile initialTile = innerTiles.allTiles[randomIndex].GetComponent<InnerTile>();
                petContainer.transform.position = initialTile.transform.position;

                // Initialize currentPlayerTile in PlayerMovement
                PlayerMovement petMovement = petContainer.GetComponent<PlayerMovement>();
                if (petMovement != null)
                {
                    petMovement.InitializeCurrentPlayerTile(initialTile);
                    buildingSystem.playerMovements.Add(petMovement);
                }
            }
            else
            {
                Debug.LogError("wolfPrefab is not assigned!");
            }
        }
    }

    public void MovePlayer(int playerID, int diceResult)
    {
        MoveCameraToPlayer(playerID); // Move the camera to the player at the start of the turn

        int currentPosition = playerPositions[playerID];
        int newPosition = (currentPosition + diceResult) % boardSpaces.Length; // Ensure the position wraps around the board

        playerPositions[playerID] = newPosition;
        StartCoroutine(MovePlayerCoroutine(playerID, boardSpaces[newPosition].position + new Vector3(0, 1f, 0)));
    }

    private void MoveCameraToPlayer(int playerID)
    {
        Transform playerTransform = players[playerID];
        StartCoroutine(MoveCameraCoroutine(new Vector3(playerTransform.position.x, 15f, playerTransform.position.z + 13f)));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 targetPosition)
    {
        while (Vector3.Distance(cameraTransform.position, targetPosition) > 0.1f)
        {
            cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);
            yield return null;
        }

        cameraTransform.position = targetPosition; // Ensure the camera reaches the exact target position
    }

    private IEnumerator MovePlayerCoroutine(int playerID, Vector3 targetPosition)
    {
        Transform playerTransform = players[playerID];
        playerTransform.position += new Vector3(0, 1f, 0);
        Rigidbody playerRigidbody = playerTransform.GetComponent<Rigidbody>();
        playerRigidbody.isKinematic = true;
        playerRigidbody.GetComponent<Player>().isOnTile = false;

        while (Vector3.Distance(playerTransform.position, targetPosition) > 0.1f)
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        playerTransform.position = targetPosition; // Ensure the player reaches the exact target position
        playerRigidbody.isKinematic = false;
    }
}