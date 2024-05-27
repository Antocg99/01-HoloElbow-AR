using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class handTrackingInfo : MonoBehaviour
{
    public Vector3 indexTipPosition;
    public Quaternion indexTipQuaternion;
    public GameObject sphereMarkerPrefab;
    private GameObject markerSphereContainer;
    private GameObject marker1container;
    private GameObject marker2container;
    private GameObject marker3container;
    private GameObject indexTipContainer;
    private GameObject referenceFrameFromRealMarkers;
    private GameObject referenceFrameFromVirtualMarkers;
    private GameObject referenceFrameTraslated;
    private int counterMarker1 = 0;
    private int counterMarker2 = 0;
    private int counterMarker3 = 0;
    private GameObject marker1_average;
    private GameObject marker2_average;
    private GameObject marker3_average;
    public int acquisitionPoints = 100;
    public TextMeshProUGUI textMeshProUGUI;
    private bool acquiredMarkerAverage=false;
    private Vector3 xAxis;
    private Vector3 yAxis;
    private Vector3 zAxis;
    public GameObject virtualSystemStatic;
    private GameObject marker1virtual;
    private GameObject marker2virtual;
    private GameObject marker3virtual;
    private bool appliedTransaltionAndRotation=false;
    private Vector3 neededTranslation;
    private Quaternion neededRotation;
    public GameObject sceneContainer;
    public bool method1;
    public bool method2;
    public bool resetRegistration;
    private GameObject sphereIndexTip;
    // Start is called before the first frame update
    void Start()
    {
        markerSphereContainer = new GameObject();
        markerSphereContainer.transform.SetParent(transform);
        marker1container = new GameObject();
        marker1container.transform.SetParent(markerSphereContainer.transform);
        marker1container.name = "marker1container";
        marker2container = new GameObject();
        marker2container.transform.SetParent(markerSphereContainer.transform);
        marker2container.name = "marker2container";
        marker3container = new GameObject();
        marker3container.transform.SetParent(markerSphereContainer.transform);
        marker3container.name = "marker3container";
        sphereIndexTip = new GameObject();
        sphereIndexTip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereIndexTip.gameObject.transform.localScale = Vector3.one/100f;
        sphereIndexTip.name = "spereIndexTip";
        sphereIndexTip.SetActive(false);
        indexTipContainer = new GameObject();
        indexTipContainer.transform.SetParent(markerSphereContainer.transform);
        indexTipContainer.name = "indexTipContainer";


    }

    // Update is called once per frame
    void Update()
    {
        sphereIndexTip.SetActive(false);
        HandTracking();
        /*
        if (acquiredMarkerAverage)
        {
            Debug.DrawRay(marker1_average.transform.localPosition, xAxis, Color.red);
            Debug.DrawRay(marker2_average.transform.localPosition, yAxis, Color.green);
            Debug.DrawRay(marker3_average.transform.localPosition, zAxis, Color.blue);
        }
        */
        if(resetRegistration)
        {
            acquiredMarkerAverage = false;
            appliedTransaltionAndRotation = false;
            DestroyImmediate(referenceFrameFromRealMarkers);
            DestroyImmediate(referenceFrameFromVirtualMarkers);
            resetRegistration = false;
}
        if (appliedTransaltionAndRotation)
        {
            textMeshProUGUI.text = $"Applied:\n-translation:{neededTranslation.ToString()} \n-rotation:-\n quaternion:{neededRotation.ToString()}; -\n euler:{neededRotation.eulerAngles.ToString()} ";
        }


    }

    public void HandTracking()
    {
        if (HandJointUtils.TryGetJointPose(Microsoft.MixedReality.Toolkit.Utilities.TrackedHandJoint.IndexTip, Microsoft.MixedReality.Toolkit.Utilities.Handedness.Left, out MixedRealityPose pose))
        {
            sphereIndexTip.SetActive(true);
            sphereIndexTip.transform.position = pose.Position;
            sphereIndexTip.transform.rotation = pose.Rotation;
            if (Input.GetKey(KeyCode.A))
            {
                indexTipPosition = pose.Position;
                indexTipQuaternion = pose.Rotation;
                GameObject newObject = Instantiate(sphereMarkerPrefab, indexTipPosition, indexTipQuaternion, indexTipContainer.transform);

            }


        }


        if (HandJointUtils.TryGetJointPose(Microsoft.MixedReality.Toolkit.Utilities.TrackedHandJoint.IndexTip, Microsoft.MixedReality.Toolkit.Utilities.Handedness.Left, out  MixedRealityPose pose1) && (appliedTransaltionAndRotation==false))
        {
            indexTipPosition = pose1.Position;
            indexTipQuaternion = pose1.Rotation;
            if (counterMarker1 < acquisitionPoints)
            {
                textMeshProUGUI.text = "Marker 1 acquisition points, hold 1 \n" + "Points acquired:" +  counterMarker1.ToString();
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    GameObject newObject = Instantiate(sphereMarkerPrefab, indexTipPosition, indexTipQuaternion, marker1container.transform);
                    counterMarker1++;
                }
                    
            }
            else if (counterMarker2 < acquisitionPoints)
            {
                textMeshProUGUI.text = "Marker 2 acquisition points, hold 2 \n" + "Points acquired:" + counterMarker2.ToString();
                if (Input.GetKey(KeyCode.Alpha2))
                {
                    GameObject newObject = Instantiate(sphereMarkerPrefab, indexTipPosition, indexTipQuaternion, marker2container.transform);
                    counterMarker2++;
                }

            }
            else if (counterMarker3 < acquisitionPoints)
            {
                textMeshProUGUI.text = "Marker 3 acquisition points, hold 3 \n" + "Points acquired:" + counterMarker3.ToString();
                if (Input.GetKey(KeyCode.Alpha3))
                {
                    GameObject newObject = Instantiate(sphereMarkerPrefab, indexTipPosition, indexTipQuaternion, marker3container.transform);
                    counterMarker3++;
                }
            }
            else 
            {
                textMeshProUGUI.text = "Acqusition Point Completed, hold C to calculate average markers";
                if(Input.GetKey(KeyCode.C) && acquiredMarkerAverage==false)
                {
                    marker1_average = averagePositionRotation(marker1container);
                    marker2_average = averagePositionRotation(marker2container);
                    marker3_average = averagePositionRotation(marker3container);
                    marker1_average.name = "Marker1 averaged";
                    marker2_average.name = "Marker2 averaged";
                    marker3_average.name = "Marker3 averaged";
                    acquiredMarkerAverage = true;
                    referenceFrameFromRealMarkers = new GameObject();
                    referenceFrameFromRealMarkers.transform.SetParent(transform);
                    CalculateLocalReferenceFrame(referenceFrameFromRealMarkers, marker1_average, marker2_average, marker3_average);
                    referenceFrameFromRealMarkers.name = "referenceFrameFromRealMarkers";

                    marker1container.SetActive(false);
                    marker2container.SetActive(false);
                    marker3container.SetActive(false);

                    referenceFrameFromVirtualMarkers = new GameObject();
                    referenceFrameFromVirtualMarkers.transform.SetParent(transform);
                    referenceFrameFromVirtualMarkers.name = "referenceFrameFromVirtualMarkers";
                    VirtualReferenceFrameCalc(referenceFrameFromVirtualMarkers);

                    referenceFrameTraslated = new GameObject();
                    referenceFrameTraslated.transform.SetParent(transform);
                    referenceFrameTraslated.name = "referenceFrameTraslated";
                    VirtualReferenceFrameCalc(referenceFrameTraslated);

                    sceneContainer.transform.SetParent(referenceFrameTraslated.transform);
                    AlignReferenceSystems(referenceFrameTraslated, referenceFrameFromRealMarkers, out neededTranslation, out neededRotation);

                    appliedTransaltionAndRotation = true;
                }
            }


        }
    }

    GameObject averagePositionRotation(GameObject markersContainer)
    {
        Transform[] children = markersContainer.GetComponentsInChildren<Transform>();
        Vector3 totalPosition = Vector3.zero;
        Vector3 totalRotation = Vector3.zero;

        foreach (Transform child in children)
        {
            totalPosition += child.position;
            totalRotation += child.eulerAngles; 
        }

        Vector3 averagePosition = totalPosition/children.Length;
        Vector3 averageRotation = totalRotation/children.Length;

        GameObject newObject = Instantiate(sphereMarkerPrefab, averagePosition, Quaternion.Euler(averageRotation), markerSphereContainer.transform);
        return newObject;
    }

    void CalculateLocalReferenceFrame(GameObject referenceFrame, GameObject marker1, GameObject marker2, GameObject marker3)
    {
        // Calcola il primo asse (X) come la differenza tra marker1 e marker2
        xAxis = marker2.transform.localPosition - marker1.transform.localPosition;
        xAxis.Normalize(); // Assicura che il vettore sia normalizzato

        // Calcola il vettore perpendicolare al piano formato da marker1, marker2 e marker3 (asse Y)
        Vector3 planeNormal = Vector3.Cross(marker2.transform.localPosition - marker1.transform.localPosition, marker3.transform.localPosition - marker2.transform.localPosition);
        zAxis = Vector3.Cross(xAxis, planeNormal);
        zAxis.Normalize(); // Assicura che il vettore sia normalizzato

        // Calcola l'asse Zeta come perpendicolare ai precedenti assi
        yAxis = Vector3.Cross(xAxis, zAxis);
        yAxis.Normalize(); // Assicura che il vettore sia normalizzato

        // Applica la trasformazione locale basata sui nuovi assi
        referenceFrame.transform.localRotation = Quaternion.LookRotation(-xAxis, zAxis);
        referenceFrame.transform.localPosition = marker1.transform.localPosition;

        Debug.DrawRay(marker1.transform.localPosition, xAxis, Color.red);
        Debug.DrawRay(marker1.transform.localPosition, yAxis, Color.green);
        Debug.DrawRay(marker1.transform.localPosition, zAxis, Color.blue);
        Debug.DrawRay(marker1.transform.localPosition, planeNormal, Color.black);
    }

    public void VirtualReferenceFrameCalc(GameObject referenceFrameFromVirtualMarkers)
    {

        foreach (Transform child in virtualSystemStatic.transform)
        {

            Debug.Log($"Name chils VirtualSystem Static: {child.name}");
            switch (child.name)
            {
                case "marker1":
                    marker1virtual = child.gameObject;
                    break;
                case "marker2":
                    marker2virtual = child.gameObject;
                    break;
                case "marker3":
                    marker3virtual = child.gameObject;
                    break;
            }
        }
        CalculateLocalReferenceFrame(referenceFrameFromVirtualMarkers, marker1virtual, marker2virtual, marker3virtual);
    }

    private void AlignReferenceSystems(GameObject virtualReferenceSystem, GameObject realReferenceSystem, out Vector3 translation, out Quaternion rotation)
    {
        translation = Vector3.zero;
        rotation = Quaternion.identity;

        if(method1)
        {
            translation = virtualReferenceSystem.transform.position - realReferenceSystem.transform.position;
            virtualReferenceSystem.transform.position -= translation;

            rotation = virtualReferenceSystem.transform.rotation * Quaternion.Inverse(realReferenceSystem.transform.rotation);
            virtualReferenceSystem.transform.rotation = rotation;
        }
        else if(method2)
        {
            virtualReferenceSystem.transform.position = realReferenceSystem.transform.position;
            virtualReferenceSystem.transform.rotation = realReferenceSystem.transform.rotation;
            virtualReferenceSystem.transform.localPosition = realReferenceSystem.transform.localPosition;
            //virtualReferenceSystem.transform.localRotation = realReferenceSystem.transform.localRotation;
        }

    }

    Quaternion CalculateAlignmentRotation(Transform source, Transform target)
    {
        // Creare matrici di rotazione dai vettori di asse
        Quaternion fromSource = Quaternion.LookRotation(source.forward, source.up);
        Quaternion toTarget = Quaternion.LookRotation(target.forward, target.up);

        // Calcolare il quaternion di rotazione che allinea source con target
        return Quaternion.Inverse(fromSource) * toTarget;
    }


    Quaternion CalculateAlignmentRotation2(Transform source, Transform target)
    {
        // Costruisci matrici di rotazione dai vettori degli assi
        Matrix4x4 sourceMatrix = Matrix4x4.identity;
        sourceMatrix.SetColumn(0, source.right);
        sourceMatrix.SetColumn(1, source.up);
        sourceMatrix.SetColumn(2, source.forward);

        Matrix4x4 targetMatrix = Matrix4x4.identity;
        targetMatrix.SetColumn(0, target.right);
        targetMatrix.SetColumn(1, target.up);
        targetMatrix.SetColumn(2, target.forward);

        // Calcola la matrice di trasformazione che mappa source in target
        Matrix4x4 transformMatrix = targetMatrix * sourceMatrix.inverse;

        // Converti la matrice di trasformazione in un quaternion
        return transformMatrix.rotation;
    }


}
