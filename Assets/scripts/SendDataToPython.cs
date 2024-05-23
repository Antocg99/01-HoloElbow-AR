using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection.Emit;

public class SendDataToPython : MonoBehaviour
{
    UdpSocket udpSocket;

    public void QuitApp()
    {
        print("Quitting");
        Application.Quit();
    }

    public void SendToPython()
    {
        string stringToSend = "reference_frame_positioning";
        udpSocket.SendData(stringToSend);
    }

    private void Start()
    {
        udpSocket = FindObjectOfType<UdpSocket>();
    }

    void Update()
    {

    }
}


