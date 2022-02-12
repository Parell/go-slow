using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [Space]
    [SerializeField] private GameObject menu;
    [Space]
    [SerializeField] private Text speedText;
    [SerializeField] private Text fuelText;
    [SerializeField] private Slider fuelSlider;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuKey();
        }

        speedText.text = string.Format("{0:0.00}m/s", player.rigidbody.velocity.magnitude);

        float deltaV = player.totalDeltaV - player.currentDeltaV;

        fuelText.text = string.Format("{0:0.00}m/s", deltaV);


        fuelSlider.maxValue = player.totalDeltaV;
        fuelSlider.value = deltaV;
    }

    public void MenuKey()
    {
        GameManager.Instance.isPaused = !menu.activeSelf;

        menu.SetActive(!menu.activeSelf);
    }
}
