using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceWindow : MonoBehaviour
{
    public GameObject windowPrefab;
    public Camera diceCamera;
    public float closeDelay = 2.0f;
    public DiceRead diceReadScript;
    public TurnManager turnManager;
    public CameraMovement cameraMovementScript;
    public Rigidbody diceRigidbody; // Declare the diceRigidbody variable

    private GameObject windowInstance;
    private RenderTexture renderTexture;
    private bool isRolling = false;

    void Start()
    {
        // Initialize or reset the flag at the start of the game
    }

    void Update()
    {
        if (diceRigidbody.velocity.magnitude > 0.1f)
        {
            if (!isRolling)
            {
                isRolling = true;
                CreateWindow();
                cameraMovementScript.enabled = false; // Disable camera movement
            }
        }
        else if (isRolling)
        {
            isRolling = false;
            StartCoroutine(CloseWindowAfterDelay());
        }
    }

    void CreateWindow()
    {
        if (windowInstance == null)
        {
            windowInstance = Instantiate(windowPrefab);
            renderTexture = new RenderTexture(256, 256, 16);
            diceCamera.targetTexture = renderTexture;
            windowInstance.GetComponentInChildren<RawImage>().texture = renderTexture;
            if (diceRigidbody == null)
            {
                Debug.LogError("Rigidbody component not found in dice window prefab.");
            }
        }
    }

    IEnumerator CloseWindowAfterDelay()
    {
        yield return new WaitForSeconds(closeDelay);
        turnManager.ReadDice(); // Call TurnManager's ReadDice method
        CloseWindow();
        cameraMovementScript.enabled = true; // Enable camera movement
    }

    public void StartNewTurn()
    {
        // Reset the flag at the start of each turn
    }

    void CloseWindow()
    {
        if (windowInstance != null)
        {
            Destroy(windowInstance);
            windowInstance = null;
        }
    }
}