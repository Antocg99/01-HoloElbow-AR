using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class SceneComparer : MonoBehaviour
{
    [MenuItem("Tools/Compare Scenes")]
    public static void CompareScenes()
    {
        string scenePath1 = "Assets/scenes/HoloElbowStableVersions/HoloElbowPc_stable1.unity"; // Percorso della prima scena
        string scenePath2 = "Assets/scenes/HoloElbowPc_xray_v3.unity"; // Percorso della seconda scena

        Scene scene1 = EditorSceneManager.OpenScene(scenePath1, OpenSceneMode.Additive);
        Scene scene2 = EditorSceneManager.OpenScene(scenePath2, OpenSceneMode.Additive);

        List<string> differences = new List<string>();

        GameObject[] rootObjects1 = scene1.GetRootGameObjects();
        GameObject[] rootObjects2 = scene2.GetRootGameObjects();

        CompareGameObjectLists(rootObjects1, rootObjects2, differences);

        if (differences.Count == 0)
        {
            Debug.Log("The scenes are identical.");
        }
        else
        {
            Debug.Log("Differences found:");
            foreach (string difference in differences)
            {
                Debug.Log(difference);
            }
        }

        EditorSceneManager.CloseScene(scene1, true);
        EditorSceneManager.CloseScene(scene2, true);
    }

    private static void CompareGameObjectLists(GameObject[] list1, GameObject[] list2, List<string> differences)
    {
        Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();
        Dictionary<string, GameObject> dict2 = new Dictionary<string, GameObject>();

        foreach (GameObject go in list1)
        {
            dict1.Add(go.name, go);
        }

        foreach (GameObject go in list2)
        {
            dict2.Add(go.name, go);
        }

        foreach (var kvp in dict1)
        {
            if (!dict2.ContainsKey(kvp.Key))
            {
                differences.Add($"GameObject '{kvp.Key}' is missing in the second scene.");
            }
            else
            {
                CompareGameObjects(kvp.Value, dict2[kvp.Key], differences);
            }
        }

        foreach (var kvp in dict2)
        {
            if (!dict1.ContainsKey(kvp.Key))
            {
                differences.Add($"GameObject '{kvp.Key}' is missing in the first scene.");
            }
        }
    }

    private static void CompareGameObjects(GameObject go1, GameObject go2, List<string> differences)
    {
        if (go1.transform.childCount != go2.transform.childCount)
        {
            differences.Add($"GameObject '{go1.name}' has different number of children.");
        }

        Component[] components1 = go1.GetComponents<Component>();
        Component[] components2 = go2.GetComponents<Component>();

        if (components1.Length != components2.Length)
        {
            differences.Add($"GameObject '{go1.name}' has different number of components.");
        }

        for (int i = 0; i < components1.Length; i++)
        {
            if (components1[i].GetType() != components2[i].GetType())
            {
                differences.Add($"GameObject '{go1.name}' has different components.");
            }
        }

        for (int i = 0; i < go1.transform.childCount; i++)
        {
            CompareGameObjects(go1.transform.GetChild(i).gameObject, go2.transform.GetChild(i).gameObject, differences);
        }
    }
}
