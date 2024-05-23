using System.Net.NetworkInformation;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class GetDeviceIPAddressScript : MonoBehaviour
{
    public TextMeshProUGUI ipAddressText1;

    void Start()
    {
        string ipAddress = GetDeviceIPAddress();
        Debug.Log("Device IP Address: " + ipAddress);
    }

    void Update()
    {
        string ipAddress = GetDeviceIPAddress();
        Debug.Log("Device IP Address: " + ipAddress);
    }

    public string GetDeviceIPAddress()
    {
        string ipAddress1 = string.Empty; // Dichiarato qui per garantire la disponibilità in tutte le casistiche

        NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

        NetworkInterface wirelessInterface = networkInterfaces.FirstOrDefault(
            ni => ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && ni.OperationalStatus == OperationalStatus.Up);

        if (wirelessInterface != null)
        {
            UnicastIPAddressInformationCollection ipAddresses1 = wirelessInterface.GetIPProperties().UnicastAddresses;
            if (ipAddresses1.Count > 1)
            {
                ipAddress1 = ipAddresses1[1].Address.ToString();
                ipAddressText1.text = ipAddress1;
                return ipAddress1;
            }
            else
            {
                Debug.LogWarning("Nessun indirizzo IP disponibile per l'interfaccia wireless.");
            }
        }
        else
        {
            Debug.LogWarning("Nessuna interfaccia wireless attiva trovata.");
        }

        return ipAddress1;
    }
}
