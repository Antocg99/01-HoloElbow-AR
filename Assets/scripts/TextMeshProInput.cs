using UnityEngine;
using TMPro;

public class TextMeshProUIInput : MonoBehaviour
{
    public TMP_InputField inputField; // Trascina il componente TMP_InputField nella tua scena nell'Editor Unity

    private string inputText = ""; // Variabile per memorizzare il testo

    private void Start()
    {
        // Registra un listener per il cambiamento del testo nell'input field
        inputField.onValueChanged.AddListener(OnTextChange);
    }

    private void OnTextChange(string newText)
    {
        inputText = newText;
    }

    // Esempio di come puoi utilizzare il testo memorizzato
    public void UseInputText()
    {
        Debug.Log("Testo memorizzato: " + inputText);
    }
}
