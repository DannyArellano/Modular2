using Unity.VisualScripting;
using UnityEngine;

public class ObjetoMover : MonoBehaviour
{
    private Player player; // Reference to the Player object
    private GameObject selectedObject; // Reference to the selected object
    public Camera mainCamera;
    public TurnManager turnManager;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    /* public void SelectObject()
    {
        if (selectedObject != null)
        {
            player = selectedObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Selected object does not have a Player component!");
            }
        }
    }

    public void MoveObject(Vector3 newPosition)
    {
        if (player != null)
        {
            player.isOnTile = false; // Set isOnTile to false when moving the player
            player.transform.position = newPosition; // Move the player to the new position
        }
        else
        {
            Debug.LogError("Player is not selected!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == ("PlayerUnit") && hit.collider.GetComponent<DeterminarTipoUnidad>().playerID == turnManager.GetCurrentPlayerTurn())
                {
                    // Select the clicked object
                    selectedObject = hit.collider.gameObject;
                    selectedObject.GetComponent<Rigidbody>().isKinematic = true;
                    selectedObject.transform.position += Vector3.up;
                    SelectObject(); // Call the new SelectObject method
                }
            }
        }

        // If we have a selected object and the left button is held down
        if (selectedObject != null && Input.GetMouseButton(0))
        {
            MoveObject();
        }

        // Release the object when the left button is released
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObject != null)
            {
                selectedObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            selectedObject = null;
        }
    }

    // Function to move the object along the X and Z axes in the plane projection
    private void MoveObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Create a plane on which the object will move (XZ, Y = height of the object)
        Plane plane = new Plane(Vector3.up, new Vector3(0, selectedObject.transform.position.y, 0));

        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 newPosition = ray.GetPoint(distance);
            MoveObject(newPosition); // Call the MoveObject method with the new position
        }
    } */
}
