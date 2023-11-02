using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] GameObject victoriScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        victoriScreen.SetActive(false);
    }
    public void GameOver()
    {
        Debug.Log("GameOver function called");
        if (gameOverScreen == null)
        {
            Debug.LogError("gameOverScreen not set");
            return;
        }
        gameOverScreen.SetActive(true);
        Debug.Log("Game Over!");
    }
    public void Win()
    {
        if (victoriScreen == null)
        {
            Debug.LogError("victoriScreen not set");
            return;
        }
        victoriScreen.SetActive(true);
        Debug.Log("You Win!");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
        
    }
}

