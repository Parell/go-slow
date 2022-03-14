using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPaused = true;
    //public float timeRemaining = 10;
    //public bool timerIsRunning = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        //if (timerIsRunning)
        //{
        //    if (timeRemaining > 0)
        //    {
        //        timeRemaining -= Time.deltaTime;
        //    }
        //    else
        //    {
        //        Debug.Log("Time has run out!");
        //        timeRemaining = 0;
        //        timerIsRunning = false;
        //    }
        //}
    }
}
