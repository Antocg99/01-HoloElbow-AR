using UnityEngine;

using UnityEditor;
using Unity.Mathematics;
//#endif
[ExecuteInEditMode]
public class DrawTarget : MonoBehaviour
{
    public Transform object1;
    private Transform object2;
    public Transform kWiresTarget;
    public Vector3 relRot;
    public Vector3 relPos;
    public RectTransform directionTargetRectTransform;
    public float multiplicator1 = 1;
    public RectTransform depthTargetRectTransform;
    public float multiplicator2 = 6;
    public GameObject cilinderOrientation;
    //#if UNITY_EDITOR

    private void OnEnable()
    {
        // Assicurati di chiamare la funzione anche quando l'Editor è in Play Mode
        EditorApplication.update += OnEditorUpdate;
    }

    private void OnDisable()
    {
        // Rimuovi la funzione di aggiornamento quando lo script è disabilitato
        EditorApplication.update -= OnEditorUpdate;
    }

    private void OnEditorUpdate()
    {
        CalculateRelativeTransform();
    }

    private void OnValidate()
    {
        // Assicurati di assegnare i tre marker nell'Editor di Unity
        if (object1 != null && object2 != null)
        {
            object2 = GetActiveChildTransform(kWiresTarget);
            CalculateRelativeTransform();
        }
    }

    private void Update()
    {
        object2 = GetActiveChildTransform(kWiresTarget);
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

        Vector3 currentPosition = new Vector3(relativePosition[2], relativePosition[1], 0f);

        // Set the new position
        directionTargetRectTransform.anchoredPosition3D = multiplicator1 * currentPosition;

        Vector3 currentPosition1 = new Vector3(0f, relativePosition[0], 0f);
        depthTargetRectTransform.anchoredPosition3D = multiplicator2 * currentPosition1;
        cilinderOrientation.transform.rotation = relativeRotation;
    }

    Transform GetActiveChildTransform(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.activeSelf)
            {
                return child;
            }
        }
        return null; // Nessun oggetto figlio è attivo
    }

}
