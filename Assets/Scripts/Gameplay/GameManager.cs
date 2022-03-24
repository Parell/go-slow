using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private TutorialManager tutorialManager;

    public bool isPaused = true;
    [Space]
    //public float timeRemaining = 10;
    //public bool timerIsRunning = false;
    //[Space]
    public int credits;

    private void Awake()
    {
        Instance = this;

        tutorialManager = GetComponent<TutorialManager>();
    }

    private void Update()
    {
        if (!isPaused)
        {
            tutorialManager.enabled = true;
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
