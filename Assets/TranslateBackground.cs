using UnityEngine;

public class TranslateBackground : MonoBehaviour
{
    public Transform target;
    public float scaleSpeed;
    public float distance;

    private void Update()
    {
        transform.position = new Vector3(target.position.x * scaleSpeed, distance, target.position.z * scaleSpeed);
    }
}
