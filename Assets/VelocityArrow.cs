using UnityEngine;

public class VelocityArrow : MonoBehaviour
{
    public Player player;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(player.rigidbody.velocity, Vector3.up);
    }
}
