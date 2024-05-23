using itk.simple;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

[ExecuteInEditMode]
public class ReferenceFrameCalculatorGeneric : MonoBehaviour
{
    public GameObject marker1;
    public GameObject marker2;
    public GameObject marker3;
    public GameObject guide;
    public Quaternion guideRotation;
    public Quaternion tempRot;
    public Vector3 diff;
    public GameObject referenceFrame;
    private Vector3 referenceFrameStartPosition;
    private Vector3 deltaPosition;


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

    private void Start()
    {
        referenceFrameStartPosition = referenceFrame.transform.position;
    }

    void Update()
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
        guideRotation = guide.transform.rotation;
        tempRot = Quaternion.LookRotation(-zAxis, yAxis);
        transform.localRotation = tempRot;
        transform.localPosition = marker1.transform.localPosition;

        Debug.DrawRay(marker1.transform.localPosition, xAxis, Color.red);
        Debug.DrawRay(marker1.transform.localPosition, yAxis, Color.green);
        Debug.DrawRay(marker1.transform.localPosition, zAxis, Color.blue);
        Debug.DrawRay(marker1.transform.localPosition, planeNormal, Color.black);

        Vector3 marker1_gloabal = marker1.transform.TransformPoint(Vector3.zero);
        Debug.DrawRay(marker1_gloabal, xAxis, Color.red);
        Debug.DrawRay(marker1_gloabal, yAxis, Color.green);
        Debug.DrawRay(marker1_gloabal, zAxis, Color.blue);
        Debug.DrawRay(marker1_gloabal, planeNormal, Color.black);
    }
}
