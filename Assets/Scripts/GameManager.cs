using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPaused = true;

    public float timeRemaining = 10;
    public bool timerIsRunning = false;

    public Task[] tasks;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        Instance = this;
        timerIsRunning = true;
    }

    private void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

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
}
