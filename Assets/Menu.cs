using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject options;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void HandleOptions()
    {
        menu.SetActive(options.activeSelf);
        options.SetActive(!options.activeSelf);
    }
}
