using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // Movement in the x and z planes
        float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        transform.Translate(movement, Space.World);

        // Rotation around the y-axis only when right mouse button is held
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float rotateHorizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotateVertical = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(rotateVertical, rotateHorizontal, 0);
        }
    }
}
