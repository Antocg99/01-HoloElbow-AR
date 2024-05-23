using UnityEngine;

[RequireComponent(typeof(Camera))]
public class HSymmetry : MonoBehaviour
{
    public Vector3 VectorProject = new Vector3(1f, -1f, 1f);

    void OnPreCull()
    {
        Matrix4x4 scale;

        if (GetComponent<Camera>().aspect > 2)
        {
            scale = Matrix4x4.Scale(VectorProject);
        }
        else
        {
            scale = Matrix4x4.Scale(VectorProject);
        }

        GetComponent<Camera>().ResetWorldToCameraMatrix();
        GetComponent<Camera>().ResetProjectionMatrix();
        GetComponent<Camera>().projectionMatrix = GetComponent<Camera>().projectionMatrix * scale;
    }

    void OnPreRender()
    {
        GL.invertCulling = true;
    }

    void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
