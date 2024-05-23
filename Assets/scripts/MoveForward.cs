using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 40.0f;
    public GameObject projectilePrefab;

    // Update is called once per frame
    void Update()
    {
        // Move the GameObject forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check if the projectilePrefab is not null and is not active in the scene
        // if (projectilePrefab != null && !projectilePrefab.activeSelf)

            // Instantiate a new projectilePrefab at the current position and rotation
        Instantiate(projectilePrefab, transform.position, transform.rotation);

    }
}
