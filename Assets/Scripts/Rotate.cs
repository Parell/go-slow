using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float torque = 1000;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddRelativeTorque(Vector3.forward * torque, ForceMode.VelocityChange);
    }
}
