using UnityEngine;

public class Startup : MonoBehaviour
{
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private string scene;

    private void Awake()
    {
        sceneHandler.LoadScene(scene);
    }
}
