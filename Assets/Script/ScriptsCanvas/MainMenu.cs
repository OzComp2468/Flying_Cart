using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void StartGamePlease()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
        print("Game Closed");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }



    public void ResetHigh()
    {
        PlayerPrefs.DeleteAll();

    }




}
