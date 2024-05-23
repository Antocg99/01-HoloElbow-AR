using UnityEngine;

public class TransformRecorder : MonoBehaviour
{
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;
    private Vector3 initialLocalScale;

    private void Start()
    {
        // Registra le trasformazioni iniziali dell'oggetto rispetto all'oggetto padre
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
        initialLocalScale = transform.localScale;
    }

    public void ResetToInitialState()
    {
        // Ripristina le trasformazioni iniziali dell'oggetto rispetto all'oggetto padre
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;

        // Ripristina anche la scala a fattore 1
        transform.localScale = initialLocalScale;
    }
}
