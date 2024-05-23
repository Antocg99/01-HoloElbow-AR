using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

[ExecuteInEditMode]
public class ReferenceFrameCalculator : MonoBehaviour
{
    public GameObject marker1;
    public GameObject marker2;
    public GameObject marker3;
    public GameObject guidakwire;

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
        CalculateLocalReferenceFrame();
    }

    private void OnValidate()
    {
        // Assicurati di assegnare i tre marker nell'Editor di Unity
        if (marker1 != null && marker2 != null && marker3 != null)
        {
            CalculateLocalReferenceFrame();
        }
        else
        {
            Debug.LogError("Assicurati di assegnare i tre marker nell'Editor di Unity.");
        }
    }

    void FixedUpdate()
    {
        // Assicurati di assegnare i tre marker nell'Editor di Unity
        if (marker1 != null && marker2 != null && marker3 != null)
        {
            CalculateLocalReferenceFrame();
        }
        else
        {
            Debug.LogError("Assicurati di assegnare i tre marker nell'Editor di Unity.");
        }
    }

    void CalculateLocalReferenceFrame()
    {
        // Calcola il primo asse (X) come la differenza tra marker1 e marker2
        Vector3 xAxis = marker2.transform.position - marker1.transform.position;
        xAxis.Normalize(); // Assicura che il vettore sia normalizzato

        // Calcola il vettore perpendicolare al piano formato da marker1, marker2 e marker3 (asse Y)
        Vector3 planeNormal = Vector3.Cross(marker2.transform.position - marker1.transform.position, marker3.transform.position - marker2.transform.position);
        Vector3 zAxis = Vector3.Cross(xAxis, planeNormal);
        zAxis.Normalize(); // Assicura che il vettore sia normalizzato

        // Calcola l'asse Zeta come perpendicolare ai precedenti assi
        Vector3 yAxis = Vector3.Cross(xAxis, zAxis);
        zAxis.Normalize(); // Assicura che il vettore sia normalizzato

        // Applica la trasformazione locale basata sui nuovi assi
        guidakwire.transform.rotation = Quaternion.LookRotation(-zAxis, yAxis);
        guidakwire.transform.position = marker1.transform.position;
    }
}
