using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Player player;

    [SerializeField] private Slider healthSlider;
    public float health;
    public float healthMax;
    [SerializeField] private GameObject respawn;
    [SerializeField] private ParticleSystem particle;

    public Action<object, EventArgs> OnHealthChanged { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();

        health = healthMax;

        SetHealthSlider();
        healthSlider.value = GetHealthPercent();
    }

    private void SetHealthSlider()
    {
        OnHealthChanged += _OnHealthChanged;
    }

    private void _OnHealthChanged(object sender, EventArgs e)
    {
        healthSlider.value = GetHealthPercent();
        Death();
    }

    public float GetHealthPercent()
    {
        return health / healthMax;
    }

    private void Death()
    {
        if (health <= 0)
        {
            respawn.SetActive(true);
            player.isDead = true;
            particle.Play();

            GameManager.Instance.timerIsRunning = false;
        }
    }

    public void FuelDeath()
    {
        respawn.SetActive(true);
        player.isDead = true;
    }

    public void Respawn()
    {
        player.isDead = false;

        particle.gameObject.SetActive(true);
        respawn.SetActive(false);

        player.rigidbody.angularVelocity = new Vector3(0, 0, 0);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);

        player.rigidbody.velocity = new Vector3(0, 0, 10);
        player.transform.position = new Vector3(0, 0, 0);

        player.rigidbody.mass = player.wetMass;

        health += healthMax;
        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}