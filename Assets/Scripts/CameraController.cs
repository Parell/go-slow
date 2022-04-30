using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private Transform target;
    [Space]
    [SerializeField] private Vector3 offset;
    public bool lockCamera = true;
    [Space]
    [SerializeField] private float minZoom = 30;
    [SerializeField] private float maxZoom = 80;
    [SerializeField] private float sensitivity = 1;
    [SerializeField] private float speed = 30;
    [Space]
    public bool startShake = false;
    public AnimationCurve curve;
    public float duration = 1f;

    private float targetZoom;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!GameManager.Instance.menuOpen)
        {
            targetZoom -= Input.mouseScrollDelta.y * sensitivity;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
            offset.y = Mathf.MoveTowards(offset.y, targetZoom, speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (startShake)
        {
            startShake = false;
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
