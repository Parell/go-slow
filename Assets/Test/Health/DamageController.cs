using System;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private float damageAmount;

    private HealthController healthController;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 20 && collision.gameObject.GetComponent<HealthController>())
        {
            healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.health -= damageAmount * collision.relativeVelocity.magnitude;
            if (healthController.health < 0) healthController.health = 0;
            if (healthController.OnHealthChanged != null) healthController.OnHealthChanged(this, EventArgs.Empty);
        }
    }
}
