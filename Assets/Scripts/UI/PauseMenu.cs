using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject tutorial;
    [Space]
    [SerializeField] private Text speed;
    [SerializeField] private Text deltaV;
    [Space]
    [SerializeField] private Player player;

    private void Start()
    {
        HandleMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleMenu();
        }

        speed.text = string.Format("{0:0.00}m/s", player.rigidbody.velocity.magnitude);
        deltaV.text = string.Format("Fuel {0:0.00}m/s", player.totalDeltaV - player.currentDeltaV);


        tutorial.SetActive(!menu.activeSelf);
    }

    public void HandleMenu()
    {
        hud.SetActive(menu.activeSelf);
        menu.SetActive(!menu.activeSelf);

        if (menu.activeSelf)
        {
            options.SetActive(!menu.activeSelf);
        }

        GameManager.Instance.menuOpen = menu.activeSelf;
    }

    public void HandleOptions()
    {
        menu.SetActive(options.activeSelf);
        options.SetActive(!options.activeSelf);
    }
}
