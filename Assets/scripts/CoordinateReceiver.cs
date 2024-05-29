using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using System.Globalization;

public class CoordinateReceiver : MonoBehaviour
{
    string tempStr = string.Empty;
    UdpSocket udpSocket;

    [Header("Add here guide markers")]
    public GameObject marker1_guide;
    public GameObject marker2_guide;
    public GameObject marker3_guide;
    public GameObject marker4_guide;
    [Header("Add here stend markers")]
    public GameObject marker1_stend;
    public GameObject marker2_stend;
    public GameObject marker3_stend;
    public GameObject marker4_stend;

    public GameObject referenceFrameGameObject;
    public GameObject guideFrameGameObject;
    public TextMeshProUGUI textStatusGuide1;
    public TextMeshProUGUI textStatusGuide2;
    // Start is called before the first frame update
    void Start()
    {
        udpSocket = FindObjectOfType<UdpSocket>();
    }
    public void UpdatePythonData(string str)
    {
        tempStr = str;
    }

    void FixedUpdate()
    {
        string dataToProcess = tempStr;
        //Debug.Log(tempStr);
        markerDataDecod markerData = JsonUtility.FromJson<markerDataDecod>(dataToProcess);
        Vector3 m1Vector = new Vector3(markerData.m1[0], markerData.m1[1], markerData.m1[2]);
        Vector3 m2Vector = new Vector3(markerData.m2[0], markerData.m2[1], markerData.m2[2]);
        Vector3 m3Vector = new Vector3(markerData.m3[0], markerData.m3[1], markerData.m3[2]);
        Vector3 m4Vector = new Vector3(markerData.m4[0], markerData.m4[1], markerData.m4[2]);

        if (markerData.type == "guide")
        {
            marker1_guide.transform.position = m1Vector;
            marker2_guide.transform.position = m2Vector;
            marker3_guide.transform.position = m3Vector;
            marker4_guide.transform.position = m4Vector;
            textStatusGuide1.text = "<color=green>kWires Status: Tracked</color>";
            textStatusGuide2.text = "<color=green>kWires Status: Tracked</color>";
            referenceFrameGameObject.GetComponent<ReferenceFrameCalculatorStand>().enabled = false;
            guideFrameGameObject.GetComponent<ReferenceFrameCalculatorGeneric>().enabled = true;
        }
        if (markerData.type == "untracked")
        {
            textStatusGuide1.text = "<color=red>kWires Status: Untracked</color>";
            textStatusGuide2.text = "<color=red>kWires Status: Untracked</color>";
        }
        if (markerData.type == "phantom")
        {
            marker1_stend.transform.position = m1Vector;
            marker2_stend.transform.position = m2Vector;
            marker3_stend.transform.position = m3Vector;
            marker4_stend.transform.position = m4Vector;
            referenceFrameGameObject.GetComponent<ReferenceFrameCalculatorStand>().enabled = true;
            guideFrameGameObject.GetComponent<ReferenceFrameCalculatorGeneric>().enabled = false;

        }

        if (markerData.type == "phantomDef")
        {
            marker1_stend.transform.position = m1Vector;
            marker2_stend.transform.position = m2Vector;
            marker3_stend.transform.position = m3Vector;
            marker4_stend.transform.position = m4Vector;
            referenceFrameGameObject.GetComponent<ReferenceFrameCalculatorStand>().enabled = false;
            guideFrameGameObject.GetComponent<ReferenceFrameCalculatorGeneric>().enabled = true;
        }
    }
}

public class markerDataDecod
{
    public string type;
    public float[] m1;
    public float[] m2;
    public float[] m3;
    public float[] m4;
}


