using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{  
    private bool timerIsRunning = false;
    public float startTime = 10;
    private float timeRemaining;
    [SerializeField]
    private Text countdownText;
    
    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning =  true;
        timeRemaining = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                DisplayTime(timeRemaining);
                timeRemaining -= Time.deltaTime; 
            }
            else{
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.LoadScene("Victory");
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
