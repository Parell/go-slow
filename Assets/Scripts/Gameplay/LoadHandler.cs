using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHandler : MonoBehaviour
{
    [HideInInspector] public bool quit;
    private Animator animator;
    [SerializeField] private string scene;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Fade(bool value)
    {
        animator.SetTrigger("FadeOut");

        quit = value;
    }

    public void OnFadeComplete()
    {
        if (quit)
        {
            Application.Quit();

            Debug.Log("Quit application");
        }
        else
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        }
    }
}
