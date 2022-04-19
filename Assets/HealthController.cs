using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Player player;
    [SerializeField] private GameObject respawnMenu;


    [SerializeField] private Slider healthSlider;
    public int health;
    public int healthMax;
    //[SerializeField] private GameObject respawn;
    //[SerializeField] private ParticleSystem particle;

    public Action<object, EventArgs> OnHealthChanged { get; private set; }

    private void Awake()
    {
        //movement = GetComponent<Player>();

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
            player.shipDisabled = true;
        }
    }

    private void Update()
    {

        if (player.shipDisabled)
        {
            // Death sequence were menu opens at end

            respawnMenu.SetActive(true);
        }
    }

    public void Respawn()
    {
        player.shipDisabled = false;

        respawnMenu.SetActive(false);

        player.rigidbody.mass = player.totalMass;
        health += healthMax;

        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}