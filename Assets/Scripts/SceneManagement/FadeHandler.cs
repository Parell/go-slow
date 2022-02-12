using UnityEngine;

public class FadeHandler : MonoBehaviour
{
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private string scene;
    [HideInInspector] public bool quit;
    private Animator animator;

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
            sceneHandler.QuitAplication();
        }
        else
        {
            sceneHandler.LoadScene(scene);
        }
    }
}
