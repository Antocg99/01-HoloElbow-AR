using UnityEngine;

public class PositionRotationSetter : MonoBehaviour
{
    // Public variables for position and rotation
    public GameObject mainCamera;
    public Vector3 offsetPosition = new Vector3(0f, 0f, 0.5f);
    public Vector3 rotationEuler = new Vector3(0f, 90f, 0f);
    public float translationSpeed = 1f;
    public Transform phantomReferenceFrameChild;
    public bool activatePositioningFunction;
    // Update is called once per frame
    void Update()
    {
        if (activatePositioningFunction)
        {
            SetPositionAndRotation();
            activatePositioningFunction = false;

        }

    }
    public void SetPositionAndRotation()
    {
        //Vector3 traslationDiff = phantomReferenceFrameChild.TransformPoint(Vector3.zero) - offsetPosition;
        Vector3 traslationDiff = mainCamera.transform.position - phantomReferenceFrameChild.position + offsetPosition;
        //Vector3 targetPosition = Vector3.Lerp(referenceFrameGlobalPosition, offsetPosition, translationSpeed);


        transform.position += traslationDiff;

        Quaternion currentRotation = mainCamera.transform.rotation;
        Quaternion newRotation = currentRotation * Quaternion.Euler(rotationEuler);
        transform.rotation = newRotation;
    }
}
