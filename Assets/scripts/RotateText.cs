using UnityEngine;
using TMPro;

public class RotateText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;  // Reference to your TextMeshPro component
    private float initRotation;

    private void Start()
    {
        initRotation = transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        if (textMeshPro != null)
        {
            // Get the rotation in the x-axis from the current object
            float rotationX = initRotation - transform.rotation.eulerAngles.x;

            // Update the TextMeshPro text with the rotation value
            textMeshPro.text = "Rotation X: " + rotationX.ToString("F2");  // F2 to format to two decimal places
        }
        else
        {
            Debug.LogWarning("Please assign textMeshPro in the inspector.");
        }
    }
}
