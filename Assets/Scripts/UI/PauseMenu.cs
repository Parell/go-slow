using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject respawn;
    [Space]
    [SerializeField] private Text speed;
    [SerializeField] private Text deltav;
    [Space]
    [SerializeField] private Color safeColor;
    [SerializeField] private Color dangerColor;
    [Space]
    [SerializeField] private Transform player;
    [SerializeField] private RectTransform velocityLayer;

    private Vector3 playerDirection;
    private Quaternion velocityDirection;

    private Movement movement;

    private void Awake()
    {
        movement = player.GetComponent<Movement>();

        HandleMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            HandleMenu();
        }

        speed.text = string.Format("{0:0.00}m/s", movement.rb.velocity.magnitude);
        deltav.text = string.Format("{0:0.00}m/s", movement.totalDeltaV - movement.currentDeltaV);

        if (movement.rb.velocity.magnitude > 80) { speed.color = dangerColor; }
        else { speed.color = safeColor; }

        if (movement.totalDeltaV - movement.currentDeltaV < movement.totalDeltaV / 5) { deltav.color = dangerColor; }
        else { deltav.color = safeColor; }

        VelocityDirection();
    }

    public void HandleMenu()
    {
        hud.SetActive(menu.activeSelf);
        menu.SetActive(!menu.activeSelf);

        if (menu.activeSelf)
        {
            options.SetActive(!menu.activeSelf);
        }

        GameManager.Instance.isPaused = menu.activeSelf;
    }

    public void HandleOptions()
    {
        menu.SetActive(options.activeSelf);
        options.SetActive(!options.activeSelf);
    }

    private void VelocityDirection()
    {
        playerDirection.z = player.eulerAngles.y;

        velocityDirection = Quaternion.LookRotation(movement.rb.velocity);

        velocityDirection.z = -velocityDirection.y;
        velocityDirection.x = 0f;
        velocityDirection.y = 0f;

        velocityLayer.localRotation = velocityDirection * Quaternion.Euler(playerDirection);
    }
}
