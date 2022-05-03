using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public float damageSpeedThreshold;
    [Space]
    public GameObject original;
    public GameObject fractured;

    public bool destroyOnStart;

    private int hasCollided = 1;

    private void Start()
    {
        if (destroyOnStart)
        {
            Destroy();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > damageSpeedThreshold && hasCollided != 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        original.SetActive(false);

        hasCollided -= 1;

        if (fractured != null)
        {
            GameObject fracturedObject = Instantiate(fractured, this.gameObject.transform) as GameObject;
        }
    }
}