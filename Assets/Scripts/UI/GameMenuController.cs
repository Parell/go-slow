using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [Space]
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject options;
    [Space]
    [SerializeField] private Text speed;
    [SerializeField] private Text deltav;
    [Space]
    [SerializeField] private Color safeColor;
    [SerializeField] private Color dangerColor;
    [Space]
    [SerializeField] private Transform player;
    public RectTransform targetLayer;
    public Vector3 playerDirection;
    public Quaternion targetDirection;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            HandleMenu();
        }

        speed.text = string.Format("{0:0.00}m/s", movement.rigidbody.velocity.magnitude);
        deltav.text = string.Format("{0:0.00}m/s", movement.totalDeltaV - movement.currentDeltaV);

        if (movement.rigidbody.velocity.magnitude > 80) { speed.color = dangerColor; }
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

        targetDirection = Quaternion.LookRotation(movement.rigidbody.velocity);

        targetDirection.z = -targetDirection.y;
        targetDirection.x = 0f;
        targetDirection.y = 0f;

        targetLayer.localRotation = targetDirection * Quaternion.Euler(playerDirection);
    }
}
