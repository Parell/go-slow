using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : ScriptableObject
{
    public void LoadScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    public void QuitAplication()
    {
        Application.Quit();

        Debug.Log("Quit application");
    }
}
