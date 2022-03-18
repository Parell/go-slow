using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody rb;

    [Space]
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool lockCamera;

    [Space]
    [SerializeField] private float minZoom = 30;
    [SerializeField] private float maxZoom = 80;
    [SerializeField] private float sensitivity = 1;
    [SerializeField] private float speed = 30;

    private Camera cam;
    private float targetZoom;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        targetZoom -= Input.mouseScrollDelta.y * sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, targetZoom, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        transform.position = target.position + offset;

        if (lockCamera)
        {
            transform.eulerAngles = new Vector3(90, target.eulerAngles.y, 0);
        }
    }
}
