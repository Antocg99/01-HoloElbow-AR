using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

[ExecuteInEditMode]
public class ReferenceFrameCalculatorStand : MonoBehaviour
{
    public GameObject marker1;
    public GameObject marker2;
    public GameObject marker3;

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
        Vector3 xAxis = marker2.transform.localPosition - marker1.transform.localPosition;
        xAxis.Normalize(); // Assicura che il vettore sia normalizzato

        // Calcola il vettore perpendicolare al piano formato da marker1, marker2 e marker3 (asse Y)
        Vector3 planeNormal = Vector3.Cross(marker2.transform.localPosition - marker1.transform.localPosition, marker3.transform.localPosition - marker2.transform.localPosition);
        Vector3 zAxis = Vector3.Cross(xAxis, planeNormal);
        zAxis.Normalize(); // Assicura che il vettore sia normalizzato

        // Calcola l'asse Zeta come perpendicolare ai precedenti assi
        Vector3 yAxis = Vector3.Cross(xAxis, zAxis);
        zAxis.Normalize(); // Assicura che il vettore sia normalizzato

        // Applica la trasformazione locale basata sui nuovi assi
        transform.localRotation = Quaternion.LookRotation(-xAxis, zAxis);
        transform.localPosition = marker1.transform.localPosition;

        Debug.DrawRay(marker1.transform.localPosition, xAxis,  Color.red);
        Debug.DrawRay(marker1.transform.localPosition, yAxis, Color.green);
        Debug.DrawRay(marker1.transform.localPosition, zAxis, Color.blue);
        Debug.DrawRay(marker1.transform.localPosition, planeNormal, Color.black);
    }
}
