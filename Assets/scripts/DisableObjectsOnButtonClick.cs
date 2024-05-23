using UnityEngine;

public class ToggleObjectsOnClick : MonoBehaviour
{
    public GameObject[] objectsToToggle; // Array di oggetti da attivare/disattivare
    private bool areObjectsActive = true; // Stato corrente degli oggetti

    public void ToggleObjects()
    {
        // Cambia lo stato di visibilità degli oggetti
        areObjectsActive = !areObjectsActive;

        // Attiva o disattiva gli oggetti nel array in base allo stato corrente
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(areObjectsActive);
        }
    }
}
