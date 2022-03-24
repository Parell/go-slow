using System;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private int damageSpeedThreshold = 20;

    private HealthController healthController;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > damageSpeedThreshold && collision.gameObject.GetComponent<HealthController>())
        {
            healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.health -= damageAmount * (int)(collision.relativeVelocity.magnitude);
            CameraController.Instance.start = true;

            if (healthController.health < 0) healthController.health = 0;
            if (healthController.OnHealthChanged != null) healthController.OnHealthChanged(this, EventArgs.Empty);
        }
    }
}
