using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

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

    [Space]
    public bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;

    private Camera cam;
    private float targetZoom;

    private void Awake()
    {
        Instance = this;
        cam = GetComponent<Camera>();
    }

    private void Update()
    {

        targetZoom -= Input.mouseScrollDelta.y * sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, targetZoom, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
        else
        {
            transform.position = target.position + offset;
        }


        if (lockCamera)
        {
            transform.eulerAngles = new Vector3(90, target.eulerAngles.y, 0);
        }
    }

    private IEnumerator Shaking()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = target.position + offset + (Random.insideUnitSphere * strength);
            yield return null;
        }
    }
}
