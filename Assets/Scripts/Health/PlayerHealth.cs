using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Player player;
    [SerializeField] private GameObject respawnMenu;
    [SerializeField] private Transform respawnPosition;
    [Space]
    [SerializeField] private GameObject original;
    [SerializeField] private GameObject fractured;
    [Space]
    public int health;
    public int healthMax;
    public float time;
    public float timeMax;
    [SerializeField] private int damageSpeedThreshold = 20;

    public Action<object, EventArgs> OnHealthChanged { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();

        health = healthMax;

        OnHealthChanged += _OnHealthChanged;
    }

    private void _OnHealthChanged(object sender, EventArgs e)
    {
        if (health == 0)
        {
            player.shipDisabled = true;

            original.SetActive(false);

            if (fractured != null)
            {
                GameObject fracturedObject = Instantiate(fractured, player.transform) as GameObject;

                player.transform.GetChild(12).gameObject.transform.parent = null;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > damageSpeedThreshold && health != 0)
        {
            if (collision.relativeVelocity.magnitude > damageSpeedThreshold * 3)
            {
                health -= 3;
            }
            else if (collision.relativeVelocity.magnitude > damageSpeedThreshold * 2)
            {
                health -= 2;
            }
            else
            {
                health -= 1;
            }

            CameraController.Instance.startShake = true;

            if (health < 0) health = 0;
            if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        if (player.shipDisabled == true)
        {
            CameraController.Instance.lockCamera = false;

            respawnMenu.SetActive(true);
            time -= Time.deltaTime;

            respawnMenu.GetComponentInChildren<Text>().text = string.Format("Respawn in {0:0}", time); ;

            if (time <= 0)
            {
                Respawn();
            }
        }
        else
        {
            respawnMenu.SetActive(false);
            time = timeMax;
        }
    }

    public void Respawn()
    {
        health = healthMax;
        player.currentMass = player.totalMass;

        original.SetActive(true);

        player.rigidbody.angularVelocity = new Vector3(0, 0, 0);
        player.transform.position = respawnPosition.position;
        player.transform.rotation = respawnPosition.rotation;
        player.rigidbody.velocity = respawnPosition.rotation * new Vector3(-10, 0, 0);
        player.shipDisabled = false;

        CameraController.Instance.lockCamera = true;
    }
}