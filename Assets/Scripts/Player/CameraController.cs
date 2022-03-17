using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 nextPosition = target.position + offset + (rb.velocity * 0.3f);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, nextPosition, smoothSpeed);

        transform.position = smoothPosition;

        transform.eulerAngles = new Vector3(90, target.eulerAngles.y, 0);
    }
}
