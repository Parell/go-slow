using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private Transform radarPingPrefab;
    [SerializeField] private LayerMask radarLayerMask;

    [SerializeField] private float rotationSpeed = 200;
    [SerializeField] private float radarDistance = 200;
    //private List<Collider> colliderList = new List<Collider>();

    private void Update()
    {
        //float previousRotation = (transform.eulerAngles.y % 360) - 180;
        transform.eulerAngles -= new Vector3(0, rotationSpeed * Time.deltaTime, 0);
        //float currentRotation = (transform.eulerAngles.y % 360) - 180;

        //if (previousRotation < 0 && currentRotation >= 0)
        //{
        //    // Half rotation
        //    colliderList.Clear();
        //}

        RaycastHit[] raycastHitArray = Physics.RaycastAll(transform.position, GetVectorFromAngle(transform.eulerAngles.y), radarDistance, radarLayerMask);
        foreach (RaycastHit raycastHit in raycastHitArray)
        {
            if (raycastHit.collider != null)
            {
                RadarPing radarPing = Instantiate(radarPingPrefab, raycastHit.point, Quaternion.identity).GetComponent<RadarPing>();

                //if (!colliderList.Contains(raycastHit.collider))
                //{
                //    colliderList.Add(raycastHit.collider);

                //    //RadarPing radarPing = Instantiate(radarPingPrefab, raycastHit.point, Quaternion.identity).GetComponent<RadarPing>();

                //    //radarPing.SetDisappearTimer(360f / rotationSpeed * 1f);
                //}
            }
        }
    }

    public Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}