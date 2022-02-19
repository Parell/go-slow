using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneHandler : ScriptableObject
{
    public void LoadScene(string scene)
    {
        Addressables.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    public void QuitAplication()
    {
        Application.Quit();

        Debug.Log("Quit application");
    }
}
