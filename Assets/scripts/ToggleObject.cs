using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleObject : MonoBehaviour
{
    public GameObject objectToToggle;
    public TextMeshPro debugText;
    public string nameObject;

    // Metodo da chiamare quando si preme il pulsante
    private void Start()
    {
        nameObject = debugText.text;


        if (objectToToggle.activeSelf == true)
        {
            debugText.text = nameObject + ": active.\n";
        }
        else
        {
            debugText.text = nameObject + ": not active.\n";
        }

    }
    public void ToggleGameObject()
    {
        // Verifica se l'oggetto è attivo o meno
        bool isActive = objectToToggle.activeSelf;

        // Attiva o disattiva l'oggetto in base al suo stato attuale
        objectToToggle.SetActive(!isActive);

        if (objectToToggle.activeSelf == true)
        {
                debugText.text = nameObject + ": active\n";
        }
        else
        {
                debugText.text = nameObject + ": not active\n";
         }
  }
}
