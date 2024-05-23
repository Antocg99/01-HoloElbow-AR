using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ScriptActivatorMRTK : MonoBehaviour
{
    // List of scripts to activate/deactivate
    public List<MonoBehaviour> scriptsToToggle;

    // Reference to a TextMeshPro object for debugging
    public TextMeshPro debugText;


    // Function to toggle the activation state of scripts
    public void ToggleScripts()
    {
        foreach (var script in scriptsToToggle)
        {
            // Toggle the activation state of the script
            script.enabled = !script.enabled;

            // Log the activation state of the script
            if (debugText != null)
            {
                if (script.enabled)
                {
                    debugText.text = "Position lock: active\n";
                }
                else
                {
                    debugText.text = "Position lock: not active\n";
                }
            }
        }
    }
}
