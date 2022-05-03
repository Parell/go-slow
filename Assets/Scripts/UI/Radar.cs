using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private Transform pingPrefab;
    [SerializeField] private LayerMask radarLayerMask;

    [SerializeField] private float rotationSpeed = 200;
    [SerializeField] private float radarDistance = 200;

    private void Update()
    {
        transform.eulerAngles -= new Vector3(0, rotationSpeed * Time.deltaTime, 0);

        RaycastHit[] raycastHitArray = Physics.RaycastAll(transform.position, GetVectorFromAngle(transform.eulerAngles.y), radarDistance, radarLayerMask);
        foreach (RaycastHit raycastHit in raycastHitArray)
        {
            if (raycastHit.rigidbody != null)
            {
                RadarPing radarPing = Instantiate(pingPrefab, raycastHit.point, Quaternion.identity).GetComponent<RadarPing>();
            }
        }
    }

    public Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}