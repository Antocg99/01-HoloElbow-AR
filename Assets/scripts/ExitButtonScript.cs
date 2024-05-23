using UnityEngine;

public class ExitButtonScript : MonoBehaviour
{
    public void ExitApp()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
        // Su alcuni dispositivi mobile, potresti voler usare anche il comando sottostante
        // System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
    }
}
