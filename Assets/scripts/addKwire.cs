using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addKwire : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public GameObject kwirecontainer;
    public Vector3 newkwireoffset = new Vector3(0.1f, 0f, 0f);
    public float factor=2;
    public GameObject lookDirectionMarker;
    public GameObject lookOriginMarker;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //LogGazeDirectionOrigin();


    }

    public void instantiateKWire()
    {
        GameObject newkwire = Instantiate(prefab);
        newkwire.transform.parent = kwirecontainer.transform;
        newkwire.transform.localScale = new Vector3(10f, 10f, 10f);
        newkwire.transform.position = transform.position + newkwireoffset;

        BoxCollider boxcollidercomponent = newkwire.AddComponent<BoxCollider>();
        boxcollidercomponent.size = new Vector3(boxcollidercomponent.size.x, boxcollidercomponent.size.y * factor, boxcollidercomponent.size.z * factor); 
        ObjectManipulator objectmanipulatorcomponenent = newkwire.AddComponent<ObjectManipulator>();
        NearInteractionGrabbable nearinteractiongrabablecomponent = newkwire.AddComponent<NearInteractionGrabbable>();
        
    }

    public void LogGazeDirectionOrigin()
    {
        //Vector3 lookDirection = CoreServices.InputSystem.GazeProvider.GazeDirection;
        Vector3 lookDirection = CoreServices.InputSystem.EyeGazeProvider.HitPosition;
        Debug.Log("Gaze is looking in direction:" + lookDirection);
        lookDirectionMarker.transform.position = lookDirection;

        //Vector3 gazeOrigin = CoreServices.InputSystem.GazeProvider.GazeOrigin;
        Vector3 gazeOrigin = CoreServices.InputSystem.EyeGazeProvider.GazeOrigin + CoreServices.InputSystem.EyeGazeProvider.GazeDirection.normalized; 
        Debug.Log("Gaze origin is:" + gazeOrigin);
        lookOriginMarker.transform.position = gazeOrigin;
    }
}
