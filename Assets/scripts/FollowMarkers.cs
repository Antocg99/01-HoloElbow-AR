using UnityEngine;

public class FollowMarkers : MonoBehaviour
{
    public GameObject markers;  // Contiene i marker come figli
    public GameObject kwires;   // L'oggetto da muovere
    public Vector3 addVector;
    private void OnValidate()
    {
        // Verifica che ci siano almeno 4 marker come figli
        if (markers != null && markers.transform.childCount >= 4)
        {
            // Calcola la posizione media dei marker
            Vector3 averagePosition = CalculateAverageMarkerPosition();

            // Muovi kwires in base alla posizione media dei marker
            kwires.transform.position = averagePosition + addVector;

            // Mantieni l'orientamento relativo
            //kwires.transform.rotation = CalculateAverageMarkerRotation();
        }

    }
    void Update()
    {
        // Verifica che ci siano almeno 4 marker come figli
        if (markers.transform.childCount >= 4)
        {
            // Calcola la posizione media dei marker
            Vector3 averagePosition = CalculateAverageMarkerPosition();

            // Muovi kwires in base alla posizione media dei marker
            kwires.transform.position = averagePosition + addVector;
        }
        else
        {
            Debug.LogError("Attenzione: Assicurati che 'markers' contenga almeno 4 marker come figli.");
        }
    }

    Vector3 CalculateAverageMarkerPosition()
    {
        // Inizializza la somma delle posizioni dei marker
        Vector3 sumPosition = Vector3.zero;


        // Itera attraverso i figli di 'markers' e somma le loro posizioni
        for (int i = 1; i <= 4; i++)
        {
            // Assicurati che i marker siano chiamati "marker1", "marker2", ecc.
            Transform marker = markers.transform.Find("marker" + i);

            if (marker != null)
            {
                sumPosition += marker.position;
            }
            else
            {
                Debug.LogError($"Attenzione: Impossibile trovare il marker{1} come figlio di 'markers'. Assicurati che i marker siano chiamati correttamente.");
            }
        }

        // Calcola la posizione media dividendo per il numero di marker
        Vector3 averagePosition = sumPosition / 4f;

        return averagePosition;
    }
}
