using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class UnusedScriptsChecker : EditorWindow
{
    [MenuItem("Tools/Check Unused Scripts")]
    public static void CheckUnusedScripts()
    {
        // Specifica la scena di riferimento
        string referenceScenePath = "Assets/scenes/HoloElbowPc_xray_simulation.unity";
        string scriptsFolderPath = "Assets/scripts";
        string resultFilePath = "Assets/UnusedScriptsReport3.txt";

        // Apri la scena di riferimento
        EditorSceneManager.OpenScene(referenceScenePath, OpenSceneMode.Single);

        // Ottieni tutti i file di script nella cartella specificata
        string[] scriptFiles = Directory.GetFiles(scriptsFolderPath, "*.cs", SearchOption.AllDirectories);

        // Crea un dizionario per tracciare l'uso degli script
        Dictionary<string, bool> scriptUsage = new Dictionary<string, bool>();

        // Inizializza tutti gli script come non utilizzati
        foreach (string scriptFile in scriptFiles)
        {
            string scriptName = Path.GetFileNameWithoutExtension(scriptFile);
            scriptUsage[scriptName] = false;
        }

        // Ottieni tutti i prefab e le scene nel progetto
        string[] prefabFiles = Directory.GetFiles("Assets", "*.prefab", SearchOption.AllDirectories);
        string[] sceneFiles = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories);

        // Controlla l'uso nei prefab
        foreach (string prefabFile in prefabFiles)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabFile);
            CheckGameObjectForScripts(prefab, scriptUsage);
        }

        // Controlla l'uso nelle scene
        foreach (string sceneFile in sceneFiles)
        {
            if (sceneFile != referenceScenePath)
            {
                EditorSceneManager.OpenScene(sceneFile, OpenSceneMode.Additive);
                GameObject[] rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
                foreach (GameObject rootObject in rootObjects)
                {
                    CheckGameObjectForScripts(rootObject, scriptUsage);
                }
                EditorSceneManager.CloseScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene(), true);
            }
        }

        // Stampa gli script non utilizzati in un file .txt
        using (StreamWriter writer = new StreamWriter(resultFilePath))
        {
            writer.WriteLine("Unused Scripts:");
            foreach (var script in scriptUsage)
            {
                if (!script.Value)
                {
                    writer.WriteLine(script.Key);
                }
            }
        }

        Debug.Log($"Unused scripts report saved to {resultFilePath}");

        // Chiudi la scena di riferimento
        EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByPath(referenceScenePath), true);
    }

    private static void CheckGameObjectForScripts(GameObject obj, Dictionary<string, bool> scriptUsage)
    {
        if (obj == null) return;

        MonoBehaviour[] scripts = obj.GetComponentsInChildren<MonoBehaviour>(true);
        foreach (var script in scripts)
        {
            if (script != null)
            {
                string scriptName = script.GetType().Name;
                if (scriptUsage.ContainsKey(scriptName))
                {
                    scriptUsage[scriptName] = true;
                }
            }
        }
    }
}
