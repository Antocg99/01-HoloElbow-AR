using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust this variable to control the rotation speed.

    void Update()
    {
        // Rotation around the x-axis
        if (Input.GetKey(KeyCode.A))
        {
            RotateObject(Vector3.right, true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateObject(Vector3.right, false);
        }

        // Rotation around the y-axis
        if (Input.GetKey(KeyCode.W))
        {
            RotateObject(Vector3.up, true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            RotateObject(Vector3.up, false);
        }
    }

    void RotateObject(Vector3 axis, bool clockwise)
    {
        // Determine the rotation direction based on the passed parameter.
        float direction = clockwise ? 1f : -1f;

        // Calculate the rotation angle based on the speed and the current frame.
        float rotationAngle = rotationSpeed * Time.deltaTime * direction;

        // Apply the rotation to the object around the specified axis.
        transform.Rotate(axis, rotationAngle);
    }
}
