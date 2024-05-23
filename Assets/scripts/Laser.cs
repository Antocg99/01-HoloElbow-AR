using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    public GameObject startPoint;
    private void Awake()
    {

        lr = startPoint.GetComponent<LineRenderer>();
        lr.SetPosition(0, startPoint.transform.position);
        lr.SetPosition(1, startPoint.transform.up * 10);

    }
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = startPoint.GetComponent<LineRenderer>();
        lr.SetPosition(0, startPoint.transform.position);
        lr.SetPosition(1, startPoint.transform.up * 10);
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, startPoint.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(startPoint.transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, startPoint.transform.up * 5000);
    }
    
}
