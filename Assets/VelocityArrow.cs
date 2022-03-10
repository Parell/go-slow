using UnityEngine;

public class VelocityArrow : MonoBehaviour
{
    public new Rigidbody rigidbody;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rigidbody.velocity, Vector3.up);
    }
}
