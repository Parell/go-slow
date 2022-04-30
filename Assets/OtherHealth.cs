using UnityEngine;

public class OtherHealth : MonoBehaviour
{
    public float damageSpeedThreshold;
    [Space]
    public GameObject original;
    public GameObject fractured;

    private int hasCollided = 1;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > damageSpeedThreshold && hasCollided != 0)
        {
            original.SetActive(false);

            hasCollided -= 1;

            if (fractured != null)
            {
                GameObject fracturedObject = Instantiate(fractured, this.gameObject.transform) as GameObject;
            }
        }
    }
}