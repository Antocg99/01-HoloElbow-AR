using UnityEngine;
using UnityEditor;

public class RelativeTransformCalculator : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public Vector3 relRot;
    public Vector3 relPos;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        CalculateRelativeTransform();
    }

    void OnDrawGizmosSelected()
    {
        CalculateRelativeTransform();
    }

    private void Update()
    {
        CalculateRelativeTransform();
    }

    void CalculateRelativeTransform()
    {
        if (object1 == null || object2 == null)
            return;

        // Calcola la rotazione relativa
        Quaternion relativeRotation = Quaternion.Inverse(object1.rotation) * object2.rotation;

        // Calcola la posizione relativa
        Vector3 relativePosition = Quaternion.Inverse(object1.rotation) * (object2.position - object1.position);

        relRot = relativeRotation.eulerAngles;
        relPos = relativePosition;

        // Stampa i risultati
        Handles.Label(object1.position, "Rotazione Relativa: " + relativeRotation.eulerAngles);
        Handles.Label(object1.position + new Vector3(0, -0.1f, 0), "Posizione Relativa: " + relativePosition);
    }
#endif
}
