using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject credits;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void HandleOptions()
    {
        menu.SetActive(options.activeSelf);
        options.SetActive(!options.activeSelf);
    }

    public void HandleCredits()
    {
        menu.SetActive(credits.activeSelf);
        credits.SetActive(!credits.activeSelf);
    }
}
