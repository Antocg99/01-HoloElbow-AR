using UnityEngine;
using TMPro;

public class CameraController : MonoBehaviour
{
    public Camera xray_front;
    public Camera xray_lat;

    public TextMeshProUGUI text_pic_counter;
    public TextMeshProUGUI cArmStatus;
    private int pic_counter = 0;

    public TextMeshProUGUI text_kwire_tracking;

    public GameObject[] kwires; // Array di K wires

    public GameObject arrowCamLat;
    public GameObject arrowCamFront;


    private float last_pic_time;

    void Start()
    {
        if (xray_front == null || xray_lat == null)
        {
            Debug.LogWarning("Camera not set! Drag and drop a camera into the script component.");
            enabled = false; // Disabilita lo script se la camera non è impostata
        }
        else
        {
            // Disabilita la camera inizialmente
            xray_front.enabled = false;
            xray_lat.enabled = false;
        }
        arrowCamFront.SetActive(false);
        arrowCamLat.SetActive(false);
        text_pic_counter.text = "scintigraphy count: " + pic_counter.ToString();
        text_kwire_tracking.text = "not tracked";
        text_kwire_tracking.color = Color.red;

        last_pic_time = Time.time;
    }

    void Update()
    {
        foreach (GameObject kwire in kwires)
        {
            if (!kwire.activeSelf)
            {
                text_kwire_tracking.text = "kwire not active";
                text_kwire_tracking.color = Color.red;
            }
            else if (Time.time - last_pic_time < 0.3f)
            {
                text_kwire_tracking.text = "xray cooldown";
                text_kwire_tracking.color = Color.yellow;
            }
            else
            {
                text_kwire_tracking.text = "kwire active";
                text_kwire_tracking.color = Color.green;

                if (Input.GetKey(KeyCode.F))
                {
                    xray_front.enabled = true;
                    xray_lat.enabled = false;
                    last_pic_time = Time.time;
                    pic_counter += 1;
                    text_pic_counter.text = "scintigraphy count: " + pic_counter.ToString();
                    arrowCamFront.SetActive(!arrowCamFront.activeSelf);
                    arrowCamLat.SetActive(false);
                    cArmStatus.text = "Upper View";
                }

                if (Input.GetKey(KeyCode.L))
                {
                    xray_front.enabled = false;
                    xray_lat.enabled = true;
                    last_pic_time = Time.time;
                    pic_counter += 1;
                    text_pic_counter.text = "scintigraphy count: " + pic_counter.ToString();
                    arrowCamFront.SetActive(false);
                    arrowCamLat.SetActive(!arrowCamLat.activeSelf);
                    cArmStatus.text = "";
                }
            }
        }
    }
}
