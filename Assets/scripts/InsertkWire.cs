using UnityEngine;
using TMPro; 

public class InsertkWire : MonoBehaviour
{
    public Transform targetParent; // Il genitore in cui copiare l'oggetto figlio
    public GameObject objectToCopy; // L'oggetto figlio da copiare
    public TextMeshProUGUI text_kWire_counter;
    private int kWire_counter = 0;
    void Update()
    {
        // Verifica se il tasto I è premuto
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Controlla se l'oggetto da copiare è assegnato
            if (objectToCopy != null)
            {
                kWire_counter += 1;
                text_kWire_counter.text = "kWires count: " + kWire_counter.ToString();
                // Copia l'oggetto figlio nella posizione specifica del genitore
                CopyChildObject(objectToCopy, targetParent);
            }
            else
            {
                Debug.LogError("Object to copy not assigned!");
            }
        }
    }

    void CopyChildObject(GameObject originalObject, Transform parentTransform)
    {
        // Crea una copia dell'oggetto figlio
        GameObject copiedObject = Instantiate(originalObject);

        // Imposta il genitore della copia
        copiedObject.transform.parent = parentTransform;

        // Imposta la posizione della copia uguale alla posizione dell'oggetto originale
        copiedObject.transform.position = originalObject.transform.position;

        // Imposta la rotazione della copia uguale alla rotazione dell'oggetto originale
        copiedObject.transform.rotation = originalObject.transform.rotation;
    }
}
