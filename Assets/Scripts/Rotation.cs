using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 startRotation;
    public float rotationSpeed;

    private void Update()
    {
        transform.eulerAngles = new Vector3(startRotation.x, startRotation.y, startRotation.z);

        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime, Space.Self);
    }
}
