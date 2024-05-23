using UnityEngine;

public class MoveDashedLinesRect : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberOfPoints = 50;
    public float speed = 2f;
    public float angle = 2f;

    private float t = 0f;

    void Start()
    {
        lineRenderer.positionCount = numberOfPoints;
        UpdateLineRenderer();
    }

    void Update()
    {
        lineRenderer = GetComponent<LineRenderer>();
        t += Time.deltaTime * speed;
        UpdateLineRenderer();
    }

    void UpdateLineRenderer()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            //float angle = Mathf.Lerp(0, 360, (float)i / (float)numberOfPoints);
            //float x = Mathf.Cos(angle * Mathf.Deg2Rad) * t;
            //float z = Mathf.Sin(angle * Mathf.Deg2Rad) * t;
            //Vector3 point = new Vector3(x, 0f, z);
            //float y = Mathf.Sin(angle * Mathf.Deg2Rad) * t;
            //Vector3 point = new Vector3(0f, y, 0f );
            lineRenderer.SetPosition(i, transform.up * i * Mathf.Sin(speed*t));
        }
    }
}
