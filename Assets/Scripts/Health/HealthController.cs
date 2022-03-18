using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Movement movement;
    [SerializeField] private GameObject respawnMenu;


    [SerializeField] private Slider healthSlider;
    public int health;
    public int healthMax;
    //[SerializeField] private GameObject respawn;
    //[SerializeField] private ParticleSystem particle;

    public Action<object, EventArgs> OnHealthChanged { get; private set; }

    private void Awake()
    {
        movement = GetComponent<Movement>();

        health = healthMax;

        healthSlider.maxValue = healthMax;
        healthSlider.value = health;
        SetHealthSlider();
    }

    private void SetHealthSlider()
    {
        OnHealthChanged += _OnHealthChanged;
    }

    private void _OnHealthChanged(object sender, EventArgs e)
    {
        healthSlider.value = health;

        // damage sequence

        if (health <= 0)
        {
            movement.shipDisabled = true;
        }
    }

    private void Update()
    {

        if (movement.shipDisabled)
        {
            // Death sequence were menu opens at end

            respawnMenu.SetActive(true);
        }
    }

    public void Respawn()
    {
        movement.shipDisabled = false;

        respawnMenu.SetActive(false);

        movement.rb.mass = movement.totalMass;
        health += healthMax;

        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}