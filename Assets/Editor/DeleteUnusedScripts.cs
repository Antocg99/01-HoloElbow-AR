using System.IO;
using UnityEditor;
using UnityEngine;

public class DeleteUnusedScripts : EditorWindow
{
    private static readonly string[] scriptsToDelete = new string[]
    {
        "ActivateDeactivateCamera",
        "CountdownTimer",
        "DisableObjectsOnButtonClick",
        "DrawTarget",
        "ExitButtonScript",
        "FollowMarkers",
        "GetDeviceIPAddressScript",
        "HSymmetry",
        "InsertkWire",
        "Laser",
        "MoveDashedLines",
        "MoveDashedLinesRect",
        "MoveForward",
        "ObjectRotation",
        "ReferenceFrameCalculator",
        "RelativePosition",
        "RelativeTransformCalculator",
        "RotateText",
        "SceneLoader",
        "SimpleTarget",
        "TextMeshProInput",
        "ToggleObjectWithButton",
        "TransformRecorder",
        "xray-machine"
    };

    [MenuItem("Tools/Delete Unused Scripts")]
    public static void DeleteScripts()
    {
        string scriptsFolderPath = "Assets/Scripts";

        foreach (string scriptName in scriptsToDelete)
        {
            string scriptPath = Path.Combine(scriptsFolderPath, scriptName + ".cs");
            if (File.Exists(scriptPath))
            {
                AssetDatabase.DeleteAsset(scriptPath);
                Debug.Log($"Deleted script: {scriptPath}");
            }
            else
            {
                Debug.LogWarning($"Script not found: {scriptPath}");
            }
        }

        // Refresh the AssetDatabase to reflect changes
        AssetDatabase.Refresh();
    }
}
