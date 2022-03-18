using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPaused = true;
    [Space]
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    [Space]
    public int credits;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    private void LoadGame()
    {

    }

    private void SaveGame()
    {

    }

    private void NewGame()
    {
        credits = 10000;

        PlayerPrefs.SetInt("credits", credits);
    }
}
