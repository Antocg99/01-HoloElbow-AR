using UnityEngine;
using UnityEngine.UI;

public class ToggleObjectWithButton : MonoBehaviour
{
    public Toggle toggleButton1;
    public GameObject targetObject1;
    public Toggle toggleButton2;
    public GameObject targetObject2;
    public Toggle toggleButton3;
    public GameObject targetObject3;

    void Start()
    {
        // Assicurati di assegnare gli oggetti target e i Toggle nell'Editor di Unity
        // Puoi farlo trascinando e rilasciando gli oggetti sugli appositi campi nell'Inspector

        // Assicurati che i Toggle abbiano un listener per l'evento di cambio di stato
        if (toggleButton1 != null)
        {
            toggleButton1.onValueChanged.AddListener((isOn) => ToggleObjectVisibility(isOn, targetObject1));
        }
        else
        {
            Debug.LogError("Assicurati di assegnare il Toggle 1 nell'Editor di Unity.");
            enabled = false; // Disabilita lo script se il Toggle non è stato assegnato
        }

        if (toggleButton2 != null)
        {
            toggleButton2.onValueChanged.AddListener((isOn) => ToggleObjectVisibility(isOn, targetObject2));
        }
        else
        {
            Debug.LogError("Assicurati di assegnare il Toggle 2 nell'Editor di Unity.");
            enabled = false; // Disabilita lo script se il Toggle non è stato assegnato
        }

        if (toggleButton3 != null)
        {
            toggleButton3.onValueChanged.AddListener((isOn) => ToggleObjectVisibility(isOn, targetObject3));
        }
        else
        {
            Debug.LogError("Assicurati di assegnare il Toggle 3 nell'Editor di Unity.");
            enabled = false; // Disabilita lo script se il Toggle non è stato assegnato
        }

        // Verifica se gli oggetti target sono stati assegnati
        if (targetObject1 == null || targetObject2 == null || targetObject3 == null)
        {
            Debug.LogError("Assicurati di assegnare tutti gli oggetti target nell'Editor di Unity.");
            enabled = false; // Disabilita lo script se uno degli oggetti non è stato assegnato
        }
    }

    void ToggleObjectVisibility(bool isOn, GameObject targetObject)
    {
        // Disattiva tutti gli altri targetObject
        if (isOn)
        {
            if (targetObject != null)
            {
                targetObject.SetActive(true);

                // Disattiva gli altri targetObject
                if (targetObject != targetObject1 && toggleButton1.isOn)
                {
                    targetObject1.SetActive(false);
                }
                if (targetObject != targetObject2 && toggleButton2.isOn)
                {
                    targetObject2.SetActive(false);
                }
                if (targetObject != targetObject3 && toggleButton3.isOn)
                {
                    targetObject3.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("L'oggetto target non è stato assegnato.");
            }
        }
        // Se il Toggle viene spento, disattiva il targetObject corrispondente
        else
        {
            if (targetObject != null)
            {
                targetObject.SetActive(false);
            }
            else
            {
                Debug.LogError("L'oggetto target non è stato assegnato.");
            }
        }
    }
}
