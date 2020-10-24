using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    [SerializeField]
    private GameObject playAgainButton;
    [SerializeField]
    private GameObject titleScreenButton;
    [SerializeField]
    private GameObject Map2Button;
    [SerializeField]
    private GameObject Map3Button;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SingleplayerGame");
    }
    
    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void Map2()
    {
        SceneManager.LoadScene("Map2");
    }

    public void Map3()
    {
        SceneManager.LoadScene("Map3");
    }
}
